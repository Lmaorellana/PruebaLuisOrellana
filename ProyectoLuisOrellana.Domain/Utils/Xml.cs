using System.Text;
using System.Reflection;
using System.Xml;
using System.Collections;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
//using SuperNova.Erp.Base.Domain.Utils;
//using SuperNova.Erp.Base.Domain.Utils;



namespace SuperNova.Erp.Base.Domain.Utils
{
    public sealed class Xml
    {

        public static string UnicodeId
        {
            //get { return "UTF-16"; }
            // get { return "UTF-8"; }
            get { return ""; }
        }

        public static string Version
        {
            get { return "1.0"; }
        }

       
    }

    public sealed class Format
    {
        static internal string PrefixKey
        {
            get { return "K"; }
        }

        public static string Separator
        {
            get { return "~"; }
        }

        public static string BackSlash
        {
            get { return "\\"; }
        }

        public static string Lines()
        {
            return Lines(2);
        }

        public static string Lines(int number)
        {
            string returnValue = string.Empty;
            for (int ind = 1; ind <= number; ind++)
            {
                returnValue += ControlChars.NewLine;
            }

            return returnValue;
        }

        public static string Quotes
        {
            get { return "\""; }
        }

        public static string BeginTag
        {
            get { return "&lt;"; }
        }

        public static string EndTag
        {
            get { return "&gt;"; }
        }

        public static string Apostrophe
        {
            get { return "&apos;"; }
        }

        public static string QuotationMark
        {
            get { return "&quot;"; }
        }

        public static string Ampersand
        {
            get { return "&amp;"; }
        }

        public static string ChartViewerTitle
        {
            get { return "{1}{0}{0}'{2}', agrupado por '{3}',{0}aplicando la función '{4}' y graficando como '{5}'"; }
        }

        private static System.Globalization.CultureInfo _SqlCultureInfo;
        public static System.Globalization.CultureInfo SqlCultureInfo
        {
            get
            {
                if (_SqlCultureInfo == null)
                {
                    _SqlCultureInfo = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.LCID);
                    _SqlCultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
                    _SqlCultureInfo.NumberFormat.CurrencyGroupSeparator = ",";
                    _SqlCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                    _SqlCultureInfo.NumberFormat.NumberGroupSeparator = ",";
                    _SqlCultureInfo.NumberFormat.PercentDecimalSeparator = ".";
                    _SqlCultureInfo.NumberFormat.PercentGroupSeparator = ",";

                    _SqlCultureInfo.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";

                }
                return _SqlCultureInfo;
            }
        }

    }
}

namespace SuperNova.Xml
{
    #region " XmlPropertiesInfo "

    /// <summary>
    /// Provee información para especializar la generación de XML a partir de los elementos contenidos en una colección. (Ver el método <b>ToXml</b> de <see cref="GenericCollection(Of T)"></see>)
    /// </summary>
    /// <remarks></remarks>
    public class XmlPropertiesInfo : BindingList<string>
    {

        /// <summary>
        /// Inicializa una instancia de <see cref="XmlPropertiesInfo"></see>.
        /// </summary>
        /// <param name="memberType">Un <see cref="Type"></see> que representa el tipo de elemento que se desea incluir en la generación del XML.</param>
        /// <param name="propertyNames">Una serie de <see cref="string"></see> que representan los nombres de las propiedades del tipo de elementos que se desea incluir en la generación del XML.</param>
        /// <remarks></remarks>
        public XmlPropertiesInfo(Type memberType, params string[] propertyNames)
            : this(string.Empty, string.Empty, memberType, propertyNames)
        {
        }

        /// <summary>
        /// Inicializa una instancia de <see cref="XmlPropertiesInfo"></see> indicando el nombre de elemento.
        /// </summary>
        /// <param name="elementName">Un <see cref="string"></see> que representa el nombre de elemento que se usará para identificar el tipo en la generación del XML.</param>
        /// <param name="memberType">Un <see cref="Type"></see> que representa el tipo de elemento que se desea incluir en la generación del XML.</param>
        /// <param name="propertyNames">Una serie de <see cref="string"></see> que representan los nombres de las propiedades del tipo de elementos que se desea incluir en la generación del XML.</param>
        /// <remarks></remarks>
        public XmlPropertiesInfo(string elementName, Type memberType, params string[] propertyNames)
            : this(string.Empty, elementName, memberType, propertyNames)
        {
        }

