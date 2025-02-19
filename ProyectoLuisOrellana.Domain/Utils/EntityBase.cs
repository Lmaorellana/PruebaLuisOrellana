using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SuperNova.Erp.Base.Domain.Utils
{
    public abstract class EntityBase
    {

        #region IIdentifiable Members

        [NotMapped()]
        public virtual string Key { get { return string.Empty; } }

        [NotMapped()]
        public virtual string DescriptionName { get { return string.Empty; } }

        #endregion

        public virtual void OnCreate()
        {
        }

        //public List<Parameter> GetParameters(string[] exludeProperties = null)
        //{
        //    return Rp3.Data.Parameter.GetPropertiesParameters(this, exludeProperties);
        //}

        #region " Xml Representation "

        /// <summary>
        /// Devuelve la representación Xml de la información contenida en el objeto.
        /// </summary>
        /// <returns>Una cadena que es la representación de la información contenida en el objeto.</returns>
        public virtual string ToXml()
        {
            return ToXml((SuperNova.Xml.XmlPropertiesInfo[])null);
        }

        /// <summary>
        /// Devuelve la representación Xml de la información contenida en el objeto considerando las propiedades identificadas 
        /// por propertyNames.
        /// </summary>
        /// <returns>Una cadena que es la representación de la información contenida en el objeto.</returns>
        public virtual string ToXml(params string[] propertyNames)
        {
            return SuperNova.Xml.XmlConverter.ToXml(this, propertyNames);
        }

        /// <summary>
        /// Devuelve la representación Xml de la información contenida en el objeto considerando las propiedades identificadas 
        /// por propertiesInfo.
        /// </summary>
        /// <returns>Una cadena que es la representación de la información contenida en el objeto.</returns>
        public virtual string ToXml(params SuperNova.Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return SuperNova.Xml.XmlConverter.ToXml(this, propertiesInfo);
        }

        #endregion
    }
}
