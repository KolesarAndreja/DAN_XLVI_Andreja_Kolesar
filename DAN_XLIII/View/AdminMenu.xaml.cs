using System.Windows;


namespace DAN_XLIII.View
{
    /// <summary>
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.AdminViewModel(this);
        }

    }
}
