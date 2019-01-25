using EISv3.Model;
using EISv3.Utils;
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

        #region Properties

        private EmpInfo _EmpInfo;
        public EmpInfo EmpInfo
        {
            get => _EmpInfo;
            set { _EmpInfo = value; OnPropertyChanged("EmpInfo"); }
        }

        private Visibility _VendorVisibily;
        public Visibility VendorVisibily
        {
            get => _VendorVisibily;
            set { _VendorVisibily = value; OnPropertyChanged("VendorVisibily"); }
        }

        #endregion

        Boolean isUserPresent;

        public ProfileViewModel()
        {
            Logger.logging("-----Started ProfileView------");
            EmpInfo = Mediator.getVar("EmpInfo") as EmpInfo;   //Getting the Employee which is to be updated
            if (EmpInfo != null) isUserPresent = true;
            else EmpInfo = new EmpInfo();

            //Setting Employee_id same as Logged in user 
            Login user = Mediator.getVar("Login") as Login;
            EmpInfo.emp_id = user.emp_id;

            //If user is contractor show Vendor Box otherwise hide it
            VendorVisibily = user.role.Equals("contractor") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            EmpInfo.IsContractor = user.role.Equals("contractor");

            Logger.logging("-----Finding data for Selected Employee------");

            //Bring the Employee info if already present
            string findQuery = "select * from EmpInfo where emp_id = '" + user.emp_id + "'";
            List<EmpInfo> EmpInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;


            isUserPresent = (EmpInfoList.Count != 0);


            if (isUserPresent)
            {
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

            Logger.logging("-----Empployee Information Updated------");

            MessageBox.Show("Profile successfully updated");
            Mediator.performAction("EnableButtons");
            Mediator.performAction("SwitchToDashBoardView");
        }
    }
}
