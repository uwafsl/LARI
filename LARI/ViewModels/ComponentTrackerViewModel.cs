using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml;
using LARI.Models;
using LARI.Utilities;
using LARI.Views;
using UW.LARI.Datatypes;
using UW.WPF;

namespace LARI.ViewModels
{
    public class ComponentTrackerViewModel : ViewModelBaseUW, IDisposable
    {
        #region Fields
        /// <summary>
        /// Singleton equipage model for referencing the master equipage data structure.
        /// </summary>
        private EquipageModel equipageModel;

        /// <summary>
        /// Observable collection of systems.
        /// </summary>
        private ObservableCollection<AFSLSystem> systems;

        /// <summary>
        /// List of selected systems by user.
        /// </summary>
        private AFSLSystem selectedSystem;

        #region Commands
        /// <summary>
        /// Command to browse for equipage file path.
        /// </summary>
        private CommandHandler browseEquipageFilePath;

        /// <summary>
        /// Command to load database from file path.
        /// </summary>
        private CommandHandler loadDb;

        /// <summary>
        /// Command to save database to file path.
        /// </summary>
        private CommandHandler saveDb;

        /// <summary>
        /// Command to add new system.
        /// </summary>
        private CommandHandler addSystemCommand;

        /// <summary>
        /// Command to edit currently selected system.
        /// </summary>
        private CommandHandler editSystemCommand;

        /// <summary>
        /// Command to delete currently selected system.
        /// </summary>
        private CommandHandler deleteSystemCommand;

        /// <summary>
        /// Command to add new system.
        /// </summary>
        private CommandHandler addComponentCommand;

        /// <summary>
        /// Command to edit currently selected system.
        /// </summary>
        private CommandHandler editComponentCommand;

        /// <summary>
        /// Command to delete currently selected system.
        /// </summary>
        private CommandHandler deleteComponentCommand;

        /// <summary>
        /// Boolean to communicate with AddSystem/Component View about if edit has been clicked.
        /// </summary>
        private bool isInEditMode;

        #endregion

