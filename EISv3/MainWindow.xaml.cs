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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            InitializeComponent();

            //Logs Code
            log4net.Config.XmlConfigurator.Configure();
            log.Info("-----Started MainWindow------");
            /*
              *  log.info
              *  log.error
              *  log.fatal
              *  log.debug
              */

            this.Content = new LoginPage();
            Mediator.registerVar("Window", this);
            Mediator.registerAction("GoToLoginPage", () => { this.Content = new LoginPage(); });
            Mediator.registerAction("GoToMainPage", () => { this.Content = new MainPage(); });
        }

    }
}
