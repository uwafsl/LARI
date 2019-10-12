using System;
using System.Collections.Generic;
using System.Xml;

namespace UW.XML
{
    /// <summary>
    /// Provide methods to deserialize/read xml for various objects.
    /// This is in charge of reading xml which has been serialized in some fashion.  
    /// This could have occured automatically via the framework or via a custom serialization process
    /// </summary>
    public static class UWDeserializer
    {
        //TODO: Move other deserialization methods to class (for example Point)

        /// <summary>
        /// Read from an XML file.  This is intended to be used with XmlSerialization procedures.
        /// 
        /// The XML was designed to have been created by an XmlSerializer for a List<double> object.
        /// For example
        /// 
        ///     List<double> currentRow = this.returnRowAtSpecifiedIndex(m);
        ///     XmlSerializer listDoubleSerializer = new XmlSerializer(typeof(List<double>));
        ///     listDoubleSerializer.Serialize(writer, currentRow);
        /// 
        /// At method entry: reader should be positioned at a <ArrayOfDouble> tag.  
        /// At method exit:  reader should be positioned just past the </<ArrayOfDouble> tag.
        /// 
        /// http://stackoverflow.com/questions/279534/proper-way-to-implement-ixmlserializable
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="reader"></param>
        public static List<double> ReadXmlArrayOfDouble(XmlReader reader)
        {
            List<double> ret = new List<double>();

            //now at the <ArrayOfDouble> tag, move onto the first element
            reader.Read();

            //check if there are any elements within the <ArrayOfDouble> or if it is empty
            if (reader.LocalName == "double")
            {
                //continue reading elements until we get to the </ArrayOfDouble> element
                bool exitFlag = false;
                while (!exitFlag)
                {
                    //check if we are at the end tag
                    if (reader.MoveToContent() == System.Xml.XmlNodeType.EndElement && reader.LocalName == "ArrayOfDouble")
                    {
                        exitFlag = true;
                    }
                    else
                    {
                        //read and parse the current element
                        if (reader.LocalName == "double")
                        {
                            ret.Add(Convert.ToDouble(reader.ReadElementString()));
                        }
                        else
                        {
                            throw new Exception("Encountered an unexpected element tag of '" + reader.Name + "'.");
                        }
                    }
                }

                //move the reader past the </ArrayOfDouble> tag
                reader.Read();
            }
            else
            {
                //assume this has no <double> elements
            }

            return ret;
        }
    }
}
