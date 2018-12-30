using EISv3.Model;
using EISv3.Utils;
using EISv3.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace EISv3.PageModel
{
    public class MainPageModel : Notify
    {
        private ObservableCollection<UserControl> _Contents = new ObservableCollection<UserControl>();
        public ObservableCollection<UserControl> Contents
        {
            get => _Contents;
            set { _Contents = value; OnPropertyChanged("Contents"); }
        }

        public MainPageModel()
        {
            Contents.Add(new HomeView());
            _OpenMenuVisibily = Visibility.Visible;
            _CloseMenuVisibily = Visibility.Collapsed;

            Login login = Mediator.getVar("Login") as Login;
            if (!login.role.Equals("admin")) DashBoardVisibility = Visibility.Hidden;

            Mediator.registerAction("SwitchToDashBoardView", () => {
                this.Contents.Clear();
                this.Contents.Add(new DashBoardView());
            });
            Mediator.registerAction("SwitchToProfileView", () => {
                this.Contents.Clear();
                this.Contents.Add(new ProfileView());
            });
            Mediator.registerAction("DisableButtons", () => {
                DashBoardVisibility = Visibility.Hidden;
                HomeVisibility = Visibility.Hidden;
            });
            Mediator.registerAction("EnableButtons", () => {
                DashBoardVisibility = Visibility.Visible;
                HomeVisibility = Visibility.Visible;
            });
        }

        private Visibility _OpenMenuVisibily;
        public Visibility OpenMenuVisibily
        {
            get => _OpenMenuVisibily;
            set { _OpenMenuVisibily = value; OnPropertyChanged("OpenMenuVisibily"); }
        }

        private Visibility _CloseMenuVisibily;
        public Visibility CloseMenuVisibily
        {
            get => _CloseMenuVisibily;
            set { _CloseMenuVisibily = value; OnPropertyChanged("CloseMenuVisibily"); }
        }

        private Visibility _DashBoardVisibility;
        public Visibility DashBoardVisibility
        {
            get => _DashBoardVisibility;
            set { _DashBoardVisibility = value; OnPropertyChanged("DashBoardVisibility"); }
        }

        private Visibility _HomeVisibility;
        public Visibility HomeVisibility
        {
            get => _HomeVisibility;
            set { _HomeVisibility = value; OnPropertyChanged("HomeVisibility"); }
        }

        public ICommand OpenMenu => new Command(_OpenMenu);
        private void _OpenMenu(object parameter)
        {
            OpenMenuVisibily = Visibility.Collapsed;
            CloseMenuVisibily = Visibility.Visible;

        }

        public ICommand CloseMenu => new Command(_CloseMenu);
        private void _CloseMenu(object parameter)
        {
            OpenMenuVisibily = Visibility.Visible;
            CloseMenuVisibily = Visibility.Collapsed;
        }

        public ICommand SelectionChanged => new Command(_SelectionChanged);
        private void _SelectionChanged(object parameter)
        {
            Contents.Clear();
            switch ((parameter as ListViewItem).Name)
            {
                case "ItemHome":
                    Contents.Add(new HomeView());
                    break;
                case "ItemDashBoard":
                    Contents.Add(new DashBoardView());
                    break;
                case "ItemForm":
                    Contents.Add(new ProfileView());
                    break;
                default:
                    break;
            }
        }

        public ICommand Exit => new Command(_Exit);
        private void _Exit(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ICommand Logout => new Command(_Logout);
        private void _Logout(object parameter)
        {
            Mediator.removeVar("Login");
            Mediator.removeVar("EmpInfo");
            Mediator.removeAction("SwitchToDashBoardView");
            Mediator.removeAction("SwitchToProfileView");
            Mediator.removeAction("DisableButtons");
            Mediator.removeAction("EnableButtons");

            Mediator.performAction("GoToLoginPage");
        }

        public ICommand Help => new Command(_Help);
        private void _Help(object parameter)
        {
            Contents.Add(new HelpView());
        }
    }
}
