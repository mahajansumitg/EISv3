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
    public class SignUpPageModel : NotifyOnPropertyChanged
    {
        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Login _Login;
        public Login Login
        {
            get { return _Login; }
            set { _Login = value; OnPropertyChanged("Login"); }
        }

        private string password;
        public SignUpPageModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Started SignUpPage: In constructor SignUpPageModel()");
            Login = new Login();
        }

        //Password Command
        public ICommand PasswordChanged => new Command(_passwordCommand);
        private void _passwordCommand(object parameter)
        {
            password = (parameter as PasswordBox).Password;
            if (password == null) MessageBox.Show("Password should not empty");
            else
            {
                Login.PSWD = password;
            }
        }

        //SignUp Command
        public ICommand SignUp => new Command(_SignUp);
        private void _SignUp(object parameter)
        {
            try
            {
                if (Connection.setData(Login))
                {
                    MessageBox.Show("User " + Login.UserName + " with Employee Id " + Login.EmpId + " created");

                    Mediator.RegisterVar("Login", Login);
                    Mediator.PerformAction("GoToMainPage");
                }
                else MessageBox.Show("Error in Insert.");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Cancel Command
        public ICommand Cancel => new Command(_Cancel);
        private void _Cancel(object parameter)
        {
            Login = null;
            Mediator.PerformAction("GoToLoginPage");
        }
    }
}