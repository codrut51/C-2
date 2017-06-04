using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Core;
using SQLite.Net;
using SQLitePCL;
using Windows.UI.Popups;
using System.IO;
using Windows.Storage;
using System.Data.Common;
using Windows.Data.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TriviaC
{
    class Database
    {
        private HttpClient conn;
        /// <summary>
        /// constructor 
        /// </summary>
        public Database()
        {
            conn = new HttpClient();
        }
        /// <summary>
        /// displays message
        /// </summary>
        /// <param name="s"></param>
        public async void display(string s)
        {
            MessageDialog md = new MessageDialog(s);
            await md.ShowAsync();
        }
        /// <summary>
        /// adds player to the database
        /// </summary>
        /// <param name="p">the player to be added</param>

        public async void addPlayer(Player p)
        {
            try
           {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/user.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("username", p.getName().ToString()),
                    new KeyValuePair<string, string>("score", p.getScore().ToString())
                });

                HttpResponseMessage req = await conn.PostAsync("http://localhost:80/user.php", formContent);
                String content = await req.Content.ReadAsStringAsync();
            }
            catch(Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
            }
        }

        /// <summary>
        /// gets the questions from the database
        /// </summary>
        /// <returns> returns the questions </returns>
        public async Task<string> getQuestions()
        {
            try
            {
                int id = getNoQuestion("noQuestions.php").Result;
                Random r = new Random();
                int rand = r.Next((id-5) >= 5 ? (id - 5) : 1) + 1;
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/questions.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", rand.ToString())
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/questions.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return null;
            }
        }
        /// <summary>
        /// gets the questions from the database
        /// </summary>
        /// <returns> returns the questions </returns>
        public async Task<string> getFillQuestions()
        {
            try
            {
                int id = getNoQuestion("noFillQuestions.php").Result;
                Random r = new Random();
                int rand = r.Next((id - 5) >= 5 ? (id - 5) : 1) + 1;
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/fillquestions.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("number", rand.ToString())
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/fillquestions.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return null;
            }
        }
        /// <summary>
        /// gets the highest score and name from the database
        /// </summary>
        /// <returns> returns the questions </returns>
        public async Task<string> getHighest()
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/hiplayer.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", "")
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/hiplayer.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return null;
            }
        }
        /// <summary>
        /// gets the highest score and name from the database
        /// </summary>
        /// <returns> returns the questions </returns>
        public async Task<int> getNoQuestion(string phpfile)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/" + phpfile);
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", "")
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/" + phpfile, formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return int.Parse(content);
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return 0;
            }
        }


    }
}
