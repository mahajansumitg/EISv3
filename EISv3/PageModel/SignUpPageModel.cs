using EISv3.Model;
using EISv3.Utils;
using EISv3.Pages;
using log4net;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

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

        public List<string> Roles { get; set; } = new List<string> { "Admin", "Employee", "Contractor" };

        public SignUpPageModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Started SignUpPage: In constructor SignUpPageModel()");

            Login = new Login
            {
                Role = "Employee",
                HighSecPwdVisibility = Visibility.Collapsed
            };
            Login.LoginList = Connection.getData<Login>("Select * from Login");
        }

        //SignUp Command
        public ICommand SignUp => new Command(_SignUp, CanSignUp);

        private bool CanSignUp(object arg)
        {
            return !string.IsNullOrWhiteSpace(Login.UserName)
                && !string.IsNullOrWhiteSpace(Login.PSWD)
                && ((Login.Role.Equals("Admin") && !string.IsNullOrWhiteSpace(Login.HighSecPwd)) || (!Login.Role.Equals("Admin")));
        }

        private void _SignUp(object parameter)
        {
            Login.EmpId = RndEmpId(Login.LoginList);
            Login.UserName = Login.UserName.Trim();

            if (Login.Role.Equals("Admin") && !Login.HighSecPwd.Equals("Sumit123@"))
            {
                MessageBox.Show("Security key is incorrect");
            }
            else if (Connection.setData(Login))
            {
                MessageBox.Show("User " + Login.UserName + " with Employee Id " + Login.EmpId + " created");

                Mediator.RegisterVar("Login", Login);
                Mediator.PerformAction("GoToMainPage");
            }
            else MessageBox.Show("Unable to create account");

        }
        private string RndEmpId(List<Login> loginList)
        {
            string rndEmpId;
            while ((rndEmpId = GetAndCheck(loginList)) == null) { }
            return rndEmpId;
        }

        private string GetAndCheck(List<Login> loginList)
        {
            //Random random = new Random();
            //string rndEmpId = "A" + random.Next(10000, 99999);

            //foreach (Login login in loginList)
            //{
            //    if (login.emp_id.Equals(rndEmpId)) return null;
            //}
            return "A" + (int.Parse(loginList.OrderBy(i => i.EmpId).Last().EmpId.Substring(1)) + 1).ToString("00000");
        }

        //Cancel Command
        public ICommand Cancel => new Command(_Cancel);

        private void _Cancel(object parameter)
        {
            Mediator.PerformAction("GoToLoginPage");
        }
    }
}