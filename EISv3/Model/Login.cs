using EISv3.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EISv3.Model
{
    public class Login : Notify
    {
        public string user_name;
        public string pswd;
        public string role;
        public string emp_id;
        public string UserName
        {
            get { return user_name; }
            set { OnPropertyChanged(ref user_name, value); }
        }

        public string PSWD
        {
            get { return pswd; }
            set { OnPropertyChanged(ref pswd, value); }
        }
        public string Role
        {
            get { return role; }
            set { OnPropertyChanged(ref role, value); }
        }
        public string EmpId
        {
            get { return emp_id; }
            set { OnPropertyChanged(ref emp_id, value); }
        }

        public static string GetFormatedDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}
