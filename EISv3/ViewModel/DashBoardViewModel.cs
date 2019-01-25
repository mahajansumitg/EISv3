using EISv3.Model;
using EISv3.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static EISv3.Model.EmpInfo;
using System.Windows.Input;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;

namespace EISv3.ViewModel
{
    public class DashBoardViewModel : NotifyOnPropertyChanged
    {

        #region Properties

        //ListView Binding
        private ObservableCollection<EmpInfo> currentPageEmpInfoList;
        public ObservableCollection<EmpInfo> CurrentPageEmpInfoList
        {
            get => currentPageEmpInfoList;
            set
            {
                currentPageEmpInfoList = value;
                OnPropertyChanged("CurrentPageEmpInfoList");
            }
        }

        //current page no
        private int currentPage = 1;
        public String CurrentPage
        {
            get => currentPage.ToString();
            set
            {
                currentPage = Int32.Parse(value);
                OnPropertyChanged("CurrentPage");
                if (currentPage < 1) currentPage = 1;
                if (currentPage > LastPage) currentPage = LastPage;
                setPageInListView();
            }
        }

        //last page no in listView
        private int lastPage = 1;
        public int LastPage
        {
            get => lastPage;
            set { lastPage = value; OnPropertyChanged("LastPage"); }
        }

        //EmpId with which we want to search
        private String empIdSearch;
        public String EmpIdSearch
        {
            get => empIdSearch;
            set { empIdSearch = value; OnPropertyChanged("EmpIdSearch"); }
        }

        #endregion

        #region Search Properties

        //Date of joining with which we want to search
        private DateTime dojSearch;
        public String DojSearch
        {
            get => dojSearch.Year == 1 ? "" : GetFormatedDate(dojSearch);
            set
            {
                if (value == "")
                {
                    OnPropertyChanged("DojSearch");
                    return;
                }
                OnPropertyChanged(ref dojSearch, DateTime.Parse(value));
            }
        }

        //Date of leaving with which we want to search
        private DateTime dolSearch;
        public String DolSearch
        {
            get => dolSearch.Year == 1 ? "" : GetFormatedDate(dolSearch);
            set
            {
                if (value == "")
                {
                    OnPropertyChanged("DolSearch");
                    return;
                }
                OnPropertyChanged(ref dolSearch, DateTime.Parse(value));
            }
        }

        #endregion

        #region Class Variables

        private List<EmpInfo> empInfoList = new List<EmpInfo>();
        private Dictionary<int, List<EmpInfo>> empInfoDict = new Dictionary<int, List<EmpInfo>>();
        private EmpInfo currentEmployee;
        private static int RecordsPerPage = 5;

        #endregion

        public DashBoardViewModel()
        {
            Logger.logging("-----Started DashboardView------");
            InitializePaginaion();
        }

        #region Pagination Methods

        //InitializePagination when required
        private void InitializePaginaion()
        {
            Logger.logging("-----In InitializePagination------");

            string findQuery = "select * from EmpInfo;";
            empInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;
            SetEmpInfoDictionary(empInfoList);
            setPageInListView();

            Logger.logging("-----InitializePagination, SetEmpInfoDictionary, SetPageInListView done------");
        }

        //Create a dictionary of pageno, lists
        private void SetEmpInfoDictionary(List<EmpInfo> empInfoCollection)
        {
            Logger.logging("-----Setting Page In EmpInfoDictionary------");
            List<EmpInfo> empInfoList = new List<EmpInfo>(empInfoCollection);
            Dictionary<int, List<EmpInfo>> empInfoDict = new Dictionary<int, List<EmpInfo>>();
            int page = 1;
            for (int i = 0; i < empInfoList.Count; i += RecordsPerPage)
            {
                empInfoDict.Add(page++, empInfoList.GetRange(i, Math.Min(empInfoList.Count - i, RecordsPerPage)));
            }
            lastPage = --page;
            this.empInfoDict = empInfoDict;
            Logger.logging("-----Logged Out------");
        }

        //Set a particular page in ListView as per the selection
        private void setPageInListView()
        {
            if (currentPage == 0) return;

            List<EmpInfo> temp = new List<EmpInfo>();
            empInfoDict.TryGetValue(currentPage, out temp);
            CurrentPageEmpInfoList = new ObservableCollection<EmpInfo>(temp);
            OnPropertyChanged("CurrentPageEmpInfoList");
        }

        #endregion

        #region ListView Operation Commands

        //Employee Selected in ListView
        public ICommand EmpChecked => new Command(CheckEmp);
        private void CheckEmp(object parameter)
        {
            currentEmployee = parameter as EmpInfo;
            Logger.logging("-----Selected one Employee from ListView------");
        }

