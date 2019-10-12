using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UW.WPF.Templates;

namespace UW.WPF.Templates
{
    /// <summary>
    /// Interaction logic for TemplateUserControl.xaml
    /// </summary>
    public partial class TemplateUserControl : UserControl
    {
        #region Fields

        /// <summary>
        /// View-model for the control
        /// </summary>
        private TemplateUserControlViewModel viewModel = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TemplateUserControl()
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
        /// Setup the DataContext for the UserControl.
        /// </summary>
        private void acquireDataContext()
        {
            this.releaseDataContext();
            this.viewModel = new TemplateUserControlViewModel();
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

        #region Private Methods (Event Handlers)

        ////EXAMPLE: Make sure to protect event handlers with DesignerProperties.GetIsInDesignMode(this)
        //private void radioButtonAirspaceDisplayOn_Checked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    if (DesignerProperties.GetIsInDesignMode(this))
        //        return;

        //    this.viewModel.TurnOnAirspaceDisplays();
        //}

        #endregion
    }
}