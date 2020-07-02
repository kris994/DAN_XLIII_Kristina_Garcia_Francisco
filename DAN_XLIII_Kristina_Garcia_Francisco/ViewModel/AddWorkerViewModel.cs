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
    class AddWorkerViewModel : BaseViewModel
    {
        AddWorker addWorker;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add worker info window opening
        /// </summary>
        /// <param name="addWorkerOpen">opends the add worker window</param>
        public AddWorkerViewModel(AddWorker addWorkerOpen)
        {
            worker = new vwUser();
            addWorker = addWorkerOpen;
            WorkerList = service.GetAllWorkers().ToList();
        }


        /// <summary>
        /// Constructor with edit worker window opening
        /// </summary>
        /// <param name="addWorkerOpen">opens the add worker window</param>
        /// <param name="workerEdit">gets the worker info that is being edited</param>
        public AddWorkerViewModel(AddWorker addWorkerOpen, vwUser workerEdit)
        {
            worker = workerEdit;
            addWorker = addWorkerOpen;
            WorkerList = service.GetAllWorkers().ToList();
        }
        #endregion

        #region Property
        private vwUser worker;
        public vwUser Worker
        {
            get
            {
                return worker;
            }
            set
            {
                worker = value;
                OnPropertyChanged("Worker");
            }
        }

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
        /// Cheks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateWorker;
        public bool IsUpdateWorker
        {
            get
            {
                return isUpdateWorker;
            }
            set
            {
                isUpdateWorker = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new worker
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
                service.AddWorker(Worker);
                IsUpdateWorker = true;

                addWorker.Close();
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
                return Worker.IsValid;
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
                addWorker.Close();
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
