using System.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace UW.LARI.Datatypes
{
    public enum WingTypes
    {
        [Description("Unspecified")]
        Unspecified,
        [Description("Fixed Wing")]
        FixedWing,
        [Description("Quadcopter")]
        Quad,
        [Description("Octocopter")]
        Octo,
        [Description("None")]
        None
    }

    /// <summary>
    /// This class represents a system . A system is a collection of components and a related description.
    /// </summary>
    public class AFSLSystem
    {
        //Version History:
        //07/25/18: Created
        //07/30/18: Added writeAsXML method and WingType
        //07/31/18: Add static LocalName property for xml encoding

        #region Fields

        /// <summary>
        /// See Name property.
        /// </summary>
        private string name;

        /// <summary>
        /// See Description property.
        /// </summary>
        private string description;

        /// <summary>
        /// See Components property
        /// </summary>
        private List<Component> components;

        /// <summary>
        /// See Body property
        /// </summary>
        private WingTypes wingType;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an object using the passed in arguments.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="components"></param>
        public AFSLSystem(string name = DefaultName, string description = DefaultDescription,
            List<Component> components = null, WingTypes wingType = DefaultWingType)
        {
            this.Name = name;
            this.Description = description;
            this.WingType = wingType;

            if (components != null)
            {
                this.Components = components;
            } else
            {
                this.Components = new List<Component>();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The name of this system.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// A desciption of this system.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// The components associated with the system.
        /// </summary>
        public List<Component> Components
        {
            get
            {
                return this.components;
            }

            set
            {
                if(this.components == null)
                {
                    this.components = new List<Component>();
                }
                foreach (Component component in value)
                {
                    this.AddComponent(component.DeepCopy());
                }
            }
        }

        /// <summary>
        /// The body type associated with the system.
        /// </summary>
        public WingTypes WingType
        {
            get
            {
                return this.wingType;
            }

            set
            {
                this.wingType = value;
            }
        }

        /// <summary>
        /// The body type of  the associated system in its displayable form. The returned string
        /// is defined by the "Description" attribute.
        /// </summary>
        public string WingTypeName
        {
            get
            {
                // Attempt to find the attribute labeled "Description"
                FieldInfo fi = this.wingType.GetType().GetField(this.wingType.ToString());
                if (fi != null)
                {
                    object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                    {
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }

                // If no attribute labeled "Description" was found, just return the raw string
                // representation.
                return this.wingType.ToString();
            }
        }

        /// <summary>
        /// The localName to be used for xml encoding
        /// </summary>
        public const string LocalName = "afslsystem";

        /// <summary>
        /// default construction values
        /// </summary>
        public const string DefaultName = "N/A";
        public const string DefaultDescription = "N/A";
        public const WingTypes DefaultWingType = WingTypes.Unspecified;

        #endregion

        #region Public Methods

        /// <summary>
        /// Add a component to the system
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            this.components.Add(component.DeepCopy());
        }

        /// <summary>
        /// Delete component from system
        /// </summary>
        /// <param name="partNumber"
        public void DeleteComponent(int partNumber)
        {
            int removed = this.Components.RemoveAll(x => x.PartNumber == partNumber);
            if(removed < 1)
            {
                throw new KeyNotFoundException("Component not found on system");
            }
            if(removed > 1)
            {
                throw new InvalidOperationException("Duplicate part numbers found in database");
            }
        }

        /// <summary>
        /// Delete component from system and return it
        /// </summary>
        /// <param name="partNumber"
        public Component PopComponent(int partNumber)
        {
            Component popped;
            for(int i = 0; i < this.Components.Count; i++)
            {
                if(this.Components[i].PartNumber == partNumber)
                {
                    popped = this.Components[i];
                    this.Components.RemoveAt(i);
                    return popped;
                }
            }
            throw new KeyNotFoundException("Component not found on system");
        }

        /// <summary>
        /// serialize the system as xml
        /// </summary>
        /// <param name="writer"></param>
        public void WriteAsXML(XmlWriter writer)
        {
            writer.WriteStartElement(AFSLSystem.LocalName);
            writer.WriteAttributeString(nameof(this.Name), this.Name);
            // TODO: the description may be a bit long for an attribute string.
            //    Consider adding tag for description.
            writer.WriteAttributeString(nameof(this.Description), this.Description);
            writer.WriteAttributeString(nameof(this.WingType), this.WingType.ToString());
            foreach (Component comp in this.Components)
            {
                comp.WriteAsXML(writer);
            }
            writer.WriteEndElement();
        }

        public void ReadFromXML(ref XmlReader reader)
        {
            this.Components = new List<Component>();

            if (reader.LocalName != AFSLSystem.LocalName)
            {
                throw new XmlException("Unexpected xml element. Expected: " + AFSLSystem.LocalName +
                                                        ", Value: " + reader.LocalName);
            }

            this.Name = reader.GetAttribute(nameof(this.Name));
            this.Description = reader.GetAttribute(nameof(this.Description));
            this.WingType = getWingTypeFromString(reader.GetAttribute(nameof(this.WingType)));

            if (reader.IsEmptyElement)
            {
                // handle system with no components
                return;
            }

            reader.Read(); // read past AFSLSystem start tag

            while (reader.LocalName != AFSLSystem.LocalName)
            {
                Component readComponent = new Component(ref reader);
                this.AddComponent(readComponent);
                reader.Read(); // read past end tag of component
            }
        }

        /// <summary>
        /// Creates a clone of a system object with an empty components list.
        /// </summary>
        /// <returns>System with same name, description and wing type but no components.</returns>
        public AFSLSystem EmptyClone()
        {
            return new AFSLSystem(this.name, this.description, wingType: this.wingType);
        }

        #endregion

        #region Private Methods

        // TODO: There has to be a standard way to do this
        private WingTypes getWingTypeFromString(string s)
        {
            switch(s)
            {
                case nameof(WingTypes.FixedWing):
                    return WingTypes.FixedWing;
                case nameof(WingTypes.Quad):
                    return WingTypes.Quad;
                case nameof(WingTypes.Octo):
                    return WingTypes.Octo;
                case nameof(WingTypes.None):
                    return WingTypes.None;
                default:
                    return WingTypes.Unspecified;
            }
        }

        #endregion
    }
}
