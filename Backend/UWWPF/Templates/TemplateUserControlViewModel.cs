using System;

namespace UW.WPF.Templates
{
    /// <summary>
    /// A skeleton template for a UserControl view model.
    /// </summary>
    public class TemplateUserControlViewModel : ViewModelBaseUW, IDisposable
    {
        #region Constants

        //EXAMPLE
        //private const int mapUpdateTimerMilliSecsBetweenTicks = 1000;

        #endregion

        #region Fields
        
        //EXAMPLE
        //private AirspaceController airspaceController = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TemplateUserControlViewModel()
        {
            this.startErrorHandling();
            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
        }

        #endregion

        #region Properties (Exposed so that clients can wire specific models or controllers to this ViewModel)
        
        #endregion

        #region Properties
        
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)

        /// <summary>
        /// Obtain necessary controllers from appropriate sources (for example: global data providers or a factory)
        /// </summary>
        private void acquireControllers()
        {
            //EXAMPLE
            //this.airspaceController = UserInterface.Controllers.AirspaceControllerFactory.AcquireController();
        }

        /// <summary>
        /// Release controllers which do not require persistence
        /// </summary>
        private void releaseControllers()
        {
            //EXAMPLE            
            //if (this.airspaceController != null)
            //{
            //    UserInterface.Controllers.AirspaceControllerFactory.ReleaseController(this.airspaceController);
            //    this.airspaceController= null;
            //}
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
