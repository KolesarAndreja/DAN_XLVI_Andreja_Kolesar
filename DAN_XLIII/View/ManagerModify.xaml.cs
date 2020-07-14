using DAN_XLIII.ViewModel;
using System.Windows;

namespace DAN_XLIII.View
{
    /// <summary>
    /// Interaction logic for ManagerModify.xaml
    /// </summary>
    public partial class ManagerModify : Window
    {
        public ManagerModify(int id)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this,id);
        }
    }
}
