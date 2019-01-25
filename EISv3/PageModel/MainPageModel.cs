using EISv3.Model;
using EISv3.Utils;
using EISv3.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EISv3.PageModel
{
    public class MainPageModel : NotifyOnPropertyChanged
    {

        #region Properties

        private ObservableCollection<UserControl> _Contents = new ObservableCollection<UserControl>();
        public ObservableCollection<UserControl> Contents
        {
            get => _Contents;
            set { _Contents = value; OnPropertyChanged("Contents"); }
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

        #endregion

        public MainPageModel()
        {
            Logger.logging("-----On MainPage------");
            Contents.Add(new HomeView());
            _OpenMenuVisibily = Visibility.Visible;
            _CloseMenuVisibily = Visibility.Collapsed;

            Login login = Mediator.getVar("Login") as Login;
            if (!login.role.Equals("admin")) DashBoardVisibility = Visibility.Hidden;

            #region Registering Mediator Actions
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
            #endregion
        }

        //Open Menu Panal
        public ICommand OpenMenu => new Command(_OpenMenu);
        private void _OpenMenu(object parameter)
        {
            Logger.logging("-----Menu Opened------");
            OpenMenuVisibily = Visibility.Collapsed;
            CloseMenuVisibily = Visibility.Visible;
        }

        //Close Menu Panal
        public ICommand CloseMenu => new Command(_CloseMenu);
        private void _CloseMenu(object parameter)
        {
            Logger.logging("-----Menu Closed------");

            OpenMenuVisibily = Visibility.Visible;
            CloseMenuVisibily = Visibility.Collapsed;
        }

        //Switching of childs in ItemControl as per selection of Item in Manu
        public ICommand SelectionChanged => new Command(_SelectionChanged);
        private void _SelectionChanged(object parameter)
        {
            Contents.Clear();
            switch ((parameter as ListViewItem).Name)
            {
                case "ItemHome":
                    Contents.Add(new HomeView());
                    Logger.logging("-----Selected HomeView from menu------");
                    break;
                case "ItemDashBoard":
                    Contents.Add(new DashBoardView());
                    Logger.logging("-----Selected DashBoardView from menu------");
                    break;
                case "ItemForm":
                    Contents.Add(new ProfileView());
                    Logger.logging("-----Selected ProfileView from menu------");
                    break;
                default:
                    break;
            }
        }

        //Exit Application
        public ICommand Exit => new Command(_Exit);
        private void _Exit(object parameter)
        {
            Logger.logging("-----Application Exit------");
            System.Windows.Application.Current.Shutdown();
        }

        //Logout & clear all actions except SwitchToLoginPage
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

            Logger.logging("-----Logged Out------");
        }

        //Help Page
        public ICommand Help => new Command(_Help);
        private void _Help(object parameter)
        {
            Logger.logging("-----Selected Help------");
            Contents.Add(new HelpView());
        }
    }
}
