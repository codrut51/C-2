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
        private bool isChoice = true;
        private Question currentQuestion;
        /// <summary>
        /// Constructor where everything is initialiezed 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            try
            {
                game = new Game(false);
            }catch(Exception)
            {

            }
            second.Visibility = Visibility.Collapsed;
            Gquestion.Visibility = Visibility.Collapsed;
            FillQuestions.Visibility = Visibility.Collapsed;
            animate.Visibility = Visibility.Collapsed;
            Win.Visibility = Visibility.Collapsed;
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
            try
            {
                MessageDialog ms = new MessageDialog(message);
                await ms.ShowAsync();
            }
            catch (Exception) { }
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
                if(game.Data.isUnique(res).Result)
                {
                    Player p = new Player(res, 0);
                    game.Player = p;
                    Points.Text = "Points: " + p.getScore();
                    // 
                    Show(second);
                    game.addPlayer();
                }
                else
                {
                    Display("This name already exists please try again");
                }   
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
            currentQuestion = game.getNextQuesiton();
            if(isChoice)
            {
                Hide(FillQuestions);
                Show(Gquestion);
                if (currentQuestion.getQuestion() != game.End.getQuestion() && currentQuestion.getQuestion() != null)
                {
                    qdesc.Text = currentQuestion.getQuestion();
                    if (qdesc.Text.Length <= 60)
                    {
                        Thickness m = qdesc.Margin;
                        m.Top = 180;
                        qdesc.Margin = m;
                    }
                    else
                    {
                        Thickness m = qdesc.Margin;
                        m.Top = 128;
                        qdesc.Margin = m;
                    }
                    answerA.Content = currentQuestion.getAnswerA();
                    answerB.Content = currentQuestion.getAnswerB();
                    answerC.Content = currentQuestion.getAnswerC();
                    isChoice = false;
                }
            }
            else//endif
            {
                if (currentQuestion.getQuestion() != game.End.getQuestion() && currentQuestion.getQuestion() != null)
                {
                    Hide(animate);
                    Hide(Gquestion);
                    Show(FillQuestions);
                    FillCode.Text = currentQuestion.getComplitionQuestion();
                    FillDesc.Text = currentQuestion.getQuestion();
                    isChoice = true;
                }
            }
           
        }

        private void Click_Continue(object sender, RoutedEventArgs e)
        {
            Hide(animate);
            if(currentQuestion.getQuestion() != game.End.getQuestion() && currentQuestion.getQuestion() != null) 
            {
                Show(Gquestion);
            }
            else
            {
                Hide(animate);
                Hide(Gquestion);
                Hide(FillQuestions);
                Show(Win);
                Continue.Visibility = Visibility.Collapsed;
                game.Player.incrementScore(game.Player.getHeartsTotal() * 100);
                Points.Text = "Points: " + game.Player.getScore();
                MyUser.Text = "Name: " + game.Player.getName() + Environment.NewLine + "Score: " + game.Player.getScore();
                game = new Game(game.Player,game.IsMulty);
                game.updatePlayer();
                OtherColumn1.Text = "Higest Players: " + Environment.NewLine;
                OtherColumn2.Text = "";
                OtherColumn3.Text = "";
                for (int i = 0; i < 5; i++)
                {
                    Player x = game.getNextPlayer(i);
                    if (x != null)
                    {
                        if (i < 2)
                        {
                            OtherColumn1.Text += "Name: " + x.getName() + " Score: " + x.getScore() + Environment.NewLine;
                        }
                        else if (i < 4)
                        {
                            OtherColumn2.Text += "Name: " + x.getName() + " Score: " + x.getScore() + Environment.NewLine;
                        }
                        else
                        {
                            OtherColumn1.Text += "Name: " + x.getName() + " Score: " + x.getScore() + Environment.NewLine;
                        }

                    }
                }

                if (game.Player.getScore() == 400)
                {
                    try // this try catch changes the background of the grid if you have 400 points
                    {
                        String imagepath = "ms-appx:///Assets/ultimate win.png";
                        Uri uri = new Uri(imagepath, UriKind.RelativeOrAbsolute);
                        ImageBrush img = new ImageBrush();
                        img.ImageSource = new BitmapImage(uri);
                        Win.Background = img;
                    }
                    catch (Exception)
                    {
                    }
                    //var brush = new ImageBrush();
                    //brush.ImageSource = new BitmapImage(new Uri("Assets/Ultimate win.png"));
                    //Win.Background = brush;
                    Display("Because you got the maximum amount of points now you can see what is the meaning of the morse code of each background");
                }
            }
        }

        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private void button_StartSingle(object sender, RoutedEventArgs e)
        {
            getNext();
            Hide(second);
            Show(Gquestion);
        }
        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private async void MultiStart_Click(object sender, RoutedEventArgs e)
        {
            game = new Game(game,true);
            Random r = new Random();
            int next = r.Next(500000);
            while(game.isAvailable(next))
            {
                next = r.Next(500000);
            }

            ShowCode.Text = "Your game number is: " + next;
            MultiStart.Visibility = Visibility.Collapsed;
            textJoin.Visibility = Visibility.Collapsed;
            CodeJoin.Visibility = Visibility.Collapsed;
            join.Visibility = Visibility.Collapsed;
            Task t = Task.Run(() => {
                // Just loop.
                while (!game.waitingForSecondPlayer())
                {

                }
                getNext();
                Hide(second);
                Show(Gquestion);
            });
            await t.AsAsyncAction();
        }
        /// <summary>
        /// runs an animation for the complition questions
        /// </summary>
        /// <param name="s">the name of the animation</param>
        public async void runAnimation(string s)
        {
            int lenght = game.getAnimation(s).EndPoint;
            for (int i = 0; i < lenght; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.25));
                SetImage(animation, game.getAnimation(s).runAnimation());
            }
        }
        /// <summary>
        /// Button click ... this function happens on button click
        /// </summary>
        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if(game.checkCorrect(FillAnswer.Text))
            {
                Hide(FillQuestions);
                if(currentQuestion.getAnimation() != "none")
                {
                    Show(animate);
                    runAnimation(currentQuestion.getAnimation());
                    codeRes.Text = currentQuestion.getComplitionQuestion();
                }
                game.Player.incrementScore();
                Points.Text = "Points: " + game.Player.getScore();
                FillAnswer.Text = "";
                getNext();
            }
            else
            {
                heart();
                game.Player.loseHeart();
            }
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
                    r = game.checkCorrect("a");
                    break;
                case "answerB":
                    r = game.checkCorrect("b");
                    break;
                case "answerC":
                    r = game.checkCorrect("c");
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
                Continue.Visibility = Visibility.Collapsed;
                Show(animate);
                game.updatePlayer();
                game = new Game(game.Player,game.IsMulty);
                codeRes.Text = "You lost";
                codeRes.FontSize = 44;
                codeRes.FontSize = 14;
                if (game.getAnimation("die") == null)
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

        private void join_Click(object sender, RoutedEventArgs e)
        {
           if(CodeJoin.Text.Length != 0)
            {
                try
                {
                    int id = int.Parse(CodeJoin.Text);
                    if(game.joinGame(id))
                    {
                        game = new Game(game, true, id);
                        getNext();
                        Hide(second);
                        Show(Gquestion);
                    }
                    else
                    {
                        Display("The code name that you just imput is not a valid one");
                    }
                }catch(Exception)
                {
                    Display("Please insert only numbers");
                }
            }
            else
            {
                Display("Please insert a code made out of numbers");
            }
            /*
            try // this try catch changes the background of the grid if you have 400 points
            {
                String imagepath = "ms-appx:///Assets/ultimate win.png";
                Uri uri = new Uri(imagepath, UriKind.RelativeOrAbsolute);
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(uri);
                Win.Background = img;
            }
            catch (Exception)
            {
            }
            Hide(animate);
            Hide(Gquestion);
            Hide(FillQuestions);
            Show(Win);
            Continue.Visibility = Visibility.Collapsed;
            game.Player.incrementScore(game.Player.getHeartsTotal() * 100);
            Points.Text = "Points: " + game.Player.getScore();
            MyUser.Text = "Name: " + game.Player.getName() + Environment.NewLine + "Score: " + game.Player.getScore();
            OtherColumn1.Text = "Higest Players: " + Environment.NewLine;
            OtherColumn2.Text = "";
            OtherColumn3.Text = "";
            for (int i = 0; i < 5; i++)
            {
                Player x = game.getNextPlayer(i);
                if (x != null)
                {
                    if(i < 2)
                    {
                        OtherColumn1.Text += "Name: " + x.getName() + " Score: " + x.getScore() + Environment.NewLine;
                    }else if(i < 4)
                    {
                        OtherColumn2.Text += "Name: " + x.getName() + " Score: " + x.getScore() + Environment.NewLine;
                    }
                    else
                    {
                        OtherColumn1.Text += "Name: " + x.getName() + " Score: " + x.getScore() + Environment.NewLine;
                    }
                    
                }
            }*/

        }

        private void click_Retry(object sender, RoutedEventArgs e)
        {
            second.Visibility = Visibility.Collapsed;
            Gquestion.Visibility = Visibility.Collapsed;
            FillQuestions.Visibility = Visibility.Collapsed;
            animate.Visibility = Visibility.Collapsed;
            Win.Visibility = Visibility.Collapsed;
        }
    }
}
