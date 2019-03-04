using EISv3.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EISv3.Model
{
    public class EmpInfo : NotifyOnPropertyChanged, IDataErrorInfo
    {
        public string first_name;
        public string middle_name;
        public string last_name;
        public string email_id;
        public string emp_id;
        public string city;
        public string address;
        public string department;
        public string vendor;

        public DateTime dob;
        public DateTime doj;
        public DateTime dol;

        public string salary = "5000";

        public string FirstName
        {
            get { return first_name; }
            set { OnPropertyChanged(ref first_name, value); }
        }

        public string MiddleName
        {
            get { return middle_name; }
            set { OnPropertyChanged(ref middle_name, value); }
        }

        public string LastName
        {
            get { return last_name; }
            set { OnPropertyChanged(ref last_name, value); }
        }

        public string EmailId
        {
            get { return email_id; }
            set { OnPropertyChanged(ref email_id, value); }
        }

        public string EmpId
        {
            get { return emp_id; }
            set { OnPropertyChanged(ref emp_id, value); }
        }

        public string City
        {
            get { return city; }
            set { OnPropertyChanged(ref city, value); }
        }

        public string Address
        {
            get { return address; }
            set { OnPropertyChanged(ref address, value); }
        }

        public string Department
        {
            get { return department; }
            set { OnPropertyChanged(ref department, value); }
        }

        public string Vendor
        {
            get { return vendor; }
            set { OnPropertyChanged(ref vendor, value); }
        }

        public DateTime? DOB
        {
            get => dob;
            set
            {
                dob = value.GetValueOrDefault(DateTime.Now);
                OnPropertyChanged("DOB");
            }
        }

        public DateTime? DOJ
        {
            get => doj;
            set
            {
                doj = value.GetValueOrDefault(DateTime.Now);
                OnPropertyChanged("DOJ");
            }
        }

        public DateTime? DOL
        {
            get => dol;
            set
            {
                dol = value.GetValueOrDefault(DateTime.Now);
                OnPropertyChanged("DOL");
            }
        }

        public int Salary
        {
            get { return salary == null ? 0 : int.Parse(salary); }
            set { OnPropertyChanged(ref salary, value.ToString()); }
        }

        public bool IsContractor { get; set; }
        public EmpInfo()
        {
            DOB = null;
            DOJ = null;
            DOL = null;
        }

        public static string GetFormatedDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        //IDataErrorInfo
        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;
                Regex avoidSpecialChar = new Regex("^[a-zA-Z]*$");
                Regex emailPattern = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                switch (name)
                {
                    case "FirstName":
                        if (!string.IsNullOrWhiteSpace(first_name)) 
                        {
                            if (!avoidSpecialChar.IsMatch(first_name)) result = name + " should not contain numbers and special chars";
                            else if(first_name.Contains(" ")) result = name + " should not contain white space";
                        }
                        break;
                    case "MiddleName":
                        if (!string.IsNullOrWhiteSpace(middle_name))
                        {
                            if (!avoidSpecialChar.IsMatch(middle_name)) result = name + " should not contain  numbers and special chars";
                        }
                        break;
                    case "LastName":
                        if (!string.IsNullOrWhiteSpace(last_name))
                        {
                            if (!avoidSpecialChar.IsMatch(last_name)) result = name + " should not contain  numbers and special chars";
                        }
                        break;
                    case "EmailId":
                        if (!string.IsNullOrWhiteSpace(email_id))
                        {
                            if (!emailPattern.IsMatch(email_id)) result = "Invalid Email Id";
                        }
                        break;
                    case "City":
                        if (!string.IsNullOrWhiteSpace(city))
                        {
                            if (!avoidSpecialChar.IsMatch(city)) result = name + " should not contain  numbers and special chars";
                        }
                        break;
                    case "Address":
                        break;
                    case "Department":
                        if (!string.IsNullOrWhiteSpace(department))
                        {
                            if (!avoidSpecialChar.IsMatch(department)) result = name + " should not contain  numbers and special chars";
                        }
                        break;
                    case "Salary":
                        if (!string.IsNullOrWhiteSpace(salary) && int.Parse(salary) != 0)
                        {
                            if (int.Parse(salary) < 5000) result = name + " should be more than or equal to 5000";
                        }
                        break;
                    case "DOB":
                        if (DOB != null)
                        {
                            if (DOB > DateTime.Now.AddYears(-21)) result = "Age i.e. " + name + " should be 21 years";
                        }
                        break;
                    case "DOJ":
                        if (DOJ != null)
                        {
                            if (DOJ <= DOB.GetValueOrDefault(DateTime.Now).AddYears(21)) result = name + " should be greater than 21 age";
                            else if(DOJ >= DateTime.Now.AddMonths(1)) result = name + " should be within 1 month from today";
                        }
                        break;
                    case "DOL":
                        if (DOL != null)
                        {
                            if (DOL <= DOJ) result = name + " should be greater than date of joining";
                            else if(DOL > DateTime.Now) result = name + " should be less than today";
                        }
                        break;
                    case "Vendor":
                        if (IsContractor && !string.IsNullOrWhiteSpace(Vendor))
                        {
                            if (!avoidSpecialChar.IsMatch(Vendor)) result = name + " should not contain  numbers and special chars";
                        }
                        break;
                }

                //adding name, result in ErrorCollection dictionary
                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                foreach (string value in ErrorCollection.Values)
                {
                    if (value != null)
                    {
                        SubmitEnabled = false;
                        break;
                    }
                    SubmitEnabled = true;
                }

                //OnPropertyChanged("ErrorCollection");
                OnPropertyChanged("SubmitEnabled");
                return result;
            }
        }

        //Button Enable
        public bool SubmitEnabled { get; set; } = true;
    }
}
