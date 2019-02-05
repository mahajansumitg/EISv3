using EISv3.Model;
using EISv3.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EISv3.PageModel
{
    public class LoginPageModel : NotifyOnPropertyChanged
    {
        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private String _UserName;
        public String UserName
        {
            get => _UserName;
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }

        private String _PSWD;
        public String PSWD
        {
            get => _PSWD;
            set { _PSWD = value; OnPropertyChanged("PSWD"); }
        }

        public LoginPageModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("On LoginPage");
        }

        public ICommand Login => new Command(_Login, HandleVisibility);

        //Enable or disable the Login Button
        private Boolean HandleVisibility(object parameter)
        {
            return _UserName != null && (parameter as PasswordBox).Password != null;
        }

        //Perform Login
        private void _Login(object parameter)
        {
            log.Info("In LoginPage : _Login()");

            String loginQuery = "select * from Login where user_name = '" + _UserName + "'";
            List<Login> loginList = Loading.Show(() => Connection.getData<Login>(loginQuery)) as List<Login>;

            if (loginList.Count() > 0 && loginList.First().pswd == (parameter as PasswordBox).Password)
            {
                Mediator.registerVar("Login", loginList.First());
                Mediator.performAction("GoToMainPage");
                log.Info("Login Successfull Mediator calling for GoToMainPage");
            }
            else
                MessageBox.Show("Entered user_name or password is incorrect");

        }
    }
}
