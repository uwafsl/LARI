﻿using System;
using UW.LARI.Datatypes;
using UW.WPF;
using LARI.Models;
using LARI.Utilities;
using System.Windows.Input;
using System.Windows;

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
        private ManagerModel manager;

        /// <summary>
        /// System to be added/edited.
        /// </summary>
        private AFSLSystem system;

        /// <summary>
        /// Command to add new system or apply edits to currently selected system.
        /// </summary>
        private CommandHandler applySystemWindow;

        /// <summary>
        /// Command to cancel adding/editing system and exit window.
        /// </summary>
        private CommandHandler cancelSystemWindow;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddSystemViewModel()
        {
            this.startErrorHandling();
            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
        }

        #endregion

        #region Properties
        /// <summary>
        /// System name.
        /// </summary>
        public string Name
        {
            get { return this.system.Name; }
            set
            {
                this.system.Name = value;
                OnPropertyChanged("Name");
                ApplySystemWindow.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// System description.
        /// </summary>
        public string Description
        {
            get { return this.system.Description; }
            set
            {
                this.system.Description = value;
                OnPropertyChanged("Description");
                ApplySystemWindow.RaiseCanExecuteChanged();
            }
        }

        // TODO: This does not currently work. Combobox does not currently populate with wing types.
        /// <summary>
        /// System wing type.
        /// </summary>
        public WingTypes SelectedWingType
        {
            get { return this.system.WingType; }
            set
            {
                this.system.WingType = value;
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

        /// <summary>
        /// Gets cancel system command.
        /// </summary>
        public CommandHandler CancelSystemWindow
        {
            get { return this.cancelSystemWindow; }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Adds system to equipage. 
        /// </summary>
        public void ApplySystem()
        {
            this.manager.AcquireEquipage().AddSystem(this.system);
            this.clearTextFields();
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

        /// <summary>
        /// TODO: Closes window when cancel command is executed
        /// </summary>
        public void CancelSystem()
        {

        }

        /// <summary>
        /// Condition for whether "cancel" button is enabled. "Cancel" should always be enabled.
        /// </summary>
        /// <returns>Always returns true to allow user to cancel adding system, thus closing the window.</returns>
        public bool CanCancelSystem()
        {
            return true;
        }

        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)
        /// <summary>
        /// Clears all text boxes.
        /// </summary>
        private void clearTextFields()
        {
            Name = String.Empty;
            Description = String.Empty;
        }

        /// <summary>
        /// Obtain necessary controllers from appropriate sources (for example: global data providers or a factory)
        /// </summary>
        private void acquireControllers()
        {
            this.manager = ManagerModel.Instance;
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
            this.cancelSystemWindow = new CommandHandler(CancelSystem, CanCancelSystem());
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
            this.system = new AFSLSystem();
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