using DAN_XLIII.ViewModel;
using System.Windows;

namespace DAN_XLIII.View
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.AddEmployeeViewModel(this);
        }
        //for editing
        public AddEmployee(Service.tblEmployee emp)
        {
            InitializeComponent();
            this.DataContext = new AddEmployeeViewModel(this, emp);
        }
    }
}
