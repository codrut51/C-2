using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaC
{
    class Game
    {
        private Player player;
        private Question[] questions;
        private Animation[] animation;
        private Player[] hiplayer;
        private Database db;
        private int pos = -1;
        private int posP = -1;
        /// <summary>
        /// Constructor
        /// </summary>
        public Game()
        {
            player = new Player();
            questions = new Question[10];
            animation = new Animation[3];
            hiplayer = new Player[5];
            db = new Database();
            initQuestions();
            initAnimation();
            initFillQuestions();
            initHiPlayer();
        }/// <summary>
         /// Constructor
         /// </summary>
        public Game(Player p)
        {
            player = p;
            questions = new Question[10];
            animation = new Animation[3];
            hiplayer = new Player[5];
            db = new Database();
            initQuestions();
            initAnimation();
            initFillQuestions();
            initHiPlayer();
        }
        /// <summary>
        /// adds player to the database by passing it to the db instance
        /// </summary>
        /// <param name="p"> the player to be passed to the database in order to be processed</param>
        public void addPlayer()
        {
            db.addPlayer(player);
        }
        /// <summary>
        /// returns the player
        /// </summary>

        public Player Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
        /// <summary>
        /// gets the next question
        /// </summary>
        /// <returns></returns>
        public Question getNextQuesiton()
        {
            
            pos++;
            return pos < 10 ? questions[pos] : null;
        }
        /// <summary>
        /// saves the questions into an array of questions from the database
        /// </summary>
        private void initQuestions()
        {
            string s = db.getQuestions().Result;
            string[] stringSeparators = new string[] { "<br/>" };
            var res = s.Split(stringSeparators,51, StringSplitOptions.RemoveEmptyEntries);
            string parse = "";
            int k = 0;
            for(int i = 0; i < res.Length / 2; i+=5)
            {
                Question q = new Question(res[i], res[i + 1], res[i + 2], res[i + 3],res[i+4], "c#");
                parse += res[i] + " " + res[i + 1] + " " + res[i + 2] + " " + res[i + 3] + " " + res[i + 4]+" " ;
                questions[k] = q;
                k += 2;
            }
        }

        private void initFillQuestions()
        {
            string s = db.getFillQuestions().Result;
            string[] stringSeparators = new string[] { "<br/>" };
            var res = s.Split(stringSeparators, 21, StringSplitOptions.RemoveEmptyEntries);
            string parse = "";
            int k = 1;
            for (int i = 0; i < res.Length - 1; i += 4)
            {
                Question q = new Question(res[i], res[i + 1], res[i + 2], res[i + 3]);
                parse += res[i] + " " + res[i + 1] + " " + res[i + 2] + " " + res[i + 3];
                questions[k] = q;
                k += 2;
            }
        }
        /// <summary>
        /// stores the first 5 highest players in the array hiplayer
        /// </summary>
        private void initHiPlayer()
        {
            string s = db.getHighest().Result;
            string[] stringSeparators = new string[] { "<br/>" };
            var res = s.Split(stringSeparators, 11, StringSplitOptions.RemoveEmptyEntries);
            string parse = "";
            int k = 0;
            for (int i = 0; i < res.Length - 1; i += 2)
            { 
                Player p = new Player(res[i],int.Parse(res[i + 1]));
                parse += res[i] + " " + res[i + 1];
                hiplayer[k] = p;
                k++;
            }
        }
        /// <summary>
        /// gets the players from the hiplayer array
        /// </summary>
        /// <returns></returns>
        public Player getNextPlayer()
        {
            posP++;
            return posP < 5 ? hiplayer[posP] : null;
        }
        /// <summary>
        /// gets the player at the pos i
        /// </summary>
        /// <param name="i"> the position</param>
        /// <returns>returns the player</returns>
        public Player getNextPlayer(int i)
        {
            return i < 5 ? hiplayer[i] : null;
        }
        /// <summary>
        /// initializes the animations
        /// </summary>
        private void initAnimation()
        {
            animation[0] = new Animation("die", 1, 14);
            animation[1] = new Animation("jump", 1, 16);
            animation[2] = new Animation("run", 1, 9);
        }
        /// <summary>
        /// function that returns the animation needed
        /// </summary>
        /// <param name="name">the name of the needed animation</param>
        /// <returns> the needed animation </returns>
        public Animation getAnimation(string name)
        {
            switch (name)
            {
                case ("die"):
                    return animation[0];
                case "jump":
                    return animation[1];
                case "run":
                    return animation[2];
                default:
                    return null;
            }
        }
        /// <summary>
        /// checks the correct button id
        /// </summary>
        /// <param name="ID">che id to be checked</param>
        /// <returns>true or false</returns>
        public bool checkCorrect(string ID)
        {
            var res = ID;
           
            if (res == questions[pos].getCorrectAnswer())
            {
                return true;
            }
            return false;
        }
    }
}
