using MySql.Data.MySqlClient;
using System.Data;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public static string room;
        public static string returnroom()
        {
            return room;
        }
        //MyDataBase db = new MyDataBase();
        string connString = "server=127.0.0.1;port=3306;user id=root;password=q2611687q;database=test;charset=utf8;";
        MySqlConnection conn = new MySqlConnection();
        public ActionResult Index()
        {

            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sql = @" SELECT `name`, `live` FROM `room`";

            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            ViewBag.DT = dt;
            
            return View();
        }

        public ActionResult ChatView()
        {
            string a = Request.Form["a"].ToString();
            int i = int.Parse(a);
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sql = @" SELECT `name`, `live` FROM `room`";
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dt);
            room = dt.Rows[i]["name"].ToString();

            string sql2 = @" SELECT `username`, `msg`, `time` FROM `" +"r" + room + "`";
            DataTable dt2 = new DataTable();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(sql2, conn);
            adapter2.Fill(dt2);

            ViewBag.DT = dt2;
            ViewBag.roomname = room;

            return View();
        }
        
        //[HttpPost]
        public ActionResult CreatRoom(FormCollection post)
        {
            string roomname = post["Croomname"];
            string password1 = post["password1"];
            string password2 = post["password2"];

            if (string.IsNullOrWhiteSpace(password1) || password1 != password2)
            {
                return View();
            }
            else
            {
                conn.ConnectionString = connString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                string sql = @"INSERT INTO `room` (`name`, `password`, `live`) VALUES
                           ('" + roomname + "', '" + password1 + "', '" + 0 + "');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int index = cmd.ExecuteNonQuery();
                bool success = false;
                if (index > 0)
                    success = true;
                else
                    success = false;
                ViewBag.Success = success;

                string sql2 = @"CREATE TABLE "+ "r" + roomname + " (username varchar(50) NOT NULL,msg varchar(255) NOT NULL,time varchar(20) NOT NULL);";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("Index");
                return new EmptyResult();
            }
        }
        /*
        public ActionResult Transcripts(string id, string name, int score)
        {
            Student data = new Student(id, name, score);
            return View(data);
        }*/
        
    }
}