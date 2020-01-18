using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace UW.LARI.Datatypes
{
    /// <summary>
    /// This class represents a system component. A system component is
    /// composed of a unique part number and a related description.
    /// </summary>
    public class Component
    {
        //Version History:
        //07/18/18: Created
        //07/25/18: Added DeepCopy method

        #region Fields

        /// <summary>
        /// See Id property.
        /// </summary>
        private int id;

        /// <summary>
        /// See Name property.
        /// </summary>
        public string name;

        /// <summary>
        /// See StartDate property.
        /// </summary>
        private string startDate;

        /// <summary>
        /// See Description property.
        /// </summary>
        private string description;

        /// <summary>
        /// See SerialNumber property
        /// </summary>
        private string serialNumber;

        /// <summary>
        /// See FlightTime property
        /// </summary>
        private double flightTime;

        /// <summary>
        /// See Location property
        /// </summary>
        private string location;

        /// <summary>
        /// See History property
        /// </summary>
        private string history;

        /// <summary>
        /// See Damaged property
        /// </summary>
        private bool damaged;

        /// <summary>
        /// See Active property
        /// </summary>
        private bool active;

        /// <summary>
        /// See System property
        /// </summary>
        private string system;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a Component object using the passed in arguments.
        /// </summary>
        /// <param name="paramId"></param>
        /// <param name="paramName"></param>
        /// <param name="paramStartDate"></param>
        /// <param name="paramDescription"></param>
        /// <param name="paramSerialNumber"></param>
        /// <param name="paramFlightTime"></param>
        /// <param name="paramLocation"></param>
        /// <param name="paramHistory"></param>
        /// <param name="paramDamaged"></param>
        /// <param name="paramActive"></param>
        public Component(int paramId, string paramName, string paramStartDate, 
                         string paramDescription, string paramSerialNumber, double paramFlightTime,
                         string paramLocation, string paramHistory, bool paramDamaged, bool paramActive, string paramSystem)
        {
            id = paramId;
            name = paramName;
            description = paramDescription;
            serialNumber = paramSerialNumber;
            flightTime = paramFlightTime;
            startDate = paramStartDate;
            location = paramLocation;
            history = paramHistory;
            damaged = paramDamaged;
            active = paramActive;
            system = paramSystem;
        }

        /// <summary>
        /// Constructs a component from a xml encoding.
        /// Expects order of elements to match WriteAsXML
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <returns></returns>
        //[Obsolete("XML Component reader is deprecated, database is now using SQLite")]
        //public Component(ref XmlReader reader)
        //{
        //    if (reader.LocalName != Component.LocalName)
        //    {
        //        throw new System.ArgumentException("XmlReader is misaligned. Want: " + Component.LocalName +
        //                                                ", Have: " + reader.LocalName, "reader");
        //    }

        //    reader.Read();

        //    while (reader.LocalName != Component.LocalName)
        //    {
        //        switch (reader.LocalName)
        //        {
        //            case "description":
        //                description = reader.ReadElementContentAsString();
        //                break;
        //            case "id":
        //                partNumber = reader.ReadElementContentAsInt();
        //                break;
        //            case "flighttime":
        //                flightTime = reader.ReadElementContentAsDouble();
        //                break;
        //            case "location":
        //                location = reader.ReadElementContentAsString();
        //                break;
        //            case "history":
        //                history = reader.ReadElementContentAsString();
        //                break;
        //            case "crashnotes":
        //                crashNotes = reader.ReadElementContentAsString();
        //                break;
        //            case "generalnotes":
        //                generalNotes = reader.ReadElementContentAsString();
        //                break;
        //            case "active":
        //                active = reader.ReadElementContentAsBoolean();
        //                break;
        //        }
        //    }
        //}

        #endregion

        #region Properties

        /// <summary>
        /// A desciption of this component.
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
        /// The unique part number associated with this component.
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
            set
            {
                this.serialNumber = value;
            }
        }

        /// <summary>
        /// The total flight time (in minutes) logged on this component.
        /// </summary>
        public double FlightTime
        {
            get
            {
                return this.flightTime;
            }
            set
            {
                this.flightTime = value;
            }
        }

        /// <summary>
        /// The current location of this component. 
        /// </summary>
        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        /// <summary>
        /// Previous airframes and removal dates associated with this component.
        /// </summary>
        public string History
        {
            get
            {
                return this.history;
            }
            set
            {
                this.history = value;
            }
        }

        /// <summary>
        /// Notes on any previous crashes
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

        public Boolean Damaged
        {
            get
            {
                return this.damaged;
            }
            set
            {
                this.damaged = value;
            }
        }

        /// <summary>
        /// Whether or not the component is currently active.
        /// </summary>
        public Boolean Active
        {
            get
            {
                return this.active;
            }
            set
            {
                this.active = value;
            }
        }

        public string StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
            }
        }

        public int Id
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

        public string System
        {
            get
            {
                return this.system;
            }
        }



        /// <summary>
        /// Appends a component as xml to an xmlwriter
        /// Expects order of elements to match ReadFromXML
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <returns></returns>
        //[Obsolete("WriteAsXML is deprecated, database is now using SQLite")]
        //public void WriteAsXML(XmlWriter writer)
        //{
        //    // create the root node
        //    writer.WriteStartElement(Component.LocalName);

        //    // create attributes for properties
        //    writer.WriteElementString("description", this.Description);
        //    writer.WriteElementString("id", Convert.ToString(this.PartNumber));
        //    writer.WriteElementString("flighttime", Convert.ToString(this.FlightTime));
        //    writer.WriteElementString("location", this.Location);
        //    writer.WriteElementString("history", this.History);
        //    writer.WriteElementString("crashnotes", this.CrashNotes);
        //    writer.WriteElementString("generalnotes", this.GeneralNotes);
        //    writer.WriteElementString("active", Convert.ToString(this.Active));

        //    // close attribute writer
        //    writer.WriteEndElement();
        //}

        /// <summary>
        /// localName used to encode object as xml element
        /// </summary>
        //public const string LocalName = "component";

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a new object which is unique from the calling object (changes to the original do not affect the copy, and vice versa).
        /// </summary>
        /// <returns>Component copy</returns>  
        public Component DeepCopy()
        {
            //create a new object
            Component copy = new Component(this.Id, this.Name, this.StartDate,
                         this.Description, this.SerialNumber, this.FlightTime,
                         this.Location, this.History, this.Damaged, this.Active, this.System);
            return copy;
        }

        #endregion
    }
}
