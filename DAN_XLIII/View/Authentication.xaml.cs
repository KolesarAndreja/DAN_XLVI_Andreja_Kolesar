using System.Windows;

namespace DAN_XLIII.View
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.AuthenticationViewModel(this);
        }
    }
}
