using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
   public class channelModel
    {
        public int Id { get; set; }
        public string channel_ID { get; set; }
        public string show_ID { get; set; }
        public string time_start { get; set; }
        public string time_stop { get; set; }
        public string title { get; set; }
        public string delta_start { get; set; }
        public string delta_stop { get; set; }
        public string moder_status_start { get; set; }
        public string moder_status_stop { get; set; }
    }
}
