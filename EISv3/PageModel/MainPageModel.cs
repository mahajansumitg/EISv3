using EISv3.Model;
using EISv3.Utils;
using EISv3.Views;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EISv3.PageModel
{
    public class MainPageModel : NotifyOnPropertyChanged
    {
        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Properties

        private string _EmpIDMainPage;
        public string EmpIDMainPage
        {
            get { return _EmpIDMainPage; }
            set { _EmpIDMainPage = value; OnPropertyChanged("EmpIDMainPage"); }
        }
        
        private string _RoleMainPage;
        public string RoleMainPage
        {
            get { return _RoleMainPage; }
            set { _RoleMainPage = value; OnPropertyChanged("RoleMainPage"); }
        }

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

        Dictionary<string, ListViewItem> ListViewDictionary = new Dictionary<string, ListViewItem>(); 
        public MainPageModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("MainPage started: In constructor MainPageModel");

            Contents.Add(new HomeView());
            _OpenMenuVisibily = Visibility.Visible;
            _CloseMenuVisibily = Visibility.Collapsed;

            try
            {
                Login login = Mediator.GetVar("Login") as Login;
                EmpIDMainPage = login.UserName + " : " + login.EmpId;
                RoleMainPage = login.Role;
                if (!login.role.Equals("Admin"))
                {
                    DashBoardVisibility = Visibility.Collapsed;
                    HomeVisibility = Visibility.Collapsed;
                }

                #region Registering Mediator Actions
                Mediator.RegisterAction("SwitchToHomeView", () => SelectedItem = ListViewDictionary["HomeView"]);
                Mediator.RegisterAction("SwitchToProfileView", () => SelectedItem = ListViewDictionary["ProfileView"]);
                Mediator.RegisterAction("SwitchToDashBoardView", () => SelectedItem = ListViewDictionary["DashBoardView"]);

                Mediator.RegisterAction("DisableButtons", () =>
                {
                    DashBoardVisibility = Visibility.Collapsed;
                    HomeVisibility = Visibility.Collapsed;
                });
                Mediator.RegisterAction("EnableButtons", () =>
                {
                    DashBoardVisibility = Visibility.Visible;
                    HomeVisibility = Visibility.Visible;
                });
                #endregion
            }catch (Exception)
            {
                MessageBox.Show("Error occured while navigating");
            }
        }

        //Open Menu Panal
        public ICommand OpenMenu => new Command(_OpenMenu);
        private void _OpenMenu(object parameter)
        {
            log.Info("Menu Opened: _OpenMenu()");
            OpenMenuVisibily = Visibility.Collapsed;
            CloseMenuVisibily = Visibility.Visible;
        }

        //Close Menu Panal
        public ICommand CloseMenu => new Command(_CloseMenu);
        private void _CloseMenu(object parameter)
        {
            log.Info("Menu Closed: __CloseMenu");

            OpenMenuVisibily = Visibility.Visible;
            CloseMenuVisibily = Visibility.Collapsed;
        }

        public ListViewItem _SelectedItem;
        public ListViewItem SelectedItem
        {
            get => _SelectedItem;
            set
            {
                setView(value);
                OnPropertyChanged(ref _SelectedItem, value);
            }
        }

        private void setView(ListViewItem item)
        {
            Contents.Clear();
            switch (item.Name)
            {
                case "HomeView":
                    Contents.Add(new HomeView());
                    log.Info("Selected HomeView from menu: In case ItemHome");
                    break;
                case "ProfileView":
                    Contents.Add(new ProfileView());
                    log.Info("Selected ProfileView from menu: In case ItemForm");
                    break;
                case "DashBoardView":
                    Contents.Add(new DashBoardView());
                    log.Info("Selected DashBoardView from menu: In case ItemDashBoard");
                    break;
                default:
                    break;
            }
        }

        //Switching of childs in ItemControl as per selection of Item in Manu
        public ICommand SelectionChanged => new Command(_SelectionChanged);
        private void _SelectionChanged(object parameter)
        {
            if (ListViewDictionary.Count == 3) return;

            ItemCollection collection = (parameter as ListView).Items;
            foreach(Object o in collection)
            {
                ListViewItem item = o as ListViewItem;
                ListViewDictionary.Add(item.Name, item);
            }
        }


        //Exit Application
        public ICommand Exit => new Command(_Exit);
        private void _Exit(object parameter)
        {
            log.Info("-----Application Exit-----");
            Application.Current.Shutdown();
        }

        //Logout & clear all actions except SwitchToLoginPage
        public ICommand Logout => new Command(_Logout);
        private void _Logout(object parameter)
        {
            Mediator.RemoveVar("Login");
            Mediator.RemoveVar("EmpInfo");

            Mediator.RemoveAction("SwitchToHomeView");
            Mediator.RemoveAction("SwitchToDashBoardView");
            Mediator.RemoveAction("SwitchToProfileView");

            Mediator.RemoveAction("DisableButtons");
            Mediator.RemoveAction("EnableButtons");

            Mediator.PerformAction("GoToLoginPage");

            log.Info("*****Logged Out*****");
        }

        //Help Page
        public ICommand Help => new Command(_Help);
        private void _Help(object parameter)
        {
            log.Info("Selected Help: _Help");
            try
            {
                Contents.Add(new HelpView());
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
