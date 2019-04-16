using System.ComponentModel;
using System.Windows.Controls;
using LARI.ViewModels;

namespace LARI.Views
{
    /// <summary>
    /// Interaction logic for AddSystemWindow.xaml
    /// </summary>
    public partial class AddSystemWindow
    {
        //Version History:
        //07/25/18: Created

        #region Constructor

        /// <summary>
        /// Constructor that accepts a view model to assign Data Context to
        /// </summary>
        public AddSystemWindow(ComponentTrackerViewModel vM)
        {
            InitializeComponent();
            this.DataContext = new AddSystemViewModel(vM);
        }

        #endregion
    }
}
