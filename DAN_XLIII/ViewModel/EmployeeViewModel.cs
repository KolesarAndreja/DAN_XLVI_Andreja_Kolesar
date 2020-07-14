using DAN_XLIII.Command;
using DAN_XLIII.Service;
using DAN_XLIII.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII.ViewModel
{
    class EmployeeViewModel:ViewModelBase
    {
        View.Employee emp;
        //BackgroundWorker backgroundWorker = new BackgroundWorker();

        private vwReport _newReport;
        public vwReport newReport
        {
            get
            {
                return _newReport;
            }
            set
            {
                _newReport = value;
                OnPropertyChanged("newReport");
            }
        }

        private bool _isUpdatedReport;
        public bool isUpdatedReport
        {
            get
            {
                return _isUpdatedReport;
            }
            set
            {
                _isUpdatedReport = value;
            }
        }
        private Visibility _logOutVisibility = Visibility.Visible;
        public Visibility logOutVisibility
        {
            get
            {
                return _logOutVisibility;
            }
            set
            {
                _logOutVisibility = value;
                OnPropertyChanged("logOutVisibility");
            }
        }
        #region Constructor
        public EmployeeViewModel(View.Employee open, int id)
        {
            emp = open;
            newReport = new vwReport();
            newReport.employeeId = id;
            //backgroundWorker.DoWork += DoWorkAdd;
        }

        public EmployeeViewModel(View.Employee open, vwReport report)
        {
            emp = open;
            newReport = report;
            logOutVisibility = Visibility.Collapsed;
            //backgroundWorker.DoWork += DoWorkEdit;
        }

        #endregion

        //#region BackgroundWorkers's DoWork event handler
        //public void DoWorkAdd(object sender, DoWorkEventArgs e)
        //{
        //    string content = "Employee with id " + newReport.employeeId + " has added new report.";
        //    LogIntoFile.getInstance().PrintActionIntoFile(content);
        //}

        //public void DoWorkEdit(object sender, DoWorkEventArgs e)
        //{
        //    string content = "Report no. " + newReport.reportId + " has been edited.";
        //    LogIntoFile.getInstance().PrintActionIntoFile(content);
        //}
        //#endregion

        #region Commands

        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return _save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                string r = Service.Service.AddReport(newReport);
                if (r != null)
                {
                    MessageBox.Show(r);
                }
                else
                {
                    MessageBox.Show("Report has been saved.");
                    isUpdatedReport = true;
                    //backgroundWorker.RunWorkerAsync();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(newReport.project))
            { 
                return false;
            }
            else
            {
                return true;
            }
        }

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
                emp.Close();
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
