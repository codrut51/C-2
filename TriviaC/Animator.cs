using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaC
{
    class Animator
    {
        public int _xInterval;
        public int _yInterval;
        private int _maxWidth;
        private int _maxLength;
        private List<int> currentFrame;
        /// <summary>
        /// Constructor for animations
        /// </summary>
        /// <param name="maxWidth">the width of the animation that you want to use</param>
        /// <param name="maxHeight">the height</param>
        /// <param name="xInterval">the end frame point on the x axis </param>
        /// <param name="yInterval">the end frame point on the y axis </param>
        public Animator(int maxWidth, int maxHeight, int xInterval, int yInterval)
        {
            _xInterval = xInterval;
            _yInterval = yInterval;
            _maxWidth = maxWidth;
            _maxLength = maxHeight;
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
        /// <summary>
        /// resets the currentFrame
        /// </summary>
        public void ResetFrame()
        {
            currentFrame = null;
        }
    }
}
