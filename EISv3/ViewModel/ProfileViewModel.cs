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
            set { _VendorVisibily = value; OnPropertyChanged("VendorVisibily"); }
        }

        #endregion

        Boolean isUserPresent;

        public ProfileViewModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Started ProfileView: In constructor ProfileViewModel()");

            Mediator.performAction("DisableButtons");

            EmpInfo = Mediator.getVar("EmpInfo") as EmpInfo;   //Getting the Employee which is to be updated
            if (EmpInfo != null) isUserPresent = true;
            else EmpInfo = new EmpInfo();

            //Setting Employee_id same as Logged in user 
            if (!isUserPresent)
            {
                Login user = Mediator.getVar("Login") as Login;
                EmpInfo.emp_id = user.emp_id;

                VendorGrid = user.role.Equals("contractor") ? Visibility.Visible : Visibility.Hidden;
                EmpInfo.IsContractor = user.role.Equals("contractor");

                log.Info("Finding data for Employee "+ user.emp_id);
                string findQuery = "select * from EmpInfo where emp_id = '" + user.emp_id + "'";
                List<EmpInfo> EmpInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;
                if (EmpInfoList.Count != 0)        //if loggedin user is already present in EmpInfo
                {
                    EmpInfo = EmpInfoList.First();
                    isUserPresent = true;
                }             //else keep EmpInfo empty for insert
            }
            else     //For ListView Selected Employees
            {
                //checking for contractor or not
                string findRoleQuery = "select * from Login where emp_id = '" + EmpInfo.emp_id + "'";
                List<Login> TempList = Loading.Show(() => Connection.getData<Login>(findRoleQuery)) as List<Login>;
                if(TempList.Count > 0) VendorGrid = TempList.First().Role.Equals("contractor") ? Visibility.Visible : Visibility.Hidden;

                log.Info("Finding data for Selected Employee "+ EmpInfo.emp_id);
                string findQuery = "select * from EmpInfo where emp_id = '" + EmpInfo.emp_id + "'";
                List<EmpInfo> EmpInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;
                EmpInfo = EmpInfoList.First();
            }
        }

        //Insert Or Update Profile
        public ICommand UpdateProfile => new Command(_UpdateProfile);
        private void _UpdateProfile(object parameter)
        {
            if (isUserPresent)
                Connection.updateData(EmpInfo, "emp_id");
            else
                Connection.setData(EmpInfo);

            log.Info("Empployee Information Updated");

            MessageBox.Show("Profile successfully updated");
            Mediator.performAction("EnableButtons");
            Mediator.removeVar("EmpInfo");
            
            Mediator.performAction("SwitchToDashBoardView");
        }
    }
}
