using EISv3.Model;
using EISv3.Utils;
using log4net;
using System;
using System.Collections.Generic;
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

        public SignUpPageModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Started SignUpPage: In constructor SignUpPageModel()");

            Login = new Login();
            Login.LoginList = Connection.getData<Login>("Select * from Login");
        }

        //SignUp Command
        public ICommand SignUp => new Command(_SignUp, CanSignUp);

        private bool CanSignUp(object arg)
        {
            return !string.IsNullOrWhiteSpace(Login.UserName)  && !string.IsNullOrWhiteSpace(Login.PSWD) && !string.IsNullOrWhiteSpace(Login.Role);
        }

        private void _SignUp(object parameter)
        {


            Login.EmpId = RndEmpId(Login.LoginList);
            Login.UserName = Login.UserName.Trim();

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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private string RndEmpId(List<Login> loginList)
        {
            string rndEmpId;
            while ((rndEmpId = GetAndCheck(loginList)) == null) { }
            return rndEmpId;
        }

        private string GetAndCheck(List<Login> loginList)
        {
            Random random = new Random();
            string rndEmpId = "A" + random.Next(10000, 99999);

            foreach (Login login in loginList)
            {
                if (login.emp_id.Equals(rndEmpId)) return null;
            }
            return rndEmpId;
        }

        //Cancel Command
        public ICommand Cancel => new Command(_Cancel);

        private void _Cancel(object parameter)
        {
            Login = null;
            Mediator.PerformAction("GoToLoginPage");
        }

        //Password Command
        public ICommand PasswordChanged => new Command(_passwordChanged);
        private void _passwordChanged(object parameter)
        {         
            Login.PSWD = (parameter as PasswordBox).Password;

            string result = null;
            if(!string.IsNullOrEmpty(Login.PSWD))
                        {
                if (Login.PSWD.Length < 8) result = "Password should have length of atleast 8";
            }
            Login.updateErrorCollection("PSWD", result);
        }

    }       
}