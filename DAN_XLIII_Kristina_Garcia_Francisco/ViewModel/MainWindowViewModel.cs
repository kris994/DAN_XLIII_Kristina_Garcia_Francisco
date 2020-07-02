

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
            AccessModifier();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of all Workers
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

        public void AccessModifier()
        {
            if (Service.LoggedInUser[0].Access == null)
            {
                ModifyVisibility = Visibility.Hidden;
                WorkerVisibility = Visibility.Hidden;
            }
            else if (Service.LoggedInUser[0].Access.Contains("Read-Only"))
            {
                ModifyVisibility = Visibility.Hidden;
                WorkerVisibility = Visibility.Visible;
            }
            else if (Service.LoggedInUser[0].Access.Contains("Modify"))
            {
                ModifyVisibility = Visibility.Visible;
                WorkerVisibility = Visibility.Visible;
            }
        }

        #region Commands
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

                Service.LoggedInUser.RemoveAt(0);
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
        #endregion
    }
}
