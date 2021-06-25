using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;

namespace EspaLib
{
    public class EspaTrafficLog
    {
        private List<SingleEspaTrafficInfo> trafficlist = new List<SingleEspaTrafficInfo>();

        public List<SingleEspaTrafficInfo> TrafficList
        {
            get { return trafficlist; }
            set { trafficlist = value; }
        }

        public int Count
        {
            get { return trafficlist.Count; }
        }
        
        /// <summary>
        /// clear the traffic log
        /// </summary>
        public void Clear()
        {
            trafficlist.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrafficInfo"></param>
        /// <returns></returns>
        public bool Add(SingleEspaTrafficInfo TrafficInfo)
        {
            if (trafficlist == null) return false;
            if (TrafficInfo == null) return false;

            trafficlist.Add(TrafficInfo);

            return true;
        }

    }
}
