using System;
using UW.LARI.Datatypes;
using UW.WPF;
using LARI.Models;
using LARI.Utilities;
using System.Windows.Input;
using System.Windows;

namespace LARI.ViewModels
{
    /// <summary>
    /// AddComponentWindow view model.
    /// </summary>
    public class AddComponentViewModel : ViewModelBaseUW, IDisposable
    {
        //Version History:
        //07/25/18: Created

        #region Fields

        private Equipage equipage = null;
        private CommandHandler applyComponentWindow;
        private CommandHandler cancelComponentWindow;
        private string description;
        private string date;
        private double flightTime;
        private string prevAirFrames;
        private string crashes;
        private string notes;
        //int partNumber = -1;
        string location = "default location";
        Boolean isActive = true;
        private ComponentTrackerViewModel componentTracker;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddComponentViewModel()
        {
            this.componentTracker = new ComponentTrackerViewModel();
            this.startErrorHandling();
            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
            this.applyComponentWindow = new CommandHandler(ApplyComponent, CanApplyComponent());
            this.cancelComponentWindow = new CommandHandler(CancelComponent, CanCancelComponent());
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets apply system command.
        /// </summary>
        public CommandHandler ApplyComponentWindow
        {
            get { return this.applyComponentWindow; }
        }

        /// <summary>
        /// Gets cancel system command.
        /// </summary>
        public ICommand CancelComponentWindow
        {
            get { return this.cancelComponentWindow; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// What should the "apply" command do? 
        /// </summary>
        public void ApplyComponent()
        {
            bool exception = false;
            try
            {
                Component newComponent = new Component(this.description, equipage.Fleet.Count, this.flightTime, location, this.prevAirFrames, this.crashes, this.notes, isActive);
                this.equipage.AddComponent(newComponent);
                this.componentTracker.UpdateComponentDisplay();
            }
            catch (System.Exception ex)
            {
                exception = true;
                MessageBox.Show(ex.Message,
                                   "Add Component",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
            }
            if (exception == false)
            {
                if (this.componentTracker.IsInEditMode)
                {
                    //equipage.RemoveComponent(partNumber);
                    //this.manager.AcquireEquipage().RemoveSystem(this.componentTracker.SelectedSystem.Name);
                }
                //this.clearFields();
            }
        }

        /// <summary>
        /// Condition for whether apply command is enabled. 
        /// NOTE: When should this command be enabled? Always? Only when all fields are filled in?
        /// </summary>
        /// <returns>True if apply command is enabled.</returns>
        public bool CanApplyComponent()
        {
            return true;
        }

        /// <summary>
        /// Closes window when cancel command is executed
        /// </summary>
        public void CancelComponent()
        {

        }

        /// <summary>
        /// Condition for whether "cancel" button is enabled. "Cancel" should always be enabled.
        /// </summary>
        /// <returns>Always returns true to allow user to cancel adding component, thus closing the window.</returns>
        public bool CanCancelComponent()
        {
            return true;
        }

        /// <summary>
        /// Description of component being added or edited.
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("PrevAirFrames");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }

        public Boolean IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }

        public double FlightTime
        {
            get { return flightTime; }
            set
            {
                flightTime = value;
                OnPropertyChanged("FlightTime");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }

        public string PrevAirFrames
        {
            get { return prevAirFrames; }
            set
            {
                prevAirFrames = value;
                OnPropertyChanged("PrevAirFrames");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }

        public string Crashes
        {
            get { return prevAirFrames; }
            set
            {
                crashes = value;
                OnPropertyChanged("Crashes");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }

        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged("Notes");
                ApplyComponentWindow.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)

        /// <summary>
        /// Obtain necessary controllers from appropriate sources (for example: global data providers or a factory)
        /// </summary>
        private void acquireControllers()
        {
            this.equipage = EquipageModel.Instance.AcquireEquipage();
        }

        /// <summary>
        /// Release controllers which do not require persistence
        /// </summary>
        private void releaseControllers()
        {
        }

        private void createCommands()
        {
        }

        private void disposeCommands()
        {
        }

        private void subscribeToEvents()
        {
            this.unsubscribeFromEvents();

            //EXAMPLE
            //this.airspaceController.AirspaceAdded += this.airspaceController_AirspaceAdded;
        }

        private void unsubscribeFromEvents()
        {
            //EXAMPLE
            //this.airspaceController.AirspaceAdded -= this.airspaceController_AirspaceAdded;                
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
