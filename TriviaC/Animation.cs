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
        public Animation(string nameOfAnimation, int startPoint, int endPoint)
        {
            desc = nameOfAnimation;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            
        }

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

        public int StartPoint
        {
            get
            {
                return startPoint;
            }
        }
        public int EndPoint
        {
            get
            {
                return endPoint;
            }
        }

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
}
