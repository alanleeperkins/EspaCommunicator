using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace EspaCommunicator
{
    public class Helper
    {
        /// <summary>
        /// returns all serial ports found on that system
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllSerialPorts()
        {
            try
            {
                // Get a list of serial port names.
                string[] ports = SerialPort.GetPortNames();
                return ports;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecordTypeID"></param>
        /// <returns></returns>
        public static String GetRecordTypeName(int RecordTypeID)
        {
            switch (RecordTypeID)
            {
                case 1:
                    return "Call Address";

                case 2:
                    return "Display Text";

                case 3:
                    return "Beep Coding";

                case 4:
                    return "Call Type";

                case 5:
                    return "Number of Transmissions";

                case 6:
                    return "Priority Level";

                case 7:
                    return "Call Status";

                case 8:
                    return "System Status";

                default:
                    return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawespa"></param>
        /// <returns></returns>
        public static String ConvertToReadableEspa(byte[] rawespa)
        {
            String ReadableEspa = "";

            if (rawespa == null || rawespa.Length == 0) return ReadableEspa;       

            foreach (var item in rawespa)
            {
                int itemnumeric = Convert.ToInt32(item);

                if (itemnumeric < Constants.AsciiTable.Length)
                {
                    if (itemnumeric <= 31 || itemnumeric >= 127)
                    {
                        if (itemnumeric > 127)
                        {
                            ReadableEspa += String.Format("<{0}>", itemnumeric);
                        }
                        else
                        {
                            ReadableEspa += String.Format("<{0}>", Constants.AsciiTable[itemnumeric]);
                        }
                    }
                    else
                    {
                        // make a char out of it
                        ReadableEspa += Convert.ToChar(item);
                    }
                }
            }

            return ReadableEspa;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawespa"></param>
        /// <returns></returns>
        public static String ConvertToReadableEspa(char[] rawespa, int Length=-1)
        {
            String ReadableEspa = "";

            if (rawespa == null || rawespa.Length == 0) return ReadableEspa;

            if (Length == -1) Length = rawespa.Length;

            foreach (var item in rawespa)
            {
                int itemnumeric = Convert.ToInt32(item);

                if (itemnumeric < Constants.AsciiTable.Length)
                {
                    if (itemnumeric <= 31 || itemnumeric >= 127)
                    {
                        if (itemnumeric > 127)
                        {
                            ReadableEspa += String.Format("<{0}>", itemnumeric);
                        }
                        else
                        {
                            ReadableEspa += String.Format("<{0}>", Constants.AsciiTable[itemnumeric]);
                        }
                    }
                    else
                    {
                        // make a char out of it
                        ReadableEspa += Convert.ToChar(item);
                    }
                }

                if (Length-- <= 0) break;          
            }

            return ReadableEspa;
        }
   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static TrafficLog LoadTrafficLogFromFile(String Path)
        {
            TrafficLog Log = null;
            if (File.Exists(Path) == false) return Log;

            try
            {
                Log = new TrafficLog();

                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(Path);
                
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Replace('"', ' ');
                    string[] rawnewinfo = line.Split('\t');
                    if (rawnewinfo.Length != 3) continue;

                    SingleEspaTrafficInfo newinfo = new SingleEspaTrafficInfo();

                    // DateTime
                    try
                    {
                        newinfo.TrafficDate = DateTime.Parse(rawnewinfo[0]);
                    }
                    catch (Exception)
                    {
                        newinfo.TrafficDate = DateTime.Now;
                    }
                   
                    // Direction
                    if (rawnewinfo[1].Equals("Send"))
                    {
                        newinfo.Direction = eTrafficDirection.Send;
                    }
                    else
                    {
                        newinfo.Direction = eTrafficDirection.Receive;
                    }

                    // Content
                    newinfo.TrafficContent = rawnewinfo[2];

                    // add the TrafficInfo to the log
                    Log.Add(newinfo);
                }

                file.Close();
                file.Dispose();
            }
            catch (Exception exc)
            {
                Log = null;
                Console.WriteLine("ERROR: LoadTrafficLogFromFile");
                Console.WriteLine(exc.Message);
            } 

            return Log;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrafficLog"></param>
        /// <param name="Path"></param>
        /// <param name="Overrride"></param>
        /// <returns></returns>
        public static bool SaveTrafficLogInFile(TrafficLog TrafficLog, String Path, bool Overrride = false)
        {
            if (TrafficLog == null) return false;
            if (TrafficLog.Count <= 0) return false;

            // is the file already existend but the user doesn't wants us to override it?
            if (Overrride == false && File.Exists(Path)) return false;

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Path);

                string singleline = "";
                foreach (var item in TrafficLog.TrafficList)
                {
                    singleline = String.Format("{0}\t{1}\t{2}", item.TrafficDate.ToString(), item.Direction.ToString(),item.TrafficContent);
                    file.WriteLine(singleline);
                }

                file.Close();
                file.Dispose();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: SaveTrafficLogInFile");
                Console.WriteLine(exc.Message);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Path"></param>
        /// <param name="Overrride"></param>
        /// <returns></returns>
        public static bool SaveEspaMessageInFile(SingleEspaMessage EspaMessage, String Path, bool Overrride = false)
        {
            if (EspaMessage == null) return false;
            if (EspaMessage.CountRecords <=0) return false;

            // is the file already existend but the user doesn't wants us to override it?
            if (Overrride == false && File.Exists(Path)) return false;

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Path);

                string singleline = "";
                foreach (var item in EspaMessage.Records)
                {
                    singleline = String.Format("\"{0}\",\"{1}\"",item.RecordID,item.RecordData);
                    file.WriteLine(singleline);
                }

                file.Close();
                file.Dispose();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: SaveEspaMessageInFile");
                Console.WriteLine(exc.Message);
                return false;
            }  
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SingleEspaMessage LoadEspaMessageFromFile(String Path)
        {
            SingleEspaMessage EspaMessage = null; 
            
            if (File.Exists(Path) == false) return EspaMessage;

            try
            {
                EspaMessage = new SingleEspaMessage();

                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(Path);
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Replace('"', ' ');
                    string[] rawrecorddata = line.Split(',');
                    if (rawrecorddata.Length != 2) continue;

                    SingleEspaRecord newrecord = new SingleEspaRecord();

                    newrecord.RecordID = Convert.ToInt32(rawrecorddata[0]);
                    newrecord.RecordData = rawrecorddata[1].Trim();

                    // TODO: legalyze the record!!!

                    // add the record
                    EspaMessage.AddRecord(newrecord);
                }

                file.Close();
                file.Dispose();
            }
            catch (Exception exc)
            {
                EspaMessage = null;
                Console.WriteLine("ERROR: LoadEspaMessageFromFile");
                Console.WriteLine(exc.Message);
            }  

            return EspaMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="maxPacketSize"></param>
        /// <returns></returns>
        public static int CalculateBCC(char []packet, int maxPacketSize)
        {
            int bcc = 0;
	        int csum = 0;
            int packetpointer = 0;

            if (packet == null || packet.Length == 0) return 0;
            if (Constants.SOH != packet[packetpointer++]) return 0;


            while (packet[packetpointer] != Constants.ETX)
            {
                csum ^= packet[packetpointer++];
                if (maxPacketSize-- <= 0) return 0;
            }

            csum ^= packet[packetpointer++];	/* add the ETX */
            bcc = (csum & 0x7f);

	        return bcc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="maxPacketSize"></param>
        /// <returns></returns>
        public static int CheckBCC(char[] packet, int maxPacketSize)
        {
            int res = 0;
            int packetpointer = 0;

            if (Constants.SOH != packet[packetpointer++]) return 0;

            while (packet[packetpointer] != Constants.ETX)
            {
                res ^= packet[packetpointer++];
                if (maxPacketSize-- <= 0) return 0;
            }

            res ^= packet[packetpointer++];	/* add the ETX */
            res ^= packet[packetpointer++];	/* add the BCC */
	        return ((res & 0x7f) == 0) ? 1 : 0;
        }
    }
}
