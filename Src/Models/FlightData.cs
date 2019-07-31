using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace Ajax_Minimal.Models
{
    public class FlightData
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Rudder { get; set; }
        public double Throttle { get; set; }
        public string File { get; set; }


        public override string ToString()
        {
            string json = "{\"Lat\":" + Lat + ",\"Lon\":" + Lon + ",\"Rudder\":" + Rudder + ",\"Throttle\":" + Throttle + "}";
            return json + "\r\n";
            //string json = new JavaScriptSerializer().Serialize(this);
            //return json;
        }


        // singleton
        private static FlightData m_Instance = null;
        public static FlightData Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new FlightData();
                }
                return m_Instance;
            }
        }
    }
}