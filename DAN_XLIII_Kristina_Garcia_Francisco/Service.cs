using DAN_XLIII_Kristina_Garcia_Francisco.Helper;
using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_XLIII_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that includes all CRUD functions of the application
    /// </summary>
    class Service
    {
        /// <summary>
        /// Saves the loggdin user
        /// </summary>
        public List<tblUser> LoggedInUser = new List<tblUser>();

        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about workers
        /// </summary>
        /// <returns>a list of found workers</returns>
        public List<tblUser> GetAllWorkers()
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    for (int i = 0; i < GetAllUsers().Count; i++)
                    {
                        if (GetAllUsers()[i].Access == null)
                        {
                            List<tblUser> list = new List<tblUser>();
                            list.Add(GetAllUsers()[i]);
                            return list;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about managers
        /// </summary>
        /// <returns>a list of found managers</returns>
        public List<tblUser> GetAllManagers()
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    for (int i = 0; i < GetAllUsers().Count; i++)
                    {
                        if (GetAllUsers()[i].Access != null)
                        {
                            List<tblUser> list = new List<tblUser>();
                            list.Add(GetAllUsers()[i]);
                            return list;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Creates or edits a manager
        /// </summary>
        /// <param name="manager">the manager that is esing added</param>
        /// <returns>a new or edited manager</returns>
        public vwManager AddManager(vwManager manager)
        {
            InputCalculator iv = new InputCalculator();
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    if (manager.UserID == 0)
                    {
                        manager.DateOfBirth = iv.CountDateOfBirth(manager.JMBG);

                        tblUser newManager = new tblUser();
                        newManager.FirstName = manager.FirstName;
                        newManager.LastName = manager.LastName;
                        newManager.JMBG = manager.JMBG;
                        newManager.DateOfBirth = manager.DateOfBirth;
                        newManager.BankAccount = manager.BankAccount;
                        newManager.Email = manager.Email;
                        newManager.Position = manager.Position;
                        newManager.Salary = manager.Salary;
                        newManager.Username = manager.Username;
                        newManager.UserPassword = manager.UserPassword;
                        newManager.Sector = manager.Sector;
                        newManager.Access = manager.Access;

                        context.tblUsers.Add(newManager);
                        context.SaveChanges();
                        manager.UserID = newManager.UserID;
                        return manager;
                    }
                    else
                    {
                        tblUser managerToEdit = (from ss in context.tblUsers where ss.UserID == manager.UserID select ss).First();

                        // Get the date of birth
                        manager.DateOfBirth = iv.CountDateOfBirth(manager.JMBG);

                        managerToEdit.FirstName = manager.FirstName;
                        managerToEdit.LastName = manager.LastName;
                        managerToEdit.JMBG = manager.JMBG;
                        managerToEdit.DateOfBirth = manager.DateOfBirth;
                        managerToEdit.BankAccount = manager.BankAccount;
                        managerToEdit.Email = manager.Email;
                        managerToEdit.Salary = manager.Salary;
                        managerToEdit.Username = manager.Username;
                        managerToEdit.Position = manager.Position;
                        managerToEdit.UserPassword = manager.UserPassword;
                        managerToEdit.Sector = manager.Sector;
                        managerToEdit.Access = manager.Access;

                        managerToEdit.UserID = manager.UserID;

                        tblUser managerEdit = (from ss in context.tblUsers
                                                 where ss.UserID == manager.UserID
                                                 select ss).First();
                        context.SaveChanges();
                        return manager;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
