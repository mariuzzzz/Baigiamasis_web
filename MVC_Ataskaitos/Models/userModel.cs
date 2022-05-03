using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Ataskaitos.Models
{
    public class userModel
    {
        public int ID { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "This field is required")]
        public string user_name { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Not all fields filled")]
        public string password { get; set; }
        public string type { get; set; }
        public string errorMessage { get; set; }
    }
}