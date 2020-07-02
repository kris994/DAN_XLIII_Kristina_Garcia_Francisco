using DAN_XLIII_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_XLIII_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        /// <summary>
        /// Admin view
        /// </summary>
        public Admin()
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel(this);
        }
    }
}
