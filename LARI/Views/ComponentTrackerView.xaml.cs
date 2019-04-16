using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Xml;
using LARI.Models;
using LARI.ViewModels;
using UW.LARI.Datatypes;

namespace LARI.Views
{
    /// <summary>
    /// Interaction logic for ComponentTrackerView.xaml
    /// </summary>
    public partial class ComponentTrackerView : UserControl
    {
        #region Fields

        // TODO: we might want to rename "Systems" to something less simlar to "System", the library, for readability/clarity
        public Equipage Systems;
        
        private ComponentTrackerViewModel viewModel = null;


        #endregion

        #region Constructor

        public ComponentTrackerView()
        {
            InitializeComponent();
            // TODO: use main Equipage from the ManagerModel.
            //Systems = ManagerModel.Instance.AcquireEquipage();

            //Some aspects of the DataContext may require that the application is actually running.  This will cause problems when trying to
            //view the UserControl in the WPF Designer.  Therefore, only acquire the data context (ie create the view-model), if 
            //this is not in designer mode.
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.acquireDataContext();
                this.initializeControls();
            }
            displaySystems();
        }

        #endregion

        #region Fields


        #endregion

        #region Public Methods

        public String displaySystems()
        {
            return string.Format("{0}", nameof(AFSLSystem.Name) + " : " + nameof(AFSLSystem.WingType));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Setup the DataContext for the UserControl.
        /// </summary>
        private void acquireDataContext()
        {
            this.releaseDataContext();
            this.viewModel = new ComponentTrackerViewModel();
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

        /// <summary>
        /// Initialize settings for other controls contained on this UserControl
        /// </summary>
        private void initializeControls()
        {
            //EXAMPLE: Set limits on Slider controls
        }

        private ItemsControl GetParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as ItemsControl;
        }
        /* Moved to viewmodel
        private void LoadDbFromXml(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.viewModel.EquipageFilePath))
            {
                MessageBox.Show("Please select a database file to load",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                //AFSLRefactor: use viewmodel
                Systems.ReadFromXMLFile(this.viewModel.EquipageFilePath);
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

        // TODO: This should go in the View Model
        private void SaveDbToXml(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.viewModel.EquipageFilePath))
            {
                MessageBox.Show("Please select a database file",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                //AFSLRefactor: use viewmodel
                Systems.WriteToXMLFile(this.viewModel.EquipageFilePath);
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

        // TODO: this should go in the View Model
        private void SelectEquipageFilePath(object sender, RoutedEventArgs e)
        {
            // TODO: make this file path selection dialog a public function and use it in ComponentTrackerView.xaml.cs

            // Create OpenFileDialog.
            Microsoft.Win32.OpenFileDialog dialogue = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension.
            dialogue.DefaultExt = ".xml";

            // Display OpenFileDialog.
            bool? result = dialogue.ShowDialog();

            if (result == true)
            {
                // We update the UserSettingsModel indirectly in order to trigger a property changed event/binding in the view.
                this.viewModel.EquipageFilePath = dialogue.FileName;
            }
        }
        */


        // Moved to viewmodel
        /*
        private void DeleteSelectedElement(object sender, RoutedEventArgs e)
        {
            // TODO: refactor this code for a tabled components view
            
            object selectedObj = SystemsTree.SelectedItem;
            if (selectedObj == null) {
                // AFSLRefactor: make button unclickable if no item is selected
                MessageBox.Show("No item selected for deletion",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            TreeViewItem selected = selectedObj as TreeViewItem;

            // determine if AFSLSystem or Component is selected
            ItemsControl parent = GetParent(selected);
            
            if (parent is TreeView) // AFSLSystem case
            {
                // are you sure?
                MessageBoxResult result = MessageBox.Show("Delete selected system?",
                                            "Component Tracker",
                                            MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                
                Systems.RemoveSystem(selected.Header.ToString());
            } else if (parent is TreeViewItem) // Component case
            {
                // are you sure
                MessageBoxResult result = MessageBox.Show("Delete selected component?",
                                            "Component Tracker",
                                            MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

                int partNumber = 0;
                if (!int.TryParse(selected.Header.ToString(), out partNumber))
                {
                    MessageBox.Show("Failed to parse part number from selected component",
                                    "Component Tracker",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }
                Systems.RemoveComponent(partNumber);
            } else
            {
                // selected is neither
                MessageBox.Show("Selected item is not a system or component",
                                "Component Tracker",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }
            */


        #endregion

        private void ComponentsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComponentsTable_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
