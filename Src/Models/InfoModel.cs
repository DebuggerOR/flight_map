using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ajax_Minimal.Models
{
    public class InfoModel
    {
        public FlightData FlightData { get; private set; }
        public string ip { get; set; }
        public int port { get; set; }
        public int time { get; set; }
        public int rate { get; set; }

        public InfoModel()
        {
            this.FlightData = new FlightData();
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";



        // singleton
        private static InfoModel s_instace = null;

        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                }
                return s_instace;
            }
        }
    }
}