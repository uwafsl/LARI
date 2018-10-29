using System;
using UW.LARI.Datatypes;

namespace LARI.Models
{
    /// <summary>
    /// A singleton class which represents the manager for the application.
    /// 
    /// This manager is responsible for maintaining controllers which require data persistence.
    /// Data persistence implies that the controller maintains it state even thought the a panel or 
    /// map layer which uses this controller is closed or releases its reference to the controller.
    /// 
    /// The singleton pattern follows the pattern outlined in the MSDN article "Implementing Singleton in C#"
    /// http://msdn.microsoft.com/en-us/library/ff650316.aspx
    /// 
    /// AFSLRefactor: Make this inherit from an interface (do this later once we know what should go in the interface)
    /// </summary>
    public sealed class ManagerModel
    {
        // Version History
        // 05/25/18: Created

        #region Fields
        
        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static volatile ManagerModel instance;

        /// <summary>
        /// Lock for multi-threaded singleton.
        /// </summary>
        private static object syncRoot = new object();

        #region Controllers

        // A vehicle independent controllers
        private Equipage equipage = null;

        #endregion

        #endregion

        #region Constructors

        private ManagerModel()
        {
            //prevent default constructor
        }

        #endregion

        #region Properties

        /// <summary>
        /// The unique, singleton instance of the ManagerModel.
        /// </summary>
        public static ManagerModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ManagerModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Public Methods (acquire vehicle independent controllers)

        public Equipage AcquireEquipage()
        {
            return this.equipage;
        }

        public void ReadFromXMLFile(string directoryFileName)
        {
            AcquireEquipage().ReadFromXMLFile(directoryFileName);
        }

        public void WriteToXMLFile(string directoryFileName)
        {
            AcquireEquipage().WriteToXMLFile(directoryFileName);
        }

        #endregion

        #region Public Initialize and Dispose

        /// <summary>
        /// Initialize object.  Note that this should be called by the framework and initializes the persistent controllers to their default state.
        /// Typically, the client would call this only during the application start up process.
        /// </summary>
        public void InitializeToDefaultState()
        {
            //UWAFSLRefactor: Set the file location for the MessageHandler

            //dispose of controllers to make sure start from scratch before acquiring controllers
            this.disposeVehicleIndependentControllers();
            this.createVehicleIndependentControllers();
        }

        /// <summary>
        /// Shutdown method that serializes settings controller properties
        /// </summary>
        public void Shutdown()
        {
            //UWAFSL: TO DO
            //UtilitiesRoot.DataFiles.SerializeToDisk(this.fileController.SettingsDirectory + "Settings.xml", this.settingsController);
            throw new NotImplementedException("Not implemented yet");
        }

        #endregion

        #region Private Methods (create/dispose vehicle independent controllers)

        /// <summary>
        /// Create vehicle independent controllers.
        /// AFSLRefactor: Change the name of method (this has nothing to do with vehicles)
        /// </summary>
        private void createVehicleIndependentControllers()
        {
            // Version History
            // 05/25/18: Created
            
            // create/initialize vehicle independent controllers. 
            // Note that order matters. For example, if you try to create the occupancyMapController before the searchBoundary controller, 
            // you will have problems because the occupancyMapController uses the searchBoundary controller during construction.
            // The least dependent controllers need to be instantiated first.  i.e. if a particular controller (A) needs (acquires) another controller (B), 
            // then A must be "newed" up here after B.
            this.equipage = new Equipage();
        }

        /// <summary>
        /// Dispose vehicle independent controllers.  This should only be called when you no longer require the controllers because after this,
        /// the controllers will be null and data will not be persisted.
        /// </summary>
        private void disposeVehicleIndependentControllers()
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
