using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class Logfile
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Write"></param>
        /// <param name="Datastream"></param>
        public static void LogDataStream(eEspaDeviceType DeviceType, bool Write,byte[] Datastream,string comment="")
        {
/*
            if(false)
            {
                String LogContent = "";
                foreach (var item in Datastream)
                {
                    LogContent += Constants.AsciiTable[item];
                }
                WriteLog(DeviceType, "{0} [{1}] : {2}", Write ? "WRITE" : "READ", comment, LogContent);
            }
 */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteLog(eEspaDeviceType DeviceType,string format, params object[] args)
        {
/*
            if (false)
            {
                try
                {
                    String FilePath = "";
                    if (DeviceType == eEspaDeviceType.ControlStation) FilePath = @"D:\temp\logging_controlstation.log";
                    else FilePath = @"D:\temp\logging_station.log";

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(FilePath, true))
                    {
                        if (args == null || args.Length <= 0) file.WriteLine(format);
                        else file.WriteLine(format, args);
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
 */
        }
    }
}
