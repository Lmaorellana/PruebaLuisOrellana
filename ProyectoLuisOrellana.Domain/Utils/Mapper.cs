using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperNova.Erp.Base.Domain.Utils
{
    public static class Mapper
    {
        public static void MapProperties<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentNullException("Source or Destination cannot be null.");
            }

            // Obtener las propiedades de las clases fuente y destino
            var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var destProp in destinationProperties)
            {
                var sourceProp = sourceProperties.FirstOrDefault(sp => sp.Name == destProp.Name && sp.PropertyType == destProp.PropertyType);
                if (sourceProp != null && destProp.CanWrite)
                {
                    var value = sourceProp.GetValue(source);

                    // Si es una colección, manejar el mapeo recursivo
                    if (value is IEnumerable && !(value is string))
                    {
                        var listType = destProp.PropertyType.GetGenericArguments().FirstOrDefault();
                        if (listType != null)
                        {
                            var listInstance = (IList)Activator.CreateInstance(destProp.PropertyType);

                            foreach (var item in (IEnumerable)value)
                            {
                                var listItemInstance = Activator.CreateInstance(listType);
                                MapProperties(item, listItemInstance);
                                listInstance.Add(listItemInstance);
                            }
                            destProp.SetValue(destination, listInstance);
                        }
                    }
                    else if (!destProp.PropertyType.IsPrimitive && !destProp.PropertyType.IsValueType && destProp.PropertyType != typeof(string))
                    {
                        // Mapeo recursivo para propiedades complejas
                        var nestedInstance = Activator.CreateInstance(destProp.PropertyType);
                        MapProperties(value, nestedInstance);
                        destProp.SetValue(destination, nestedInstance);
                    }
                    else
                    {
                        destProp.SetValue(destination, value);
                    }
                }
            }
        }

        public static List<TDestination> MapProperties<TSource, TDestination>(IEnumerable<TSource> source)
            where TDestination : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException("Source cannot be null.");
            }

            var result = new List<TDestination>();
            foreach (var item in source)
            {
                var destinationItem = new TDestination();
                MapProperties(item, destinationItem);
                result.Add(destinationItem);
            }

            return result;
        }
    }

}
