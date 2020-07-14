using DAN_XLIII.ViewModel;
using System.Windows;

namespace DAN_XLIII.View
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        //public Employee()
        //{
        //    InitializeComponent();        
        //    this.DataContext = new EmployeeViewModel(this);
        //}

        public Employee(int id)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, id);
        }

        public Employee(Service.vwReport report)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, report);
        }
    }
}
