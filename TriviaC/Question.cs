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
        private string animation;
        private string category;
        private string answer_A;
        private string answer_B;
        private string answer_C;
        private string correctAnswer;
        private string complitionQuestion;
        private string complitionAnswer;
        /// <summary>
        /// constructor 
        /// </summary>
        public Question()
        {

        }  

        /// <summary>
        /// constructor 
        /// </summary>
        public Question(string description)
        {
            this.description = description;
        }
        /// <summary>
        /// construcotr
        /// </summary>
        /// <param name="description">question description</param>
        /// <param name="answer_A">question first answer </param>
        /// <param name="answer_B">question second answer </param>
        /// <param name="answer_c">question third answer </param>
        /// <param name="correctAnswer"> question correct ansert</param>
        /// <param name="category"> question category if needed </param>
        public Question(string description, string answer_A, string answer_B, string answer_c, string correctAnswer, string category)
        {
            this.description = description;
            this.answer_A = answer_A;
            this.answer_B = answer_B;
            this.answer_C = answer_c;
            this.correctAnswer = correctAnswer;
            this.category = category;
        }
        /// <summary>
        /// construcotr
        /// </summary>
        /// <param name="description">question description</param>
        /// <param name="answer_A">question first answer </param>
        /// <param name="answer_B">question second answer </param>
        /// <param name="answer_c">question third answer </param>
        /// <param name="correctAnswer"> question correct ansert</param>
        public Question(string description, string answer_A, string answer_B, string answer_c, string correctAnswer)
        {
            this.description = description;
            this.answer_A = answer_A;
            this.answer_B = answer_B;
            this.answer_C = answer_c;
            this.correctAnswer = correctAnswer;
        }
        /// <summary>
        /// construcor
        /// </summary>
        /// <param name="complitionQuestion">complition question description</param>
        /// <param name="complitionAnswer">complition question answer </param>
        public Question(string description, string complitionQuestion, string complitionAnswer, string animation)
        {
            this.description = description;
            this.complitionQuestion = complitionQuestion;
            this.correctAnswer = complitionAnswer;
            this.animation = animation;
        }
        /// <summary>
        /// gets complition question
        /// </summary>
        /// <returns>returns complition question</returns>
        public string getComplitionQuestion()
        {
            return complitionQuestion;
        }
        /// <summary>
        /// gets quesiton description 
        /// </summary>
        /// <returns>quesiton descirption</returns>
        public string getQuestion()
        {
            return description;
        }
        /// <summary>
        /// gets question answer a
        /// </summary>
        /// <returns>returns question answer a</returns>
        public string getAnswerA()
        {
            return answer_A;
        }
        /// <summary>
        /// gets question answer b
        /// </summary>
        /// <returns>returns question answer b</returns>
        public string getAnswerB()
        {
            return answer_B;
        }
        /// <summary>
        /// gets question answer c
        /// </summary>
        /// <returns>returns question answer c</returns>
        public string getAnswerC()
        {
            return answer_C;
        }

        /// <summary>
        /// gets question correct answer
        /// </summary>
        /// <returns>returns question correct answer</returns>
        public string getCorrectAnswer()
        {
            return correctAnswer;
        }

        public string getAnimation()
        {
            return animation;
        }
    }

    public struct question
    {
        public string description;
        public string ansa;
        public string ansb;
        public string ansc;
        public string corrans;
        public string complitionQuestion;
        public string complitionAnswer;
        public string animation;
        public bool isMulty;
        public bool isFillIn;
    }

}
