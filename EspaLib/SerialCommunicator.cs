using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace EspaLib
{
    public class SerialCommunicator
    {
        #region member variables
        private SerialPort communicationport;
        #endregion member variables

        #region events
        public event SerialDataReceivedHandler SerialDataReceivedEvent;
        #endregion events
    
        #region properties
        public SerialPort ComPort
        {
            get { return communicationport; }
            set { communicationport = value; }
        }
    
        public bool IsConnected
        {
            get {

                if (ComPort == null) return false;
                return ComPort.IsOpen;       
            }
        }
        #endregion properties

        #region constructor
        public SerialCommunicator()
        {
        
        }
        #endregion constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portname"></param>
        /// <param name="baudrate"></param>
        /// <param name="parity"></param>
        /// <param name="databits"></param>
        /// <returns></returns>
        public bool Connect(String portname, int baudrate, Parity parity, int databits)
        {
            try
            {
                ComPort = new SerialPort(portname, baudrate, parity, databits);
                ComPort.Open();
                ComPort.DiscardInBuffer();

                StartDataReceiver();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR Connect");
                Console.WriteLine(exc.Message);
                return false;
            }

            return ComPort.IsOpen;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            if (ComPort == null) return;

            try
            {
                if (ComPort.IsOpen == false) return;
                ComPort.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR Disconnect");
                Console.WriteLine(exc.Message);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartDataReceiver()
        {
            Console.WriteLine("StartDataReceiver started");
            Action serialReadAction = null;
            byte[] buffer = new byte[(int)eEspaConstants.SizeMasterResponseFiFo];
            
            serialReadAction = delegate
            {
                try
                {
                    ComPort.BaseStream.BeginRead(buffer, 0, buffer.Length, delegate(IAsyncResult ar)
                    {
                        try
                        {
                            int actualLength = ComPort.BaseStream.EndRead(ar);
                            byte[] received = new byte[actualLength];
                            Buffer.BlockCopy(buffer, 0, received, 0, actualLength);

                            Raise_SerialDataReceivedEvent(new SerialDataReceivedArgs(received, actualLength, DateTime.Now));
                        }
                        catch (IOException exc)
                        {
                            Console.WriteLine("StartDataReceiver:serialReadAction ERROR:{0}", exc.Message);
                        }
                        catch (InvalidOperationException exc)
                        {
                            Console.WriteLine("StartDataReceiver:serialReadAction ERROR:{0}", exc.Message);
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine("StartDataReceiver:serialReadAction ERROR:{0}", exc.Message);
                        }

                        serialReadAction();
                    }, null);

                }
                catch (Exception exc)
                {
                    Console.WriteLine("StartDataReceiver:serialReadAction ERROR:{0}", exc.Message);
                }
            };

            serialReadAction();

            Console.WriteLine("StartDataReceiver ended");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        public bool SendData(byte[] data, int count)
        {
            if (ComPort == null) return false;
            if (ComPort.IsOpen == false) return false;

            try
            {
                ComPort.Write(data, 0, count);                
            }
            catch (IOException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR SendData(char[] data,int count)");
                Console.WriteLine(exc.Message);
            }
            finally
            {

            }

            return true;
        }

        #region Event handler methods

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Fires the SerialDataReceivedEvent event.
        /// </summary>
        /// <param name="e"></param>
        public void Raise_SerialDataReceivedEvent(SerialDataReceivedArgs e)
        {
            if (SerialDataReceivedEvent != null)
            {
                SerialDataReceivedEvent(this, e);
            }
        }
        #endregion
    }
}
