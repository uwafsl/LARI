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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Wing_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
