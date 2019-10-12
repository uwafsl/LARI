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
        /// See Description property.
        /// </summary>
        private string description;

        /// <summary>
        /// See PartNumber property
        /// </summary>
        private int partNumber;

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
        /// See HasCrashed property
        /// </summary>
        private string crashNotes;

        /// <summary>
        /// See Notes property
        /// </summary>
        private string generalNotes;

        /// <summary>
        /// See Active property
        /// </summary>
        private bool active;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a Component object using the passed in arguments.
        /// </summary>
        /// <param name="paramDescription"></param>
        /// <param name="paramPartNumber"></param>
        /// <param name="paramFlightTime"></param>
        /// <param name="paramLocation"></param>
        /// <param name="paramHistory"></param>
        /// <param name="paramHistory"></param>
        /// <param name="paramCrashNotes"></param>
        /// <param name="paramGeneralNotes"></param>
        /// <param name="paramActive"></param>
        public Component(string paramDescription, int paramPartNumber, double paramFlightTime,
                         string paramLocation, string paramHistory, string paramCrashNotes,
                         string paramGeneralNotes, bool paramActive)
        {
            description = paramDescription;
            partNumber = paramPartNumber;
            flightTime = paramFlightTime;
            location = paramLocation;
            history = paramHistory;
            crashNotes = paramCrashNotes;
            generalNotes = paramGeneralNotes;
            active = paramActive;
        }

        /// <summary>
        /// Constructs a component from a xml encoding.
        /// Expects order of elements to match WriteAsXML
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <returns></returns>
        public Component(ref XmlReader reader)
        {
            if (reader.LocalName != Component.LocalName)
            {
                throw new System.ArgumentException("XmlReader is misaligned. Want: " + Component.LocalName +
                                                        ", Have: " + reader.LocalName, "reader");
            }

            reader.Read();

            while (reader.LocalName != Component.LocalName)
            {
                switch (reader.LocalName)
                {
                    case "description":
                        description = reader.ReadElementContentAsString();
                        break;
                    case "id":
                        partNumber = reader.ReadElementContentAsInt();
                        break;
                    case "flighttime":
                        flightTime = reader.ReadElementContentAsDouble();
                        break;
                    case "location":
                        location = reader.ReadElementContentAsString();
                        break;
                    case "history":
                        history = reader.ReadElementContentAsString();
                        break;
                    case "crashnotes":
                        crashNotes = reader.ReadElementContentAsString();
                        break;
                    case "generalnotes":
                        generalNotes = reader.ReadElementContentAsString();
                        break;
                    case "active":
                        active = reader.ReadElementContentAsBoolean();
                        break;
                }
            }
        }

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
        public int PartNumber
        {
            get
            {
                return this.partNumber;
            }
            set
            {
                this.partNumber = value;
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
        public string CrashNotes
        {
            get
            {
                return this.crashNotes;
            }
            set
            {
                this.crashNotes = value;
            }

        }

        /// <summary>
        /// Miscellaneous notes
        /// </summary>
        public string GeneralNotes
        {
            get
            {
                return this.generalNotes;
            }
            set
            {
                this.generalNotes = value;
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



        /// <summary>
        /// Appends a component as xml to an xmlwriter
        /// Expects order of elements to match ReadFromXML
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <returns></returns>
        public void WriteAsXML(XmlWriter writer)
        {
            // create the root node
            writer.WriteStartElement(Component.LocalName);

            // create attributes for properties
            writer.WriteElementString("description", this.Description);
            writer.WriteElementString("id", Convert.ToString(this.PartNumber));
            writer.WriteElementString("flighttime", Convert.ToString(this.FlightTime));
            writer.WriteElementString("location", this.Location);
            writer.WriteElementString("history", this.History);
            writer.WriteElementString("crashnotes", this.CrashNotes);
            writer.WriteElementString("generalnotes", this.GeneralNotes);
            writer.WriteElementString("active", Convert.ToString(this.Active));

            // close attribute writer
            writer.WriteEndElement();
        }

        /// <summary>
        /// localName used to encode object as xml element
        /// </summary>
        public const string LocalName = "component";

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a new object which is unique from the calling object (changes to the original do not affect the copy, and vice versa).
        /// </summary>
        /// <returns>Component copy</returns>  
        public Component DeepCopy()
        {
            //create a new object
            Component copy = new Component(this.Description, this.PartNumber, this.FlightTime, this.Location, 
                                            this.History, this.CrashNotes, this.GeneralNotes, this.Active);
            return copy;
        }

        #endregion
    }
}
