using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace UWXML.Serialization
{
    public static class UWSerializer
    {
        /// <summary>
        /// Write XML associated with the specified value.  
        /// 
        /// For example, this could write (if value = 23.43)
        /// 
        ///     <double>23.43</double>
        ///     
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteXmlDouble(XmlWriter writer, double value)
        {
            XmlSerializer listDoubleSerializer = new XmlSerializer(typeof(double));
            listDoubleSerializer.Serialize(writer, value);
        }
    }
}
