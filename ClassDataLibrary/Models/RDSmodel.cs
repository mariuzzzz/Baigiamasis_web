using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDataLibrary.Models
{
    public class RDSmodel
    {
        public string sender_id { get; set; }
        public string sender_name { get; set; }
        public string title { get; set; }
        public string artist { get; set; }
        public string rdsTimeStampCreated { get; set; }
        public string channel_id { get; set; }
        public string show_title { get; set; }
    }
}
