using DAN_XLIII.Command;
using DAN_XLIII.Service;
using DAN_XLIII.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII.ViewModel
{
    class ManagerViewModel:ViewModelBase
    {
        ManagerModify man;
        private int _idOfManager;
        public int idOfManager
        {
            get
            {
                return _idOfManager;
            }
            set
            {
                _idOfManager = value;
                OnPropertyChanged("idOfManager");
            }
        }
        #region Property reports
        private List<vwReport> _reportsList;
        public List<vwReport> reportsList
        {
            get
            {
                return _reportsList;
            }
            set
            {
                _reportsList = value;
                OnPropertyChanged("reportsList");
            }
        }

        private vwReport _report;
        public vwReport report
        {
            get
            {
                return _report;
            }
            set
            {
                _report = value;
                OnPropertyChanged("report");
            }
        }

        private bool _isDeletedReport;
        public bool isDeletedReport
        {
            get
            {
                return _isDeletedReport;
            }
            set
            {
                _isDeletedReport = value;
            }
        }
        #endregion

        #region Property employee
        private List<tblEmployee> _employeeList;
        public List<tblEmployee> employeeList
        {
            get
            {
                return _employeeList;
            }
            set
            {
                _employeeList = value;
                OnPropertyChanged("employeeList");
            }
        }


        private tblEmployee _viewEmployee;
        public tblEmployee viewEmployee
        {
            get
            {
                return _viewEmployee;
            }
            set
            {
                _viewEmployee = value;
                OnPropertyChanged("viewEmployee");
            }
        }
        private bool _isDeletedEmployee;
        public bool isDeletedEmployee
        {
            get
            {
                return _isDeletedEmployee;
            }
            set
            {
                _isDeletedEmployee = value;
            }
        }

        private tblManager _manager;
        public tblManager manager
        {
            get
            {
                return _manager;
            }
            set
            {
                _manager = value;
                OnPropertyChanged("manager");
            }
        }

        #endregion

        #region Visibility
        
        private Visibility _viewStuff = Visibility.Collapsed;
        public Visibility viewStuff
        {
            get
            {
                return _viewStuff;
            }
            set
            {
                _viewStuff = value;
                OnPropertyChanged("viewStuff");
            }
        }

        private Visibility _viewReports = Visibility.Collapsed;
        public Visibility viewReports
        {
            get
            {
                return _viewReports;
            }
            set
            {
                _viewReports = value;
                OnPropertyChanged("viewReports");
            }
        }

       //only hr and finance can view reports
        private Visibility _buttonViewReports = Visibility.Collapsed;
        public Visibility buttonViewReports
        {
            get
            {
                if(manager.sector != "RD")
                {
                    reportsList = Service.Service.GetAllReports();
                    return Visibility.Visible;
                }
                return _buttonViewReports;
            }
            set
            {
                _buttonViewReports = value;
                OnPropertyChanged("buttonViewReports");
            }
        }
        //if manager has modify access
        private Visibility _buttonAdd = Visibility.Collapsed;
        public Visibility buttonAdd
        {
            get
            {
                if (manager.access == "modify")
                {
                    return Visibility.Visible;
                }
                return _buttonAdd;
            }
            set
            {
                _buttonViewReports = value;
                OnPropertyChanged("buttonAdd");
            }
        }
        //
        private Visibility _buttonHR = Visibility.Collapsed;
        public Visibility buttonHR
        {
            get
            {
                if (manager.sector == "HR")
                {
                    return Visibility.Visible;
                }
                return _buttonHR;
            }
            set
            {
                _buttonViewReports = value;
                OnPropertyChanged("buttonHR");
            }
        }

        private ICommand _readStuff;
        public ICommand readStuff
        {
            get
            {
                if (_readStuff == null)
                {
                    _readStuff = new RelayCommand(param => ReadStuffExecute(), param => CanReadStuffExecute());
                }
                return _readStuff;
            }
        }

        private void ReadStuffExecute()
        {

            try
            {
                viewStuff = Visibility.Visible;
                viewReports = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanReadStuffExecute()
        {
            return true;
        }

        private ICommand _readReports;
        public ICommand readReports
        {
            get
            {
                if (_readReports == null)
                {
                    _readReports = new RelayCommand(param => ReadReportsExecute(), param => CanReadReportsExecute());
                }
                return _readReports;
            }
        }

        private void ReadReportsExecute()
        {

            try
            {
                viewReports = Visibility.Visible;
                viewStuff = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanReadReportsExecute()
        {
            return true;
        }
        #endregion

        #region Constructor
        public ManagerViewModel(ManagerModify openModify, int id)
        {
            man = openModify;
            employeeList = Service.Service.GetAllEmployees();
            idOfManager = id;
            //depend of sector, reports are visiable or not
            manager = Service.Service.GetManagerById(id);
        }
        #endregion

        #region DELETE commands
        //delete employee
        private ICommand _deleteThisEmployee;
        public ICommand deleteThisEmployee
        {
            get
            {
                if (_deleteThisEmployee == null)
                {
                    _deleteThisEmployee = new Command.RelayCommand(param => DeleteThisEmployeeExecute(), param => CanDeleteThisEmployeeExecute());

                }
                return _deleteThisEmployee;
            }
        }

        private void DeleteThisEmployeeExecute()
        {
            MessageBoxResult result = MessageBox.Show("Do you realy want to delete this user?", "Delete User", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Service.Service.DeleteEmployee(viewEmployee);
                //string content2 = "User  " + viewUser.fullname + " with id " + viewUser.userID + " has been deleted.";
                //LogIntoFile.getInstance().PrintActionIntoFile(content2);
                isDeletedEmployee = true;
                employeeList = Service.Service.GetAllEmployees();
            }
        }
        private bool CanDeleteThisEmployeeExecute()
        {
            return true;
        }


        //delete report
        private ICommand _deleteThisReport;
        public ICommand deleteThisReport
        {
            get
            {
                if (_deleteThisReport == null)
                {
                    _deleteThisReport = new Command.RelayCommand(param => DeleteThisReportExecute(), param => CanDeleteThisReportExecute());

                }
                return _deleteThisReport;
            }
        }

        private void DeleteThisReportExecute()
        {
            MessageBoxResult result = MessageBox.Show("Do you realy want to delete this Report?", "Delete Report", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Service.Service.DeleteReport(report);
                //string content2 = "User  " + viewUser.fullname + " with id " + viewUser.userID + " has been deleted.";
                //LogIntoFile.getInstance().PrintActionIntoFile(content2);
                isDeletedReport = true;
                reportsList = Service.Service.GetAllReports();
            }
        }
        private bool CanDeleteThisReportExecute()
        {
            return true;
        }

        #endregion

        #region EDIT command
        //open window for editing user's data
        private ICommand _editThisEmployee;
        public ICommand editThisEmployee
        {
            get
            {
                if (_editThisEmployee == null)
                {
                    _editThisEmployee = new RelayCommand(param => EditThisEmployeeExecute(), param => CanEditThisEmployeeExecute());
                }
                return _editThisEmployee;
            }
        }

        private void EditThisEmployeeExecute()
        {
            try
            {
                if (viewEmployee != null)
                {
                    AddEmployee editEmployee = new AddEmployee(viewEmployee);
                    editEmployee.ShowDialog();

                    if ((editEmployee.DataContext as AddEmployeeViewModel).isUpdatedEmployee == true)
                    {
                        employeeList = Service.Service.GetAllEmployees();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditThisEmployeeExecute()
        {
            return true;
        }

        //edit report
        private ICommand _editThisReport;
        public ICommand editThisReport
        {
            get
            {
                if (_editThisReport == null)
                {
                    _editThisReport = new RelayCommand(param => EditThisReportExecute(), param => CanEditThisReportExecute());
                }
                return _editThisReport;
            }
        }

        private void EditThisReportExecute()
        {
            try
            {
                if (report != null)
                {
                    View.Employee editReport = new Employee(report);
                    editReport.ShowDialog();
                    if ((editReport.DataContext as EmployeeViewModel).isUpdatedReport == true)
                    {
                        reportsList = Service.Service.GetAllReports();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditThisReportExecute()
        {
            return true;
        }
        #endregion

        #region ADD command

        private ICommand _addNewEmployee;
        public ICommand addNewEmployee
        {
            get
            {
                if (_addNewEmployee == null)
                {
                    _addNewEmployee = new RelayCommand(param => AddNewEmployeeExecute(), param => CanAddNewEmployeeExecute());
                }
                return _addNewEmployee;
            }
        }

        private void AddNewEmployeeExecute()
        {
            try
            {
                AddEmployee addEmployee = new AddEmployee();
                addEmployee.ShowDialog();
                if ((addEmployee.DataContext as AddEmployeeViewModel).isUpdatedEmployee == true)
                {
                    employeeList = Service.Service.GetAllEmployees();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewEmployeeExecute()
        {
            return true;
        }
        #endregion

        #region MANAGER REPORT
        private ICommand _addReport;
        public ICommand addReport
        {
            get
            {
                if (_addReport == null)
                {
                    _addReport = new RelayCommand(param => AddReportExecute(), param => CanAddReportExecute());
                }
                return _addReport;
            }
        }

        private void AddReportExecute()
        {
            try
            {     
                DAN_XLIII.View.Employee e = new DAN_XLIII.View.Employee(idOfManager);
                e.ShowDialog();
                if ((e.DataContext as EmployeeViewModel).isUpdatedReport == true)
                {
                    reportsList = Service.Service.GetAllReports();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddReportExecute()
        {
            return true;
        }
        #endregion

        #region LOGOUT
        private ICommand _logOut;
        public ICommand logOut
        {
            get
            {
                if (_logOut == null)
                {
                    _logOut = new Command.RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return _logOut;
            }
        }

        private void LogOutExecute()
        {
            try
            {
                AuthenticationWindow aw = new AuthenticationWindow();
                man.Close();
                aw.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogOutExecute()
        {
            return true;
        }

        #endregion
    }
}
