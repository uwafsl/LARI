using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UWLARI.Datatypes
{
    /// <summary>
    /// Checklist for a system. A checklist is made up of one or multiple checklist elements.
    /// </summary>
    class Checklist
    {
        #region Fields

        /// <summary>
        /// See Elements property
        /// </summary>
        private List<ChecklistElement> elements;

        #endregion

        #region Constructors

        public Checklist()
        {
            elements = new List<ChecklistElement>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new checklist element to the checklist. This checklist element will follow after the last.
        /// </summary>
        /// <param name="checkElem">Checklist element to add. A copy of it will be made.</param>
        public void AddChecklistElement(ChecklistElement checkElem)
        {
            if (checkElem == null)
            {
                throw new ArgumentNullException("checkElem is null");
            }
            elements.Add(checkElem);
        }

        #endregion

        #region Properties

        /// <summary>
        /// A list of readable-only checkilst elements.
        /// </summary>
        public ReadOnlyCollection<ChecklistElement> Elements
        {
            get
            {
                return new ReadOnlyCollection<ChecklistElement>(elements);
            }
        }

        #endregion

        // TODO: different types of checklist -- group elements into
        //       different category
    }
}
