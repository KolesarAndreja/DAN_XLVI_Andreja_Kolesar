using DAN_XLIII.Command;
using DAN_XLIII.Service;
using DAN_XLIII.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII.ViewModel
{
    class AdminViewModel:ViewModelBase
    {
        AdminMenu adminMenu;
        private tblEmployee _newEmployee;
        public tblEmployee newEmployee
        {
            get
            {
                return _newEmployee;
            }
            set
            {
                _newEmployee=value;
                OnPropertyChanged("newEmployee");
            }
        }

        public AdminViewModel(AdminMenu openAdminMenu)
        {
            adminMenu = openAdminMenu;
            newEmployee = new tblEmployee(); 
        }

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
                tblEmployee e = Service.Service.AddNewEmployeeOrManager(newEmployee, true);
                if (e != null)
                {
                    MessageBox.Show("Manager has been succesfully created!");
                }
                else
                {
                    MessageBox.Show("Error. Try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(newEmployee.firstname) || String.IsNullOrEmpty(newEmployee.lastname) || String.IsNullOrEmpty(newEmployee.jmbg) || String.IsNullOrEmpty(newEmployee.access) || String.IsNullOrEmpty(newEmployee.username) || String.IsNullOrEmpty(newEmployee.password) || newEmployee.sector==null || newEmployee.access ==null)
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
                    _logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return _logOut;
            }
        }

        private void LogOutExecute()
        {
            try
            {
                AuthenticationWindow aw = new AuthenticationWindow();
                adminMenu.Close();
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
