using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

namespace TriviaC
{
    class Database
    {
        private static MySqlConnection conn = new MySqlConnection("server=localhost;uid=root;pwd=;database=triviac;SslMode=None");
        private static MySqlCommand cmd = new MySqlCommand();
        private static string sql = "select * from `multiquestion`";
        private static List<Question> questions;
        public static void writeData()
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
                //this is like mysqli_query does the same thing
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //this is reading the data from the created sql 
                MySqlDataReader ms = cmd.ExecuteReader();
                //ms.Read() it acts like while($res = mysqli_fetch_assoc(cmd))
                while(ms.Read())
                {
                    Debug.WriteLine(ms.GetString("question"));
                    Debug.WriteLine(ms.GetString("ansa"));
                    Debug.WriteLine(ms.GetString("ansb"));
                    Debug.WriteLine(ms.GetString("ansc"));
                    Debug.WriteLine(ms.GetString("corrans"));
                }

            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
            }
            
        }
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
