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
                    new KeyValuePair<string, string>("username", p.getName().ToString())
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
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/questions.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", "")
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
    }
}
