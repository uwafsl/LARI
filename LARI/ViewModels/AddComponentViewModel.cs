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
    /// AddComponentWindow view model.
    /// </summary>
    public class AddComponentViewModel : ViewModelBaseUW, IDisposable
    {
        //Version History:
        //07/25/18: Created

        #region Fields

        private Equipage equipage;
        private CommandHandler applyComponentWindow;
        private CommandHandler cancelComponentWindow;
        private string description;
        private DateTime date;
        private double flightTime;
        private string prevAirFrames;
        private string crashes;
        private string notes;
        // int partNumber;
        string location = "Inventory"; // used to be default location
        Boolean isActive = true;
        private ComponentTrackerViewModel componentTracker;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddComponentViewModel(ComponentTrackerViewModel vM)
        {
            this.componentTracker = vM;
            this.startErrorHandling();
            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
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
            Console.WriteLine("---Original System---");
            foreach (AFSLSystem sys in this.equipage.Fleet)
            {
                Console.WriteLine("System :" +  sys.Name);
                ListComponents(sys);
            }

            AFSLSystem inventory = GetSystem(Equipage.InventorySystem);
            Console.WriteLine("---Inventory---");
            ListComponents(inventory);

            AFSLSystem specifiedSys = GetSystem(location);
            Console.WriteLine("---" + location + "---");
            ListComponents(specifiedSys);

            //ObservableCollection<Component> tempSystem = new ObservableCollection<Component>(this.equipage.Fleet[0].Components);
            Random rand = new Random();
            try
            {
                // TODO: Add to both inventory and components. There is something wrong on the backend, so this is blocked for now.
                Component newComponent = new Component(this.description, rand.Next(), this.flightTime, location, this.prevAirFrames, this.crashes, this.notes, isActive);
                // this.equipage.AddComponent(newComponent);
                this.equipage.AddComponent(newComponent, location);
                // this.componentTracker.UpdateComponentDisplay();
                Console.WriteLine(equipage.Fleet);
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
               // tempSystem.Add(this.system);
            }
            Console.WriteLine("---Modified System---");
            foreach (AFSLSystem sys in this.equipage.Fleet)
            {
                Console.WriteLine("System :" + sys.Name);
                ListComponents(sys);
            }
        }

        /// <summary>
        /// Private helper method to retrieve a system from the current fleet
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        private AFSLSystem GetSystem(string systemName)
        {
            for (int i = 0; i < this.equipage.NumVehicles; i++)
            {
                if (this.equipage.Fleet[i].Name.Equals(systemName))
                {
                    return this.equipage.Fleet[i];
                }
            }
            return null;
        }

        private void ListComponents(AFSLSystem system)
        {
            foreach (Component c in system.Components)
            {
                Console.WriteLine("\tComponent: " + c.PartNumber + ": " + c.Description);
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

        public DateTime Date
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
            this.applyComponentWindow = new CommandHandler(ApplyComponent, CanApplyComponent());
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
