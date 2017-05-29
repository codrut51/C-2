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
        private Database db;
        private int pos = -1;
        /// <summary>
        /// Constructor
        /// </summary>
        public Game()
        {
            player = new Player();
            questions = new Question[10];
            animation = new Animation[3];
            db = new Database();
            initQuestions();
            initAnimation();
        }
        /// <summary>
        /// adds player to the database by passing it to the db instance
        /// </summary>
        /// <param name="p"> the player to be passed to the database in order to be processed</param>
        public void addPlayer(Player p)
        {
            db.addPlayer(p);
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
           String s = db.getQuestions().Result;
            string[] stringSeparators = new string[] { "<br/>" };
            var res = s.Split(stringSeparators,51, StringSplitOptions.RemoveEmptyEntries);
            string parse = "";
            for(int i = 0; i < (res.Length -1); i+=5)
            {
                Question q = new Question(res[i], res[i + 1], res[i + 2], res[i + 3],res[i+4], "c#");
                parse += res[i] + " " + res[i + 1] + " " + res[i + 2] + " " + res[i + 3] + " " + res[i + 4]+" " ;
                questions[i / 5] = q;
            }
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
        public bool checkCorrect(int ID)
        {
            var res = "";
            switch(ID)
            {
                case 0:
                    res = questions[pos].getAnswerA();
                    break;
                case 1:
                    res = questions[pos].getAnswerB();
                    break;
                case 2:
                    res = questions[pos].getAnswerC();
                    break;
            }
            if (res == questions[pos].getCorrectAnswer())
            {
                return true;
            }
            return false;
        }
    }
}
