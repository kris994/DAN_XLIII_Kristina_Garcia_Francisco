using DAN_XLIII_Kristina_Garcia_Francisco.Helper;
using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

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
        public static List<tblUser> LoggedInUser = new List<tblUser>();

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
                List<tblUser> list = new List<tblUser>();
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    for (int i = 0; i < GetAllUsers().Count; i++)
                    {
                        if (GetAllUsers()[i].Access == null)
                        {
                            
                            list.Add(GetAllUsers()[i]);
                        }
                    }
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
        /// Gets all information about managers
        /// </summary>
        /// <returns>a list of found managers</returns>
        public List<tblUser> GetAllManagers()
        {
            try
            {
                List<tblUser> list = new List<tblUser>();
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    for (int i = 0; i < GetAllUsers().Count; i++)
                    {
                        if (GetAllUsers()[i].Access != null)
                        {
                            list.Add(GetAllUsers()[i]);
                            
                        }
                    }
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
        /// Gets all information about reports
        /// </summary>
        /// <returns>a list of found reports</returns>
        public List<tblReport> GetAllReports()
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    List<tblReport> list = new List<tblReport>();
                    list = (from x in context.tblReports select x).ToList();
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
        /// Gets all information about reports
        /// </summary>
        /// <returns>a list of found reports</returns>
        public List<tblReport> GetAllWorkerReports(int UserID)
        {
            try
            {
                List<tblReport> list = new List<tblReport>();
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    for (int i = 0; i < GetAllReports().Count; i++)
                    {
                        if (GetAllReports()[i].UserID == UserID)
                        {
                            list.Add(GetAllReports()[i]);

                        }
                    }
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
        /// Creates or edits a worker
        /// </summary>
        /// <param name="worker">the worker that is esing added</param>
        /// <returns>a new or edited worker</returns>
        public vwUser AddWorker(vwUser worker)
        {
            InputCalculator iv = new InputCalculator();
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    if (worker.UserID == 0)
                    {
                        worker.DateOfBirth = iv.CountDateOfBirth(worker.JMBG);

                        tblUser newWorker = new tblUser
                        {
                            FirstName = worker.FirstName,
                            LastName = worker.LastName,
                            JMBG = worker.JMBG,
                            DateOfBirth = worker.DateOfBirth,
                            BankAccount = worker.BankAccount,
                            Email = worker.Email,
                            Position = worker.Position,
                            Salary = worker.Salary,
                            Username = worker.Username,
                            UserPassword = worker.UserPassword
                        };

                        context.tblUsers.Add(newWorker);
                        context.SaveChanges();
                        worker.UserID = newWorker.UserID;
                        return worker;
                    }
                    else
                    {
                        tblUser workerToEdit = (from ss in context.tblUsers where ss.UserID == worker.UserID select ss).First();

                        // Get the date of birth
                        worker.DateOfBirth = iv.CountDateOfBirth(worker.JMBG);

                        workerToEdit.FirstName = worker.FirstName;
                        workerToEdit.LastName = worker.LastName;
                        workerToEdit.JMBG = worker.JMBG;
                        workerToEdit.DateOfBirth = worker.DateOfBirth;
                        workerToEdit.BankAccount = worker.BankAccount;
                        workerToEdit.Email = worker.Email;
                        workerToEdit.Salary = worker.Salary;
                        workerToEdit.Username = worker.Username;
                        workerToEdit.Position = worker.Position;
                        workerToEdit.UserPassword = worker.UserPassword;

                        workerToEdit.UserID = worker.UserID;

                        tblUser workerEdit = (from ss in context.tblUsers
                                               where ss.UserID == worker.UserID
                                               select ss).First();
                        context.SaveChanges();
                        return worker;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Creates or edits a report
        /// </summary>
        /// <param name="report">the report that is being added</param>
        /// <returns>a new or edited report</returns>
        public vwUserReport AddReport(vwUserReport report)
        {
            InputCalculator iv = new InputCalculator();
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    if (report.ReportID == 0)
                    {
                        tblReport newReport = new tblReport
                        {
                            Project = report.Project,
                            ReportDate = report.ReportDate,
                            ReportHours = report.ReportHours,
                            UserID = Service.LoggedInUser[0].UserID
                        };

                        context.tblReports.Add(newReport);
                        context.SaveChanges();
                        report.ReportID = newReport.ReportID;
                        return report;
                    }
                    else
                    {
                        tblReport reportToEdit = (from ss in context.tblReports where ss.ReportID == report.ReportID select ss).First();

                        reportToEdit.Project = report.Project;
                        reportToEdit.ReportDate = report.ReportDate;
                        reportToEdit.ReportHours = report.ReportHours;
                        reportToEdit.UserID = report.UserID;

                        reportToEdit.ReportID = report.ReportID;

                        tblReport reportEdit = (from ss in context.tblReports
                                              where ss.ReportID == report.ReportID
                                              select ss).First();
                        context.SaveChanges();
                        return report;
                    }
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

                        tblUser newManager = new tblUser
                        {
                            FirstName = manager.FirstName,
                            LastName = manager.LastName,
                            JMBG = manager.JMBG,
                            DateOfBirth = manager.DateOfBirth,
                            BankAccount = manager.BankAccount,
                            Email = manager.Email,
                            Position = manager.Position,
                            Salary = manager.Salary,
                            Username = manager.Username,
                            UserPassword = manager.UserPassword,
                            Sector = manager.Sector,
                            Access = manager.Access
                        };

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

        /// <summary>
        /// Search if user with that ID exists in the user table
        /// </summary>
        /// <param name="userID">Takes the user id that we want to search for</param>
        /// <returns>True if the user exists</returns>
        public bool IsUserID(int userID)
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    int result = (from x in context.tblUsers where x.UserID == userID select x.UserID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Search if report with that ID exists in the report table
        /// </summary>
        /// <param name="reportID">Takes the report id that we want to search for</param>
        /// <returns>True if the report exists</returns>
        public bool IsReportID(int reportID)
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    int result = (from x in context.tblReports where x.ReportID == reportID select x.ReportID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Deletes user, users records and identification card depending if the uderID already exists
        /// </summary>
        /// <param name="userID">the user that is being deleted</param>
        /// <returns>list of users</returns>
        public void DeleteWorker(int userID)
        {
            InputCalculator iv = new InputCalculator();
            List<tblReport> AllReports = GetAllReports();
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    bool isUser = IsUserID(userID);
                    // Delete are evidences from the user
                    for (int i = 0; i < AllReports.Count; i++)
                    {
                        if (AllReports[i].UserID == userID)
                        {
                            tblReport report = (from r in context.tblReports where r.UserID == userID select r).First();
                            context.tblReports.Remove(report);
                            context.SaveChanges();
                        }
                    }
                    if (isUser == true)
                    {
                        // find the user and identification card before removing them
                        tblUser userToDelete = (from r in context.tblUsers where r.UserID == userID select r).First();

                        context.tblUsers.Remove(userToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete the user");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Deletes user, users records and identification card depending if the uderID already exists
        /// </summary>
        /// <param name="reportID">the user that is being deleted</param>
        /// <returns>list of users</returns>
        public void DeleteReport(int reportID)
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    bool isReport = IsReportID(reportID);

                    if (isReport == true)
                    {
                        // find the user and identification card before removing them
                        tblReport reportToDelete = (from r in context.tblReports where r.ReportID == reportID select r).First();

                        context.tblReports.Remove(reportToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete the report");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
