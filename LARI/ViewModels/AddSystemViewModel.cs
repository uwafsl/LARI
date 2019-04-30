using System;
using UW.LARI.Datatypes;
using UW.WPF;
using LARI.Models;
using LARI.Utilities;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace LARI.ViewModels
{
    /// <summary>
    /// View model for AddSystemWindow.
    /// </summary>
    public class AddSystemViewModel : ViewModelBaseUW, IDisposable
    {
        //Version History:
        //07/25/18: Created

        #region Fields

        /// <summary>
        /// Singleton manager for equipage.
        /// </summary>
        private EquipageModel manager;

        /// <summary>
        /// System to be added/edited.
        /// </summary>
        private AFSLSystem system;

        /// <summary>
        /// New/current system's name.
        /// </summary>
        private string name;

        /// <summary>
        /// New/current system's description.
        /// </summary>
        private string description;

        /// <summary>
        /// New/current system's wing type.
        /// </summary>
        private WingTypes wingType;

        /// <summary>
        /// Command to add new system or apply edits to currently selected system.
        /// </summary>
        private CommandHandler applySystemWindow;

        /// <summary>
        /// ComponentTrackerViewModel which initialized current AddSystemWindow.
        /// </summary>
        private ComponentTrackerViewModel componentTracker;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddSystemViewModel(ComponentTrackerViewModel vM)
        {
            componentTracker = vM;
            this.name = AFSLSystem.DefaultName;
            this.description = AFSLSystem.DefaultDescription;
            this.wingType = AFSLSystem.DefaultWingType;

            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Name of system being added or edited.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
                ApplySystemWindow.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Description for system being added or edited.
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
                ApplySystemWindow.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Wing type for system being added or edited.
        /// </summary>
        public WingTypes SelectedWingType
        {
            get { return wingType; }
            set
            {
                wingType = value;
                OnPropertyChanged("SelectedWingType");
                ApplySystemWindow.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets apply system command.
        /// </summary>
        public CommandHandler ApplySystemWindow
        {
            get { return this.applySystemWindow; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds system to equipage. If editing component, removes old component and adds edited component.
        /// </summary>
        public void ApplySystem()
        {
            bool exception = false;
            ObservableCollection<AFSLSystem> tempSystem = this.componentTracker.Systems;
            try
            {
                AFSLSystem newSys = new AFSLSystem(this.name, this.description, wingType: this.wingType);
                this.manager.AcquireEquipage().AddSystem(newSys);
                this.componentTracker.UpdateSystemDisplay();
            }
            catch (System.Exception ex)
            {
                exception = true;
                MessageBox.Show(ex.Message,
                                   "Add System",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
            }
            if (exception == false)
            {
                if (this.componentTracker.IsInEditMode)
                {
                    tempSystem.Remove(this.componentTracker.SelectedSystem);
                    this.manager.AcquireEquipage().RemoveSystem(this.componentTracker.SelectedSystem.Name);
                }
                tempSystem.Add(this.system);
                this.clearFields();
                componentTracker.addSystemWindow.Close();
            }
        }

        /// <summary>
        /// Condition for whether apply command is enabled. 
        /// NOTE: When should this command be enabled? Always? Only when all fields are filled in?
        /// </summary>
        /// <returns>True if apply command is enabled.</returns>
        public bool CanApplySystem()
        {
            return true;
        }

        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)
        private AFSLSystem initializeSystem()
        {
            if (this.componentTracker.IsInEditMode)
            {
                return componentTracker.SelectedSystem;
            }
            return new AFSLSystem();
        }
        
        /// <summary>
        /// Clears text boxes and selected wing type.
        /// </summary>
        private void clearFields()
        {
            Name = AFSLSystem.DefaultName;
            Description = AFSLSystem.DefaultDescription;
            SelectedWingType = AFSLSystem.DefaultWingType;
            this.componentTracker.IsInEditMode = false;
        }

        /// <summary>
        /// Obtain necessary controllers from appropriate sources (for example: global data providers or a factory)
        /// </summary>
        private void acquireControllers()
        {
            this.manager = EquipageModel.Instance;
        }

        /// <summary>
        /// Release controllers which do not require persistence
        /// </summary>
        private void releaseControllers()
        {
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void createCommands()
        {
            this.applySystemWindow = new CommandHandler(ApplySystem, CanApplySystem());
        }

        private void disposeCommands()
        {
        }

        private void subscribeToEvents()
        {
            this.unsubscribeFromEvents();
        }

        private void unsubscribeFromEvents()
        {            
        }

        /// <summary>
        /// Start error handling and logging.  Mostly for displaying "toast" messages and writing messages to the log files.
        /// </summary>
        private void startErrorHandling()
        {
        }

        /// <summary>
        /// Stop error handling and logging.
        /// </summary>
        private void stopErrorHandling()
        {
        }

        /// <summary>
        /// Initialize and instantiate private fields that are not controllers, commands, or error token (these are initialized in other methods)
        /// </summary>
        private void initializeOtherPrivateFields()
        {
            this.system = initializeSystem();
        }

        /// <summary>
        /// Dispose of any other private fields which are unmanaged.  Fields which are controllers, commands, or error tokens are disposed in other methods)
        /// </summary>
        private void disposeUnmanagedOtherPrivateFields()
        {
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose the resources associated with this view model.
        /// </summary>
        public void Dispose()
        {
            this.unsubscribeFromEvents();
            this.releaseControllers();
            this.disposeCommands();
            this.disposeUnmanagedOtherPrivateFields();
            this.stopErrorHandling();
        }

        #endregion
    }
}
