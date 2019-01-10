using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApplication1.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);

            string connString = "server=127.0.0.1;port=3306;user id=root;password=q2611687q;database=test;charset=utf8;";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sql = @"INSERT INTO `"+ "r" + Controllers.HomeController.returnroom() + "` (`username`, `msg`, `time`) VALUES('" + name + "', '" + message + "', '" + 0 + "');";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int index = cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}