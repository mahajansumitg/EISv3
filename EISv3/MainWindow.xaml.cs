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
            log.Info("-----Started MainWindow------");

            this.Content = new LoginPage();
            Mediator.registerVar("Window", this);
            Mediator.registerAction("GoToLoginPage", () => { this.Content = new LoginPage(); });
            Mediator.registerAction("GoToMainPage", () => { this.Content = new MainPage(); });
        }

    }
}
