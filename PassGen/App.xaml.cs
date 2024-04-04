using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace PassGen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            File.Delete("C:\\Users\\cutec\\OneDrive\\Рабочий стол\\Gen.json");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Exit += Application_Exit;
        }
    }

}   
