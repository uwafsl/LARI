using System.Windows;
using LARI.Models;
using LARI.Views;

namespace LARI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ManagerModel.Instance.InitializeToDefaultState();
 
            //Equipage equipage = ManagerModel.Instance.AcquireEquipage();

            //// CONDOR
            //Component pixhawk01 = new Component("Pixhawk", 101);
            //Component servo01 = new Component("Servo", 102);
            //Component servo02 = new Component("Servo", 103);
            //Component camera01 = new Component("Mobius Camera", 104);

            //AFSLSystem CONDOR = new AFSLSystem();
            //CONDOR.Description = "CONDOR";
            //CONDOR.AddComponent(pixhawk01);
            //CONDOR.AddComponent(servo01);
            //CONDOR.AddComponent(servo02);
            //CONDOR.AddComponent(camera01);

            //// Peach
            //Component pixhawk02 = new Component("Pixhawk", 201);
            //Component servo03 = new Component("Servo", 202);
            //Component servo04 = new Component("Servo", 203);
            //Component servo05 = new Component("Servo", 204);
            //Component camera02 = new Component("Mobius Camera", 205);
            //Component TRAPISPayload = new Component("TRAPISPayload", 206);

            //AFSLSystem Peach = new AFSLSystem();
            //Peach.AddComponent(pixhawk02);
            //Peach.AddComponent(servo03);
            //Peach.AddComponent(servo04);
            //Peach.AddComponent(servo05);
            //Peach.AddComponent(camera02);
            //Peach.AddComponent(TRAPISPayload);

            //equipage.AddSystem(CONDOR);
            //equipage.AddSystem(Peach);

            // AFSLRefactor: Test writing out the equipage to an XML file
            
            // Create and show the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
