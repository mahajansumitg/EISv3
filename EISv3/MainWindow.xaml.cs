using EISv3.Pages;
using EISv3.Utils;
using System.Windows;
using log4net;

namespace EISv3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();

            InitializeComponent();

            //Logs Code
            log.Info("MainWindow Started");

            this.Content = new LoginPage();
            Mediator.RegisterVar("Window", this);
            Mediator.RegisterAction("GoToLoginPage", () => { this.Content = new LoginPage(); });
            Mediator.RegisterAction("GoToMainPage", () => { this.Content = new MainPage(); });
        }

    }
}