        //Update Selected Employee
        public ICommand UpdateEmployee => new Command(UpdateCurrentEmployee, PerformActionOnEmp);
        private void UpdateCurrentEmployee(object parameter)
        {
            Logger.logging("-----Selected Update from ListView------");

            Mediator.registerVar("EmpInfo", parameter);
            Mediator.performAction("DisableButtons");
            Mediator.performAction("SwitchToProfileView");
        }

        //Delete Selected Employee
        public ICommand DeleteEmployee => new Command(DeleteCurrentEmployee, PerformActionOnEmp);
        private void DeleteCurrentEmployee(object parameter)
        {
            Logger.logging("-----Selected Delete from ListView------");

            Connection.deleteData<EmpInfo>("emp_id", currentEmployee.emp_id);
            currentEmployee = null;

            Logger.logging("------Selected Employee deleted------");

            InitializePaginaion();
        }

        //Can Update or Delete. Allows only when currentEmployee is not null
        private bool PerformActionOnEmp(object sender)
        {
            return currentEmployee != null;
        }

        //Sorting commands
        public ICommand SortCommand => new Command(Sort, CanSort);

        private string _sortColumn;
        private ListSortDirection _sortDirection;
        private void Sort(object parameter)
        {
            Logger.logging("-----Clicked on Header in ListView------");

            Sort sortObj = new Sort();
            string column = parameter as string;
            if (_sortColumn == column)
            {
                if (_sortDirection == ListSortDirection.Ascending)
                {
                    _sortDirection = ListSortDirection.Descending;
                    currentPageEmpInfoList = new ObservableCollection<EmpInfo>(sortObj.OrderByLogic(CurrentPageEmpInfoList, column, _sortDirection));
                    OnPropertyChanged("CurrentPageEmpInfoList");
                }
                else
                {
                    _sortDirection = ListSortDirection.Ascending;
                    currentPageEmpInfoList = new ObservableCollection<EmpInfo>(sortObj.OrderByLogic(CurrentPageEmpInfoList, column, _sortDirection));
                    OnPropertyChanged("CurrentPageEmpInfoList");
                }
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
                currentPageEmpInfoList = new ObservableCollection<EmpInfo>(sortObj.OrderByLogic(CurrentPageEmpInfoList, column, _sortDirection));
                OnPropertyChanged("CurrentPageEmpInfoList");
            }

            Logger.logging("-----ListView Sorted------");
        }

        private bool CanSort(object sender)
        {
            return true;
        }

        #endregion

        #region Pagination Commands

        //Previous Page selected
        public ICommand PrevPage => new Command(PreviousPage);
        private void PreviousPage(object parameter)
        {
            Logger.logging("-----Clicked on Previous Page from ListView------");
            if (currentPage != 1) CurrentPage = (currentPage - 1).ToString();
        }

        //Next Page selected
        public ICommand NxtPage => new Command(NextPage);
        private void NextPage(object parameter)
        {
            Logger.logging("-----Clicked on Next Page from ListView------");
            if (currentPage != LastPage) CurrentPage = (currentPage + 1).ToString();
        }

        #endregion

        #region Search Commands

        //Search 
        public ICommand Search => new Command(SeachEmployee, PerformSearch);
        private void SeachEmployee(object parameter)
        {
            Logger.logging("-----Clicked on Search from ListView------");

            List<EmpInfo> prevEmpInfoList = new List<EmpInfo>(empInfoList);
            List<EmpInfo> newEmpInfoList = new List<EmpInfo>(empInfoList);

            foreach (EmpInfo emp in empInfoList)
            {
                if (!String.IsNullOrEmpty(empIdSearch) && !emp.emp_id.ToLower().Contains(empIdSearch.ToLower())
                    || !String.IsNullOrEmpty(DojSearch) && emp.doj != DateTime.Parse(DojSearch)
                    || !String.IsNullOrEmpty(DolSearch) && emp.dol != DateTime.Parse(DolSearch))
                    newEmpInfoList.Remove(emp);
            }

            SetEmpInfoDictionary(newEmpInfoList);

            setPageInListView();
            empInfoList = prevEmpInfoList;

            Logger.logging("-----Search Operation Completed & Displyed in ListView------");
        }

        public ICommand Clear => new Command(ClearSearch, PerformSearch);
        private void ClearSearch(object parameter)
        {
            Logger.logging("-----Clicked on Clear from ListView------");

            EmpIdSearch = DojSearch = DolSearch = "";

            SetEmpInfoDictionary(empInfoList);
            setPageInListView();
        }

        private bool PerformSearch(object sender)
        {
            return !String.IsNullOrEmpty(empIdSearch) || !String.IsNullOrEmpty(DojSearch) || !String.IsNullOrEmpty(DolSearch);
        }

        #endregion

    }
}
