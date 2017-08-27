using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TriviaC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Starting program
        /// </summary>
        //private Game game; //connection to game
        //private Image current; //the current image to be modified
        //private bool isChoice = true;
        //private Question currentQuestion;
        /// <summary>
        /// Constructor where everything is initialiezed 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            Database db = new Database();
            List<Question> q = db.GetMultiQuestion();
            List<Question> q1 = db.GetFillInQuestion();
            List<Player> p = db.GetHiPlayer();
            Debug.WriteLine(q[0].description);
            Debug.WriteLine(q1[0].description);
            Debug.WriteLine(p[0].getName());
        }
          
        }
    }
