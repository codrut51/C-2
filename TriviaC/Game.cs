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

        public Game()
        {
            player = new Player();
            questions = new Question[10];
            animation = new Animation[3];
            db = new Database();
            initQuestions();
            initAnimation();
        }

        public void addPlayer(Player p)
        {
            db.addPlayer(p);
        }

        public Player Player
        {
            get
            {
                return player;
            }
        }

        public Question getNextQuesiton()
        {
            
            pos++;
            return pos < 10 ? questions[pos] : null;
        }

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

        private void initAnimation()
        {
            animation[0] = new Animation("die", 1, 14);
            animation[1] = new Animation("jump", 1, 16);
            animation[2] = new Animation("run", 1, 9);
        }

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
