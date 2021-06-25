using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class SingleEspaTrafficInfo
    {
        #region member data
        private eTrafficDirection direction;
        private DateTime trafficdate;
        private String trafficcontent;
        #endregion

        #region propertie s      
        public eTrafficDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public DateTime TrafficDate
        {
            get { return trafficdate; }
            set { trafficdate = value; }
        }

        public String TrafficContent
        {
            get { return trafficcontent; }
            set { trafficcontent = value; }
        }      
        #endregion

        public SingleEspaTrafficInfo()
        {
            SetToDefault();
        }

        public void SetToDefault()
        {
            Direction = eTrafficDirection.Receive;
            TrafficDate = DateTime.Now;
            TrafficContent = "";
        }
    }
}
