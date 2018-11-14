using System;
using UW.LARI.Datatypes;

namespace LARI.Models
{
    /// <summary>
    /// A singleton class which contains the master equipage data structure. All updates and
    /// queries to the master equipage data structure should be performed through this class
    /// </summary>
    public sealed class EquipageModel
    {
        // Version History
        // 05/25/18: Created

        #region Fields
        
        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static volatile EquipageModel instance;

        /// <summary>
        /// Lock for multi-threaded singleton.
        /// </summary>
        private static object syncRoot = new object();

        #region Controllers

        // The master equipage data structure.
        private Equipage equipage = null;

        #endregion

        #endregion

        #region Constructors

        private EquipageModel()
        {
            // Prevent default constructor
        }

        #endregion

        #region Properties

        /// <summary>
        /// The unique, singleton instance of the EquipageModel.
        /// </summary>
        public static EquipageModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new EquipageModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Acquires the singleton, master instance of the equipage data structure. This purposely
        /// exposes the equipage data structure for querying and modification.
        /// </summary>
        /// <returns>The master equipage data structure.</returns>
        public Equipage AcquireEquipage()
        {
            return this.equipage;
        }

        /// <summary>
        /// Read equipage data from a properly formatted file.
        /// </summary>
        /// <param name="filePath">Path of file to read from.</param>
        public void ReadFromFile(string filePath)
        {
            AcquireEquipage().ReadFromXMLFile(filePath);
        }

        /// <summary>
        /// Read equipage data from a properly formatted file.
        /// </summary>
        /// <param name="filePath">Path of file to write to.</param>
        public void WriteToFile(string filePath)
        {
            AcquireEquipage().WriteToXMLFile(filePath);
        }

        #endregion

        #region Public Initialize and Dispose

        /// <summary>
        /// Initialize object. Note that this should be called by the framework and initializes
        /// the persistent controllers to their default state. Typically, the client would call
        /// this only during the application start up process.
        /// </summary>
        public void InitializeToDefaultState()
        {
            // Dispose of controllers to make sure start from scratch before acquiring controllers.
            this.disposeControllers();
            this.createControllers();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create controllers. Initializes the master equipage instance.
        /// </summary>
        private void createControllers()
        {
            this.equipage = new Equipage();
        }

        /// <summary>
        /// Dispose controllers. This should only be called when you no longer require the
        /// controllers because after this, the controllers will be null and data will not be
        /// persisted.
        /// </summary>
        private void disposeControllers()
        {
            if (this.equipage != null)
            {
                this.equipage.Dispose();
                this.equipage = null;
            }
        }

        #endregion
    }
}