        /// <summary>
        /// Inicializa una instancia de <see cref="XmlPropertiesInfo"></see> indicando el nombre de elemento.
        /// </summary>
        /// <param name="memberName">Un <see cref="string"></see> que identifica al miembro a inlcuir en la generación del XML.</param>
        /// <param name="elementName">Un <see cref="string"></see> que representa el nombre de elemento que se usará para identificar el tipo en la generación del XML.</param>
        /// <param name="memberType">Un <see cref="Type"></see> que representa el tipo de elemento que se desea incluir en la generación del XML.</param>
        /// <param name="propertyNames">Una serie de <see cref="string"></see> que representan los nombres de las propiedades del tipo de elementos que se desea incluir en la generación del XML.</param>
        /// <remarks></remarks>
        public XmlPropertiesInfo(string memberName, string elementName, Type memberType, params string[] propertyNames)
            : base()
        {
            if (memberName != null)
            {
                _MemberName = memberName.Trim();
            }
            if (elementName != null)
            {
                _ElementName = elementName.Trim();
            }
            _MemberType = memberType;
            if (propertyNames.Length > 0)
            {
                foreach (string propertyName in propertyNames)
                {
                    Add(propertyName);
                }
            }
            else if (memberType != null)
            {
                foreach (PropertyInfo info in memberType.GetProperties())
                {
                    Add(info.Name);
                }
            }
        }

        private Type _MemberType;
        /// <summary>
        /// Obtiene el valor que representa el tipo de elemento que debe ser incluído en la generación del XML.
        /// </summary>
        /// <value>Un <see cref="Type"></see>.</value>
        /// <returns>El tipo de elemento a ser incluído en la generación del XML.</returns>
        /// <remarks></remarks>
        public Type MemberType
        {
            get { return _MemberType; }
        }

        private string _MemberName = string.Empty;
        /// <summary>
        /// Obtiene el nombre del miembro que identifica de manera única la definición del elemento a incluir en la generación XML.
        /// </summary>
        public string MemberName
        {
            get { return _MemberName; }
        }

        /// <summary>
        /// Obtiene el nombre de elemento con el cual se identifica el tipo en la generación XML.
        /// </summary>
        private string _ElementName = string.Empty;
        public string ElementName
        {
            get { return _ElementName; }
        }

        /// <summary>
        /// Asigna el tipo de elemento.
        /// </summary>
        /// <param name="memberType">Un <see cref="Type"></see>.</param>
        /// <remarks></remarks>
        internal void SetMemberType(Type memberType)
        {
            _MemberType = memberType;
        }

    }

    #endregion

    #region " XmlParameterConstructorInfo "

    /// <summary>
    /// Provee información que es utilizada para la creación de instancias de elementos de colección, cuando éstos son creados a partir de un XML. (Ver el método <b>LoadXml</b> de <see cref="GenericCollection(Of T)"></see>)
    /// </summary>
    /// <remarks></remarks>
    public class XmlParameterConstructorInfo
    {

        /// <summary>
        /// Crea e inicializa una nueva instancia de <see cref="XmlParameterConstructorInfo"></see>.
        /// </summary>
        /// <param name="type">Un <see cref="Type"></see> que representa el tipo de parámetro que debe ser enviado al constructor del elemento.</param>
        /// <param name="name">Un <see cref="string"></see> que representa el nombre del atributo dentro del XML que debe ser enviado como parámetro al constructor del elemento.</param>
        /// <remarks></remarks>
        public XmlParameterConstructorInfo(Type type, string name)
        {
            _Type = type;
            _Name = name;
        }

        private Type _Type;
        /// <summary>
        /// Obtiene el valor que representa el tipo de parámetro que debe ser enviado al constructor del elemento.
        /// </summary>
        /// <value>Un <see cref="Type"></see>.</value>
        /// <returns>Referencia al tipo de parámetro.</returns>
        /// <remarks></remarks>
        public Type Type
        {
            get { return _Type; }
        }

        private string _Name;
        /// <summary>
        /// Obtiene el valo que representa el nombre del atributo que debe ser enviado como parámetro al constructor del elemento.
        /// </summary>
        /// <value>Un <see cref="string"></see>.</value>
        /// <returns>El nombre del atributo.</returns>
        /// <remarks></remarks>
        public string Name
        {
            get { return _Name; }
        }
    }

    #endregion

    #region " XmlConverter "

    public sealed class XmlConverter
    {

        private XmlConverter()
        {
        }

        #region " Private Shared Methods and Fields "

