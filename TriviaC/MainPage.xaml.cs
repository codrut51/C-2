using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
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
        private Question current;
        private int correct; // 1 correct 0 none -1 incorrect
        private Image currentImage;
        //private Image current; //the current image to be modified
        //private bool isChoice = true;
        //private Question currentQuestion;
        /// <summary>
        /// Constructor where everything is initialiezed 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            game = new Game();
            SetImage();
            looseHeart();
            currentImage = h1;
            correct = 0;
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

        private async void looseHeart()
        {
            try
            {
                DispatcherTimer x = new DispatcherTimer();
                x.Interval = new TimeSpan(1000000); //1 = 100 nanoseconds => 1s = 10^9 nanoseconds
                x.Start();
                x.Tick += X_Tick;
            }
            catch(Exception ex)
            {

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
                String imagepath = "ms-appx:///Assets/Images/" + s;
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
        private void X_Tick(object sender, object e)
        {
            if(correct == -1)
            {
                var animation = game.Animations["heart"];
                if (animation.CurrentPoint < animation.EndPoint)
                {
                    SetImage(currentImage,animation.runAnimation());
                }
                else
                {
                    correct = 0;
                    if(currentImage == h1)
                    {
                        currentImage = h2;
                    }
                    else if(currentImage == h2)
                    {
                        currentImage = h3;
                    }
                    else
                    {
                        Hide(Gquestion);
                        Hide(second);
                        Hide(FillQuestions);
                        Show(Win);
                    }
                }
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

        private void createPlayer_Click(object sender, RoutedEventArgs e)
        {
            if(game.addPlayer(username.Text))
            {
                Show(second);
                Points.Text = "Points: " + game.player.getScore();
            }
        }
        private void nextQuestion()
        {
           current = game.nextQuestion();
           if(!current.description.Equals("End"))
            {
                if (current.isMulty)
                {
                    /*Hide(FillQuestions);
                    Show(Gquestion);*/
                    qdesc.Text = current.description;
                    answerA.Content = current.ansa;
                    answerB.Content = current.ansb;
                    answerC.Content = current.ansc;
                }
                else
                {
                   /* Hide(Gquestion);
                    Show(FillQuestions);
                    FillCode.Text = current.complitionQuestion;
                    FillDesc.Text = current.description;*/
                }
            }
            else
            {
                HiScore1.Text = "";
                HiScore.Text = "";
                foreach(Player p in game.Datab.GetHiPlayer())
                {
                    HiScore.Text += p.getName() + " => " + p.getScore() + Environment.NewLine;  
                }
                MyUser.Text = game.player.getName() + " => " + game.player.getScore();
                Hide(Gquestion);
                Hide(FillQuestions);
                game.updatePlayer();
                Show(Win);
            }
        }
        private void SingleP_Click(object sender, RoutedEventArgs e)
        {
            Hide(second);
            Show(Gquestion);
            nextQuestion();
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string choice = "";
            if(button != null)
            {
                switch(button.Name)
                {
                    case "answerA":
                        choice = "a";
                        break;
                    case "answerB":
                        choice = "b";
                        break;
                    case "answerC":
                        choice = "c";
                        break;
                    case "SubmitAnswer":
                        choice = FillAnswer.Text;
                        FillAnswer.Text = "";
                        break;
                }
                if(!game.checkAnswer(choice))
                {
                    looseHeart();
                    correct = -1;
                }
                Points.Text = "Points: " + game.player.getScore();
                nextQuestion();
            }
        }
    }
 }
