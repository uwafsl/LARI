using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace UWLARI.Datatypes
{
    // TODO: We may want to create a seperate non-boolean checklist element class that will be
    //       tied to specific components and systems, then used to generate checkable checklist
    //       elements. If this is done, consider having this class inherit from it / extend it.

    /// <summary>
    /// A checklist element, made up of one or more checklist entries which can be marked as
    /// checked or unchecked.
    /// </summary>
    class ChecklistElement
    {
        #region Fields

        // A list of checklist entries
        private List<Tuple<string, bool>> entries;

        #endregion

        #region Constructors

        public ChecklistElement()
        {
            entries = new List<Tuple<string, bool>>();
        }
        
        public ChecklistElement(ref XmlReader reader)
        {
            // remove all entries every time we laod in a checklist file
            entries.Clear();

            if (reader.LocalName != ChecklistElement.LocalName)
            {
                throw new System.ArgumentException("XmlReader is misaligned. Want: " + ChecklistElement.LocalName +
                                                        ", Have: " + reader.LocalName, "reader");

                reader.Read();

                while(reader.LocalName != ChecklistElement.LocalName)
                {
                    String desc = reader.ReadElementContentAsString();
                    entries.Add(new Tuple<string, bool>(desc, false));
                }
            }
        }

        

        #endregion

        // TODO: figure ways to load and save checklist elements

        #region Public Methods

        /// <summary>
        /// Adds a new checklist entry and sets it as unchecked.
        /// </summary>
        /// <param name="desc">Description of new checklist entry.</param>
        public void AddEntry(string desc)
        {
            // TODO
            entries.Add(new Tuple<string, bool>(desc, false));
        }

        #endregion

        #region Properties

        /// <summary>
        /// A list of readable-only entries of string with a boolean value
        /// to keep track of whether it's being checked
        /// </summary>
        public ReadOnlyCollection<Tuple<string, bool>> Entries
        {
            get
            {
                return new ReadOnlyCollection<Tuple<string, bool>>(entries);
            }
        }

        /// <summary>
        /// localName used to encode object as xml element
        /// </summary>
        public const string LocalName = "entries";

        /// <summary>
        /// Appends a component as xml to an xmlwriter
        /// Expects order of elements to match ReadFromXML
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <returns></returns>
        public void WriteAsXML(XmlWriter writer)
        {
            // create the root node
            writer.WriteStartElement(ChecklistElement.LocalName);

            // create attributes for properties
            // TODO: 
            foreach (Tuple<String, bool> entry in entries)
            {
                writer.WriteElementString("entry", entry.Item1);
            }

            // close attribute writer
            writer.WriteEndElement();
        }
        #endregion

        // TODO: Read and Write XML file

        // TODO: add methods


    }
}
