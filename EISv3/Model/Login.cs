using EISv3.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EISv3.Model
{
    public class Login : NotifyOnPropertyChanged, IDataErrorInfo
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

        //IDataErrorInfo
        public string Error => throw new NotImplementedException();

        public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();

        public List<Login> LoginList { get; set; } = new List<Login>();

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "UserName":
                        if (!string.IsNullOrEmpty(UserName))
                        {
                            string userName = UserName.Trim();

                            if (string.IsNullOrEmpty(UserName)) break;

                            if (userName.Contains(" "))
                            {
                                result = name + " should not contain white space";
                                break;
                            }
                            foreach (Login login in LoginList)
                            {
                                if (login.UserName.Equals(userName))
                                {
                                    result = "User Name" + userName + " is aleardy taken by someone";
                                    break;
                                }
                            }
                            //if (!avoidSpecialChar.IsMatch(first_name)) result = name + " should not contain numbers and special chars";

                        }
                        break;
                }

                updateErrorCollection(name, result);
               
                return result;
            }
        }

        //adding name, result in ErrorCollection dictionary
        public void updateErrorCollection(string name, string result)
        {
            if (ErrorCollection.ContainsKey(name))
                ErrorCollection[name] = result;
            else if (result != null)
                ErrorCollection.Add(name, result);

            foreach (string value in ErrorCollection.Values)
            {
                if (value != null)
                {
                    SignUpEnable = false;
                    break;
                }
                SignUpEnable = true;
            }

            OnPropertyChanged("ErrorCollection");
            OnPropertyChanged("SignUpEnable");
        }

        public bool SignUpEnable { get; set; } = true;
    }
}