        private static void CheckPropertiesInfo(Type mainMemberType, XmlPropertiesInfo[] propertiesInfo)
        {
            if (propertiesInfo == null)
                return;
            if (propertiesInfo.Length == 0)
                return;
            if (propertiesInfo.Length > 1)
            {
                foreach (XmlPropertiesInfo propertyInfo in propertiesInfo)
                {
                    if (propertyInfo.MemberType == null)
                    {
                        throw new ArgumentNullException("MemberType");
                        return;
                    }
                }
            }
            else
            {
                if (propertiesInfo[0].MemberType == null)
                    propertiesInfo[0].SetMemberType(mainMemberType);
            }
        }

        private static bool IncludeProperty(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.IsDefined(typeof(Erp.Base.Domain.Utils.NonSerializableToXmlAttribute), true))
            {
                PropertyDescriptorCollection restrictProperties = TypeDescriptor.GetProperties(typeof(Erp.Base.Domain.Utils.EntityBase));
                string[] allowProperties = {
                    "ID",
                    "DESCRIPTION",
                    "KEY"
                };
                if (Array.IndexOf(allowProperties, propertyInfo.Name.ToUpper()) != -1)
                {
                    return true;
                }
                else
                {
                    return restrictProperties.Find(propertyInfo.Name, true) == null;
                }
            }
            return false;
        }

        private static bool IncludeProperty(Type parentType, string memberName, Type memberType, PropertyInfo propertyInfo, XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            if (xmlPropertiesInfo == null || xmlPropertiesInfo.Length == 0)
            {
                return IncludeProperty(propertyInfo);
            }
            else if (xmlPropertiesInfo.Length > 0)
            {
                XmlPropertiesInfo info = FindPropertiesInfo(parentType, memberName, memberType, xmlPropertiesInfo);
                if (info == null)
                {
                    foreach (XmlPropertiesInfo xmlPropertyInfo in xmlPropertiesInfo)
                    {
                        if (ReferenceEquals(xmlPropertyInfo.MemberType, parentType) && xmlPropertyInfo.IndexOf(memberName) != -1)
                        {
                            return IncludeProperty(propertyInfo);
                        }
                    }
                }
                else if (IncludeProperty(propertyInfo))
                {
                    return info.IndexOf(propertyInfo.Name) != -1;
                }
                return false;
            }

            return false;
        }

