using DAN_XLIII_Kristina_Garcia_Francisco.Command;
using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using DAN_XLIII_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
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

        #region Constructor
        /// <summary>
        /// Constructor with the add manager info window opening
        /// </summary>
        /// <param name="addCardOpen">opends the add manager window</param>
        public AddManagerViewModel(AddManager addManagerOpen)
        {
            manager = new vwManager();
            addManager = addManagerOpen;
            ManagerList = service.GetAllManagers().ToList();
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

        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
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
    }
}
