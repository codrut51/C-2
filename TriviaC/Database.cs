using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private int randQuestion;
        private int randFill;
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
            try
            {
                MessageDialog md = new MessageDialog(s);
                await md.ShowAsync();
            }catch(Exception)
            {

            }
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
        /// updates player to the database
        /// </summary>
        /// <param name="p">the player to be added</param>

        public async void updatePlayer(Player p)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/updatePlayer.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("username", p.getName().ToString()),
                    new KeyValuePair<string, string>("score", p.getScore().ToString())
                });

                HttpResponseMessage req = await conn.PostAsync("http://localhost:80/updatePlayer.php", formContent);
                String content = await req.Content.ReadAsStringAsync();
            }
            catch (Exception)
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
                randQuestion = r.Next((id-5) >= 5 ? (id - 5) : 1) + 1;
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/questions.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", randQuestion.ToString())
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
                randFill = r.Next((id - 5) >= 5 ? (id - 5) : 1) + 1;
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/fillquestions.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("number", randFill.ToString())
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
    /*
        /// <summary>
        /// sets the same questions for the players in the multiple player game
        /// </summary>
        public async void setMultiPlayerQuestions()
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/questionsID.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", randQuestion.ToString()),
                    new KeyValuePair<string, string>("id1", randFill.ToString())
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/questionsID.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();

            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
            }
        } 
        */
        /// <summary>
        /// sets the same questions for the players in the multiple player game
        /// </summary>
        public async Task<string> getMultiPlayerQuestion(int s)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/getMultiPlayerQuestion.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", randQuestion.ToString()),
                    new KeyValuePair<string, string>("id1", randFill.ToString()),
                    new KeyValuePair<string, string>("multiID",s.ToString())
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/getMultiPlayerQuestion.php", formContent).Result;
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
        /// checks the avabiliti of an id passed from the game class
        /// </summary>
        /// <param name="id"> the id passed from the game class</param>
        /// <param name="name"> the player's name passed from the game class</param>
        /// <param name="questionID"> the questions for the multiplayer game </param>
        /// <returns></returns>
        public async Task<bool> checkAvailable(int id,string name,int questionID)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/multiPlayerAvailability.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", id.ToString()),
                    new KeyValuePair<string, string>("name", name),
                    new KeyValuePair<string, string>("questionID", questionID.ToString())

                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/multiPlayerAvailability.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return bool.Parse(content);
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return false;
            }
        }
        /// <summary>
        /// checks if the second player is in the game by id
        /// </summary>
        /// <param name="id">the id of the game </param>
        /// <returns>true or false</returns>
        public async Task<bool> isSecond(int id)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/isPlayer.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", id.ToString())

                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/isPlayer.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return bool.Parse(content);
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return false;
            }
        }
        /// <summary>
        /// adds the second player to the game 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> addSecond(int id,string name)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/isPlayer.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", id.ToString()),
                    new KeyValuePair<string, string>("name", id.ToString())
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/isPlayer.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return bool.Parse(content);
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return false;
            }
        }
        /// <summary>
        /// checks if the name chousen is unique 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> isUnique(string name)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/isUnique.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("name", name)
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/isUnique.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return bool.Parse(content);
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return false;
            }
        } 
        /// <summary>
        /// checks if the name chousen is unique 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> joinGame(Player p, int id)
        {
            try
            {
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/joinGame.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("username", p.getName()),
                    new KeyValuePair<string, string>("id",id.ToString())
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/joinGame.php", formContent).Result;
                string content = await req.Content.ReadAsStringAsync();
                return bool.Parse(content);
            }
            catch (Exception)
            {
                display("Cannot initiate a connection to the database please try again later");
                return false;
            }
        }

        /// <summary>
        /// checks if the name chousen is unique 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> MultiPlayerQuestions(int id)
        {
            try
            {
                //display(id.ToString());
                conn = new HttpClient();
                conn.BaseAddress = new Uri("http://localhost:80/MultiPlayerQuestions.php");
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id",id.ToString())
                });

                HttpResponseMessage req = conn.PostAsync("http://localhost:80/MultiPlayerQuestions.php", formContent).Result;
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
