using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaC
{
    class Player
    {
        private string name;
        private int score;
        private Animation heart;
        private int heartsTotal;
        /// <summary>
        /// constructors
        /// </summary>
        public Player()
        {
            heartsTotal = 3;
            heart = new Animation("heart", 1, 5);
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">player name</param>
        /// <param name="score">player score</param>
        public Player(string name, int score)
        {
            this.name = name;
            this.score = score;
            heartsTotal = 3;
            heart = new Animation("heart",1,5);
        }
        /// <summary>
        /// returns the heart animation
        /// </summary>
        public Animation Heart
        {
            get
            {
                return heart;
            }
        }

        /// <summary>
        /// returns the player name
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return name;
        }
        /// <summary>
        /// returns the player score
        /// </summary>
        /// <returns></returns>
        public int getScore()
        {
            return score;
        }
        /// <summary>
        /// increments the player score
        /// </summary>
        public void incrementScore()
        {
            score += 10;
        }
        /// <summary>
        /// increment the player score by a specified amount
        /// </summary>
        /// <param name="score">the amount specified</param>
        public void incrementScore(int score)
        {
            this.score += score;
        }
        /// <summary>
        /// gets the player lifes/tries
        /// </summary>
        /// <returns></returns>
        public int getHeartsTotal()
        {
            return heartsTotal;
        }
        /// <summary>
        /// player losses a heart
        /// </summary>
        public void loseHeart()
        {
            heartsTotal -= 1;
        }
    }
}
