using Ajax_Minimal.Models;
using Ajax_Minimal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.IO;


namespace Ajax_Minimal.Controllers
{
    public class FirstController : Controller
    {
        public ActionResult Index()
        {
            Session["Lat"] = 50;
            Session["Lon"] = 50;
            return View();
        }


        [HttpGet]
        public ActionResult Display(string ip, int port)
        {
            IPAddress ipa;
            if (!IPAddress.TryParse(ip, out ipa))
            {
                return RedirectToAction("DisplayAnime", new RouteValueDictionary(new { controller = "First", action = "DisplayAnime", file = ip, rate = port })); ;
            }

            Connection.Instance.Connect(ip, port);

            Session["lat"] = PercConverter.Instance.YToPerc(Connection.Instance.GetLat());
            Session["lon"] = PercConverter.Instance.XToPerc(Connection.Instance.GetLon());

            Connection.Instance.Close();

            return View();
        }


        [HttpGet]
        public ActionResult DisplayRate(string ip, int port, int rate)
        {
            ViewBag.rate = rate;
            Connection.Instance.Connect(ip, port);

            double lon = Connection.Instance.GetLon();
            double lat = Connection.Instance.GetLat();
            Session["lon"] = PercConverter.Instance.XToPerc(lon);
            Session["lat"] = PercConverter.Instance.YToPerc(lat);

            return View();
        }


        [HttpGet]
        public ActionResult DisplayAnime(string file, int rate)
        {
            FlightData.Instance.File = file;
            ViewBag.rate = rate;

            return View();
        }
        

        [HttpGet]
        public ActionResult Save(string ip, int port, int rate, int time, string file)
        {
            ViewBag.rate = rate;
            ViewBag.time = time;
            Connection.Instance.Connect(ip, port);

            double lon = Connection.Instance.GetLon();
            double lat = Connection.Instance.GetLat();
            Session["lon"] = PercConverter.Instance.XToPerc(lon);
            Session["lat"] = PercConverter.Instance.YToPerc(lat);

            FlightData.Instance.Lon = lon;
            FlightData.Instance.Lat = lat;
            FlightData.Instance.Rudder = Connection.Instance.GetRudder();
            FlightData.Instance.Throttle = Connection.Instance.GetThrottle();
            FlightData.Instance.File = file;

            return View();
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";

        [HttpPost]
        public void DataSave()
        {
            string path = HttpContext.Server.MapPath(String.Format(SCENARIO_FILE, FlightData.Instance.File));
            DBHandler.Instance.Save(path, FlightData.Instance.ToString());
        }


        [HttpPost]
        public string DataLoad()
        {
            string path = HttpContext.Server.MapPath(String.Format(SCENARIO_FILE, FlightData.Instance.File));
            return DBHandler.Instance.Load(path);
        }
       

        [HttpPost]
        public string GetLatLon()
        {
            double lon = PercConverter.Instance.XToPerc(Connection.Instance.GetLon());
            double lat = PercConverter.Instance.YToPerc(Connection.Instance.GetLat());

            FlightData.Instance.Lon = lon;
            FlightData.Instance.Lat = lat;
            FlightData.Instance.Rudder = Connection.Instance.GetRudder();
            FlightData.Instance.Throttle = Connection.Instance.GetThrottle();

            return lon + "," + lat;
        }
    }
}