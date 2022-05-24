using ClassDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ClassDataLibrary.DataAccess;

namespace ClassDataLibrary.Logic
{
    public static class Ataskaitos
    {
        public static List<channelModel> LoadChannels()
        {
            string sql = "select * from show_details";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannels()
        {
            string sql = "select * from show_details where moder_status_start != '0' OR moder_status_stop != '0'";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<RDSmodel> LoadRDS()
        {
            string sql = "select * from rds_details";
            return SqlDataAccess.LoadData<RDSmodel>(sql);
        }
        public static List<channelModel> LoadChannelsTimeBothForClient(string start, string stop, string id)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_start <= '" + stop + " 23:59:59.00' AND channel_ID = '" + id + "'";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannelsTimeBothForClient(string start, string stop, string id)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_start <= '" + stop + " 23:59:59.00' AND channel_ID = '" + id + "' AND (moder_status_start != '0' OR moder_status_stop != '0')";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadChannelsTimeBoth(string start, string stop)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_start <= '" + stop + " 23:59:59.00'";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannelsTimeBoth(string start, string stop)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_start <= '" + stop + " 23:59:59.00' AND (moder_status_start != '0' OR moder_status_stop != '0')";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadChannelsTimeFirstForClient(string start, string id)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_stop <= GETDATE() AND channel_ID = '" + id + "'";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannelsTimeFirstForClient(string start, string id)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_stop <= GETDATE() AND channel_ID = '" + id + "' AND (moder_status_start != '0' OR moder_status_stop != '0')";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadChannelsTimeFirst(string start)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_stop <= GETDATE()";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannelsTimeFirst(string start)
        {
            string sql = "select * from show_details where time_start >= '" + start + "' AND time_stop <= GETDATE() AND (moder_status_start != '0' OR moder_status_stop != '0')";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadChannelsTimeSecondForClient(string stop, string id)
        {
            string sql = "select * from show_details where time_start <= '" + stop + " 23:59:59.00'AND channel_ID = '" + id + "'";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannelsTimeSecondForClient(string stop, string id)
        {
            string sql = "select * from show_details where time_start <= '" + stop + " 23:59:59.00'AND channel_ID = '" + id + "' AND (moder_status_start != '0' OR moder_status_stop != '0')";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadChannelsTimeSecond(string stop)
        {
            string sql = "select * from show_details where time_start <= '" + stop + " 23:59:59.00'";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannelsTimeSecond(string stop)
        {
            string sql = "select * from show_details where time_start <= '" + stop + " 23:59:59.00' AND (moder_status_start != '0' OR moder_status_stop != '0')";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<userModel> LoadUsers(string login, string password)
        {
            string sql = "select * from user_details where login = '" + login + "' AND password = '" + password + "'";
            return SqlDataAccess.LoadData<userModel>(sql);
        }
        public static List<clientModel> LoadClientChannels(string client)
        {
            string sql = "select channel_id from client_details where client_name = '" + client + "'";
            return SqlDataAccess.LoadData<clientModel>(sql);
        }
        public static List<channelModel> LoadChannelsForClient(string id)
        {
            string sql = "select * from show_details where channel_ID = '" + id + "'";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }
        public static List<channelModel> LoadErrorChannelsForClient(string id)
        {
            string sql = "select * from show_details where channel_ID = '" + id + "' AND (moder_status_start != '0' OR moder_status_stop != '0')";
            return SqlDataAccess.LoadData<channelModel>(sql);
        }

    }
}
