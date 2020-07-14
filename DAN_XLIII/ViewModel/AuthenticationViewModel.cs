using DAN_XLIII.Command;
using DAN_XLIII.Model;
using DAN_XLIII.Service;
using DAN_XLIII.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLIII.ViewModel
{
    class AuthenticationViewModel : ViewModelBase
    {
        AuthenticationWindow authentication;

        private User _currentEmployee;
        public User currentEmployee
        {
            get
            {
                return _currentEmployee;
            }

            set
            {
                _currentEmployee = value;
                OnPropertyChanged("currentEmployee");
            }
        }


        public AuthenticationViewModel(AuthenticationWindow authenticationWindow)
        {
            authentication = authenticationWindow;
            currentEmployee = new User();
        }

        #region Commands
        private ICommand _login;
        public ICommand login
        {
            get
            {
                if (_login == null)
                {
                    _login = new RelayCommand(LoginExecute,CanLoginExecute);
                }
                return _login;
            }
        }

        private void LoginExecute(object obj)
        {
            currentEmployee.password = (obj as PasswordBox).Password;
            try
            {
                switch (currentEmployee.role)
                {
                    case "admin":
                        AdminMenu adminMenu = new AdminMenu();
                        authentication.Close();
                        adminMenu.ShowDialog();
                        break;
                    case "employee":
                        DAN_XLIII.View.Employee e = new DAN_XLIII.View.Employee(currentEmployee.id);
                        authentication.Close();
                        e.ShowDialog();
                        break;
                    case "manager":
                        tblManager m = Service.Service.GetManagerById(currentEmployee.id);
                        ManagerModify managerModify = new ManagerModify(currentEmployee.id);
                        authentication.Close();
                        managerModify.ShowDialog();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLoginExecute(object obj)
        {
            return true;
        }
        #endregion
    }
}