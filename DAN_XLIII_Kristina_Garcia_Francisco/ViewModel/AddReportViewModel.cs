using DAN_XLIII_Kristina_Garcia_Francisco.Command;
using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using DAN_XLIII_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII_Kristina_Garcia_Francisco.ViewModel
{
    class AddReportViewModel : BaseViewModel
    {
        AddReport addReport;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add report info window opening
        /// </summary>
        /// <param name="addReportOpen">opends the add report window</param>
        public AddReportViewModel(AddReport addReportOpen)
        {
            report = new vwUserReport();
            addReport = addReportOpen;
            ReportList = service.GetAllReports().ToList();
            WorkerReportList = service.GetAllWorkerReports(Service.LoggedInUser[0].UserID).ToList();
        }


        /// <summary>
        /// Constructor with edit report window opening
        /// </summary>
        /// <param name="addReportOpen">opens the add report window</param>
        /// <param name="reportEdit">gets the report info that is being edited</param>
        public AddReportViewModel(AddReport addReportOpen, vwUserReport reportEdit)
        {
            report = reportEdit;
            addReport = addReportOpen;
            ReportList = service.GetAllReports().ToList();
            WorkerReportList = service.GetAllWorkerReports(Service.LoggedInUser[0].UserID).ToList();
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of all Reports
        /// </summary>
        private List<tblReport> reportList;
        public List<tblReport> ReportList
        {
            get
            {
                return reportList;
            }
            set
            {
                reportList = value;
                OnPropertyChanged("ReportList");
            }
        }

        /// <summary>
        /// List of all Workers
        /// </summary>
        private List<tblReport> workerReportList;
        public List<tblReport> WorkerReportList
        {
            get
            {
                return workerReportList;
            }
            set
            {
                workerReportList = value;
                OnPropertyChanged("WorkerReportList");
            }
        }

        /// <summary>
        /// Specific Report
        /// </summary>
        private vwUserReport report;
        public vwUserReport Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
                OnPropertyChanged("Report");
            }
        }

        /// <summary>
        /// Cheks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateReport;
        public bool IsUpdateReport
        {
            get
            {
                return isUpdateReport;
            }
            set
            {
                isUpdateReport = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new report
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                service.AddReport(Report);
                IsUpdateReport = true;

                addReport.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the worker
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return Report.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the add worker or edit worker window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addReport.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
