using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private int p1ScoreHolder = -1;
        private int p2ScoreHolder = -1;

        public static MainWindow window;
        Border lParentP1;
        Border lParentP2;

        Pods podA;
        Pods podB;

        public MainWindow()
        {
            InitializeComponent();
            window = this;

            const long gameTime = 405555555;// 305555555:20s, 455555555: 48s, 555555555:1m, 300000:debug
            podA = new Pods("COM3", gameTime, true);
            podB = new Pods("COM4", gameTime, false);

            lParentP1 = window.p1AvatarABorder;
            lParentP2 = window.p2AvatarBBorder;

            
            // Open the ports at the same time to perform power on self test at same time         
            Thread powerOnPodA = new Thread(() => podA.openPort());
            Thread powerOnPodB = new Thread(() => podB.openPort());
            powerOnPodA.Start();
            powerOnPodB.Start();

            //wait until the devices are ready to begin operation
            powerOnPodA.Join();
            powerOnPodB.Join();

        }

        //remove border from previous avatar selection, identify the new selection and apply new thickness to the parent
        private void AvatarP1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lParentP1.BorderThickness = new Thickness(0);
            Border tBorder = (Border)((Image)sender).Parent;
            tBorder.BorderThickness = new Thickness(5);
            lParentP1 = tBorder;

            //Finally, update the players avatar with the corresponding image
            Image imgClk = (Image)sender;
            window.p1Avatar.Source = imgClk.Source;
        }

        //remove border from previous avatar selection, identify the new selection and apply new thickness to the parent
        private void AvatarP2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lParentP2.BorderThickness = new Thickness(0);
            Border tBorder = (Border)((Image)sender).Parent;
            tBorder.BorderThickness = new Thickness(5);
            lParentP2 = tBorder;

            //Finally, update the players avatar with the corresponding image
            Image imgClk = (Image)sender;
            window.p2Avatar.Source = imgClk.Source;
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {

            p1ScoreHolder = -1;
            p2ScoreHolder = -1;
            window.p1Score.Text = "0";
            window.p2Score.Text = "0";

            //disable the button
            btnStartGame.IsEnabled = !btnStartGame.IsEnabled;

            //start the games at the "same" time
            Thread GameAThreat = new Thread(() => podA.startGame()) { IsBackground = true };
            Thread GameBThreat = new Thread(() => podB.startGame()) { IsBackground = true };
            GameAThreat.Start();
            GameBThreat.Start();

            /* waitng till the game finishes here causes the GUI to freeze, and won't render score updates
            GameAThreat.Join();
            GameBThreat.Join();
            */


        }

        public void showWinner(bool isFirstPlayer, int score)
        {
            if (isFirstPlayer) { p1ScoreHolder = score; }
            else { p2ScoreHolder = score; }

            if (p1ScoreHolder != -1 && p2ScoreHolder != -1)
            {
                //Once both games have finished and updated theyr scores, launch the winners window
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    Image winnerImage = p1ScoreHolder > p2ScoreHolder ? window.p1Avatar : window.p2Avatar;
                    int winnerScore = p1ScoreHolder > p2ScoreHolder ? p1ScoreHolder : p2ScoreHolder;
                    WinnerWindow winnerWindow = new WinnerWindow(winnerImage, winnerScore);

                    winnerWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    winnerWindow.Show();
                    winnerWindow.WindowState = WindowState.Maximized;
                });

                //clear previous score
                p1ScoreHolder = -1;
                p2ScoreHolder = -1;
                
            }
            //reenable button
            //try this if not its gone
            MainWindow.window.Dispatcher.Invoke(new Action(delegate (){ MainWindow.window.btnStartGame.IsEnabled = true; }));

        }

        internal string p1ScoreCnt { set { Dispatcher.Invoke(new Action(() => { window.p1Score.Text = value; })); } }
        internal string p2ScoreCnt { set { Dispatcher.Invoke(new Action(() => { window.p2Score.Text = value; })); } }
    }
}
