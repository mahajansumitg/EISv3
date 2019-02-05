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
using log4net;
using System.Windows;

namespace EISv3.ViewModel
{
    public class DashBoardViewModel : NotifyOnPropertyChanged
    {
        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            log4net.Config.XmlConfigurator.Configure();

            log.Info("Started DashboardView: In constructor DashBoardViewModel");
            InitializePaginaion();
        }

        #region Pagination Methods

        //InitializePagination when required
        private void InitializePaginaion()
        {
            log.Info("In InitializePagination()");

            string findQuery = "select * from EmpInfo;";
            empInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;

            SetEmpInfoDictionary(empInfoList);
            setPageInListView();

            log.Info("InitializePagination, SetEmpInfoDictionary, SetPageInListView done");
        }

        //Create a dictionary of pageno, lists
        private void SetEmpInfoDictionary(List<EmpInfo> empInfoCollection)
        {
            log.Info("Setting Pages In EmpInfoDictionary: SetEmpInfoDictionary()");

            List<EmpInfo> empInfoList = new List<EmpInfo>(empInfoCollection);
            Dictionary<int, List<EmpInfo>> empInfoDict = new Dictionary<int, List<EmpInfo>>();
            int page = 1;
            for (int i = 0; i < empInfoList.Count; i += RecordsPerPage)
            {
                empInfoDict.Add(page++, empInfoList.GetRange(i, Math.Min(empInfoList.Count - i, RecordsPerPage)));
            }
            lastPage = --page;
            this.empInfoDict = empInfoDict;
            log.Info("*******Logged Out******");
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
            Mediator.registerVar("EmpInfo", currentEmployee);
            log.Info("Selected Employee " + currentEmployee.EmpId + " from ListView");
        }

        //Update Selected Employee
        public ICommand UpdateEmployee => new Command(UpdateCurrentEmployee, PerformActionOnEmp);
        private void UpdateCurrentEmployee(object parameter)
        {
            log.Info("Selected Update from ListView: UpdateCurrentEmployee()");
            Mediator.performAction("SwitchToProfileView");
        }

        //Delete Selected Employee
        public ICommand DeleteEmployee => new Command(DeleteCurrentEmployee, PerformActionOnEmp);
        private void DeleteCurrentEmployee(object parameter)
        {
            log.Info("Selected Delete from ListView: DeleteCurrentEmployee()");

            if (Connection.deleteData<EmpInfo>("emp_id", currentEmployee.emp_id)) MessageBox.Show(currentEmployee.emp_id+" is deleted."); ;

            log.Info("Selected Employee "+ currentEmployee.emp_id + " deleted");

            currentEmployee = null;

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
            
            string column = parameter as string;

            log.Info("Clicked on Header "+ column + " in ListView");

            if (_sortColumn == column)
            {
                if (_sortDirection == ListSortDirection.Ascending)
                {
                    _sortDirection = ListSortDirection.Descending;

                    PerformSort(column);
                }
                else
                {
                    _sortDirection = ListSortDirection.Ascending;

                    PerformSort(column);
                }
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;

                PerformSort(column);
            }

            log.Info("ListView Sorted");
        }

        private void PerformSort(string column)
        {
            Sort sortObj = new Sort();
            sortObj.OrderByLogic(ref empInfoList, column, _sortDirection);
            SetEmpInfoDictionary(empInfoList);
            setPageInListView();
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
            log.Info("Clicked on Previous Page from ListView: PreviousPage()");
            if (currentPage != 1) CurrentPage = (currentPage - 1).ToString();
        }

        //Next Page selected
        public ICommand NxtPage => new Command(NextPage);
        private void NextPage(object parameter)
        {
            log.Info("Clicked on Next Page from ListView: NextPage()");
            if (currentPage != LastPage) CurrentPage = (currentPage + 1).ToString();
        }

        #endregion

        #region Search Commands

        //Search 
        public ICommand Search => new Command(SeachEmployee, PerformSearch);
        private void SeachEmployee(object parameter)
        {
            log.Info("Clicked on Search from ListView: SeachEmployee()");

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

            log.Info("Search Operation Completed & Displyed in ListView");
        }

        public ICommand Clear => new Command(ClearSearch, PerformSearch);
        private void ClearSearch(object parameter)
        {
            log.Info("Cleared search");

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
