using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;

namespace EspaCommunicator
{
    public class SerialComVars
    {
        public int Baudrate;
        public System.IO.Ports.Parity Parity;
        public int SerialDataBits;
        public System.IO.Ports.StopBits StopBits;
    }

    public class SettingsVars
    {
        public SerialComVars SerialComSettings;

        public SettingsVars()
        {
            SerialComSettings = new SerialComVars();
        }
    }
}
