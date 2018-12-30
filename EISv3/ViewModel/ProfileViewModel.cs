using EISv3.Model;
using EISv3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EISv3.ViewModel
{
    public class ProfileViewModel : Notify
    {
        Boolean isUserPresent;

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

        public ProfileViewModel()
        {
            EmpInfo = Mediator.getVar("EmpInfo") as EmpInfo;
            if (EmpInfo != null) isUserPresent = true;
            else EmpInfo = new EmpInfo();

            Login user = Mediator.getVar("Login") as Login;
            EmpInfo.emp_id = user.emp_id;

            VendorVisibily = user.role.Equals("contractor") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            EmpInfo.IsContractor = user.role.Equals("contractor");

            string findQuery = "select * from EmpInfo where emp_id = '" + user.emp_id + "'";
            List<EmpInfo> EmpInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;


            isUserPresent = (EmpInfoList.Count != 0);


            if (isUserPresent)
            {
                EmpInfo = EmpInfoList.First();
            }

        }

        private void updateProfile(object sender, RoutedEventArgs e)
        {
            if (isUserPresent)
                Connection.updateData(EmpInfo, "emp_id");
            else
                Connection.setData(EmpInfo);

            MessageBox.Show("Profile successfully updated");
            Mediator.performAction("SwitchToDashBordView");
        }

        public ICommand UpdateProfile => new Command(_UpdateProfile);
        private void _UpdateProfile(object parameter)
        {
            if (isUserPresent)
                Connection.updateData(EmpInfo, "emp_id");
            else
                Connection.setData(EmpInfo);

            MessageBox.Show("Profile successfully updated");
            Mediator.performAction("EnableButtons");
            Mediator.performAction("SwitchToDashBoardView");
        }
    }
}
