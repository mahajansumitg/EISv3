using EISv3.Model;
using EISv3.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static EISv3.Model.EmpInfo;
using System.Windows.Input;

namespace EISv3.ViewModel
{
    public class DashBoardViewModel : Notify
    {
        private ObservableCollection<EmpInfo> currentPageEmpInfoList = new ObservableCollection<EmpInfo>();
        public ObservableCollection<EmpInfo> CurrentPageEmpInfoList
        {
            get => currentPageEmpInfoList;
            set { currentPageEmpInfoList = value; OnPropertyChanged("CurrentPageEmpInfoList"); }
        }

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

        private String empIdSearch;
        public String EmpIdSearch
        {
            get => empIdSearch;
            set { empIdSearch = value; OnPropertyChanged("EmpIdSearch"); }
        }

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

        private int lastPage = 1;
        public int LastPage
        {
            get => lastPage;
            set { lastPage = value; OnPropertyChanged("LastPage"); }
        }

        private List<EmpInfo> empInfoList = new List<EmpInfo>();
        private Dictionary<int, List<EmpInfo>> empInfoDict = new Dictionary<int, List<EmpInfo>>();
        private EmpInfo currentEmployee;
        private static int RecordsPerPage = 5;

        public DashBoardViewModel()
        {
            InitializePaginaion();
        }

        private void InitializePaginaion()
        {
            string findQuery = "select * from EmpInfo;";
            empInfoList = Loading.Show(() => Connection.getData<EmpInfo>(findQuery)) as List<EmpInfo>;
            SetEmpInfoDictionary(empInfoList);
            setPageInListView();
        }

        private void setPageInListView()
        {
            if (currentPage == 0) return;

            List<EmpInfo> temp = new List<EmpInfo>();
            empInfoDict.TryGetValue(currentPage, out temp);
            currentPageEmpInfoList = new ObservableCollection<EmpInfo>(temp);
            OnPropertyChanged("CurrentPageEmpInfoList");
        }

        private void SetEmpInfoDictionary(List<EmpInfo> empInfoCollection)
        {
            List<EmpInfo> empInfoList = new List<EmpInfo>(empInfoCollection);
            Dictionary<int, List<EmpInfo>> empInfoDict = new Dictionary<int, List<EmpInfo>>();
            int page = 1;
            for (int i = 0; i < empInfoList.Count; i += RecordsPerPage)
            {
                empInfoDict.Add(page++, empInfoList.GetRange(i, Math.Min(empInfoList.Count - i, RecordsPerPage)));
            }
            lastPage = --page;
            this.empInfoDict = empInfoDict;
        }

        //Employee Updation
        public ICommand EmpChecked => new Command(CheckEmp);
        private void CheckEmp(object parameter)
        {
            currentEmployee = parameter as EmpInfo;
        }

        public ICommand UpdateEmployee => new Command(UpdateCurrentEmployee, PerformActionOnEmp);
        private void UpdateCurrentEmployee(object parameter)
        {
            Mediator.registerVar("EmpInfo", parameter);
            Mediator.performAction("DisableButtons");
            Mediator.performAction("SwitchToProfileView");
        }

        public ICommand DeleteEmployee => new Command(DeleteCurrentEmployee, PerformActionOnEmp);
        private void DeleteCurrentEmployee(object parameter)
        {
            Connection.deleteData<EmpInfo>("emp_id", currentEmployee.emp_id);
            currentEmployee = null;
            InitializePaginaion();
        }
        private bool PerformActionOnEmp(object sender)
        {
            return currentEmployee != null;
        }

        //Pagination Commands
        public ICommand PrevPage => new Command(PreviousPage);
        private void PreviousPage(object parameter)
        {
            if (currentPage != 1) CurrentPage = (currentPage - 1).ToString();
        }

        public ICommand NxtPage => new Command(NextPage);
        private void NextPage(object parameter)
        {
            if (currentPage != LastPage) CurrentPage = (currentPage + 1).ToString();
        }

        //Searching Commands
        public ICommand Search => new Command(SeachEmployee, PerformSearch);
        private void SeachEmployee(object parameter)
        {
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
        }

        public ICommand Clear => new Command(ClearSearch, PerformSearch);
        private void ClearSearch(object parameter)
        {
            EmpIdSearch = DojSearch = DolSearch = "";

            SetEmpInfoDictionary(empInfoList);
            setPageInListView();
        }
        private bool PerformSearch(object sender)
        {
            return !String.IsNullOrEmpty(empIdSearch) || !String.IsNullOrEmpty(DojSearch) || !String.IsNullOrEmpty(DolSearch);
        }
    }
}
