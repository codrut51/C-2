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
using Windows.Storage.Streams;
using Windows.UI.Core;
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
        private Uri old = null;
        private List<BitmapImage> preloadImages;
        private int runAnim = 0;
        private Animator local;
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
            current.isMulty = true;
            currentImage = h1;
            correct = 0;
            preloadImages = new List<BitmapImage>();
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
                x.Interval = new TimeSpan(1500000); //1 = 100 nanoseconds => 1s = 10^9 nanoseconds
                x.Start();
                x.Tick += X_Tick;
                DispatcherTimer y = new DispatcherTimer();
                y.Interval = new TimeSpan(1400000); //1 = 100 nanoseconds => 1s = 10^9 nanoseconds
                y.Start();
                y.Tick += Y_Tick;
            }
            catch(Exception ex)
            {

            }
        }

        private void Y_Tick(object sender, object e)
        {
            try
            {
                // do some work connected with the UI
                if (!current.isMulty && correct == 1)
               {
                   if (current.animation != "none")
                   {
                       var v = local.NextFrame();
                       SpriteSheetOffset.X = v[0];
                       SpriteSheetOffset.Y = v[1];
                   }
               }
               try
               {
                   if (game.player.getHeartsTotal() == 0 && correct == -1)
                    {
                        var v = local.NextFrame();
                        SpriteSheetOffset.X = v[0];
                        SpriteSheetOffset.Y = v[1];
                    }
               }
               catch (Exception)
               {

               }
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
        private BitmapImage getImage(String s)
        {
            try
            {
                String imagepath = "ms-appx:///Assets/Images/" + s;
                Uri uri = new Uri(imagepath, UriKind.RelativeOrAbsolute);
                BitmapImage b = new BitmapImage(uri);
               
                return b;
            }
            catch (Exception)
            {
                return null;
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
                    animation.CurrentPoint = 0;
                    correct = 0;
                    if(currentImage == h1)
                    {
                        currentImage = h2;
                    }
                    else if(currentImage == h2)
                    {
                        currentImage = h3;
                    }
                    Debug.WriteLine(currentImage.Name);
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
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
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
                    Hide(FillQuestions);
                    Show(Gquestion);
                    qdesc.Text = current.description;
                    answerA.Content = current.ansa;
                    answerB.Content = current.ansb;
                    answerC.Content = current.ansc;
                }
                else
                {
                    Hide(Gquestion);
                    Show(FillQuestions);
                    FillCode.Text = current.complitionQuestion;
                    FillDesc.Text = current.description;
                }
            }
            else
            {
                HiScore1.Text = "";
                HiScore2.Text = "";
                HiScore.Text = "";
                int pos = 0;
                if(game.player.getHeartsTotal() == 3)
                {
                    game.player.incrementScore(300);
                }
                foreach(Player p in game.Datab.GetHiPlayer())
                {
                    if (pos < 3)
                    {
                        HiScore.Text += p.getName() + " => " + p.getScore() + Environment.NewLine;
                    } else
                    {
                        HiScore2.Text += p.getName() + " => " + p.getScore() + Environment.NewLine;
                    }
                    pos++;
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
                bool ch = game.checkAnswer(choice);
                if (!ch)
                {
                    correct = -1;
                    if (game.player.getHeartsTotal() == 0)
                    {
                            try
                            {
                                String imagepath = "ms-appx:///Assets/" + "die" + ".png";
                                Uri uri = new Uri(imagepath, UriKind.RelativeOrAbsolute);
                                anim.ImageSource = new BitmapImage(uri);
                            }
                            catch (Exception)
                            {

                            }
                        local = game.Animator["die"];
                        Canvas.Height = local._yInterval;
                        Canvas.Width = local._xInterval;
                        Rectangle.Width = local._xInterval;
                        Rectangle.Height = local._yInterval;
                        codeRes.Text = "You Lost!";
                        Hide(Gquestion);
                        Hide(FillQuestions);
                        Show(animate);
                    }
                    else
                    {
                        nextQuestion();
                    }
                }
                else if (ch && !current.isMulty)
                {
                    correct = 1;
                    if(current.animation != "none")
                    {
                        try
                        {
                            String imagepath = "ms-appx:///Assets/" + current.animation + ".png";
                            Uri uri = new Uri(imagepath, UriKind.RelativeOrAbsolute);
                            anim.ImageSource = new BitmapImage(uri);
                        }
                        catch (Exception)
                        {

                        }
                        local = game.Animator[current.animation];
                        Canvas.Height = local._yInterval;
                        Canvas.Width = local._xInterval;
                        Rectangle.Width = local._xInterval;
                        Rectangle.Height = local._yInterval;
                        codeRes.Text = current.complitionQuestion;
                        Hide(Gquestion);
                        Hide(FillQuestions);
                        Show(animate);
                    }
                    Points.Text = "Points: " + game.player.getScore();
                }
                if(ch && current.isMulty)
                {

                    Points.Text = "Points: " + game.player.getScore();
                    nextQuestion();
                }
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            Hide(animate);
            game.Animator[current.animation].ResetFrame();
            nextQuestion();
            correct = 0;
        }
    }
 }
