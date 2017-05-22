using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaC
{
    class Question
    {
        private string description;
        private string category;
        private string answer_A;
        private string answer_B;
        private string answer_C;
        private string correctAnswer;
        private string complitionQuestion;
        private string complitionAnswer;

        public Question()
        {

        }
        public Question(string description, string answer_A, string answer_B, string answer_c, string correctAnswer, string category)
        {
            this.description = description;
            this.answer_A = answer_A;
            this.answer_B = answer_B;
            this.answer_C = answer_c;
            this.correctAnswer = correctAnswer;
            this.category = category;
        }

        public Question(string complitionQuestion, string complitionAnswer)
        {
            this.complitionQuestion = complitionQuestion;
            this.complitionAnswer = complitionAnswer;
        }

        public string getComplitionQuestion()
        {
            return complitionQuestion;
        }

        public string getComplitionAnswer()
        {
            return complitionAnswer;
        }

        public string getQuestion()
        {
            return description;
        }

        public string getAnswerA()
        {
            return answer_A;
        }

        public string getAnswerB()
        {
            return answer_B;
        }

        public string getAnswerC()
        {
            return answer_C;
        }

        public string getCorrectAnswer()
        {
            return correctAnswer;
        }
    }
}
