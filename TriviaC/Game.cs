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
        private Dictionary<String, Animator> animator;
        public Game()
        {
            db = new Database();
            i = j = x = -1;
            End.description = "End";
            animations = new Dictionary<string, Animation>();
            animator = new Dictionary<string, Animator>();
            animations["heart"] = new Animation("heart", 1, 5);
            animator["dance"] = new Animator(4160, 1368, 384, 456);
            animator["run"] = new Animator(2493, 975, 310, 353);
            animator["jump"] = new Animator(2000, 975, 301, 351);
            animator["die"] = new Animator(4096, 370, 324, 319);

        }
        /// <summary>
        /// Add the player to the database also to the game class
        /// </summary>
        /// <param name="name">The name of the player passed from mainPage.xaml.cs </param>
        /// <returns>returns true if the name is not in the database</returns>
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

        public Dictionary<String, Animation> Animations
        {
            get
            {
                return animations;
            }
        }

        public Dictionary<String, Animator> Animator
        {
            get
            {
                return animator;
            }
        }
        /// <summary>
        /// Returns the next question compiled from the database class 
        /// </summary>
        /// <returns></returns>
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
                return j < db.GetFillInQuestion().Count ? db.GetFillInQuestion()[j] : End;
            }            
        }
        /// <summary>
        /// returns true if the answer passed to this function is correct false otherwise
        /// also incrementing the score / decrementing the amount of hearths of the player
        /// </summary>
        /// <param name="yourAnswer">the answer passed</param>
        /// <returns></returns>
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
    }
}