        private static XmlPropertiesInfo FindPropertiesInfo(Type parentType, string memberName, Type memberType, XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            if (xmlPropertiesInfo != null)
            {
                foreach (XmlPropertiesInfo xmlPropertyInfo in xmlPropertiesInfo)
                {
                    if (ReferenceEquals(xmlPropertyInfo.MemberType, memberType))
                    {
                        if (string.IsNullOrEmpty(memberName) || string.IsNullOrEmpty(xmlPropertyInfo.MemberName) && string.IsNullOrEmpty(xmlPropertyInfo.ElementName))
                        {
                            return xmlPropertyInfo;
                        }
                        if (memberName == xmlPropertyInfo.MemberName)
                        {
                            return xmlPropertyInfo;
                        }
                        if (memberName == xmlPropertyInfo.ElementName)
                        {
                            return xmlPropertyInfo;
                        }
                    }
                }
                //JCardenas: 21-08-2009
                //Permite que se pueda realizar una conversión a Xml de una clase cabecera haciendo referencia a un tipo de clase base o interfaz compatible, no
                //necesariamente el mismo tipo de la clase.
                if (parentType == null)
                {
                    foreach (XmlPropertiesInfo xmlPropertyInfo in xmlPropertiesInfo)
                    {
                        //Verifica si el tipo memberType es equivalente al tipo declarado en xmlPropertyInfo
                        if (memberType.IsSubclassOf(xmlPropertyInfo.MemberType) || xmlPropertyInfo.MemberType.IsAssignableFrom(memberType))
                        {
                            if (string.IsNullOrEmpty(memberName) || string.IsNullOrEmpty(xmlPropertyInfo.MemberName) && string.IsNullOrEmpty(xmlPropertyInfo.ElementName))
                            {
                                return xmlPropertyInfo;
                            }
                            if (memberName == xmlPropertyInfo.MemberName)
                            {
                                return xmlPropertyInfo;
                            }
                            if (memberName == xmlPropertyInfo.ElementName)
                            {
                                return xmlPropertyInfo;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private static string GetElementName(Type parentType, string memberName, Type memberType, XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            string returnValue = string.Empty;
            XmlPropertiesInfo propInfo = FindPropertiesInfo(parentType, memberName, memberType, xmlPropertiesInfo);
            if (propInfo != null)
                returnValue = propInfo.ElementName;
            if (string.IsNullOrEmpty(returnValue))
            {
                string[] names = memberType.Name.Split('`');
                returnValue = Convert.ToString(names.Length > 0 ? names[0] : memberType.Name);
            }
            return returnValue;
        }

        static internal XmlElement CreateXmlElement(XmlDocument xmlDocument, string memberName, object value, Type parentType, XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            XmlElement xmlNode = default;
            object currentValue = null;
            //Type type = value.GetType().BaseType;

            Type type = value.GetType();

            if (type.IsSealed)
                type = type.BaseType;

            xmlNode = xmlDocument.CreateElement(GetElementName(parentType, memberName, type, xmlPropertiesInfo));
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            //For i As Integer = properties.Length - 1 to 0 step -1
            //    Dim propertyInfo As PropertyInfo = properties(i)
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    if (IncludeProperty(parentType, memberName, type, propertyInfo, xmlPropertiesInfo) && propertyInfo.CanRead)
                    {
                        try
                        {
                            currentValue = propertyInfo.GetValue(value, null);
                            if (currentValue != null)
                            {
                                Type propertyType = currentValue.GetType();
                                if (ReferenceEquals(propertyType, typeof(bool)))
                                {
                                    if (Convert.ToBoolean(currentValue))
                                    {
                                        xmlNode.SetAttribute(propertyInfo.Name, "1");
                                    }
                                    else
                                    {
                                        xmlNode.SetAttribute(propertyInfo.Name, "0");
                                    }
                                }
                                else if (ReferenceEquals(propertyType, typeof(string)) | ReferenceEquals(propertyType, typeof(object)))
                                {
                                    //JFCardenas 12-Ago-2006
                                    //Comenté llamada a Format xq xmlNode.SetAttribute realiza el formateo.
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToString(currentValue));
                                    // Format(CStr(currentValue)))
                                }
                                else if (ReferenceEquals(propertyType, typeof(int)) || ReferenceEquals(propertyType, typeof(long)) || ReferenceEquals(propertyType, typeof(short)) || ReferenceEquals(propertyType, typeof(uint)) || ReferenceEquals(propertyType, typeof(ushort)) || ReferenceEquals(propertyType, typeof(ulong)) || ReferenceEquals(propertyType, typeof(byte)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToString(currentValue));
                                }
                                else if (ReferenceEquals(propertyType, typeof(decimal)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToDecimal(currentValue).ToString(Erp.Base.Domain.Utils.Format.SqlCultureInfo.NumberFormat));
                                }
                                else if (ReferenceEquals(propertyType, typeof(double)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToDouble(currentValue).ToString(Erp.Base.Domain.Utils.Format.SqlCultureInfo.NumberFormat));
                                }
                                else if (ReferenceEquals(propertyType, typeof(float)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToSingle(currentValue).ToString(Erp.Base.Domain.Utils.Format.SqlCultureInfo.NumberFormat));
                                }
                                else if (ReferenceEquals(propertyType, typeof(DateTime)))
                                {
                                    //If IsDate(currentValue) AndAlso CDate(currentValue) > Date.MinValue Then xmlNode.SetAttribute(propertyInfo.Name, String.Format("{0:yyyy-MM-dd}", currentValue) & " " & CType(currentValue, Date).ToLongTimeString())
                                    if (Information.IsDate(currentValue) && Convert.ToDateTime(currentValue) > DateTime.MinValue)
                                        xmlNode.SetAttribute(propertyInfo.Name, string.Format("{0:yyyyMMdd} {1:HH:mm:ss}", currentValue, currentValue));
                                }
                                else if (propertyType.IsEnum)
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToString(currentValue));
                                }
                                else if (ReferenceEquals(propertyType, typeof(byte[])))
                                {
                                    //xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize(new byte[] { Convert.ToByte(currentValue) }));
                                    xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize((byte[])currentValue));
                                }
                                //else if (typeof(System.Drawing.Image).IsInstanceOfType(currentValue))
                                //{
                                //    xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize((System.Drawing.Image)currentValue));
                                //}
                                //else if (typeof(System.Drawing.Icon).IsInstanceOfType(currentValue))
                                //{
                                //    xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize((System.Drawing.Icon)currentValue));
                                //}
                                else
                                {
                                    XmlElement subElement = null;
                                    IEnumerable list = currentValue as IEnumerable;
                                    if (list != null)
                                    {
                                        //CType(currentValue, IEnumerable)
                                        foreach (object item in list)
                                        {
                                            subElement = CreateXmlElement(xmlDocument, propertyInfo.Name, item, type, xmlPropertiesInfo);
                                            if (subElement != null)
                                                xmlNode.AppendChild(subElement);
                                        }
                                    }
                                    else
                                    {
                                        ICollection coll = currentValue as ICollection;
                                        if (coll != null)
                                        {
                                            foreach (object item in coll)
                                            {
                                                subElement = CreateXmlElement(xmlDocument, propertyInfo.Name, item, type, xmlPropertiesInfo);
                                                if (subElement != null)
                                                    xmlNode.AppendChild(subElement);
                                            }
                                        }
                                        else
                                        {
                                            subElement = CreateXmlElement(xmlDocument, propertyInfo.Name, currentValue, type, xmlPropertiesInfo);
                                            if (subElement != null)
                                                xmlNode.AppendChild(subElement);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                xmlNode.SetAttribute(propertyInfo.Name, string.Empty);
                            }
                        }
                        catch (Exception ex)
                        {
                            ex = null;
                        }
                    }
                }
            }
            return xmlNode;
        }

        private static bool FindOverloadedProperty(PropertyInfo propertyInfo)
        {
            return false;
        }

        #endregion

        #region " ToXml Implementation "

        public static XmlDocument ToXmlDocument(object value)
        {
            return ToXmlDocument(null, value, Erp.Base.Domain.Utils.Xml.UnicodeId);
        }

        public static XmlDocument ToXmlDocument(object value, string encoding)
        {
            return ToXmlDocument(null, value, encoding);
        }

        public static XmlDocument ToXmlDocument(object value, params string[] propertyNames)
        {
            return ToXmlDocument(null, value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertyNames);
        }

        public static XmlDocument ToXmlDocument(object value, string encoding, params string[] propertyNames)
        {
            return ToXmlDocument(null, value, encoding, propertyNames);
        }

        public static XmlDocument ToXmlDocument(object value, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(null, value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(object value, string encoding, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(null, value, encoding, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value)
        {
            return ToXmlDocument(previousXmlDocument, value, Erp.Base.Domain.Utils.Xml.UnicodeId);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, string encoding)
        {
            return ToXmlDocument(previousXmlDocument, value, encoding);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, params string[] propertyNames)
        {
            return ToXmlDocument(previousXmlDocument, value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertyNames);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, string encoding, params string[] propertyNames)
        {
            XmlPropertiesInfo propertyInfo = new XmlPropertiesInfo(value.GetType(), propertyNames);
            return ToXmlDocument(previousXmlDocument, value, encoding, propertyInfo);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(previousXmlDocument, value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, string encoding, params XmlPropertiesInfo[] propertiesInfo)
        {
            XmlDocument xmlDocument = previousXmlDocument;
            XmlElement xmlNode = default;
            CheckPropertiesInfo(value.GetType(), propertiesInfo);
            try
            {
                if (xmlDocument == null)
                {
                    xmlDocument = new XmlDocument();
                    //Declaration
                    xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration(Erp.Base.Domain.Utils.Xml.Version, encoding, "yes"));
                }
                xmlNode = CreateXmlElement(xmlDocument, string.Empty, value, null, propertiesInfo);
                if (xmlNode != null)
                {
                    if (previousXmlDocument == null)
                    {
                        xmlDocument.AppendChild(xmlNode);
                    }
                    else
                    {
                        xmlDocument.ChildNodes[1].AppendChild(xmlNode);
                    }
                }
            }
            finally
            {
            }
            return xmlDocument;
        }

        public static XmlDocument ToXmlDocument(string elementName, IQueryable collection, string encode, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(elementName, (IEnumerable)collection, encode, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(string elementName, IEnumerable collection, string encode, params XmlPropertiesInfo[] propertiesInfo)
        {
            object member = null;
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement xmlRootNode = default;
            string numberDecimalSeparator = ".";
            string numberGroupSeparator = ",";
            bool changeSeparator = false;
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.LCID);

            if (cultureInfo.NumberFormat.NumberDecimalSeparator != ".")
            {
                numberDecimalSeparator = cultureInfo.NumberFormat.NumberDecimalSeparator;
                numberGroupSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
                cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                cultureInfo.NumberFormat.NumberGroupSeparator = ",";
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                changeSeparator = true;
            }

            //Declaration
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration(Erp.Base.Domain.Utils.Xml.Version, encode, "yes"));
            //Root
            if (string.IsNullOrEmpty(elementName))
            {
                string[] collectionName = collection.GetType().Name.Split('`');
                elementName = Convert.ToString(collectionName.Length > 0 ? collectionName[0] : collection.GetType().Name);
            }
            xmlRootNode = xmlDocument.CreateElement(elementName);
            xmlDocument.AppendChild(xmlRootNode);

            foreach (object member1 in collection)
            {
                ToXmlDocument(xmlDocument, member1, encode, propertiesInfo);
            }

            if (changeSeparator)
            {
                cultureInfo.NumberFormat.NumberDecimalSeparator = numberDecimalSeparator;
                cultureInfo.NumberFormat.NumberGroupSeparator = numberGroupSeparator;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            return xmlDocument;
        }

        //public static string ToXml<TValue>(TValue[] values)
        //{
        //    return SelectedItemSortedList.ToXml<TValue>(values);
        //}

        //public static string ToXml(object[] values)
        //{
        //    return SelectedItemSortedList.ToXml(values);
        //}       

        public static string ToXml(IEnumerable value, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument("IEnumerable", value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertiesInfo).InnerXml;
        }

        public static string ToXml(IQueryable value, params string[] propertyNames)
        {
            XmlPropertiesInfo propertiesInfo = new XmlPropertiesInfo(value.ElementType, propertyNames);
            return ToXmlDocument("IQueryable", value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertiesInfo).InnerXml;
        }

        public static string ToXml(IQueryable value, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument("IQueryable", value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertiesInfo).InnerXml;
        }

        public static string ToXml(object value)
        {
            return ToXml(value, (XmlPropertiesInfo[])null);
        }

        public static string ToXml(object value, string encoding)
        {
            return ToXml(value, encoding);
        }

        public static string ToXml(object value, params string[] propertyNames)
        {
            return ToXml(value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertyNames);
        }

        public static string ToXml(object value, string encoding, params string[] propertyNames)
        {
            XmlPropertiesInfo propertiesInfo = new XmlPropertiesInfo(value.GetType(), propertyNames);
            return ToXml(value, encoding, propertiesInfo);
        }

        public static string ToXml(object value, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXml(value, Erp.Base.Domain.Utils.Xml.UnicodeId, propertiesInfo);
        }

        public static string ToXml(object value, string encoding, params XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(null, value, encoding, propertiesInfo).InnerXml;
        }

        #endregion

        #region " LoadXml Implementation "

        public static void LoadXml(object value, XmlDocument xmlDocument)
        {
            try
            {
                if (xmlDocument.ChildNodes.Count > 0)
                {
                    foreach (XmlElement xmlElement in xmlDocument.ChildNodes)
                    {
                        if (value != null)
                        {
                            foreach (PropertyInfo propertyInfo in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase))
                            {
                                if (propertyInfo.CanWrite)
                                {
                                    XmlAttribute xmlAttribute = xmlElement.Attributes[propertyInfo.Name];
                                    if (xmlAttribute != null)
                                    {
                                        try
                                        {
                                            if (ReferenceEquals(propertyInfo.PropertyType, typeof(bool)))
                                            {
                                                propertyInfo.SetValue(value, xmlAttribute.Value == "1", null);
                                            }
                                            else if (propertyInfo.PropertyType.IsEnum)
                                            {
                                                propertyInfo.SetValue(value, Enum.Parse(propertyInfo.PropertyType, xmlAttribute.Value), null);
                                            }
                                            //else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(System.Drawing.Icon)))
                                            //{
                                            //    propertyInfo.SetValue(value, Imaging.Converter.DeserializeToIcon(xmlAttribute.Value), null);
                                            //}
                                            //else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(System.Drawing.Image)))
                                            //{
                                            //    propertyInfo.SetValue(value, Imaging.Converter.DeserializeToImage(xmlAttribute.Value), null);
                                            //}
                                            else if (ReferenceEquals(propertyInfo.PropertyType, typeof(byte[])))
                                            {
                                                propertyInfo.SetValue(value, Imaging.Converter.DeserializeToBytes(xmlAttribute.Value), null);
                                            }
                                            else if (ReferenceEquals(propertyInfo.PropertyType, typeof(int)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToInt32(xmlAttribute.Value), null);
                                            }
                                            else if (ReferenceEquals(propertyInfo.PropertyType, typeof(string)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToString(xmlAttribute.Value), null);
                                            }
                                            else if (ReferenceEquals(propertyInfo.PropertyType, typeof(short)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToInt16(xmlAttribute.Value), null);
                                            }
                                            else if (ReferenceEquals(propertyInfo.PropertyType, typeof(byte)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToByte(xmlAttribute.Value), null);
                                            }
                                            else if (ReferenceEquals(propertyInfo.PropertyType, typeof(decimal)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToDecimal(xmlAttribute.Value), null);
                                            }
                                            else if (ReferenceEquals(propertyInfo.PropertyType, typeof(DateTime)))
                                            {
                                                DateTime dateValue = new DateTime(Convert.ToInt32(xmlAttribute.Value.Substring(0, 4)), Convert.ToInt32(xmlAttribute.Value.Substring(4, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(6, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(9, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(12, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(15, 2)));
                                                propertyInfo.SetValue(value, dateValue, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ex = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = null;
            }
        }

        public static void LoadXml(object value, string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            if (xmlDocument.ChildNodes[0] is XmlDeclaration)
                xmlDocument.RemoveChild(xmlDocument.ChildNodes[0]);
            LoadXml(value, xmlDocument);
        }

        #endregion

        #region " Replace special Xml marks "

        public static string UnFormat(string source)
        {
            string returnValue = string.Empty;
            if (ReferenceEquals(source.GetType(), typeof(string)))
            {
                returnValue = source.Replace(Erp.Base.Domain.Utils.Format.Ampersand, "&");
                returnValue = source.Replace(Erp.Base.Domain.Utils.Format.BeginTag, "<");
                returnValue = source.Replace(Erp.Base.Domain.Utils.Format.EndTag, ">");
                returnValue = source.Replace(Erp.Base.Domain.Utils.Format.Apostrophe, "'");
                returnValue = source.Replace(Erp.Base.Domain.Utils.Format.QuotationMark, Erp.Base.Domain.Utils.Format.Quotes);
            }
            return returnValue;
        }

        public static string Format(string source)
        {
            string returnValue = string.Empty;
            if (ReferenceEquals(source.GetType(), typeof(string)))
            {
                returnValue = source.Replace("&", Erp.Base.Domain.Utils.Format.Ampersand);
                returnValue = source.Replace(Erp.Base.Domain.Utils.Format.Quotes, Erp.Base.Domain.Utils.Format.QuotationMark);
                returnValue = source.Replace("<", Erp.Base.Domain.Utils.Format.BeginTag);
                returnValue = source.Replace(">", Erp.Base.Domain.Utils.Format.EndTag);
                returnValue = source.Replace("'", Erp.Base.Domain.Utils.Format.Apostrophe);
            }
            return returnValue;
        }
        #endregion

    }

    #endregion
}




namespace SuperNova.Imaging
{

    public sealed class Converter
    {

        #region " Capture Image "

        //public static Image CaptureScreen()
        //{
        //    return CaptureWindow(User32.GetDesktopWindow());
        //}
        ////CaptureScreen


        //public static Image CaptureWindow(IntPtr handle)
        //{
        //    // get te hDC of the target window
        //    IntPtr hdcSrc = User32.GetWindowDC(handle);
        //    // get the size
        //    User32.RECT windowRect = new User32.RECT();
        //    User32.GetWindowRect(handle, ref windowRect);
        //    int width = windowRect.right - windowRect.left;
        //    int height = windowRect.bottom - windowRect.top;
        //    // create a device context we can copy to
        //    IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);
        //    // create a bitmap we can copy it to,
        //    // using GetDeviceCaps to get the width/height
        //    IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
        //    // select the bitmap object
        //    IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
        //    // bitblt over
        //    Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Gdi32.SRCCOPY);
        //    // restore selection
        //    Gdi32.SelectObject(hdcDest, hOld);
        //    // clean up 
        //    Gdi32.DeleteDC(hdcDest);
        //    User32.ReleaseDC(handle, hdcSrc);

        //    // get a .NET image object for it
        //    Image img = Image.FromHbitmap(hBitmap);
        //    // free up the Bitmap object
        //    Gdi32.DeleteObject(hBitmap);

        //    return img;
        //}
        //CaptureWindow


        //public static void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        //{
        //    Image img = CaptureWindow(handle);
        //    img.Save(filename, format);
        //}
        ////CaptureWindowToFile


        //public static void CaptureScreenToFile(string filename, ImageFormat format)
        //{
        //    Image img = CaptureScreen();
        //    img.Save(filename, format);
        //}
        //CaptureScreenToFile

        private class Gdi32
        {

            public const int SRCCOPY = 0xcc0020;
            // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(nint hObject, int nXDest, int nYDest, int nWidth, int nHeight, nint hObjectSource, int nXSrc, int nYSrc, int dwRop);

            [DllImport("gdi32.dll")]
            public static extern nint CreateCompatibleBitmap(nint hDC, int nWidth, int nHeight);

            [DllImport("gdi32.dll")]
            public static extern nint CreateCompatibleDC(nint hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(nint hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(nint hObject);

            [DllImport("gdi32.dll")]
            public static extern nint SelectObject(nint hDC, nint hObject);
        }
        //GDI32

        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            //RECT

            [DllImport("user32.dll")]
            public static extern nint GetDesktopWindow();

            [DllImport("user32.dll")]
            public static extern nint GetWindowDC(nint hWnd);

            [DllImport("user32.dll")]
            public static extern nint ReleaseDC(nint hWnd, nint hDC);

            [DllImport("user32.dll")]
            public static extern nint GetWindowRect(nint hWnd, ref RECT rect);
        }
        //User32

        #endregion

        #region " Load Icons from File "

        [DllImport("shell32.dll")]
        private static extern nint ExtractIcon(nint hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("shell32.dll", EntryPoint = "ExtractIconExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int ExtractIconEx(string lpszFile, int nIconIndex, ref int phiconLarge, ref int phiconSmall, int nIcons);
        [DllImport("shell32.dll", EntryPoint = "ExtractIconExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        private static extern int ExtractIconEx(string lpszFile, int nIconIndex, [In(), Out()]
int[] phIconLarge, [In(), Out()]
int[] phIconSmall, int nIcons);
        [DllImport("shell32.dll", EntryPoint = "ExtractIconA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int DestroyIcon(int hIcon);


        private static int GetNumberOfIcons(string fileName)
        {
            int i = 0;
            return ExtractIconEx(fileName, -1, ref i, ref i, 0);
        }





        #endregion

        public static string ColorToHexadecimal(Color color)
        {
            return ColorTranslator.ToHtml(color);
            //Return String.Format("#{0}{1}{2}", Hex(color.R), Hex(color.G), Hex(color.B))
        }



        public static Color HexadecimalToColor(string color)
        {
            if (color.StartsWith("#") & color.Length <= 7)
            {
                return ColorTranslator.FromHtml(color);
            }
            else
            {
                return Color.Transparent;
            }
        }



        public static XmlDocument SerializeToXmlDocument(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            XmlTextWriter xmlWriter = new XmlTextWriter(writer);
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(bytes.GetType());
            XmlDocument xmlDocument = new XmlDocument();
            serializer.Serialize(xmlWriter, bytes);
            xmlWriter.Close();
            xmlDocument.LoadXml(builder.ToString());
            return xmlDocument;
        }

        public static string Serialize(byte[] bytes)
        {
            return SerializeToXmlDocument(bytes).ChildNodes[1].InnerText;
        }

        public static byte[] DeserializeToBytes(string value)
        {
            byte[] bytes = { 0 };
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNode = xmlDocument.CreateElement("base64Binary");
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration(Erp.Base.Domain.Utils.Xml.Version, Erp.Base.Domain.Utils.Xml.UnicodeId, string.Empty));
            xmlNode.InnerText = value;
            xmlDocument.AppendChild(xmlNode);
            using (MemoryStream stream = new MemoryStream())
            {
                xmlDocument.Save(stream);
                stream.Position = 0;
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(bytes.GetType());
                bytes = new byte[] { Convert.ToByte(serializer.Deserialize(stream)) };
            }
            return bytes;
        }



        public static string SetImageFile(string fullPath)
        {
            string imageInfo = "";
            FileInfo infoReader = default;
            //infoReader = Computer.FileSystem.GetFileInfo(fullPath);
            if (infoReader == null)
                return "";
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement xmlElement = default;
            xmlElement = xmlDocument.CreateElement("ImageInfo");
            xmlElement.SetAttribute("FileName", infoReader.Name);
            xmlElement.SetAttribute("Extension", infoReader.Extension);
            xmlElement.SetAttribute("Size", infoReader.Length.ToString());
            xmlElement.SetAttribute("DateModified", infoReader.LastWriteTime.ToShortDateString());
            xmlElement.SetAttribute("TimeModified", infoReader.LastWriteTime.ToShortTimeString());
            xmlElement.SetAttribute("Folder", infoReader.DirectoryName);
            return xmlElement.OuterXml;
        }

        public static string SetFormatImageFile(string innerXml)
        {
            string format = "";
            if (string.IsNullOrEmpty(innerXml))
                return "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(innerXml);
            foreach (XmlAttribute attribute in xmlDocument.FirstChild.Attributes)
            {
                format = format + attribute.Name + ": " + attribute.Value;// +Microsoft.VisualBasic.vbNewLine;
            }
            return format;
        }



        //Public Shared Function GetImageFromFile(ByVal fileName As String) As Drawing.Image
        //    Return GetImageFromFile(fileName, Drawing.Imaging.ImageFormat.Jpeg)
        //End Function











    }

}