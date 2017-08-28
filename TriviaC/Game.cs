using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaC
{
    class Game
    {
        private Database db;
        private Player p;
        private Question End;
        private int i, j, x;
        private Dictionary<String, Animation> animations;
        public Game()
        {
            db = new Database();
            i = j = x = -1;
            End.description = "End";
            animations = new Dictionary<string, Animation>();
            animations["die"] = new Animation("die", 1, 14);
            animations["heart"] = new Animation("heart", 1, 5);
            animations["jump"] = new Animation("jump", 1, 16);
            animations["run"] = new Animation("run", 1, 9);
        }

        public bool addPlayer(String name)
        {
            if (db.CreatePlayer(name))
            {
                p = new Player(name, 0);
                return true;
            }
            return false;
        }

        public Player player
        {
            get
            {
                return p;
            }
        }

        public Database Datab
        {
            get
            {
                return db;
            }
        }

        public void updatePlayer()
        {
            db.UpdatePlayer(p);
        }

        public Dictionary<String, Animation> Animations{
            get
            {
                return animations;
            }
        }

        public Question nextQuestion()
        {
            x++;
            if (x % 2 == 0)
            {
                i++;
                return i < db.GetMultiQuestion().Count ? db.GetMultiQuestion()[i] : End;
            }
            else
            {
                j++;
                Debug.WriteLine("i= " + i + " j= " + j);
                return j < db.GetFillInQuestion().Count ? db.GetFillInQuestion()[j] : End;
            }            
        }

        public bool checkAnswer(string yourAnswer)
        {
            if(x % 2 == 0)
            {
                if(db.GetMultiQuestion()[i].corrans == yourAnswer)
                {
                    p.incrementScore();
                    return true;
                }
                else
                {
                    p.loseHeart();
                    return false;
                }
                
            }
            else
            {
                if(db.GetFillInQuestion()[j].complitionAnswer == yourAnswer)
                {
                    p.incrementScore();
                    return true;
                }
                else
                {
                    p.loseHeart();
                    return false;
                }
            }
        }
        /* 
        private Player player;
        private Question[] questions;
        private Question end;
        private Animation[] animation;
        private Player[] hiplayer;
        private Database db;
        private int pos = -1;
        private int posP = -1;
        private bool isMulty = false;
        private bool isJoin = false;
        private int multiQuestionID;
        private int multiPlayerID;
        /// <summary>
        /// Constructor
        /// </summary>
        public Game(bool isMulty)
        {
            player = new Player();
            questions = new Question[10];
            animation = new Animation[3];
            hiplayer = new Player[5];
            db = new Database();
            end = new Question("End");
            initQuestions();
            initAnimation();
            initFillQuestions();
            initHiPlayer();
            this.isMulty = isMulty;
            if (isMulty && !isJoin)
            {
               // db.setMultiPlayerQuestions();
                MultiplePlayerQuestion();
            }
            else if (isJoin)
            {
                initMultiPlayerQuestions();
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Game(Player p, bool isMulty)
        {
            player = p;
            questions = new Question[10];
            animation = new Animation[3];
            hiplayer = new Player[5];
            db = new Database();
            end = new Question("End");
            initQuestions();
            initAnimation();
            initFillQuestions();
            initHiPlayer();
            this.isMulty = isMulty;
            if (isMulty && !isJoin)
            {
               // db.setMultiPlayerQuestions();
                MultiplePlayerQuestion();
            }
            else if (isJoin)
            {
                initMultiPlayerQuestions();
            }
        }

        public Game(Game g, bool isMulty, int multiPlayerID)
        {
            this.player = g.Player;
            this.questions = g.Questions;
            this.animation = g.animation;
            this.multiPlayerID = multiPlayerID;
            this.multiQuestionID = g.MultiQuestionID;
            hiplayer = new Player[5];
            end = new Question("End");
            this.db = g.Data;
            this.isJoin = g.Join;
            initHiPlayer();
            this.isMulty = isMulty;
            if (isMulty && !isJoin)
            {
                //db.setMultiPlayerQuestions();
                MultiplePlayerQuestion();
            }
            else if (isJoin)
            {
                initMultiPlayerQuestions();
            }
        }
       

        public Game(Game g, bool isMulty)
        {
            this.player = g.Player;
            this.questions = g.Questions;
            this.animation = g.animation;
            this.multiPlayerID = g.MultiPlayerID;
            this.multiQuestionID = g.MultiQuestionID;
            hiplayer = new Player[5];
            end = new Question("End");
            this.db = g.Data;
            this.isJoin = g.Join;
            initHiPlayer();
            this.isMulty = isMulty;
            if (isMulty && !isJoin)
            {
                //db.setMultiPlayerQuestions();
                MultiplePlayerQuestion();
            }else if (isJoin)
            {
                initMultiPlayerQuestions();
            }
        }

        /// <summary>
        /// adds player to the database by passing it to the db instance
        /// </summary>
        /// <param name="p"> the player to be passed to the database in order to be processed</param>
        public void addPlayer()
        {
            db.addPlayer(player);
        }

        public void updatePlayer()
        {
            db.updatePlayer(player);
        }
        public bool IsMulty
        {
            get
            {
                return isMulty;
            }
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
        /// gets animations array
        /// </summary>
        public Animation[] Animations
        {
            get
            {
                return animation;
            }
        }
        /// <summary>
        /// gets questions array
        /// </summary>
        public Question[] Questions
        {
            get
            {
                return questions;
            }
        }
        /// <summary>
        /// gets database 
        /// </summary>
        public Database Data
        {
            get
            {
                return db;
            }
        }

        public bool Join
        {
            get
            {
                return isJoin;
            }
        }

        public int MultiQuestionID
        {
            get
            {
                return multiQuestionID;
            }
        }

        public int MultiPlayerID
        {
            get
            {
                return multiPlayerID;
            }
        }
        /// <summary>
        /// gets the next question
        /// </summary>
        /// <returns></returns>
        public Question getNextQuesiton()
        {
            pos++;
            return pos < 10 ? questions[pos] : end;
        }

        public Question End
        {
            get
            {
                return end;
            }
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
                Question q = new Question(res[i], res[i + 1], res[i + 2], res[i + 3],res[i+4]);
                parse += res[i] + " " + res[i + 1] + " " + res[i + 2] + " " + res[i + 3] + " " + res[i + 4]+" " ;
                questions[k] = q;
                k += 2;
            }
        }

        public bool joinGame(int id)
        {
            isJoin = true;
            return db.joinGame(player, id).Result;
        }

        public bool waitingForSecondPlayer()
        {
            return db.isSecond(multiPlayerID).Result;
        }
        public bool isAvailable(int id)
        {
            multiPlayerID = id;
            if (db.checkAvailable(id, player.getName(), multiQuestionID).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// function that saves in the database the questions for the multiplayer game
        /// </summary>
        private void MultiplePlayerQuestion()
        {
            Random rt = new Random();
            multiQuestionID = rt.Next(500000);
            var res = db.getMultiPlayerQuestion(multiQuestionID).Result;
            while(res == "false")
            {
                multiQuestionID = rt.Next(500000);
                res = db.getMultiPlayerQuestion(multiQuestionID).Result;
            }
        }

        private void initMultiPlayerQuestions()
        {
            string s = db.MultiPlayerQuestions(multiPlayerID).Result;
            //db.display(s);
            string[] stringSeparators = new string[] { "<br/>" };
            var res = s.Split(stringSeparators, 51, StringSplitOptions.RemoveEmptyEntries);
            int j = 0;
            int i = 0;
            bool sw = true;
            questions = new Question[10];
            while(j < res.Length)
            {
                if(sw)
                {
                    questions[i] = new Question(res[j], res[j+1], res[j+2], res[j+3], res[j+4]);
                    i++;
                    j += 5;
                    sw = false;
                }
                else
                {
                    questions[i] = new Question(res[j], res[j + 1], res[j + 2], res[j + 3]);
                    i++;
                    j += 4;
                    sw = true;
                }
            }
        }
        /// <summary>
        /// stores the fill in questions into the quesitons array
        /// </summary>
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
        } */
    }
}
