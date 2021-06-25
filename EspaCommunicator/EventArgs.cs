using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    public delegate void SerialDataReceivedHandler(object sender, SerialDataReceivedArgs e);

    public class SerialDataReceivedArgs : EventArgs
    {

        private char[] receiveddata;
        private int datalength;
        private DateTime date;

        public char[] ReceivedData
        {
            get { return receiveddata; }
            set { receiveddata = value; }
        }

        public int DataLength
        {
            get { return datalength; }
            set { datalength = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="date"></param>
        public SerialDataReceivedArgs(char[] data,int datalength, DateTime date)
        {
            ReceivedData = data;
            DataLength = datalength;
            Date = date;
        }
    }

}