        /// <summary>
        /// See EquipageFilePath property.
        /// </summary>
        private string equipageFilePath;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ComponentTrackerViewModel()
        {
            this.startErrorHandling();
            this.initializePrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Path of file where equipage data is stored.
        /// </summary>
        public string EquipageFilePath
        {
            get
            {
                return this.equipageFilePath;
            }
            set
            {
                this.equipageFilePath = value;
                OnPropertyChanged("EquipageFilePath");
            }
        }

        /// <summary>
        /// Collection of systems available to user.
        /// </summary>
        public ObservableCollection<AFSLSystem> Systems
        {
            get { return this.systems; }
        }

        // TODO: Make this so that multiple selections can be saved in a list to allow for deleting multiple systems at a time.
        /// <summary>
        /// List of systems selected by user.
        /// </summary>
        public AFSLSystem SelectedSystem
        {
            get { return this.selectedSystem; }
            set
            {
                this.selectedSystem = value;
                OnPropertyChanged("SelectedSystem");
                EditSystemCommand.RaiseCanExecuteChanged();
                DeleteSystemCommand.RaiseCanExecuteChanged();

            }
        }

        /// <summary>
        /// Gets browse command.
        /// </summary>
        public CommandHandler BrowseEquipageFilePath
        {
            get { return this.browseEquipageFilePath; }
        }

        /// <summary>
        /// Gets load file command.
        /// </summary>
        public CommandHandler LoadDb
        {
            get { return this.loadDb; }
        }

        /// <summary>
        /// Gets save file command.
        /// </summary>
        public CommandHandler SaveDb
        {
            get { return this.saveDb; }
        }

        /// <summary>
        /// Gets add system command.
        /// </summary>
        public CommandHandler AddSystemCommand
        {
            get { return this.addSystemCommand; }
        }
        
        /// <summary>
        /// Gets edit system command.
        /// </summary>
        public CommandHandler EditSystemCommand
        {
            get { return this.editSystemCommand; }
        }

        /// <summary>
        /// Gets delete system command.
        /// </summary>
        public CommandHandler DeleteSystemCommand
        {
            get { return this.deleteSystemCommand; }
        }

        /// <summary>
        /// Gets add component command.
        /// </summary>
        public CommandHandler AddComponentCommand
        {
            get { return this.addComponentCommand; }
        }

        /// <summary>
        /// Gets edit Component command.
        /// </summary>
        public CommandHandler EditComponentCommand
        {
            get { return this.editComponentCommand; }
        }

        /// <summary>
        /// Gets delete Component command.
        /// </summary>
        public CommandHandler DeleteComponentCommand
        {
            get { return this.deleteComponentCommand; }
        }

        public bool IsInEditMode
        {
            get { return this.isInEditMode; }
            set { this.isInEditMode = value; }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Opens file explorer to allow user to select file path.
        /// </summary>
        public void BrowseFilePath()
        {
            // Create OpenFileDialog.
            Microsoft.Win32.OpenFileDialog dialogue = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension.
            dialogue.DefaultExt = ".xml";

            // Display OpenFileDialog.
            bool? result = dialogue.ShowDialog();

            if (result == true)
            {
                // We update the UserSettingsModel indirectly in order to trigger a property changed event/binding in the view.
                EquipageFilePath = dialogue.FileName;
            }
        }

        // TODO: Make this populate the componenets table too.
        /// <summary>
        /// Loads data from xml file path. Populates systems table.
        /// </summary>
        public void LoadFilePath()
        {
            if (String.IsNullOrEmpty(EquipageFilePath))
            {
                MessageBox.Show("Please select a database file to load",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                this.equipageModel.ReadFromFile(EquipageFilePath);
                this.UpdateSystemDisplay();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Selected database file was not found",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }
            catch (XmlException)
            {
                MessageBox.Show("Failed to read selected database file",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }
        }
        
        // TODO: Make this functional.
        /// <summary>
        /// Saves database to file path.
        /// </summary>
        public void SaveFilePath()
        {
            if (String.IsNullOrEmpty(EquipageFilePath))
            {
                MessageBox.Show("Please select a database file",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                this.equipageModel.WriteToFile(EquipageFilePath);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Selected database file was not found",
                    "Component Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Database saved to selected file path.",
                "Component Tracker",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Opens window to allow user to add new system.
        /// </summary>
        public void AddSystem()
        {
            IsInEditMode = false;
            AddSystemWindow addSystemWindow = new AddSystemWindow(this);
            addSystemWindow.ShowDialog();
        }

        /// <summary>
        /// Updates the systems display.
        /// </summary>
        public void UpdateSystemDisplay()
        {
            this.systems = new ObservableCollection<AFSLSystem>(this.equipageModel.AcquireEquipage().Fleet);
            OnPropertyChanged("Systems");
        }

        /// <summary>
        /// Determines whether the "Add" system button is enabled.
        /// </summary>
        /// <returns>True if "Add" button is enabled, False if not enabled.</returns>
        public bool CanAddSystem()
        {
            return true;
        }

        /// <summary>
        /// TODO: Opens new AddSystemWindow with the currently selected system's fields populated.
        /// </summary>
        public void EditSystem()
        {
            if (CanEditOrDeleteSystem())
            {
                IsInEditMode = true;
                AddSystemWindow addSystemWindow = new AddSystemWindow(this);
                addSystemWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("A single system must be selected to edit",
                                   "Component Tracker",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Removes selected system from systems.
        /// </summary>
        public void DeleteSystem()
        {
            if (CanEditOrDeleteSystem())
            {
                Systems.Remove(SelectedSystem);
            }
            else
            {
                MessageBox.Show("A single system must be selected to delete",
                                   "Component Tracker",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Condition for whether delete command is enabled.
        /// </summary>
        /// <returns>True if a system is selected. False if a system is not selected.</returns>
        public bool CanEditOrDeleteSystem()
        {
            if (SelectedSystem != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Opens window to allow user to add new system.
        /// </summary>
        public void AddComponent()
        {
            AddComponentWindow addComponentWindow = new AddComponentWindow();
            addComponentWindow.ShowDialog();
        }

        /// <summary>
        /// Determines whether the "Add" component button is enabled.
        /// </summary>
        /// <returns>True if "Add" button is enabled, False if not enabled.</returns>
        public bool CanAddComponent()
        {
            return true;
        }

        /// <summary>
        /// TODO: Opens new AddComponentWindow with the currently selected component's fields populated.
        /// </summary>
        public void EditComponent()
        {
            if (CanEditOrDeleteComponent())
            {
                // edit functionality goes here
            }
            else
            {
                MessageBox.Show("A single component must be selected to edit",
                                   "Component Tracker",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Condition for whether the edit button is enabled.
        /// </summary>
        /// <returns>True if a component is selected. False if none are selected.</returns>
        public bool CanEditOrDeleteComponent()
        {
            /* TODO: Change this from selected system to selected component
            if (SelectedSystem != null)
            {
                return true;
            }
            */
            return false;
        }

        /// <summary>
        /// Removes selected component from components.
        /// </summary>
        public void DeleteComponent()
        {
            if (CanEditOrDeleteComponent())
            {
                // delete functionality goes here
            }
            else
            {
                MessageBox.Show("A single component must be selected to delete",
                                   "Component Tracker",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
            }
        }

        #endregion

        #region Private Methods
       
        /// <summary>
        /// Aquire controllers.
        /// </summary>
        private void acquireControllers()
        {
        }

        /// <summary>
        /// Release controllers which do not require persistence.
        /// </summary>
        private void releaseControllers()
        {
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void createCommands()
        {
            browseEquipageFilePath = new CommandHandler(BrowseFilePath, true);
            loadDb = new CommandHandler(LoadFilePath, true);
            saveDb = new CommandHandler(SaveFilePath, true);
            addSystemCommand = new CommandHandler(AddSystem, CanAddSystem());
            editSystemCommand = new CommandHandler(EditSystem, true);
            deleteSystemCommand = new CommandHandler(DeleteSystem, true);
            addComponentCommand = new CommandHandler(AddComponent, CanAddComponent());
            editComponentCommand = new CommandHandler(EditComponent, true);
            deleteComponentCommand = new CommandHandler(DeleteComponent, true);
        }

        private void disposeCommands()
        {
        }

        private void subscribeToEvents()
        {
        }

        private void unsubscribeFromEvents()
        {
        }

        /// <summary>
        /// Start error handling and logging.
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
        private void initializePrivateFields()
        {
            this.equipageModel = EquipageModel.Instance;
            this.equipageFilePath = string.Empty;
            this.systems = new ObservableCollection<AFSLSystem>(this.equipageModel.AcquireEquipage().Fleet);
            this.selectedSystem = null;
            this.isInEditMode = false;
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
