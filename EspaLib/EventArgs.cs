using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;

namespace EspaLib
{

    #region EspaDataReceivedArgs
    public delegate void EspaDataReceivedHandler(object sender, EspaDataReceivedArgs e);
    public class EspaDataReceivedArgs : EventArgs
    {
        private byte[] receiveddata;
        private int datalength;
        private DateTime date;

        public byte[] ReceivedData
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
        public EspaDataReceivedArgs(byte[] data, int datalength, DateTime date)
        {
            ReceivedData = data;
            DataLength = datalength;
            Date = date;
        }
    }
    #endregion

    #region SerialDataReceivedArgs
    public delegate void SerialDataReceivedHandler(object sender, SerialDataReceivedArgs e);
    public class SerialDataReceivedArgs : EventArgs
    {
        private byte[] receiveddata;
        private int datalength;
        private DateTime date;

        public byte[] ReceivedData
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
        public SerialDataReceivedArgs(byte[] data, int datalength, DateTime date)
        {
            ReceivedData = data;
            DataLength = datalength;
            Date = date;
        }
    }
    #endregion

    #region UpdateStatusCommunicationLineArgs
    public delegate void UpdateStatusCommunicationLineHandler(object sender, UpdateStatusCommunicationLineArgs e);
    public class UpdateStatusCommunicationLineArgs : EventArgs
    {
        private EspaDeviceStatus status;

        public EspaDeviceStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="date"></param>
        public UpdateStatusCommunicationLineArgs(EspaDeviceStatus devstatus)
        {
            Status = devstatus;
        }
    }
    #endregion

    #region UpdateTrafficLogArgs
    public delegate void UpdateTrafficLogHandler(object sender, UpdateTrafficLogArgs e);
    public class UpdateTrafficLogArgs : EventArgs
    {
        private SingleEspaTrafficInfo logdata;

        public SingleEspaTrafficInfo LogData
        {
            get { return logdata; }
            set { logdata = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="date"></param>
        public UpdateTrafficLogArgs(SingleEspaTrafficInfo logData)
        {
            LogData = logData;
        }
    }
    #endregion

}
