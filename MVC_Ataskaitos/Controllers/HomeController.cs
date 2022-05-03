using ClassDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ClassDataLibrary.Logic.Ataskaitos;
using System.Globalization;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace MVC_Ataskaitos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewData(string deltaText, string DateStart, string DateStop)
        {
            ViewBag.Message = "Channel list";
            List<userModel> data = new List<userModel>();
            var type = Session["type"];
            List<channelModel> channels = new List<channelModel>();
            CultureInfo provider = CultureInfo.InvariantCulture;
            int deltaFromText = 0;
            if (Int32.TryParse(deltaText, out deltaFromText))
            {

                if (!string.IsNullOrEmpty(DateStart?.ToString()) && !string.IsNullOrEmpty(DateStop?.ToString()))
                    CheckDeltasWithDate(channels, deltaFromText, DateStart, DateStop, type.ToString());
                else if (!string.IsNullOrEmpty(DateStart?.ToString()) && string.IsNullOrEmpty(DateStop?.ToString()))
                    CheckDeltasWithOneDate(channels, deltaFromText, DateStart, type.ToString());
                else if (!string.IsNullOrEmpty(DateStop?.ToString()) && string.IsNullOrEmpty(DateStart?.ToString()))
                    CheckDeltasWithOneSecondDate(channels, deltaFromText, DateStop, type.ToString());
                else
                    CheckDeltas(channels, deltaFromText, type.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(DateStart?.ToString()) && !string.IsNullOrEmpty(DateStop?.ToString()))
                    CheckDeltasWithDate(channels, 1200, DateStart, DateStop, type.ToString());
                else if (!string.IsNullOrEmpty(DateStart?.ToString()) && string.IsNullOrEmpty(DateStop?.ToString()))
                    CheckDeltasWithOneDate(channels, 1200, DateStart, type.ToString());
                else if (!string.IsNullOrEmpty(DateStop?.ToString()) && string.IsNullOrEmpty(DateStart?.ToString()))
                    CheckDeltasWithOneSecondDate(channels, 1200, DateStop, type.ToString());
                else
                    CheckDeltas(channels, 1200, type.ToString());
                    
            }

            List<channelModel> sorted = channels.OrderBy(o => o.channel_ID).ThenBy(o => o.time_start).ToList();
            return View(sorted);
        }

        public ActionResult ViewRDSdata()
        {
            ViewBag.Message = "RDS list";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var type = Session["type"];
            List<RDSmodel> data = new List<RDSmodel>();
            if (type.ToString() == "admin")
            {
                data = LoadRDS();
            }
            
            List<RDSmodel> rds = new List<RDSmodel>();
            foreach (var row in data)
            {
                string dateString = row.rdsTimeStampCreated;
                string format = "yyyy-MM-dd HH:mm:ssff";
                DateTime rdsDate;
                provider = new CultureInfo("fr-FR");
                rdsDate = DateTime.ParseExact(dateString, format, provider);
                string seconds = DateTime.Now.Subtract(rdsDate).TotalSeconds.ToString();
                double time = Convert.ToDouble(seconds);
                double roundedTime = Math.Round(time, 2);

                if (roundedTime > 600 && string.IsNullOrEmpty(row.show_title))
                {
                    rds.Add(new RDSmodel
                    {
                        sender_id = row.sender_id,
                        sender_name = row.sender_name,
                        title = row.title,
                        artist = row.artist,
                        rdsTimeStampCreated = row.rdsTimeStampCreated,
                        show_title = row.show_title
                    });
                }
            }
            return View(rds);
        }
        public void CheckDeltas(List<channelModel> channels, int deltaFromText, string type )
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            
            var data = new List<channelModel>();
            var clientChannels = new List<clientModel>();
            if (type != null)
            {
                if (type.ToString() == "admin")
                {
                    data = LoadChannels();
                    AddToChannels(channels, deltaFromText, null, null, data);
                }
                else
                {
                    clientChannels = LoadClientChannels(type.ToString());
                    foreach(var r in clientChannels)
                    {
                        data = LoadChannelsForClient(r.channel_id);
                        AddToChannels(channels, deltaFromText, null, null, data); 
                    }
                }
               
            }
        }
        public void CheckDeltasWithDate(List<channelModel> channels, int deltaFromText, string dateStart, string dateStop, string type)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var data = new List<channelModel>();
            var clientChannels = new List<clientModel>();
            if (type != null)
            {
                if (type.ToString() == "admin")
                {
                    data = LoadChannelsTimeBoth(dateStart, dateStop);
                    AddToChannels(channels, deltaFromText, dateStart, dateStop, data);
                }
                else
                {
                    clientChannels = LoadClientChannels(type.ToString());
                    foreach (var r in clientChannels)
                    {
                        data = LoadChannelsTimeBothForClient(dateStart, dateStop, r.channel_id);
                        AddToChannels(channels, deltaFromText, dateStart, dateStop, data);
                    }
                }
            }
        }
        public void CheckDeltasWithOneDate(List<channelModel> channels, int deltaFromText, string dateStart, string type)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var data = new List<channelModel>();
            var clientChannels = new List<clientModel>();
            if (type != null)
            {
                if (type.ToString() == "admin")
                {
                    data = LoadChannelsTimeFirst(dateStart);
                    AddToChannels(channels, deltaFromText, dateStart, null, data);
                }
                else
                {
                    clientChannels = LoadClientChannels(type.ToString());
                    foreach (var r in clientChannels)
                    {
                        data = LoadChannelsTimeFirstForClient(dateStart, r.channel_id);
                        AddToChannels(channels, deltaFromText, dateStart, null, data);
                    }
                }
            }
        }
        public void CheckDeltasWithOneSecondDate(List<channelModel> channels, int deltaFromText, string dateStop, string type)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var data = new List<channelModel>();
            var clientChannels = new List<clientModel>();
            if (type != null)
            {
                if (type.ToString() == "admin")
                {
                    data = LoadChannelsTimeSecond(dateStop);
                    AddToChannels(channels, deltaFromText, null, dateStop, data);
                }
                else
                {
                    clientChannels = LoadClientChannels(type.ToString());
                    foreach (var r in clientChannels)
                    {
                        data = LoadChannelsTimeSecondForClient(dateStop, r.channel_id);
                        AddToChannels(channels, deltaFromText, null, dateStop, data);
                    }
                }
            }
        }
        public void AddToChannels(List<channelModel> channels, int deltaFromText, string dateStop, string dateStart, List<channelModel> data)
        {
            foreach (var row in data)
            {
                if ((!string.IsNullOrEmpty(row.delta_start)) && (!string.IsNullOrEmpty(row.delta_stop)))
                {
                    int val_start = Int32.Parse(row.delta_start);
                    int val_stop = Int32.Parse(row.delta_stop);
                    if (val_start > deltaFromText || val_start < -deltaFromText || val_stop > deltaFromText || val_stop < -deltaFromText)
                    {
                        string dateStringStart = row.time_start.ToString();
                        string dateStringStop = row.time_stop.ToString();
                        string format = "M/d/yyyy h:mm:ss tt zzz";
                        DateTime start;
                        DateTime stop;
                        start = DateTime.ParseExact(dateStringStart, format, CultureInfo.InvariantCulture);
                        stop = DateTime.ParseExact(dateStringStop, format, CultureInfo.InvariantCulture);
                        channels.Add(new channelModel
                        {
                            channel_ID = row.channel_ID,
                            channel_name = row.channel_name,
                            show_ID = row.show_ID,
                            time_start = start,
                            time_stop = stop,
                            title = row.title,
                            delta_start = row.delta_start,
                            delta_stop = row.delta_stop,
                            moder_status_start = row.moder_status_start,
                            moder_status_stop = row.moder_status_stop
                        });
                    }
                }

            }

        }
    }
}