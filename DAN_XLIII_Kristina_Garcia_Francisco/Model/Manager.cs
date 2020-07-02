using DAN_XLIII_Kristina_Garcia_Francisco.Helper;
using System.ComponentModel;

namespace DAN_XLIII_Kristina_Garcia_Francisco.Model
{
    public partial class vwManager : IDataErrorInfo
    {
        Validation validation = new Validation();

        /// <summary>
        /// Total amount of propertis we are checking
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "JMBG",
            "Gender",
            "BankAccount",
            "Email",
            "Salary",
            "Sector",
            "Access",
            "Username"
        };

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    // there is an error
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Checks if the inputs are incorrect
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Does validations on the property location
        /// </summary>
        /// <param name="propertyName">property we are checking</param>
        /// <returns>if the property is valid (null) or error (string)</returns>
        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case "JMBG":
                        result = this.validation.JMBGChecker(JMBG, UserID);
                        break;

                    case "Username":
                        result = this.validation.UsernameChecker(Username, UserID);
                        break;

                    case "BankAccount":
                        result = this.validation.TooShort(BankAccount, 6);
                        break;

                    case "Email":
                        result = this.validation.IsValidEmailAddress(Email, UserID);
                        break;

                    case "Salary":
                        result = this.validation.IsDouble(Salary);
                        break;

                    case "Sector":
                        result = this.validation.CannotBeEmpty(Sector);
                        break;

                    case "Access":
                        result = this.validation.CannotBeEmpty(Access);
                        break;

                    default:
                        result = null;
                        break;
                }

                return result;
            }
        }
    }
}
