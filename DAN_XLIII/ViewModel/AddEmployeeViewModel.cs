using DAN_XLIII.Command;
using DAN_XLIII.Service;
using DAN_XLIII.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII.ViewModel
{
    class AddEmployeeViewModel : ViewModelBase
    {
        #region Property
        AddEmployee addEmployee;
        //BackgroundWorker backgroundWorker = new BackgroundWorker();
        private tblEmployee _newEmployee;
        public tblEmployee newEmployee
        {
            get
            {
                return _newEmployee;
            }
            set
            {
                _newEmployee = value;
                OnPropertyChanged("newEmployee");
            }
        }
        private bool _isUpdatedEmployee;
        public bool isUpdatedEmployee
        {
            get
            {
                return _isUpdatedEmployee;
            }
            set
            {
                _isUpdatedEmployee = value;
            }
        }
        #endregion
        #region constructor
        public AddEmployeeViewModel(AddEmployee open)
        {
            addEmployee = open;
            newEmployee = new tblEmployee();
            //backgroundWorker.DoWork += DoWorkAdd;
        }

        public AddEmployeeViewModel(AddEmployee open, tblEmployee userEdit)
        {
            addEmployee = open;
            newEmployee = userEdit;
            //backgroundWorker.DoWork += DoWorkEdit;

        }
        #endregion

        //#region BackgroundWorkers's DoWork event handler
        //public void DoWorkAdd(object sender, DoWorkEventArgs e)
        //{
        //    string content = "Employee " + newEmployee.firstname + " " + newEmployee.lastname  + " with employeeId " + newEmployee.employeeId + " has been added.";
        //    LogIntoFile.getInstance().PrintActionIntoFile(content);
        //}

        //public void DoWorkEdit(object sender, DoWorkEventArgs e)
        //{
        //    string content = "Employee " + newEmployee.firstname + " " + newEmployee.lastname + " with employeeId " + newEmployee.employeeId + " has been edited.";
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
                Service.Service.AddNewEmployeeOrManager(newEmployee, false);
                isUpdatedEmployee = true;
                //backgroundWorker.RunWorkerAsync();
                addEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(newEmployee.firstname) || String.IsNullOrEmpty(newEmployee.lastname) || String.IsNullOrEmpty(newEmployee.jmbg) || String.IsNullOrEmpty(newEmployee.username) || String.IsNullOrEmpty(newEmployee.password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand _close;
        public ICommand close
        {
            get
            {
                if (_close == null)
                {
                    _close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return _close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                addEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion
    }
}
