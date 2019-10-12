using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace UW.WPF
{
    /// <summary>
    /// Interaction logic for IPAddressControl.xaml
    /// </summary>
    public partial class IPAddressControl : UserControl
    {
        #region Fields

        /// <summary>
        /// View-model for the control
        /// </summary>
        private IPAddressControlViewModel viewModel = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public IPAddressControl()
        {
            InitializeComponent();

            //Some aspects of the DataContext may require that the application is actually running.  This will cause problems when trying to
            //view the UserControl in the WPF Designer.  Therefore, only acquire the data context (ie create the view-model), if 
            //this is not in designer mode.
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.acquireDataContext();
                this.initializeControls();
            }
        }

        #endregion

        #region Private Methods (acquire/release DataContext)

        /// <summary>
        /// Setup the DataContext for the User Control.
        /// </summary>
        private void acquireDataContext()
        {
            this.releaseDataContext();
            this.viewModel = new IPAddressControlViewModel();
            this.DataContext = this.viewModel;
        }

        private void releaseDataContext()
        {
            if (this.viewModel != null)
            {
                this.viewModel.Dispose();
                this.viewModel = null;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle the selected vehicle changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectedVehicleChanged(object sender, EventArgs e)
        {
            this.acquireDataContext();
        }

        #endregion

        #region Private Methods (initialize controls)

        /// <summary>
        /// Initialize settings for other controls contained on this UserControl
        /// </summary>
        private void initializeControls()
        {
            //EXAMPLE: Set limits on Slider controls
        }

        #endregion

        #region Private Methods (Radio Button Event Handlers)

        ////EXAMPLE: Make sure to protect event handlers with DesignerProperties.GetIsInDesignMode(this)
        //private void radioButtonPatternDirectionCounterClockwise_Checked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    if (DesignerProperties.GetIsInDesignMode(this))
        //        return;

        //    this.viewModel.SetPatternDirectionToCounterClockwise();
        //}

        #endregion
    }
}
