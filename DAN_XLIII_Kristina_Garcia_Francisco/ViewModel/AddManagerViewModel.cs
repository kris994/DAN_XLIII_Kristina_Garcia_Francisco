using DAN_XLIII_Kristina_Garcia_Francisco.Command;
using DAN_XLIII_Kristina_Garcia_Francisco.Helper;
using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using DAN_XLIII_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII_Kristina_Garcia_Francisco.ViewModel
{
    class AddManagerViewModel : BaseViewModel
    {       
        AddManager addManager;
        Service service = new Service();
        private readonly BackgroundWorker bgWorker = new BackgroundWorker();

        #region Constructor
        /// <summary>
        /// Constructor with the add manager info window opening
        /// </summary>
        /// <param name="addManagerOpen">opends the add manager window</param>
        public AddManagerViewModel(AddManager addManagerOpen)
        {
            manager = new vwManager();
            addManager = addManagerOpen;
            ManagerList = service.GetAllManagers().ToList();
            bgWorker.DoWork += WorkerOnDoWork;
        }
        #endregion

        #region Property
        private vwManager manager;
        public vwManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private List<tblUser> managerList;
        public List<tblUser> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
            }
        }

        /// <summary>
        /// Cheks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateManager;
        public bool IsUpdateManager
        {
            get
            {
                return isUpdateManager;
            }
            set
            {
                isUpdateManager = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new manager
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
                Admin adminView = new Admin();
                service.AddManager(Manager);
                IsUpdateManager = true;

                if (!bgWorker.IsBusy)
                {
                    // This method will start the execution asynchronously in the background
                    bgWorker.RunWorkerAsync();
                }

                addManager.Close();
                adminView.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the manager
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return Manager.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the add user or edit user window
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
                Admin adminView = new Admin();
                addManager.Close();
                adminView.Show();
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

        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">DoWorkEventArgs e</param>
        public void WorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            LogMessage log = new LogMessage();
            string message = log.Message("Added", Manager.FirstName, Manager.LastName);

            log.LogFile(message);
        }
    }
}
