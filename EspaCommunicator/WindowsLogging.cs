using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Diagnostics;

namespace EspaCommunicator
{
    public enum EVENTS { COMMON, CONVERSION, FILEIO };

    public class WindowsLogging
    {
        private String Name_Log;        // Ereignisanzeige(Lokal)/Anwendungs- und Dienstprotokolle/[Name_Log]
        private String Name_Machine;
        private String Name_Source;
        private EventLog myLog;

        public WindowsLogging(String Log, String Machine, String Source)
        {
            Name_Log = Log;
            Name_Machine = Machine;
            Name_Source = Source;
        }

        /// <summary>
        /// creates an formatted string out of the given exception
        /// </summary>
        /// <param name="exc">the active Exception</param>
        /// <returns> returns a formatted String</returns>
        public String GetExceptionString(Exception exc)
        {
            return String.Format("StackTrace:{0}{1}{2}{3}Message:{4}{5}", System.Environment.NewLine,
                                                                        exc.StackTrace,
                                                                        System.Environment.NewLine,
                                                                        System.Environment.NewLine,
                                                                        System.Environment.NewLine,
                                                                        exc.Message
                                                                        );
        }


        public void LogError(Exception exc, EVENTS eventID)
        {
            LogError(GetExceptionString(exc), eventID);
        }

        public void LogError(String Message, EVENTS eventID)
        {
            WriteEntry(Message, EventLogEntryType.Error, (int)eventID);
        }


        public void LogFailureAudit(Exception exc, EVENTS eventID)
        {
            LogFailureAudit(GetExceptionString(exc), eventID);
        }
        public void LogFailureAudit(String Message, EVENTS eventID)
        {
            WriteEntry(Message, EventLogEntryType.FailureAudit, (int)eventID);
        }


        public void LogInformation(Exception exc, EVENTS eventID)
        {
            LogInformation(GetExceptionString(exc), eventID);
        }
        public void LogInformation(String Message, EVENTS eventID)
        {
            WriteEntry(Message, EventLogEntryType.Information, (int)eventID);
        }


        public void LogSuccessAudit(Exception exc, EVENTS eventID)
        {
            LogSuccessAudit(GetExceptionString(exc), eventID);
        }
        public void LogSuccessAudit(String Message, EVENTS eventID)
        {
            WriteEntry(Message, EventLogEntryType.SuccessAudit, (int)eventID);
        }

        public void LogWarning(Exception exc, EVENTS eventID)
        {
            LogWarning(GetExceptionString(exc), eventID);
        }
        public void LogWarning(String Message, EVENTS eventID)
        {
            WriteEntry(Message, EventLogEntryType.Warning, (int)eventID);
        }

        /// <summary>
        /// write an entry into a spefific subfolder 'Name_Log'
        /// </summary>
        /// <param name="Message">the message what should be shown in the Logfile</param>
        /// <param name="Type">the type of EventLogEntry</param>
        /// <param name="eventID">the software specific event type</param>
        private void WriteEntry(String Message, EventLogEntryType Type, int eventID)
        {
            try
            {
                if ((!IsUserAdministrator()) || (!Program.GlobalVars.OutputWindowVisible))
                {
                    // you should be in debug mode and the app should run with admin rights!
                    // you need the admin rights for creating the Log-File if it doesn't exists!
                    // the log entries can be created with standard rights!
                    return;
                }

                //EventLog.Delete(Name_Log, Name_Machine);
                if (!EventLog.Exists(Name_Log, Name_Machine))
                {
                    CreateLog(Name_Log, Name_Machine);
                    /*
                    if (!IsUserAdministrator())
                    {
                        Console.Out.WriteLine("permission error");
                    }
                    */
                }
                myLog = new EventLog(Name_Log, Name_Machine, Name_Source);
                myLog.WriteEntry(Message + myLog.MachineName, Type);
            }
            catch
            {

            }

            Console.Out.WriteLine("new event added: TYPE={0}", Type.ToString());
        }

        private bool CreateLog(String Log, String Machine)
        {
            myLog = new EventLog(Name_Log, Name_Machine, Name_Source);
            return true;
        }

        /// <summary>
        /// writes an entry into the main eventfolder, where the most of the events are logged
        /// </summary>
        /// <param name="Message">the message what should be shown in the Logfile</param>
        /// <param name="Type">the type of EventLogEntry</param>
        /// <param name="eventID">the software specific event type</param>
        private void WriteGlobalEntry(String Message, EventLogEntryType Type, int eventID)
        {
            if (!EventLog.SourceExists(Name_Source))
                EventLog.CreateEventSource(Name_Source, Name_Log);
            EventLog.WriteEntry(Name_Source, Message, Type, eventID);

            Console.Out.WriteLine("new event added: TYPE={0}", Type.ToString());
        }

        public bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Out.WriteLine(ex.Message);
                isAdmin = false;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                isAdmin = false;
            }
            return isAdmin;
        }


    }
}
