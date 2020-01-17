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
        private ICommand applyComponentWindow;
        private ICommand cancelComponentWindow;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddComponentViewModel()
        {
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
        public ICommand ApplyComponentWindow
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
            Console.WriteLine("test");
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
