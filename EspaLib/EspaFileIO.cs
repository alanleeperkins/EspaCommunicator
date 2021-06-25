using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class EspaFileIO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static EspaTrafficLog LoadTrafficLogFromFile(String Path)
        {
            EspaTrafficLog Log = null;
            if (File.Exists(Path) == false) return Log;

            try
            {
                Log = new EspaTrafficLog();

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
        public static bool SaveTrafficLogInFile(EspaTrafficLog TrafficLog, String Path, bool Overrride = false)
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
                    singleline = String.Format("{0}\t{1}\t{2}", item.TrafficDate.ToString(), item.Direction.ToString(), item.TrafficContent);
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
        /// <param name="EspaDataBlock"></param>
        /// <param name="Path"></param>
        /// <param name="Overrride"></param>
        /// <returns></returns>
        public static bool SaveEspaDataBlockInFile(SingleEspaDataBlock EspaDataBlock, String Path, bool Overrride = false)
        {
            if (EspaDataBlock == null) return false;
            if (EspaDataBlock.CountRecords <= 0) return false;

            // is the file already existend but the user doesn't wants us to override it?
            if (Overrride == false && File.Exists(Path)) return false;

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Path);

                string singleline = "";
                foreach (var item in EspaDataBlock.Records)
                {
                    singleline = String.Format("\"{0}\",\"{1}\"", item.RecordID, item.RecordData);
                    file.WriteLine(singleline);
                }

                file.Close();
                file.Dispose();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: SaveEspaDataBlockInFile");
                Console.WriteLine(exc.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SingleEspaDataBlock LoadEspaDataBlockFromFile(String Path)
        {
            SingleEspaDataBlock EspaDataBlock = null;

            if (File.Exists(Path) == false) return EspaDataBlock;

            try
            {
                EspaDataBlock = new SingleEspaDataBlock();
                EspaDataBlock.DataBlockType = eEspaDataBlockType.Records;

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
                    EspaDataBlock.AddRecord(newrecord);
                }

                file.Close();
                file.Dispose();
            }
            catch (Exception exc)
            {
                EspaDataBlock = null;
                Console.WriteLine("ERROR: LoadEspaDataBlockFromFile");
                Console.WriteLine(exc.Message);
            }

            return EspaDataBlock;
        }
    }
}
