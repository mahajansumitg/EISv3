using EISv3.Model;
using EISv3.Pages;
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

        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }

        private string _PSWD;
        public string PSWD
        {
            get => _PSWD;
            set { _PSWD = value; OnPropertyChanged("PSWD"); }
        }

        public LoginPageModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("On LoginPage");
        }

        //Login Command 
        public ICommand Login => new Command(_Login, HandleVisibility);
        //Perform Login
        private void _Login(object parameter)
        {
            log.Info("In LoginPage : _Login()");

            string loginQuery = "select * from Login where user_name = '" + UserName + "'";
            try
            {
                List<Login> loginList = Loading.Show(() => Connection.getData<Login>(loginQuery)) as List<Login>;

                if (loginList.Count() <= 0)
                {
                    MessageBox.Show("Entered user_name is incorrect");
                }
                else if(loginList.First().pswd == (parameter as PasswordBox).Password)
                {
                    Mediator.RegisterVar("Login", loginList.First());
                    Mediator.PerformAction("GoToMainPage");
                    log.Info("Login Successfull Mediator calling for GoToMainPage");
                }
                else
                    MessageBox.Show("Entered password is incorrect");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Enable or disable the Login Button
        private bool HandleVisibility(object parameter)
        {
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrEmpty((parameter as PasswordBox).Password);
        }

        //SignUp Command
        public ICommand SignUp => new Command(_SignUp);
        private void _SignUp(object parameter)
        {
            Mediator.PerformAction("GoToSignUpPage");
        }
    }
}