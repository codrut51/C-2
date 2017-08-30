using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TriviaC
{
    class Animation
    {
        private string desc;
        private int startPoint;
        private int endPoint;
        private int pos = 0;
        /// <summary>
        /// Constructor for the animation used to save the starting and end point of an animation
        /// </summary>
        /// <param name="nameOfAnimation"> name of the animation </param>
        /// <param name="startPoint"> where the animation starts </param>
        /// <param name="endPoint"> where the animation ends </param>
        public Animation(string nameOfAnimation, int startPoint, int endPoint)
        {
            desc = nameOfAnimation;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            
        }
        /// <summary>
        /// returns the picture that needs to be displayed
        /// </summary>
        /// <returns>returns the picture that needs to be displayed</returns>
        public string runAnimation()
        {
            pos++;
            if(pos <= endPoint)
            {
                return desc + pos + ".png";
            }
            else
            {
                pos = startPoint;
                return desc + pos + ".png";
            }
        }
        /// <summary>
        /// getter for the starting Point
        /// </summary>
        public int StartPoint
        {
            get
            {
                return startPoint;
            }
        }
        /// <summary>
        /// getter for the endPoint
        /// </summary>
        public int EndPoint
        {
            get
            {
                return endPoint;
            }
        }

        public int CurrentPoint
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }
        /// <summary>
        /// returns the picture that needs to be displayed
        /// </summary>
        /// <returns>returns the picture that needs to be displayed</returns>
        public string Animate()
        {
            pos++;
            if (pos <= endPoint)
            {
                return desc + pos + ".png";
            }
            else
            {
                return null;
            }
        }
    }
    class Animator
    {
        private int _xInterval;
        private int _yInterval;
        private int _maxWidth;
        private int _maxLength;
        private List<int> currentFrame;

        public Animator(int maxWidth, int maxLength, int xInterval, int yInterval)
        {
            _xInterval = xInterval;
            _yInterval = yInterval;
            _maxWidth = maxWidth;
            _maxLength = maxLength;
        }

        /// <summary>
        /// Returns the coordinates of the next frame in the spritesheet
        /// Index 0 = x
        /// Index 1 = y
        /// </summary>
        /// <returns>The coordinates of the next frame</returns>
        public List<int> NextFrame()
        {
            // First frame
            if (currentFrame == null)
            {
                currentFrame = new List<int> { 0, 0 };
                return currentFrame;
            }

            currentFrame[0] -= _xInterval;

            // If maxWidth has been succeeded, move one frame down and start the new row
            if (currentFrame[0] <= (_maxWidth * -1))
            {
                currentFrame[0] = 0;
                currentFrame[1] -= _yInterval;
            }

            return currentFrame;
        }
    }
}
