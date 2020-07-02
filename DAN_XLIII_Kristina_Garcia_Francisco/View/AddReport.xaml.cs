using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using DAN_XLIII_Kristina_Garcia_Francisco.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DAN_XLIII_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddReport.xaml
    /// </summary>
    public partial class AddReport : Window
    {
        /// <summary>
        /// Window constructor for editing report
        /// </summary>
        /// <param name="reportEdit">report that is bing edited</param>
        public AddReport(vwUserReport reportEdit)
        {
            InitializeComponent();
            this.DataContext = new AddReportViewModel(this, reportEdit);
        }

        /// <summary>
        /// Window constructor for creating reports
        /// </summary>
        public AddReport()
        {
            InitializeComponent();
            this.DataContext = new AddReportViewModel(this);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
