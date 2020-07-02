using DAN_XLIII_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DAN_XLIII_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Validates the user inputs
    /// </summary>
    class Validation
    {
        InputCalculator iv = new InputCalculator();

        /// <summary>
        /// Validates the given string if its an email or not
        /// </summary>
        /// <param name="s">string that is validated</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string IsValidEmailAddress(string email, int id)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Service service = new Service();

            List<tblUser> AllUsers = service.GetAllUsers();
            string currentEmail = "";

            try
            {
                // Get the current users email
                for (int i = 0; i < AllUsers.Count; i++)
                {
                    if (AllUsers[i].UserID == id)
                    {
                        currentEmail = AllUsers[i].Email;
                        break;
                    }
                }

                // Check if the email already exists, but it is not the current user email
                for (int i = 0; i < AllUsers.Count; i++)
                {
                    if (AllUsers[i].Email == email && currentEmail != email)
                    {
                        return "This Email already exists!";
                    }
                }

                if (regex.IsMatch(email) == false)
                {
                    return "Invalid email";
                }
            }
            catch (ArgumentNullException)
            {
                return "Email cannot be empty";
            }

            return null;
        }

        public string IsDouble(string salary)
        {
            if (double.TryParse(salary, out double value) == false || value < 0)
            {
                return "Not a valid number";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the jmbg is correct
        /// </summary>
        /// <param name="jmbg">the jmbg we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string JMBGChecker(string jmbg, int id)
        {
            Service service = new Service();

            List<tblUser> AllUsers = service.GetAllUsers();
            DateTime dt = default(DateTime);
            string currentJbmg = "";

            try
            {
                // Get the current users jmbg
                for (int i = 0; i < AllUsers.Count; i++)
                {
                    if (AllUsers[i].UserID == id)
                    {
                        currentJbmg = AllUsers[i].JMBG;
                        break;
                    }
                }

                // Check if the jmbg already exists, but it is not the current user jmbg
                for (int i = 0; i < AllUsers.Count; i++)
                {
                    if (AllUsers[i].JMBG == jmbg && currentJbmg != jmbg)
                    {
                        return "This JMBG already exists!";
                    }
                }

                if (!(jmbg.Length == 13))
                {
                    return "Please enter a number with 13 characters.";
                }

                // Get date
                dt = iv.CountDateOfBirth(jmbg);

                if (dt == default(DateTime))
                {
                    return "Incorrect JMBG Format.";
                }
            }
            catch (NullReferenceException)
            {
                return "Please enter a number with 13 characters.";
            }

            return null;
        }

        /// <summary>
        /// Checks if the Username is exists
        /// </summary>
        /// <param name="username">the username we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string UsernameChecker(string username, int id)
        {
            Service service = new Service();

            List<tblUser> AllUsers = service.GetAllUsers();
            string currectUsername = "";

            try
            {
                // Get the current users username
                for (int i = 0; i < AllUsers.Count; i++)
                {
                    if (AllUsers[i].UserID == id)
                    {
                        currectUsername = AllUsers[i].Username;
                        break;
                    }
                }

                // Check if the username already exists, but it is not the current user username
                for (int i = 0; i < AllUsers.Count; i++)
                {
                    if (AllUsers[i].Username == username && currectUsername != username)
                    {
                        return "This Username already exists!";
                    }
                }
            }
            catch (NullReferenceException)
            {
                return "Username cannot be empty.";
            }

            return null;
        }

        /// <summary>
        /// The input cannot be empty
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CannotBeEmpty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Cannot be empty";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Input cannot be shorter than expected
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <param name="number">length of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string TooShort(string name, int number)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < number)
            {
                return "Cannot be shorter than " + number + " characters.";
            }
            else
            {
                return null;
            }
        }
    }
}
