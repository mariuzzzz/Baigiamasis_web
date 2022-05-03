using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Ataskaitos.Models
{
    public class channelModel
    {
        public int Id { get; set; }
        public string channel_ID { get; set; }
        public string show_ID { get; set; }
        public DateTimeOffset time_start { get; set; }
        public DateTimeOffset time_stop { get; set; }
        public string title { get; set; }
        public string delta_start { get; set; }
        public string delta_stop { get; set; }
        public string moder_status_start { get; set; }
        public string moder_status_stop { get; set; }
        public string channel_name { get; set; }
    }
}