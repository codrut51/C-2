using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;
using System.Diagnostics;

namespace TriviaC
{
    class Database
    {
        private static MySqlConnection conn = new MySqlConnection("server=localhost;uid=root;pwd=;database=triviac;SslMode=None");
        private static MySqlCommand cmd = new MySqlCommand();
        private static string sql;
        private static List<Question> multiq = new List<Question>();
        private static List<Question> fillinq = new List<Question>();
        private static List<Player> hip = new List<Player>();
        public Database()
        {
            try
            {
                ///This is here just because the EncodingProvider is throwing an exception for no reason
                System.Text.EncodingProvider ppp;
                ppp = System.Text.CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(ppp);
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                conn.Close();
                getMultiQuestions();
                getFillInQuestions();
                getHighPlayer();
            }
            catch(Exception ex)
            {
                Display("Something went wrong with the connection: " + ex.ToString());
            }
        }
        /// <summary>
        /// function used to read all the multiple questions from the database
        /// </summary>
        private static void getMultiQuestions()
        {
            try
            {
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `multiquestion` where difficulty = 1 order by RAND() limit 2";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                MySqlDataReader ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while (ms.Read())
                {
                    Question q = new Question();
                    q.isMulty = true;
                    q.description = ms.GetString("question");
                    q.ansa = ms.GetString("ansa");
                    q.ansb = ms.GetString("ansb");
                    q.ansc = ms.GetString("ansc");
                    q.corrans = ms.GetString("corrans");
                    q.difficulty = int.Parse(ms.GetString("difficulty"));
                    multiq.Add(q);
                }
                conn.Close();
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `multiquestion` where difficulty = 2 order by RAND() limit 2";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while (ms.Read())
                {
                    Question q = new Question();
                    q.isMulty = true;
                    q.description = ms.GetString("question");
                    q.ansa = ms.GetString("ansa");
                    q.ansb = ms.GetString("ansb");
                    q.ansc = ms.GetString("ansc");
                    q.corrans = ms.GetString("corrans");
                    q.difficulty = int.Parse(ms.GetString("difficulty"));
                    multiq.Add(q);
                }
                conn.Close();
                conn.Close();
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `multiquestion` where difficulty = 3 order by RAND() limit 1";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while (ms.Read())
                {
                    Question q = new Question();
                    q.isMulty = true;
                    q.description = ms.GetString("question");
                    q.ansa = ms.GetString("ansa");
                    q.ansb = ms.GetString("ansb");
                    q.ansc = ms.GetString("ansc");
                    q.corrans = ms.GetString("corrans");
                    q.difficulty = int.Parse(ms.GetString("difficulty"));
                    multiq.Add(q);
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// function used to read all the multiple questions from the database
        /// </summary>
        private static void getFillInQuestions()
        {
            try
            {
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `fillquestion` where difficulty = '1' order by RAND() limit 2";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                MySqlDataReader ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while (ms.Read())
                {
                    Question q = new Question();
                    q.isMulty = false;
                    q.description = ms.GetString("question");
                    q.complitionQuestion = ms.GetString("code");
                    q.complitionAnswer = ms.GetString("corrans");
                    q.animation = ms.GetString("animation");
                    fillinq.Add(q);
                }
                conn.Close();
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `fillquestion` where difficulty = 2 order by RAND() limit 2";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while (ms.Read())
                {
                    Question q = new Question();
                    q.isMulty = false;
                    q.description = ms.GetString("question");
                    q.complitionQuestion = ms.GetString("code");
                    q.complitionAnswer = ms.GetString("corrans");
                    q.animation = ms.GetString("animation");
                    fillinq.Add(q);
                }
                conn.Close();
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `fillquestion` where difficulty = 3 order by RAND() limit 1";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while (ms.Read())
                {
                    Question q = new Question();
                    q.isMulty = false;
                    q.description = ms.GetString("question");
                    q.complitionQuestion = ms.GetString("code");
                    q.complitionAnswer = ms.GetString("corrans");
                    q.animation = ms.GetString("animation");
                    fillinq.Add(q);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// function used to read all the multiple questions from the database
        /// </summary>
        private static void getHighPlayer()
        {
            try
            {
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `user` order by hiscore desc, id desc limit 5";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                MySqlDataReader ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while (ms.Read())
                {
                    Player p = new Player(ms.GetString("name"), int.Parse(ms.GetString("hiscore")));
                    hip.Add(p);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// Creating an user 
        /// </summary>
        /// <returns></returns>
        public bool CreatePlayer(String name)
        {
            try
            {
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "select * from `user` where name = '" + name + "'";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                MySqlDataReader ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                if (ms.Read())
                {
                    Display("The name already exists please try again with another name");
                    conn.Close();
                    return false;
                }
                else
                {
                    conn.Close();
                    cmd = new MySqlCommand();
                    //this will open a connection to the connection string imputed into MySqlConnection
                    conn.Open();
                    //this will copy the connection for using it in different commands
                    cmd.Connection = conn;
                    sql = "insert into `user` (`name`) values ('" + name + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public void UpdatePlayer(Player p)
        {
            try
            {
                cmd = new MySqlCommand();
                //this will open a connection to the connection string imputed into MySqlConnection
                conn.Open();
                //this will copy the connection for using it in different commands
                cmd.Connection = conn;
                sql = "UPDATE `user` SET `name`='" + p.getName() + "',`hiscore`='" + p.getScore() + "' WHERE name = '" + p.getName() + "'";
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        //gets a list of the multiple coice question
        public List<Question> GetMultiQuestion()
        {
            return multiq;
        }
        //gets a list of the fill in questions
        public List<Question> GetFillInQuestion()
        {
            return fillinq;
        }
        //gets a list of the firt 5 players
        public List<Player> GetHiPlayer()
        {
            return hip;
        }
        /// <summary>
        /// Function used to display on message popup 
        /// </summary>
        /// <param name="message"> the message to be displayed </param>
        private static async void Display(string message)
        {
            try
            {
                MessageDialog ms = new MessageDialog(message);
                await ms.ShowAsync();
            }
            catch (Exception) { }
        }
    }
}
