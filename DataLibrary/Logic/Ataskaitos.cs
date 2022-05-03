using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataLibrary.DataAccess;

namespace DataLibrary.Logic
{
    public static class Ataskaitos
    {
        public static List<channelModel> LoadChannels()
        {
            string sql = "select * from show_details";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
    }
}
