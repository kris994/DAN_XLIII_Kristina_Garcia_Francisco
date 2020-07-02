

using DAN_XLIII_Kristina_Garcia_Francisco.Command;
using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using DAN_XLIII_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Main Window view model
    /// </summary>
    class MainWindowViewModel : BaseViewModel
    {
        MainWindow main;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with Main Window param
        /// </summary>
        /// <param name="mainOpen">opens the main window</param>
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            WorkerList = service.GetAllWorkers().ToList();
            ReportList = service.GetAllReports().ToList();
            WorkerReportList = service.GetAllWorkerReports(Service.LoggedInUser[0].UserID).ToList();
            AccessModifier();
            SectorModifier();
            ReportVisibilityModifier();
            
        }
        #endregion

        #region Property
        /// <summary>
        /// List of personal Reports
        /// </summary>
        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

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
        /// List of all worker reports
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
        /// List of all Workers
        /// </summary>
        private List<tblUser> workerList;
        public List<tblUser> WorkerList
        {
            get
            {
                return workerList;
            }
            set
            {
                workerList = value;
                OnPropertyChanged("WorkerList");
            }
        }

        /// <summary>
        /// Specific Report
        /// </summary>
        private tblReport report;
        public tblReport Report
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
        /// Specific User
        /// </summary>
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// User with modify access
        /// </summary>
        private Visibility modifyVisibility;
        public Visibility ModifyVisibility
        {
            get
            {
                return modifyVisibility;
            }
            set
            {
                modifyVisibility = value;
                OnPropertyChanged("ModifyVisibility");
            }
        }

        /// <summary>
        /// User with crud access
        /// </summary>
        private Visibility workerVisibility;
        public Visibility WorkerVisibility
        {
            get
            {
                return workerVisibility;
            }
            set
            {
                workerVisibility = value;
                OnPropertyChanged("WorkerVisibility");
            }
        }

        /// <summary>
        /// User with sector access
        /// </summary>
        private Visibility sectorVisibility;
        public Visibility SectorVisibility
        {
            get
            {
                return sectorVisibility;
            }
            set
            {
                sectorVisibility = value;
                OnPropertyChanged("SectorVisibility");
            }
        }

        /// <summary>
        /// User with all report access
        /// </summary>
        private Visibility allReportsVisibility;
        public Visibility AllReportsVisibility
        {
            get
            {
                return allReportsVisibility;
            }
            set
            {
                allReportsVisibility = value;
                OnPropertyChanged("AllReportsVisibility");
            }
        }

        /// <summary>
        /// User with personal report access
        /// </summary>
        private Visibility reportVisibility;
        public Visibility ReportVisibility
        {
            get
            {
                return reportVisibility;
            }
            set
            {
                reportVisibility = value;
                OnPropertyChanged("ReportVisibility");
            }
        }
        #endregion

        /// <summary>
        /// This method searches for the selected Worker
        /// </summary>
        /// <returns>the found worker</returns>
        private vwUser Worker()
        {
            try
            {
                using (ReportDBEntities db = new ReportDBEntities())
                {
                    vwUser user = new vwUser();
                    user = db.vwUsers.Where(x => x.UserID == User.UserID).First();
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// This method searches for the selected Report
        /// </summary>
        /// <returns>the found report</returns>
        private vwUserReport UserReport()
        {
            try
            {
                using (ReportDBEntities db = new ReportDBEntities())
                {
                    vwUserReport user = new vwUserReport();
                    user = db.vwUserReports.Where(x => x.ReportID == Report.ReportID).First();
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Checks on what users can see and do with other users
        /// </summary>
        public void AccessModifier()
        {
            if (Service.LoggedInUser[0].Access == null)
            {
                ModifyVisibility = Visibility.Collapsed;
                WorkerVisibility = Visibility.Collapsed;
            }
            else if (Service.LoggedInUser[0].Access.Contains("Read-Only"))
            {
                ModifyVisibility = Visibility.Collapsed;
                WorkerVisibility = Visibility.Visible;
            }
            else if (Service.LoggedInUser[0].Access.Contains("Modify"))
            {
                ModifyVisibility = Visibility.Visible;
                WorkerVisibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Checks what users can do to reports
        /// </summary>
        public void SectorModifier()
        {
            if (Service.LoggedInUser[0].Sector == null)
            {
                AllReportsVisibility = Visibility.Collapsed;
            }
            else if (Service.LoggedInUser[0].Sector.Contains("Financial"))
            {
                AllReportsVisibility = Visibility.Visible;
                SectorVisibility = Visibility.Collapsed;
            }
            else if (Service.LoggedInUser[0].Sector.Contains("HR"))
            {
                AllReportsVisibility = Visibility.Visible;
                SectorVisibility = Visibility.Visible;
            }
            else if (Service.LoggedInUser[0].Sector.Contains("RD"))
            {
                AllReportsVisibility = Visibility.Collapsed;
                sectorVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Only visible to the user that owns the reports
        /// </summary>
        public void ReportVisibilityModifier()
        {
            if (Service.LoggedInUser[0].Sector == null)
            {
                reportVisibility = Visibility.Visible;
            }
            else
            {
                reportVisibility = Visibility.Hidden;
            }
        }

        #region Commands
        /// <summary>
        /// Command that tries to delete the worker
        /// </summary>
        private ICommand deleteReport;
        public ICommand DeleteReport
        {
            get
            {
                if (deleteReport == null)
                {
                    deleteReport = new RelayCommand(param => DeleteReportExecute(), param => CanDeleteReportExecute());
                }
                return deleteReport;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteReportExecute()
        {
            // Checks if the user really wants to delete the worker
            var result = MessageBox.Show("Are you sure you want to delete the report?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (UserReport() != null)
                    {
                        int reportID = UserReport().ReportID;
                        service.DeleteReport(reportID);
                        ReportList = service.GetAllReports().ToList();
                        WorkerReportList = service.GetAllWorkerReports(Service.LoggedInUser[0].UserID).ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the report can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteReportExecute()
        {
            if (ReportList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to open the edit report window
        /// </summary>
        private ICommand editReport;
        public ICommand EditReport
        {
            get
            {
                if (editReport == null)
                {
                    editReport = new RelayCommand(param => EditReportExecute(), param => CanEditReportExecute());
                }
                return editReport;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditReportExecute()
        {
            try
            {
                if (UserReport() != null)
                {
                    AddReport addReport = new AddReport(UserReport());
                    addReport.ShowDialog();

                    if ((addReport.DataContext as AddReportViewModel).IsUpdateReport == true)
                    {
                        ReportList = service.GetAllReports().ToList();
                        WorkerReportList = service.GetAllWorkerReports(Service.LoggedInUser[0].UserID).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the report can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditReportExecute()
        {
            if (WorkerList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to add a new report
        /// </summary>
        private ICommand addNewReport;
        public ICommand AddNewReport
        {
            get
            {
                if (addNewReport == null)
                {
                    addNewReport = new RelayCommand(param => AddNewReportExecute(), param => CanAddNewReportExecute());
                }
                return addNewReport;
            }
        }

        /// <summary>
        /// Executes the add Worker command
        /// </summary>
        private void AddNewReportExecute()
        {
            try
            {
                AddReport addReport = new AddReport();
                addReport.ShowDialog();
                if ((addReport.DataContext as AddReportViewModel).IsUpdateReport == true)
                {
                    ReportList = service.GetAllReports().ToList();
                    WorkerReportList = service.GetAllWorkerReports(Service.LoggedInUser[0].UserID).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new report
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewReportExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                Login login = new Login();
                Service.LoggedInUser.RemoveAt(0);
                main.Close();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to delete the worker
        /// </summary>
        private ICommand deleteWorker;
        public ICommand DeleteWorker
        {
            get
            {
                if (deleteWorker == null)
                {
                    deleteWorker = new RelayCommand(param => DeleteWorkerExecute(), param => CanDeleteWorkerExecute());
                }
                return deleteWorker;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteWorkerExecute()
        {
            // Checks if the user really wants to delete the worker
            var result = MessageBox.Show("Are you sure you want to delete the worker?\nAll worker reports will be deleted as well", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Worker() != null)
                    {
                        int userID = Worker().UserID;
                        service.DeleteWorker(userID);
                        WorkerList = service.GetAllWorkers().ToList();
                        UserList = service.GetAllUsers().ToList();
                        ReportList = service.GetAllReports().ToList();
                        WorkerReportList = service.GetAllWorkerReports(Service.LoggedInUser[0].UserID).ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the worker can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteWorkerExecute()
        {
            if (WorkerList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to open the edit worker window
        /// </summary>
        private ICommand editWorker;
        public ICommand EditWorker
        {
            get
            {
                if (editWorker == null)
                {
                    editWorker = new RelayCommand(param => EditWorkerExecute(), param => CanEditWorkerExecute());
                }
                return editWorker;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditWorkerExecute()
        {
            try
            {
                if (Worker() != null)
                {
                    AddWorker addWorker = new AddWorker(Worker());
                    addWorker.ShowDialog();

                    if ((addWorker.DataContext as AddWorkerViewModel).IsUpdateWorker == true)
                    {
                        WorkerList = service.GetAllWorkers().ToList();
                        UserList = service.GetAllUsers().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the worker can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditWorkerExecute()
        {
            if (WorkerList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to add a new worker
        /// </summary>
        private ICommand addNewWorker;
        public ICommand AddNewWorker
        {
            get
            {
                if (addNewWorker == null)
                {
                    addNewWorker = new RelayCommand(param => AddNewWorkerExecute(), param => CanAddNewWorkerExecute());
                }
                return addNewWorker;
            }
        }

        /// <summary>
        /// Executes the add Worker command
        /// </summary>
        private void AddNewWorkerExecute()
        {
            try
            {
                AddWorker addWorker = new AddWorker();
                addWorker.ShowDialog();
                if ((addWorker.DataContext as AddWorkerViewModel).IsUpdateWorker == true)
                {
                    WorkerList = service.GetAllWorkers().ToList();
                    UserList = service.GetAllManagers().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new Worker
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewWorkerExecute()
        {
            return true;
        }
        #endregion
    }
}
