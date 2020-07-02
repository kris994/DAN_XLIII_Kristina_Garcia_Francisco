using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLIII_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Class that creates a log message and saves it to the file
    /// </summary>
    class LogMessage
    {
        // File where to save the message
        private readonly string file = "LogFile.txt";
        // The lock object protecting the entrance to the file
        private readonly object locker = new object();

        /// <summary>
        /// Message that will be saved in the log file
        /// </summary>
        /// <param name="type">The type of message</param>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <returns></returns>
        public string Message(string type, string firstName, string lastName)
        {
            return type + " manager: " + firstName + " " + lastName;
        }

        /// <summary>
        /// Save the log message in the file
        /// </summary>
        /// <param name="message">message that will be saved</param>
        public void LogFile(string message)
        {
            lock (locker)
            {
                try
                {
                    Thread.Sleep(2500);
                    StreamWriter streamWriter = new StreamWriter(file, append: true);
                    string logMessage = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "] " + message;
                    streamWriter.WriteLine(logMessage.ToString());
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                catch (FileNotFoundException)
                {
                    File.Create(file);
                }
            }
        }
    }
}
