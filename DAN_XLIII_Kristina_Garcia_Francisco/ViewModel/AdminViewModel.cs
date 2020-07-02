using DAN_XLIII_Kristina_Garcia_Francisco.Command;
using DAN_XLIII_Kristina_Garcia_Francisco.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIII_Kristina_Garcia_Francisco.ViewModel
{
    class AdminViewModel : BaseViewModel
    {
        Admin view;
        #region Constructor
        public AdminViewModel(Admin adminView)
        {
            view = adminView;
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to add a new manager
        /// </summary>
        private ICommand addManager;
        public ICommand AddManager
        {
            get
            {
                if (addManager == null)
                {
                    addManager = new RelayCommand(param => AddManagerExecute(), param => CanAddManagerExecute());
                }
                return addManager;
            }
        }

        /// <summary>
        /// Tries the execute the add manager command
        /// </summary>
        private void AddManagerExecute()
        {
            try
            {
                AddManager addManager = new AddManager();
                view.Close();
                addManager.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the manager
        /// </summary>
        protected bool CanAddManagerExecute()
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
                view.Close();
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
