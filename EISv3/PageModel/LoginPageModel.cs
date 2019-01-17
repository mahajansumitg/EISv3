using EISv3.Model;
using EISv3.Utils;
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
    public class LoginPageModel : Notify
    {
        private String _UserName;
        public String UserName
        {
            get => _UserName;
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }

        private String _PSWD;

        public LoginPageModel()
        {
            Logger.logging("-----On LoginPage------");
        }

        public String PSWD
        {
            get => _PSWD;
            set { _PSWD = value; OnPropertyChanged("PSWD"); }
        }

        public ICommand Login => new Command(_Login, HandleVisibility);
        private Boolean HandleVisibility(object parameter)
        {
            return _UserName != null && (parameter as PasswordBox).Password != null;
        }

        private void _Login(object parameter)
        {
            Logger.logging("-----Clicked on Login in LoginPage------");
            String loginQuery = "select * from Login where user_name = '" + _UserName + "'";
            List<Login> loginList = Loading.Show(() => Connection.getData<Login>(loginQuery)) as List<Login>;

            if (loginList.Count() > 0 && loginList.First().pswd == (parameter as PasswordBox).Password)
            {
                Mediator.registerVar("Login", loginList.First());
                Mediator.performAction("GoToMainPage");
                Logger.logging("------Login Successfull------");
            }
            else
                MessageBox.Show("Entered user_name or password is incorrect");

        }
    }
}
