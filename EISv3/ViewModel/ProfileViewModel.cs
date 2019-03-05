using EISv3.Model;
using EISv3.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EISv3.ViewModel
{
    public class ProfileViewModel : NotifyOnPropertyChanged
    {
        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Properties

        private EmpInfo _EmpInfo;
        public EmpInfo EmpInfo
        {
            get => _EmpInfo;
            set { _EmpInfo = value; OnPropertyChanged("EmpInfo"); }
        }

        private Visibility _VendorVisibily;
        public Visibility VendorGrid
        {
            get => _VendorVisibily;
            set { _VendorVisibily = value; OnPropertyChanged("VendorGrid"); }
        }

        public List<string> Departments { get; set; } = new List<string> { "Management", "Development", "Analytics", "Testing", "Other" };
        #endregion

        bool isUserPresent;
        bool isAdmin;

        public ProfileViewModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Started ProfileView: In constructor ProfileViewModel()");

            try
            {
                Mediator.PerformAction("DisableButtons");

                EmpInfo = Mediator.GetVar("EmpInfo") as EmpInfo;   //Getting the Employee which is to be updated from listview
                if (EmpInfo != null) isUserPresent = true;
                else EmpInfo = new EmpInfo();

                if (isUserPresent)                //For ListView Selected Employees
                {
                    isAdmin = true;

                    //checking for contractor or not
                    string findRoleQuery = "select * from Login where emp_id = '" + EmpInfo.emp_id + "'";
                    List<Login> TempList = Loading.Show(() => Connection.getData<Login>(findRoleQuery)) as List<Login>;
                    if (TempList.Count > 0) VendorGrid = TempList.First().Role.Equals("contractor") ? Visibility.Visible : Visibility.Collapsed;

                    log.Info("Finding data for Selected Employee " + EmpInfo.emp_id);
                    string findQuery = "select * from EmpInfo where emp_id = '" + EmpInfo.emp_id + "'";
                    List<EmpInfo> EmpInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;
                    EmpInfo = EmpInfoList.First();
                }
                else     //If not coming from listview selection
                {
                    Login user = Mediator.GetVar("Login") as Login;            //Setting Employee_id same as Logged in user 
                    EmpInfo.emp_id = user.emp_id;

                    isAdmin = user.role.Equals("Admin") ? true : false;
                    VendorGrid = user.role.Equals("Contractor") ? Visibility.Visible : Visibility.Collapsed;
                    EmpInfo.IsContractor = user.role.Equals("Contractor");

                    log.Info("Finding data for Employee " + user.emp_id);

                    string findQuery = "select * from EmpInfo where emp_id = '" + user.emp_id + "'";
                    List<EmpInfo> EmpInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;
                    if (EmpInfoList.Count != 0)        //if loggedin user is already present in EmpInfo
                    {
                        EmpInfo = EmpInfoList.First();
                        isUserPresent = true;
                    }             //else keep EmpInfo empty for insert
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Insert Or Update Profile
        public ICommand UpdateProfile => new Command(_UpdateProfile, CanUpdate);
        private void _UpdateProfile(object parameter)
        {
            try
            {
                if (isUserPresent)
                    if (Connection.updateData(EmpInfo, "emp_id"))
                    {
                        MessageBox.Show("Profile successfully updated");
                        log.Info("Empployee Information Updated");
                    }
                    else MessageBox.Show("Error in Updation.");
                else
                    if (Connection.setData(EmpInfo))
                {
                    MessageBox.Show("Profile successfully inserted");
                    log.Info("Empployee Information Inserted");
                }
                else MessageBox.Show("Error in Insertion");

                //Clearing employee object
                Mediator.RemoveVar("EmpInfo");
                SwitchEnable();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //CanUpdate methid to check for empty textboxes
        private bool CanUpdate(object parameter)
        {
            return !string.IsNullOrWhiteSpace(EmpInfo.FirstName) && !string.IsNullOrWhiteSpace(EmpInfo.MiddleName) && !string.IsNullOrWhiteSpace(EmpInfo.LastName) && !string.IsNullOrWhiteSpace(EmpInfo.EmailId) && !string.IsNullOrWhiteSpace(EmpInfo.EmpId) && !string.IsNullOrWhiteSpace(EmpInfo.Salary.ToString()) && EmpInfo.Salary != 0 && EmpInfo.DOB!=null && EmpInfo.DOJ!=null && !string.IsNullOrWhiteSpace(EmpInfo.City) && !string.IsNullOrWhiteSpace(EmpInfo.Address) && !string.IsNullOrWhiteSpace(EmpInfo.Department);
        }

        //Switch & enable buttons
        private void SwitchEnable()
        {
            if (isAdmin)
            {
                Mediator.PerformAction("EnableButtons");

                Mediator.PerformAction("SwitchToHomeView");
            }
            else
            {
                Mediator.PerformAction("SwitchToHomeView");
            }
        }

        //GoToHomePage Command
        public ICommand GoToHomePage => new Command(_GoToHomePage);
        private void _GoToHomePage(object parameter)
        {
            SwitchEnable();
        }
    }
}
