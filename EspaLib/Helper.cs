using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class Helper
    {   
        /// <summary>
        /// returns the result of (lasttime - firsttime)
        /// </summary>
        /// <param name="firsttime"></param>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public static double GetTimeDiff(DateTime lasttime, DateTime firsttime)
        {       
            double seconds = 0.0;

            try
            {
                seconds = (lasttime - firsttime).TotalSeconds;
            }
            catch (Exception)
            {
                seconds = 0;
            }

            return seconds;
        }

        /// <summary>
        /// converts the real received data into a stream of symbolic signs (ASCII)
        /// </summary>
        /// <param name="rawespa"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string ConvIntoSymbAsciiCharStream(byte[] rawespa, int Length)
        {
            String symbolicasciistream = "";

            if (rawespa == null || rawespa.Length == 0) return symbolicasciistream;

            if (Length == -1) Length = rawespa.Length;

            foreach (var item in rawespa)
            {
                int itemnumeric = Convert.ToInt32(item);

                if (itemnumeric < Constants.AsciiTable.Length)
                {
                    symbolicasciistream += Constants.AsciiTable[itemnumeric];
                }
                else
                {
                    symbolicasciistream += String.Format("<{0:X}>", itemnumeric);
                }

                if (--Length <= 0) break;
            }
            return symbolicasciistream;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string GetFileName(string FilePath)
        {
            string[] subs = FilePath.Split('\\');
            if (subs.Length > 1)
            {
                return subs[subs.Length - 1];
            }
            else return subs[0];
        }
    }
}
