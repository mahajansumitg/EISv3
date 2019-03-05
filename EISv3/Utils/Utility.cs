using EISv3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISv3.Utils
{
    public class Utility
    {
        public static void Sort(ref List<EmpInfo> currentList, string key, ListSortDirection direction)
        {
            if (direction == ListSortDirection.Ascending)
            {
                switch (key)
                {
                    case "First Name":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.first_name));
                        break;
                    case "Middle Name":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.middle_name));
                        break;
                    case "Last Name":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.last_name));
                        break;
                    case "Email":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.email_id));
                        break;
                    case "Employee Id":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.emp_id));
                        break;
                    case "Date of Birth":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.dob));
                        break;
                    case "Date of Joining":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.doj));
                        break;
                    case "Date of Leaving":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.dol));
                        break;
                    case "City":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.city));
                        break;
                    case "Address":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.address));
                        break;
                    case "Department":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.department));
                        break;
                    case "Salary":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.salary));
                        break;
                    case "Vendor":
                        currentList = new List<EmpInfo>(currentList.OrderBy(i => i.vendor));
                        break;
                }
            }
            else
            {
                switch (key)
                {
                    case "First Name":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.first_name));
                        break;
                    case "Middle Name":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.middle_name));
                        break;
                    case "Last Name":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.last_name));
                        break;
                    case "Email":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.email_id));
                        break;
                    case "Employee Id":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.emp_id));
                        break;
                    case "Date of Birth":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.dob));
                        break;
                    case "Date of Joining":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.doj));
                        break;
                    case "Date of Leaving":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.dol));
                        break;
                    case "City":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.city));
                        break;
                    case "Address":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.address));
                        break;
                    case "Department":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.department));
                        break;
                    case "Salary":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.salary));
                        break;
                    case "Vendor":
                        currentList = new List<EmpInfo>(currentList.OrderByDescending(i => i.vendor));
                        break;
                }
            }

        }
    }
}
