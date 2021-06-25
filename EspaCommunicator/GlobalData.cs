using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    public class GlobalData
    {
        public SingleEspaMessage ActiveEspaMessage = new SingleEspaMessage();
        public TrafficLog ActiveTrafficLogData = new TrafficLog();
        public SerialCommunicator SerialInterface = new SerialCommunicator();

        public String DisplayApplicationName;
        public String BuildDeveloper;
        public String CopyrightInformation;
        public String BuildTime;
        public String BuildType;
        public String BuildVersion;

        public GlobalData()
        {

        }
    }

}
