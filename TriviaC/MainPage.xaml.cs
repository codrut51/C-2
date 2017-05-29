using System;
using System.Collections.Generic;
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
        private Game game; //connection to game
        private Image current; //the current image to be modified
        /// <summary>
        /// Constructor where everything is initialiezed 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            game = new Game();
            second.Visibility = Visibility.Collapsed;
            Gquestion.Visibility = Visibility.Collapsed;
            FillQuestions.Visibility = Visibility.Collapsed;
            animate.Visibility = Visibility.Collapsed;
            Thickness margin = main.Margin;
            SetImage();
            margin.Left = 0;
            main.Margin = margin;
            SetImage();
            current = h1;
        }

        /// <summary>
        /// when the player loses all the hearts
        /// all the grids are collapsing
        /// </summary>

        private void lose()
        {
            second.Visibility = Visibility.Collapsed;
            Gquestion.Visibility = Visibility.Collapsed;
            FillQuestions.Visibility = Visibility.Collapsed;
            animate.Visibility = Visibility.Collapsed;
        }

         /// <summary>
         /// sets the image for the player life (hearts) 
         /// </summary>

        private async void SetImage()
        {
            try
            {
                String imagepath = "ms-appx:///Assets/Images/heart1.png";
                Uri uri = new Uri(imagepath, UriKind.RelativeOrAbsolute);
                ImageSource img = new BitmapImage(uri);
                h1.Source = img;
                h2.Source = img;
                h3.Source = img;
            }
            catch (Exception)
            {
                var warning = new MessageDialog("Your image is not there!");
                await warning.ShowAsync();
            }
        }
        /// <summary>
        /// sets a image to a Image component in the assignment
        /// </summary>
        /// <param name="a">the image component</param>
        /// <param name="s">the image name and extention</param>
        private async void SetImage(Image a, String s)
        {
            try
            {
                String imagepath = "ms-appx:///Assets/Images/"+s;
                Uri uri = new Uri(imagepath, UriKind.RelativeOrAbsolute);
                ImageSource img = new BitmapImage(uri);
                a.Source = img;
            }
            catch (Exception)
            {
                var warning = new MessageDialog("Your image is not there!");
                await warning.ShowAsync();
            }
        }
        /// <summary>
        /// shows a specific grid and moves the hearts if necessary
        /// </summary>
        /// <param name="m">the grid to be shown</param>
        private void Show(Grid m)
        {
            try
            {
                m.Children.Add(h1);
                m.Children.Add(h2);
                m.Children.Add(h3);
                m.Children.Add(Points);
            }
            catch (Exception)
            {
            }
            Thickness margin = m.Margin;
            margin.Left = 0;
            margin.Right = 0;
            m.Margin = margin;
            m.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// hides a specific grid and moves the hearts if necessary
        /// </summary>
        /// <param name="m">the grid to be hiden</param>
        private void Hide(Grid m)
        {
            try
            {
                m.Children.Remove(h1);
                m.Children.Remove(h2);
                m.Children.Remove(h3);
                m.Children.Remove(Points);
            }
            catch (Exception)
            {
            }
            m.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// this function is used to display a user message
        /// </summary>
        /// <param name="message">the message to be displayed</param>
        private async void Display(string message)
        {
            MessageDialog ms = new MessageDialog(message);
            await ms.ShowAsync();
        }
        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var x = username.Text.Split(' ');
            string res = "";
            for (int i = 0; i < x.Length; i++)
            {
                res += x[i];
            }
            
            if (res.Length >= 4 && res.Length <= 50)
            {
                username.Text = res;
                Player p = new Player(res, 0);
                game.addPlayer(p);
                Points.Text = "Points: " + p.getScore();
               // 
                Show(second);
            }
            else
            {
                Display("Please insert a name with at least 4 charactes");
            }
        }
        /// <summary>
        /// Gets the next question available
        /// </summary>
        private void getNext()
        {
            Question q = game.getNextQuesiton();
            if(q != null)
            {
                qdesc.Text = q.getQuestion();
                answerA.Content = q.getAnswerA();
                answerB.Content = q.getAnswerB();
                answerC.Content = q.getAnswerC();
            }
            else
            {
                Hide(Gquestion);
                Hide(FillQuestions);
                Show(animate);
                game.Player.incrementScore(game.Player.getHeartsTotal() * 100);
                Points.Text = "Points: " + game.Player.getScore();
                codeRes.Text = "You won!!!";
            }
        }

        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private void button_StartSingle(object sender, RoutedEventArgs e)
        {
            //FillQuestion.Text = "for(int i =                       )" + Environment.NewLine + " { " + Environment.NewLine + "       jump(); " + Environment.NewLine + " } ";
            getNext();
            Hide(second);
            Show(Gquestion);
        }
        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private void MultiStart_Click(object sender, RoutedEventArgs e)
        {
            Hide(FillQuestions);
            Show(Gquestion);
        }
        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            Hide(FillQuestions);
            Show(animate);
        }

        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private void button_ClickAnswer(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string s =(String) b.Name;
            bool r = false;
            switch(s)
            {
                case "answerA":
                    r = game.checkCorrect(0);
                    break;
                case "answerB":
                    r = game.checkCorrect(1);
                    break;
                case "answerC":
                    r = game.checkCorrect(2);
                    break;
            }
            if(r)
            {
                game.Player.incrementScore();
                Points.Text = "Points: " + game.Player.getScore();
                getNext();
            }
            else
            {
                heart();
                game.Player.loseHeart();
            }
        }
        /// <summary>
        /// Makes an animation for each heart and when the player loses
        /// </summary>
        public async void heart()
        {
            for(int i = 0; i < 5; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.25));
                SetImage(current, game.Player.Heart.runAnimation());
            }
            if(current.Equals(h1))
            {
                current = h2;
            }
            else if (current.Equals(h2))
            {
                current = h3;
            }else if (current.Equals(h3))
            {
                lose();
                Hide(Gquestion);
                Hide(FillQuestions);
                Show(animate);
                    if(game.getAnimation("die") == null)
                    {
                        Display("Sorry something went wrong");
                }else
                {
                    int lenght = game.getAnimation("die").EndPoint;
                    for (int i = 0; i < lenght; i++)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(0.25));
                        SetImage(animation, game.getAnimation("die").runAnimation());
                    }
                }
            }
        }
    }
}
