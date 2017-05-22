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

        public Player()
        {
            heartsTotal = 3;
            heart = new Animation("heart", 1, 5);
        }
        public Player(string name, int score)
        {
            this.name = name;
            this.score = score;
            heartsTotal = 3;
            heart = new Animation("heart",1,5);
        }

        public Animation Heart
        {
            get
            {
                return heart;
            }
        }
        public string getName()
        {
            return name;
        }

        public int getScore()
        {
            return score;
        }

        public void incrementScore()
        {
            score += 10;
        }
        public void incrementScore(int score)
        {
            this.score += score;
        }

        public int getHeartsTotal()
        {
            return heartsTotal;
        }

        public void loseHeart()
        {
            heartsTotal -= 1;
        }
    }
}
