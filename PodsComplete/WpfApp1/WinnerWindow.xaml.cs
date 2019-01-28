using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Globalization;
using System.IO.Ports;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for newMainWindow.xaml
    /// </summary>
    public partial class WinnerWindow : Window
    {
        private string playerOne;
        public string NameOfKid { get; set; }
        public string Scoreing { get; set; }

        public string SchoolOfKid { get; set; }

        public string[] NameArray { get; set; }
        public string[] ScoreArray { get; set; }
        public string[] SchoolArray { get; set; }

        string[] NameAndScore = new string[2];

        string[] NameAndScoreAndSchool = new string[3];

        public string PlayerOne { get; set; }


        //private SerialPort ports = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);

        bool Update = false;
        byte b = 0x00;
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "PODS";
        public static SpreadsheetsResource.ValuesResource.AppendRequest ValueRange { get; private set; }
        string[,] arrangedArrayString = new string[10, 2];

        string[,] arrangedArrayStringNew = new string[10, 3];

        UInt32 TopScore = 120000;
        DispatcherTimer Ready;

        DispatcherTimer timer;

        UInt32 P1Score = 0;
        UInt32 P2Score = 0;
        UInt16 ReadyCount;

        bool P1Done = false;
        bool P2Done = false;
        public WinnerWindow(Image winnerImage, int winnerScore)
        {
            try
            {
                UserCredential credential;

                using (var stream = new FileStream("client_secret_760998655212-jaiqgr057l0vlvoghc1dipf668mge3md.apps.googleusercontent.com.json"/*"credentials.json"*/, FileMode.Open, FileAccess.ReadWrite))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Sheets API service.
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define request parameters.
                // String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
                //"1tANpFI2yTk6vCX8HOLRlWT8-inTJf0zJw1RFYhhjgUI"
                String spreadsheetId = "1tANpFI2yTk6vCX8HOLRlWT8-inTJf0zJw1RFYhhjgUI";
                String range = "PODS!A2:C11"; //"PODS!A2:B6";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);

                // Prints the names and majors of students in a sample spreadsheet:
                ValueRange response = request.Execute();

                IList<IList<Object>> values = response.Values;

                IList<object> values3;

                //string[,] array = new string[values.Count,2];
                string[] array = new string[values.Count];
                string[] array2 = new string[values.Count];
                string[] array3 = new string[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    //array[i] = values[i];
                }

                if (values != null && values.Count > 0)
                {
                    Console.WriteLine("Name, Score");
                    int i = 0;
                    foreach (var row in values)
                    {
                        array[i] = row[0].ToString();
                        array2[i] = row[1].ToString();
                        array3[i] = row[2].ToString();
                        // Print columns A and E, which correspond to indices 0 and 4.
                        Console.WriteLine("{0}, {1} , {2}", row[0], row[1], row[2]);
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }

                /////////////////////////////////////////////////////////////////////////
                int[] Organised = new int[values.Count];
                int[] OutOfOrder = new int[values.Count];

                bool flag = true;
                int temp;
                int numLength = OutOfOrder.Length;

                for (int i = 0; i < values.Count; i++)
                {
                    OutOfOrder[i] = Int32.Parse(array2[i]);
                }

                string StoreName;
                string StoreScore;
                string SchoolScore;
                //sorting an array

                string[,] arrangedArray = new string[values.Count, 2];

                string[,] arrangedArrayNew = new string[values.Count, 3];

                for (int i = 0; i < values.Count; i++)
                {

                    arrangedArrayNew[i, 0] = array[i];
                    arrangedArrayNew[i, 1] = array2[i];
                    arrangedArrayNew[i, 2] = array3[i];

                }

                for (int i = 1; (i <= (numLength - 1)) && flag; i++)
                {
                    flag = false;
                    for (int j = 0; j < (numLength - 1); j++)
                    {
                        if (OutOfOrder[j + 1] > OutOfOrder[j])
                        {
                            temp = OutOfOrder[j];

                            StoreName = arrangedArrayNew[j, 0];
                            StoreScore = arrangedArrayNew[j, 1];
                            SchoolScore = arrangedArrayNew[j, 2];

                            OutOfOrder[j] = OutOfOrder[j + 1];

                            OutOfOrder[j + 1] = temp;

                            arrangedArrayNew[j, 0] = arrangedArrayNew[j + 1, 0];
                            arrangedArrayNew[j, 1] = arrangedArrayNew[j + 1, 1];
                            arrangedArrayNew[j, 2] = arrangedArrayNew[j + 1, 2];

                            arrangedArrayNew[j + 1, 0] = StoreName;
                            arrangedArrayNew[j + 1, 1] = StoreScore;
                            arrangedArrayNew[j + 1, 2] = SchoolScore;

                            flag = true;
                        }
                        else
                        {
                            temp = OutOfOrder[j];

                            StoreName = arrangedArrayNew[j, 0];
                            StoreScore = arrangedArrayNew[j, 1];
                            SchoolScore = arrangedArrayNew[j, 2];

                            arrangedArrayNew[j, 0] = StoreName;
                            arrangedArrayNew[j, 1] = StoreScore;
                            arrangedArrayNew[j, 2] = SchoolScore;

                        }
                    }
                }


                IList<Object> obj = new List<Object>();
                obj.Add(arrangedArrayNew[0, 0]);
                obj.Add(arrangedArrayNew[0, 1]);
                obj.Add(arrangedArrayNew[0, 2]);


                IList<Object> obj2 = new List<Object>();
                obj2.Add(arrangedArrayNew[1, 0]);
                obj2.Add(arrangedArrayNew[1, 1]);
                obj2.Add(arrangedArrayNew[1, 2]);


                IList<Object> obj3 = new List<Object>();
                obj3.Add(arrangedArrayNew[2, 0]);
                obj3.Add(arrangedArrayNew[2, 1]);
                obj3.Add(arrangedArrayNew[2, 2]);

                IList<Object> obj4 = new List<Object>();
                obj4.Add(arrangedArrayNew[3, 0]);
                obj4.Add(arrangedArrayNew[3, 1]);
                obj4.Add(arrangedArrayNew[3, 2]);

                IList<Object> obj5 = new List<Object>();
                obj5.Add(arrangedArrayNew[4, 0]);
                obj5.Add(arrangedArrayNew[4, 1]);
                obj5.Add(arrangedArrayNew[4, 2]);


                IList<Object> obj6 = new List<Object>();
                obj6.Add(arrangedArrayNew[5, 0]);
                obj6.Add(arrangedArrayNew[5, 1]);
                obj6.Add(arrangedArrayNew[5, 2]);

                IList<Object> obj7 = new List<Object>();
                obj7.Add(arrangedArrayNew[6, 0]);
                obj7.Add(arrangedArrayNew[6, 1]);
                obj7.Add(arrangedArrayNew[6, 2]);

                IList<Object> obj8 = new List<Object>();
                obj8.Add(arrangedArrayNew[7, 0]);
                obj8.Add(arrangedArrayNew[7, 1]);
                obj8.Add(arrangedArrayNew[7, 2]);

                IList<Object> obj9 = new List<Object>();
                obj9.Add(arrangedArrayNew[8, 0]);
                obj9.Add(arrangedArrayNew[8, 1]);
                obj9.Add(arrangedArrayNew[8, 2]);


                IList<Object> obj10 = new List<Object>();
                obj10.Add(arrangedArrayNew[9, 0]);
                obj10.Add(arrangedArrayNew[9, 1]);
                obj10.Add(arrangedArrayNew[9, 2]);


                int k = obj.Count;

                IList<IList<Object>> values2 = new List<IList<Object>>();
                values2.Add(obj);
                values2.Add(obj2);
                values2.Add(obj3);
                values2.Add(obj4);
                values2.Add(obj5);
                values2.Add(obj6);
                values2.Add(obj7);
                values2.Add(obj8);
                values2.Add(obj9);
                values2.Add(obj10);

                SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = service.Spreadsheets.Values.Update(new ValueRange() { Values = values2 }, spreadsheetId, range);
                //updateRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.;
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                var response2 = updateRequest.Execute();
                //SpreadsheetsResource.ValuesResource.AppendRequest append = service.Spreadsheets.Values.Append(ValueRange row2, spreadsheetId, "MARK");

                Console.WriteLine("\nRearranging scores highest to lowest");
                Console.WriteLine("\nName:\tScore:");
                for (int i = 0; i < values2.Count; i++)
                {
                    Console.WriteLine(arrangedArray[i, 0] + "\t" + arrangedArray[i, 1]);

                    arrangedArrayStringNew[i, 0] = arrangedArrayNew[i, 0];
                    arrangedArrayStringNew[i, 1] = arrangedArrayNew[i, 1];
                    arrangedArrayStringNew[i, 2] = arrangedArrayNew[i, 2];
                }
            }
            catch (Exception CodeTrial)
            {

            }


            try
            {

                string[] CompNames = new string[10];
                string[] ScoresOfComp = new string[10];
                string[] SchoolNames = new string[10];

                for (int i = 0; i < 10; i++)
                {

                    ScoresOfComp[i] = arrangedArrayStringNew[i, 1];
                    CompNames[i] = arrangedArrayStringNew[i, 0];
                    SchoolNames[i] = arrangedArrayStringNew[i, 2];

                }

                NameArray = new string[11];
                NameArray[0] = "Name";
                ScoreArray = new string[11];
                ScoreArray[0] = "Score";
                SchoolArray = new string[11];
                SchoolArray[0] = "School";


                for (int j = 1; j < 11; j++)
                {
                    NameArray[j] = (string)(CompNames[j - 1]);
                    ScoreArray[j] = (string)(ScoresOfComp[j - 1]);
                    SchoolArray[j] = (string)(SchoolNames[j - 1]);
                }


                InitializeComponent();
                winnerAvatarHolder.Source = winnerImage.Source;

                DataContext = this;


                Player1.Content = "Score";
                player1Score.Content = winnerScore;

            }
            catch (Exception e)
            {

            }
        }

        public void Start_click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            Ready = new DispatcherTimer();
            Ready.Interval = TimeSpan.FromMilliseconds(400);
            Ready.Tick += ready_timer;
            Ready.Start();
        }

        void ready_timer(object sender, EventArgs e)
        {
            Ready.Stop();
            System.Windows.Application.Current.Shutdown();
        }



        void timer_Tick(object sender, EventArgs e)
        {
        }

        private UInt32 CheckGame()
        {
            bool startBitRecieved = false;
            bool endByteRecieved = false;
            bool playerOneComplete = false;
            bool playerTwoComplete = false;


            if (P2Done == false)
            {
                P2Score = 0;
                //player2Score.Content = TopScore;
                //   P2Score = Topsc(UInt32)player2Score.Content;
            }

            if (P1Done == false)
            {
                // player1Score.Content = TopScore;
                P1Score = 0; //= (UInt32)player1Score.Content;
            }




            //ports.ReadTimeout = 10;

            // ports.ReadTimeout = 10;

            /*   while (!startBitRecieved)
               {
                   startBitRecieved = ports.ReadByte() == 0xa0 ? true : false;
               }*/

            try
            {
                b = 0;
                //  b = (byte)ports.ReadByte();
            }
            catch (Exception e)
            {
                return 0xffffffff;
            }

            try
            {
                if (b != null)
                {
                    switch (b)
                    {
                        case 50:
                            playerOneComplete = true;
                            break;
                        case 49:
                            playerTwoComplete = true;
                            break;
                        case 3:
                            playerOneComplete = true;
                            playerTwoComplete = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception Err)
            {
                return 0xffffffff;
            }

            //playerOneComplete = true;

            if (playerOneComplete)
            {
                player1Score.Content = TopScore;
                P1Score = (UInt32)player1Score.Content;
                P1Done = true;
                //OnPropertyChanged(() => this.PlayerOne);
            }


            if (playerTwoComplete)
            {
                //player2Score.Content = TopScore;
                // P2Score = (UInt32)player2Score.Content;
                P2Done = true;
                //OnPropertyChanged(() => this.playerTwo);
            }



            UInt32 chickenDinner = (playerOneComplete && playerTwoComplete) ?
                (P1Score > P2Score ? P1Score : P2Score) : 0xffffffff;

            return chickenDinner;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void Reset_click(object sender, RoutedEventArgs e)
        {

            StartButton.IsEnabled = false;
            ScoreEnteryButton.IsEnabled = false;
            RestartButton.IsEnabled = false;

            BindingExpression binding = RankingArray.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding2 = RankingArray2.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding3 = RankingArray3.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding4 = RankingArray4.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding5 = RankingArray5.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding6 = RankingArray6.GetBindingExpression(TextBox.TextProperty);

            BindingExpression Scorebinding = NamePos.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding2 = NamePos2.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding3 = NamePos3.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding4 = NamePos4.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding5 = NamePos5.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding6 = NamePos6.GetBindingExpression(TextBox.TextProperty);


            BindingExpression Schoolbinding = SchoolPos.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding2 = SchoolPos2.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding3 = SchoolPos3.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding4 = SchoolPos4.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding5 = SchoolPos5.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding6 = SchoolPos6.GetBindingExpression(TextBox.TextProperty);

            BindingExpression bindingName = NameField.GetBindingExpression(TextBox.TextProperty);
            BindingExpression bindingScore = ScoreField.GetBindingExpression(TextBox.TextProperty);
            BindingExpression bindingSchool = SchoolField.GetBindingExpression(TextBox.TextProperty);


            NameField.Clear();
            ScoreField.Clear();
            SchoolField.Clear();
            bindingName.UpdateSource();
            bindingSchool.UpdateSource();
            bindingScore.UpdateSource();



            try
            {
                UserCredential credential;

                using (var stream = new FileStream("client_secret_760998655212-jaiqgr057l0vlvoghc1dipf668mge3md.apps.googleusercontent.com.json"/*"credentials.json"*/, FileMode.Open, FileAccess.ReadWrite))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Sheets API service.
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define request parameters.
                // String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
                //"1tANpFI2yTk6vCX8HOLRlWT8-inTJf0zJw1RFYhhjgUI"
                String spreadsheetId = "1tANpFI2yTk6vCX8HOLRlWT8-inTJf0zJw1RFYhhjgUI";
                String range = "PODS!A2:C11"; //"PODS!A2:B6";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);

                // Prints the names and majors of students in a sample spreadsheet:
                ValueRange response = request.Execute();

                IList<IList<Object>> values = response.Values;

                IList<object> values3;

                //string[,] array = new string[values.Count,2];
                string[] array = new string[values.Count];
                string[] array2 = new string[values.Count];
                string[] array3 = new string[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    //array[i] = values[i];
                }

                if (values != null && values.Count > 0)
                {
                    Console.WriteLine("Name, Score");
                    int i = 0;
                    foreach (var row in values)
                    {
                        array[i] = row[0].ToString();
                        array2[i] = row[1].ToString();
                        array3[i] = row[2].ToString();
                        // Print columns A and E, which correspond to indices 0 and 4.
                        Console.WriteLine("{0}, {1} , {2}", row[0], row[1], row[2]);
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }

                /////////////////////////////////////////////////////////////////////////
                int[] Organised = new int[values.Count];
                int[] OutOfOrder = new int[values.Count];

                bool flag = true;
                int temp;
                int numLength = OutOfOrder.Length;

                for (int i = 0; i < values.Count; i++)
                {
                    OutOfOrder[i] = Int32.Parse(array2[i]);
                }

                string StoreName;
                string StoreScore;
                string SchoolScore;
                //sorting an array

                string[,] arrangedArray = new string[values.Count, 2];

                string[,] arrangedArrayNew = new string[values.Count, 3];

                for (int i = 0; i < values.Count; i++)
                {

                    arrangedArrayNew[i, 0] = array[i];
                    arrangedArrayNew[i, 1] = array2[i];
                    arrangedArrayNew[i, 2] = array3[i];

                }

                for (int i = 1; (i <= (numLength - 1)) && flag; i++)
                {
                    flag = false;
                    for (int j = 0; j < (numLength - 1); j++)
                    {
                        if (OutOfOrder[j + 1] > OutOfOrder[j])
                        {
                            temp = OutOfOrder[j];

                            StoreName = arrangedArrayNew[j, 0];
                            StoreScore = arrangedArrayNew[j, 1];
                            SchoolScore = arrangedArrayNew[j, 2];

                            OutOfOrder[j] = OutOfOrder[j + 1];

                            OutOfOrder[j + 1] = temp;

                            arrangedArrayNew[j, 0] = arrangedArrayNew[j + 1, 0];
                            arrangedArrayNew[j, 1] = arrangedArrayNew[j + 1, 1];
                            arrangedArrayNew[j, 2] = arrangedArrayNew[j + 1, 2];

                            arrangedArrayNew[j + 1, 0] = StoreName;
                            arrangedArrayNew[j + 1, 1] = StoreScore;
                            arrangedArrayNew[j + 1, 2] = SchoolScore;

                            flag = true;
                        }
                        else
                        {
                            temp = OutOfOrder[j];

                            StoreName = arrangedArrayNew[j, 0];
                            StoreScore = arrangedArrayNew[j, 1];
                            SchoolScore = arrangedArrayNew[j, 2];

                            arrangedArrayNew[j, 0] = StoreName;
                            arrangedArrayNew[j, 1] = StoreScore;
                            arrangedArrayNew[j, 2] = SchoolScore;

                        }
                    }
                }


                IList<Object> obj = new List<Object>();
                obj.Add(arrangedArrayNew[0, 0]);
                obj.Add(arrangedArrayNew[0, 1]);
                obj.Add(arrangedArrayNew[0, 2]);


                IList<Object> obj2 = new List<Object>();
                obj2.Add(arrangedArrayNew[1, 0]);
                obj2.Add(arrangedArrayNew[1, 1]);
                obj2.Add(arrangedArrayNew[1, 2]);


                IList<Object> obj3 = new List<Object>();
                obj3.Add(arrangedArrayNew[2, 0]);
                obj3.Add(arrangedArrayNew[2, 1]);
                obj3.Add(arrangedArrayNew[2, 2]);

                IList<Object> obj4 = new List<Object>();
                obj4.Add(arrangedArrayNew[3, 0]);
                obj4.Add(arrangedArrayNew[3, 1]);
                obj4.Add(arrangedArrayNew[3, 2]);

                IList<Object> obj5 = new List<Object>();
                obj5.Add(arrangedArrayNew[4, 0]);
                obj5.Add(arrangedArrayNew[4, 1]);
                obj5.Add(arrangedArrayNew[4, 2]);


                IList<Object> obj6 = new List<Object>();
                obj6.Add(arrangedArrayNew[5, 0]);
                obj6.Add(arrangedArrayNew[5, 1]);
                obj6.Add(arrangedArrayNew[5, 2]);

                IList<Object> obj7 = new List<Object>();
                obj7.Add(arrangedArrayNew[6, 0]);
                obj7.Add(arrangedArrayNew[6, 1]);
                obj7.Add(arrangedArrayNew[6, 2]);

                IList<Object> obj8 = new List<Object>();
                obj8.Add(arrangedArrayNew[7, 0]);
                obj8.Add(arrangedArrayNew[7, 1]);
                obj8.Add(arrangedArrayNew[7, 2]);

                IList<Object> obj9 = new List<Object>();
                obj9.Add(arrangedArrayNew[8, 0]);
                obj9.Add(arrangedArrayNew[8, 1]);
                obj9.Add(arrangedArrayNew[8, 2]);


                IList<Object> obj10 = new List<Object>();
                obj10.Add(arrangedArrayNew[9, 0]);
                obj10.Add(arrangedArrayNew[9, 1]);
                obj10.Add(arrangedArrayNew[9, 2]);


                int k = obj.Count;

                IList<IList<Object>> values2 = new List<IList<Object>>();
                values2.Add(obj);
                values2.Add(obj2);
                values2.Add(obj3);
                values2.Add(obj4);
                values2.Add(obj5);
                values2.Add(obj6);
                values2.Add(obj7);
                values2.Add(obj8);
                values2.Add(obj9);
                values2.Add(obj10);

                SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = service.Spreadsheets.Values.Update(new ValueRange() { Values = values2 }, spreadsheetId, range);
                //updateRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.;
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                var response2 = updateRequest.Execute();
                //SpreadsheetsResource.ValuesResource.AppendRequest append = service.Spreadsheets.Values.Append(ValueRange row2, spreadsheetId, "MARK");

                Console.WriteLine("\nRearranging scores highest to lowest");
                Console.WriteLine("\nName:\tScore:");
                for (int i = 0; i < values2.Count; i++)
                {
                    Console.WriteLine(arrangedArray[i, 0] + "\t" + arrangedArray[i, 1]);

                    arrangedArrayStringNew[i, 0] = arrangedArrayNew[i, 0];
                    arrangedArrayStringNew[i, 1] = arrangedArrayNew[i, 1];
                    arrangedArrayStringNew[i, 2] = arrangedArrayNew[i, 2];
                }


                string[] CompNames = new string[10];
                string[] ScoresOfComp = new string[10];
                string[] SchoolNames = new string[10];

                for (int i = 0; i < 10; i++)
                {

                    ScoresOfComp[i] = arrangedArrayStringNew[i, 1];
                    CompNames[i] = arrangedArrayStringNew[i, 0];
                    SchoolNames[i] = arrangedArrayStringNew[i, 2];

                }

                NameArray[0] = "Name";
                ScoreArray[0] = "Score";
                SchoolArray[0] = "School";


                for (int j = 1; j < 11; j++)
                {
                    NameArray[j] = (string)(CompNames[j - 1]);
                    ScoreArray[j] = (string)(ScoresOfComp[j - 1]);
                    SchoolArray[j] = (string)(SchoolNames[j - 1]);
                }

                //InitializeComponent();
                RankingArray.Text = ScoreArray[0];
                RankingArray2.Text = ScoreArray[1];
                RankingArray3.Text = ScoreArray[2];
                RankingArray4.Text = ScoreArray[3];
                RankingArray5.Text = ScoreArray[4];
                RankingArray6.Text = ScoreArray[5];

                NamePos.Text = NameArray[0];
                NamePos2.Text = NameArray[1];
                NamePos3.Text = NameArray[2];
                NamePos4.Text = NameArray[3];
                NamePos5.Text = NameArray[4];
                NamePos6.Text = NameArray[5];

                SchoolPos.Text = SchoolArray[0];
                SchoolPos2.Text = SchoolArray[1];
                SchoolPos3.Text = SchoolArray[2];
                SchoolPos4.Text = SchoolArray[3];
                SchoolPos5.Text = SchoolArray[4];
                SchoolPos6.Text = SchoolArray[5];
            }
            catch (Exception errRelaod)
            {
            }
            RestartButton.IsEnabled = true;
            StartButton.IsEnabled = true;
            ScoreEnteryButton.IsEnabled = true;
        }


        public void BUtton_click(object sender, RoutedEventArgs e)
        {
            ScoreEnteryButton.IsEnabled = false;
            RestartButton.IsEnabled = false;
            StartButton.IsEnabled = false;

            BindingExpression binding = RankingArray.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding2 = RankingArray2.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding3 = RankingArray3.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding4 = RankingArray4.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding5 = RankingArray5.GetBindingExpression(TextBox.TextProperty);
            BindingExpression binding6 = RankingArray6.GetBindingExpression(TextBox.TextProperty);

            BindingExpression Scorebinding = NamePos.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding2 = NamePos2.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding3 = NamePos3.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding4 = NamePos4.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding5 = NamePos5.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Scorebinding6 = NamePos6.GetBindingExpression(TextBox.TextProperty);


            BindingExpression Schoolbinding = SchoolPos.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding2 = SchoolPos2.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding3 = SchoolPos3.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding4 = SchoolPos4.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding5 = SchoolPos5.GetBindingExpression(TextBox.TextProperty);
            BindingExpression Schoolbinding6 = SchoolPos6.GetBindingExpression(TextBox.TextProperty);

            BindingExpression bindingName = NameField.GetBindingExpression(TextBox.TextProperty);
            BindingExpression bindingScore = ScoreField.GetBindingExpression(TextBox.TextProperty);
            BindingExpression bindingSchool = SchoolField.GetBindingExpression(TextBox.TextProperty);
            try
            {
                char[] Newchecker = new char[65536];
                char[] Schoolchecker = new char[65536];

                bool invalidCharacters = false;
                bool InvalidNamelength = false;
                bool invalidSchoolCharacters = false;
                bool InvalidSchoolNamelength = false;
                bool SpaceInName = false;

                if (NameOfKid == null)
                {
                    Newchecker[0] = '0';
                }
                else
                {
                    Newchecker = NameOfKid.ToCharArray();
                }


                for (int i = 0; i < Newchecker.Length; i++)
                {
                    if ((Newchecker.Length == 0) | ((Newchecker[i] <= 31) | ((Newchecker[i] <= 38) & (Newchecker[i] >= 33)) | ((Newchecker[i] <= 64) & (Newchecker[i] >= 40)) | (Newchecker[i] >= 91 & Newchecker[i] <= 96) | (Newchecker[i] >= 123)))
                    {
                        invalidCharacters = true;
                    }

                }

                if (Newchecker.Length >= 18)
                {
                    InvalidNamelength = true;
                }

                if (SchoolOfKid == null)
                {
                    Schoolchecker[0] = '0';
                }
                else
                {
                    Schoolchecker = SchoolOfKid.ToCharArray();
                }

                for (int i = 0; i < Schoolchecker.Length; i++)
                {
                    if ((Schoolchecker.Length == 0) | ((Schoolchecker[i] <= 31) | ((Schoolchecker[i] <= 38) & (Schoolchecker[i] >= 33)) | ((Schoolchecker[i] <= 64) & (Schoolchecker[i] >= 40)) | (Schoolchecker[i] >= 91 & Schoolchecker[i] <= 96) | (Schoolchecker[i] >= 123)))
                    {
                        invalidSchoolCharacters = true;
                    }

                }

                if (Schoolchecker.Length >= 18)
                {
                    InvalidSchoolNamelength = true;
                }


                char[] scoreChecker = new char[65536];

                if (Scoreing == null)
                {
                    scoreChecker[0] = '@';
                }
                else
                {
                    scoreChecker = Scoreing.ToCharArray();
                }

                if (invalidCharacters == true)
                {
                    //                    NameField.Text = "";
                    NameField.Clear();
                    bindingName.UpdateSource();
                    Update = false;
                    MessageBox.Show("Please enter an actual name for the player", "", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else if (InvalidNamelength == true)
                {
                    Update = false;
                    NameField.Clear();
                    bindingName.UpdateSource();
                    MessageBox.Show("Please enter a players name less than 18 characters long ", "", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else if (invalidSchoolCharacters == true)
                {
                    SchoolField.Clear();
                    bindingSchool.UpdateSource();
                    Update = false;
                    MessageBox.Show("Please enter an actual school name", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (InvalidSchoolNamelength == true)
                {
                    SchoolField.Clear();
                    bindingSchool.UpdateSource();
                    Update = false;
                    MessageBox.Show("Please enter a school name less then 18 characters long", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if ((scoreChecker[0] <= 47 || scoreChecker[0] >= 58) || (scoreChecker.Length == 0))
                {
                    Update = false;
                    ScoreField.Clear();
                    bindingScore.UpdateSource();
                    MessageBox.Show("Please enter a number greater than or equal to zero", "", MessageBoxButton.OK);
                }
                else
                {
                    //MessageBox.Show("Name and Score has been submitted updating leaderboard", "", MessageBoxButton.OK);
                    bool Correction = false;
                    int CorrectionPos = 1;
                    if (scoreChecker.Length > 1)
                    {
                        while (Correction == false)
                        {
                            if (scoreChecker[CorrectionPos] <= 47 || scoreChecker[CorrectionPos] >= 58)
                            {
                                Scoreing = Scoreing.Remove(CorrectionPos);
                                Correction = true;
                            }
                            else if (CorrectionPos == (scoreChecker.Length - 1))
                            {
                                Correction = true;
                            }
                            CorrectionPos++;
                        }
                    }
                    else
                    {

                    }

                    NameAndScoreAndSchool[0] = NameOfKid;
                    NameAndScoreAndSchool[1] = Scoreing;
                    NameAndScoreAndSchool[2] = SchoolOfKid;

                    Update = true;
                    ScoreField.Clear();
                    NameField.Clear();
                    SchoolField.Clear();
                    bindingScore.UpdateSource();
                }



                if (Update == true)
                {
                    try
                    {
                        UserCredential credential;

                        using (var stream = new FileStream("client_secret_760998655212-jaiqgr057l0vlvoghc1dipf668mge3md.apps.googleusercontent.com.json"/*"credentials.json"*/, FileMode.Open, FileAccess.ReadWrite))
                        {
                            string credPath = "token.json";
                            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                GoogleClientSecrets.Load(stream).Secrets,
                                Scopes,
                                "user",
                                CancellationToken.None,
                                new FileDataStore(credPath, true)).Result;
                            Console.WriteLine("Credential file saved to: " + credPath);
                        }

                        // Create Google Sheets API service.
                        var service = new SheetsService(new BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = ApplicationName,
                        });

                        // Define request parameters.
                        // String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
                        String spreadsheetId = "1tANpFI2yTk6vCX8HOLRlWT8-inTJf0zJw1RFYhhjgUI";
                        String range = "PODS!A2:C11"; //"PODS!A2:B6";
                        SpreadsheetsResource.ValuesResource.GetRequest request =
                                service.Spreadsheets.Values.Get(spreadsheetId, range);

                        // Prints the names and majors of students in a sample spreadsheet:
                        ValueRange response = request.Execute();

                        IList<IList<Object>> values = response.Values;

                        IList<object> values3;

                        //string[,] array = new string[values.Count,2];
                        string[] array = new string[values.Count];
                        string[] array2 = new string[values.Count];
                        string[] array3 = new string[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            //array[i] = values[i];
                        }

                        if (values != null && values.Count > 0)
                        {
                            Console.WriteLine("Name, Score");
                            int i = 0;
                            foreach (var row in values)
                            {
                                array[i] = row[0].ToString();
                                array2[i] = row[1].ToString();
                                array3[i] = row[2].ToString();
                                // Print columns A and E, which correspond to indices 0 and 4.
                                Console.WriteLine("{0}, {1} , {2}", row[0], row[1], row[2]);
                                i++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data found.");
                        }

                        /////////////////////////////////////////////////////////////////////////
                        int[] Organised = new int[values.Count];
                        int[] OutOfOrder = new int[values.Count];

                        bool flag = true;
                        int temp;
                        int numLength = OutOfOrder.Length;

                        for (int i = 0; i < values.Count; i++)
                        {
                            OutOfOrder[i] = Int32.Parse(array2[i]);
                        }

                        string StoreName;
                        string StoreScore;
                        string SchoolScore;
                        //sorting an array

                        string[,] arrangedArray = new string[values.Count, 2];

                        string[,] arrangedArrayNew = new string[values.Count, 3];

                        for (int i = 0; i < values.Count; i++)
                        {

                            arrangedArrayNew[i, 0] = array[i];
                            arrangedArrayNew[i, 1] = array2[i];
                            arrangedArrayNew[i, 2] = array3[i];

                        }

                        for (int i = 1; (i <= (numLength - 1)) && flag; i++)
                        {
                            flag = false;
                            for (int j = 0; j < (numLength - 1); j++)
                            {
                                if (OutOfOrder[j + 1] > OutOfOrder[j])
                                {
                                    temp = OutOfOrder[j];

                                    StoreName = arrangedArrayNew[j, 0];
                                    StoreScore = arrangedArrayNew[j, 1];
                                    SchoolScore = arrangedArrayNew[j, 2];

                                    OutOfOrder[j] = OutOfOrder[j + 1];

                                    OutOfOrder[j + 1] = temp;

                                    arrangedArrayNew[j, 0] = arrangedArrayNew[j + 1, 0];
                                    arrangedArrayNew[j, 1] = arrangedArrayNew[j + 1, 1];
                                    arrangedArrayNew[j, 2] = arrangedArrayNew[j + 1, 2];

                                    arrangedArrayNew[j + 1, 0] = StoreName;
                                    arrangedArrayNew[j + 1, 1] = StoreScore;
                                    arrangedArrayNew[j + 1, 2] = SchoolScore;

                                    flag = true;
                                }
                                else
                                {
                                    temp = OutOfOrder[j];



                                    StoreName = arrangedArrayNew[j, 0];
                                    StoreScore = arrangedArrayNew[j, 1];
                                    SchoolScore = arrangedArrayNew[j, 2];

                                    arrangedArrayNew[j, 0] = StoreName;
                                    arrangedArrayNew[j, 1] = StoreScore;
                                    arrangedArrayNew[j, 2] = SchoolScore;

                                }
                            }
                        }
                        //////////////////////////////////////////////////////////////////////////


                        IList<Object> obj = new List<Object>();
                        obj.Add(arrangedArrayNew[0, 0]);
                        obj.Add(arrangedArrayNew[0, 1]);
                        obj.Add(arrangedArrayNew[0, 2]);


                        IList<Object> obj2 = new List<Object>();
                        obj2.Add(arrangedArrayNew[1, 0]);
                        obj2.Add(arrangedArrayNew[1, 1]);
                        obj2.Add(arrangedArrayNew[1, 2]);


                        IList<Object> obj3 = new List<Object>();
                        obj3.Add(arrangedArrayNew[2, 0]);
                        obj3.Add(arrangedArrayNew[2, 1]);
                        obj3.Add(arrangedArrayNew[2, 2]);

                        IList<Object> obj4 = new List<Object>();
                        obj4.Add(arrangedArrayNew[3, 0]);
                        obj4.Add(arrangedArrayNew[3, 1]);
                        obj4.Add(arrangedArrayNew[3, 2]);

                        IList<Object> obj5 = new List<Object>();
                        obj5.Add(arrangedArrayNew[4, 0]);
                        obj5.Add(arrangedArrayNew[4, 1]);
                        obj5.Add(arrangedArrayNew[4, 2]);


                        IList<Object> obj6 = new List<Object>();
                        obj6.Add(arrangedArrayNew[5, 0]);
                        obj6.Add(arrangedArrayNew[5, 1]);
                        obj6.Add(arrangedArrayNew[5, 2]);

                        IList<Object> obj7 = new List<Object>();
                        obj7.Add(arrangedArrayNew[6, 0]);
                        obj7.Add(arrangedArrayNew[6, 1]);
                        obj7.Add(arrangedArrayNew[6, 2]);

                        IList<Object> obj8 = new List<Object>();
                        obj8.Add(arrangedArrayNew[7, 0]);
                        obj8.Add(arrangedArrayNew[7, 1]);
                        obj8.Add(arrangedArrayNew[7, 2]);

                        IList<Object> obj9 = new List<Object>();
                        obj9.Add(arrangedArrayNew[8, 0]);
                        obj9.Add(arrangedArrayNew[8, 1]);
                        obj9.Add(arrangedArrayNew[8, 2]);


                        IList<Object> obj10 = new List<Object>();
                        obj10.Add(arrangedArrayNew[9, 0]);
                        obj10.Add(arrangedArrayNew[9, 1]);
                        obj10.Add(arrangedArrayNew[9, 2]);


                        int k = obj.Count;

                        IList<IList<Object>> values2 = new List<IList<Object>>();
                        values2.Add(obj);
                        values2.Add(obj2);
                        values2.Add(obj3);
                        values2.Add(obj4);
                        values2.Add(obj5);
                        values2.Add(obj6);
                        values2.Add(obj7);
                        values2.Add(obj8);
                        values2.Add(obj9);
                        values2.Add(obj10);

                        SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = service.Spreadsheets.Values.Update(new ValueRange() { Values = values2 }, spreadsheetId, range);
                        //updateRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.;
                        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                        var response2 = updateRequest.Execute();
                        //SpreadsheetsResource.ValuesResource.AppendRequest append = service.Spreadsheets.Values.Append(ValueRange row2, spreadsheetId, "MARK");

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //
                        //          added the update here
                        //
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        NumberStyles style = NumberStyles.AllowDecimalPoint;

                        //uint Newscore = UInt32.Parse(NameAndScore[1], style);

                        uint Newscore = UInt32.Parse(NameAndScoreAndSchool[1], style);
                        string[] UpdateArray = new string[((values.Count) + 1)];
                        string[] UpdateArray2 = new string[((values.Count) + 1)];
                        string[] UpdateArray3 = new string[((values.Count) + 1)];

                        UpdateArray[(values.Count)] = NameAndScoreAndSchool[0];
                        UpdateArray2[(values.Count)] = NameAndScoreAndSchool[1];
                        UpdateArray3[(values.Count)] = NameAndScoreAndSchool[2];

                        if (values != null && values.Count > 0)
                        {
                            int i = 0;
                            foreach (var row in values)
                            {
                                UpdateArray[i] = row[0].ToString();
                                UpdateArray2[i] = row[1].ToString();
                                UpdateArray3[i] = row[2].ToString();
                                i++;
                            }
                        }
                        else
                        {
                        }


                        int[] Organised2 = new int[((values.Count) + 1)];
                        int[] OutOfOrder2 = new int[((values.Count) + 1)];
                        flag = true;
                        numLength = OutOfOrder2.Length;

                        for (int i = 0; i < (values.Count + 1); i++)
                        {
                            OutOfOrder2[i] = Int32.Parse(UpdateArray2[i]);
                        }

                        //sorting an array

                        string[,] arrangedArray2 = new string[((values.Count) + 1), 2];


                        string[,] arrangedArrayNew2 = new string[((values.Count) + 1), 3];

                        for (int i = 0; i < (values.Count + 1); i++)
                        {

                            arrangedArrayNew2[i, 0] = UpdateArray[i];
                            arrangedArrayNew2[i, 1] = UpdateArray2[i];
                            arrangedArrayNew2[i, 2] = UpdateArray3[i];

                        }

                        UInt16 UpdatePos = 0;
                        UInt32 Newcore = UInt32.Parse(NameAndScoreAndSchool[1]);
                        UInt32 ScoreCompare = 0;
                        bool UpdatePosFound = false;

                        for (UInt16 i = 0; i < numLength; i++)
                        {
                            ScoreCompare = UInt32.Parse(arrangedArrayNew2[i, 1]);
                            if (Newcore >= ScoreCompare && !UpdatePosFound)
                            {
                                UpdatePos = i;
                                UpdatePosFound = true;
                            }
                        }


                        switch (UpdatePos)
                        {
                            case 0:

                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = arrangedArrayNew2[6, 0];
                                arrangedArrayNew2[7, 1] = arrangedArrayNew2[6, 1];
                                arrangedArrayNew2[7, 2] = arrangedArrayNew2[6, 2];

                                arrangedArrayNew2[6, 0] = arrangedArrayNew2[5, 0];
                                arrangedArrayNew2[6, 1] = arrangedArrayNew2[5, 1];
                                arrangedArrayNew2[6, 2] = arrangedArrayNew2[5, 2];

                                arrangedArrayNew2[5, 0] = arrangedArrayNew2[4, 0];
                                arrangedArrayNew2[5, 1] = arrangedArrayNew2[4, 1];
                                arrangedArrayNew2[5, 2] = arrangedArrayNew2[4, 2];

                                arrangedArrayNew2[4, 0] = arrangedArrayNew2[3, 0];
                                arrangedArrayNew2[4, 1] = arrangedArrayNew2[3, 1];
                                arrangedArrayNew2[4, 2] = arrangedArrayNew2[3, 2];

                                arrangedArrayNew2[3, 0] = arrangedArrayNew2[2, 0];
                                arrangedArrayNew2[3, 1] = arrangedArrayNew2[2, 1];
                                arrangedArrayNew2[3, 2] = arrangedArrayNew2[2, 2];

                                arrangedArrayNew2[2, 0] = arrangedArrayNew2[1, 0];
                                arrangedArrayNew2[2, 1] = arrangedArrayNew2[1, 1];
                                arrangedArrayNew2[2, 2] = arrangedArrayNew2[1, 2];

                                arrangedArrayNew2[1, 0] = arrangedArrayNew2[0, 0];
                                arrangedArrayNew2[1, 1] = arrangedArrayNew2[0, 1];
                                arrangedArrayNew2[1, 2] = arrangedArrayNew2[0, 2];

                                arrangedArrayNew2[0, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[0, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[0, 2] = NameAndScoreAndSchool[2];
                                break;
                            case 1:

                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = arrangedArrayNew2[6, 0];
                                arrangedArrayNew2[7, 1] = arrangedArrayNew2[6, 1];
                                arrangedArrayNew2[7, 2] = arrangedArrayNew2[6, 2];

                                arrangedArrayNew2[6, 0] = arrangedArrayNew2[5, 0];
                                arrangedArrayNew2[6, 1] = arrangedArrayNew2[5, 1];
                                arrangedArrayNew2[6, 2] = arrangedArrayNew2[5, 2];

                                arrangedArrayNew2[5, 0] = arrangedArrayNew2[4, 0];
                                arrangedArrayNew2[5, 1] = arrangedArrayNew2[4, 1];
                                arrangedArrayNew2[5, 2] = arrangedArrayNew2[4, 2];

                                arrangedArrayNew2[4, 0] = arrangedArrayNew2[3, 0];
                                arrangedArrayNew2[4, 1] = arrangedArrayNew2[3, 1];
                                arrangedArrayNew2[4, 2] = arrangedArrayNew2[3, 2];

                                arrangedArrayNew2[3, 0] = arrangedArrayNew2[2, 0];
                                arrangedArrayNew2[3, 1] = arrangedArrayNew2[2, 1];
                                arrangedArrayNew2[3, 2] = arrangedArrayNew2[2, 2];

                                arrangedArrayNew2[2, 0] = arrangedArrayNew2[1, 0];
                                arrangedArrayNew2[2, 1] = arrangedArrayNew2[1, 1];
                                arrangedArrayNew2[2, 2] = arrangedArrayNew2[1, 2];

                                arrangedArrayNew2[1, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[1, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[1, 2] = NameAndScoreAndSchool[2];

                                break;

                            case 2:

                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = arrangedArrayNew2[6, 0];
                                arrangedArrayNew2[7, 1] = arrangedArrayNew2[6, 1];
                                arrangedArrayNew2[7, 2] = arrangedArrayNew2[6, 2];

                                arrangedArrayNew2[6, 0] = arrangedArrayNew2[5, 0];
                                arrangedArrayNew2[6, 1] = arrangedArrayNew2[5, 1];
                                arrangedArrayNew2[6, 2] = arrangedArrayNew2[5, 2];

                                arrangedArrayNew2[5, 0] = arrangedArrayNew2[4, 0];
                                arrangedArrayNew2[5, 1] = arrangedArrayNew2[4, 1];
                                arrangedArrayNew2[5, 2] = arrangedArrayNew2[4, 2];

                                arrangedArrayNew2[4, 0] = arrangedArrayNew2[3, 0];
                                arrangedArrayNew2[4, 1] = arrangedArrayNew2[3, 1];
                                arrangedArrayNew2[4, 2] = arrangedArrayNew2[3, 2];

                                arrangedArrayNew2[3, 0] = arrangedArrayNew2[2, 0];
                                arrangedArrayNew2[3, 1] = arrangedArrayNew2[2, 1];
                                arrangedArrayNew2[3, 2] = arrangedArrayNew2[2, 2];

                                arrangedArrayNew2[2, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[2, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[2, 2] = NameAndScoreAndSchool[2];

                                break;

                            case 3:
                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = arrangedArrayNew2[6, 0];
                                arrangedArrayNew2[7, 1] = arrangedArrayNew2[6, 1];
                                arrangedArrayNew2[7, 2] = arrangedArrayNew2[6, 2];

                                arrangedArrayNew2[6, 0] = arrangedArrayNew2[5, 0];
                                arrangedArrayNew2[6, 1] = arrangedArrayNew2[5, 1];
                                arrangedArrayNew2[6, 2] = arrangedArrayNew2[5, 2];

                                arrangedArrayNew2[5, 0] = arrangedArrayNew2[4, 0];
                                arrangedArrayNew2[5, 1] = arrangedArrayNew2[4, 1];
                                arrangedArrayNew2[5, 2] = arrangedArrayNew2[4, 2];

                                arrangedArrayNew2[4, 0] = arrangedArrayNew2[3, 0];
                                arrangedArrayNew2[4, 1] = arrangedArrayNew2[3, 1];
                                arrangedArrayNew2[4, 2] = arrangedArrayNew2[3, 2];

                                arrangedArrayNew2[3, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[3, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[3, 2] = NameAndScoreAndSchool[2];

                                break;

                            case 4:
                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = arrangedArrayNew2[6, 0];
                                arrangedArrayNew2[7, 1] = arrangedArrayNew2[6, 1];
                                arrangedArrayNew2[7, 2] = arrangedArrayNew2[6, 2];

                                arrangedArrayNew2[6, 0] = arrangedArrayNew2[5, 0];
                                arrangedArrayNew2[6, 1] = arrangedArrayNew2[5, 1];
                                arrangedArrayNew2[6, 2] = arrangedArrayNew2[5, 2];

                                arrangedArrayNew2[5, 0] = arrangedArrayNew2[4, 0];
                                arrangedArrayNew2[5, 1] = arrangedArrayNew2[4, 1];
                                arrangedArrayNew2[5, 2] = arrangedArrayNew2[4, 2];

                                arrangedArrayNew2[4, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[4, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[4, 2] = NameAndScoreAndSchool[2];

                                break;

                            case 5:
                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = arrangedArrayNew2[6, 0];
                                arrangedArrayNew2[7, 1] = arrangedArrayNew2[6, 1];
                                arrangedArrayNew2[7, 2] = arrangedArrayNew2[6, 2];

                                arrangedArrayNew2[6, 0] = arrangedArrayNew2[5, 0];
                                arrangedArrayNew2[6, 1] = arrangedArrayNew2[5, 1];
                                arrangedArrayNew2[6, 2] = arrangedArrayNew2[5, 2];

                                arrangedArrayNew2[5, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[5, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[5, 2] = NameAndScoreAndSchool[2];

                                break;

                            case 6:
                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = arrangedArrayNew2[6, 0];
                                arrangedArrayNew2[7, 1] = arrangedArrayNew2[6, 1];
                                arrangedArrayNew2[7, 2] = arrangedArrayNew2[6, 2];

                                arrangedArrayNew2[6, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[6, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[6, 2] = NameAndScoreAndSchool[2];
                                break;

                            case 7:
                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = arrangedArrayNew2[7, 0];
                                arrangedArrayNew2[8, 1] = arrangedArrayNew2[7, 1];
                                arrangedArrayNew2[8, 2] = arrangedArrayNew2[7, 2];

                                arrangedArrayNew2[7, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[7, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[7, 2] = NameAndScoreAndSchool[2];

                                break;

                            case 8:
                                arrangedArrayNew2[9, 0] = arrangedArrayNew2[8, 0];
                                arrangedArrayNew2[9, 1] = arrangedArrayNew2[8, 1];
                                arrangedArrayNew2[9, 2] = arrangedArrayNew2[8, 2];

                                arrangedArrayNew2[8, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[8, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[8, 2] = NameAndScoreAndSchool[2];
                                break;

                            case 9:

                                arrangedArrayNew2[9, 0] = NameAndScoreAndSchool[0];
                                arrangedArrayNew2[9, 1] = NameAndScoreAndSchool[1];
                                arrangedArrayNew2[9, 2] = NameAndScoreAndSchool[2];
                                break;
                        }

                        IList<Object> objHighScore = new List<Object>();
                        objHighScore.Add(arrangedArrayNew2[0, 0]);
                        objHighScore.Add(arrangedArrayNew2[0, 1]);
                        objHighScore.Add(arrangedArrayNew2[0, 2]);


                        IList<Object> objHighScore2 = new List<Object>();
                        objHighScore2.Add(arrangedArrayNew2[1, 0]);
                        objHighScore2.Add(arrangedArrayNew2[1, 1]);
                        objHighScore2.Add(arrangedArrayNew2[1, 2]);


                        IList<Object> objHighScore3 = new List<Object>();
                        objHighScore3.Add(arrangedArrayNew2[2, 0]);
                        objHighScore3.Add(arrangedArrayNew2[2, 1]);
                        objHighScore3.Add(arrangedArrayNew2[2, 2]);

                        IList<Object> objHighScore4 = new List<Object>();
                        objHighScore4.Add(arrangedArrayNew2[3, 0]);
                        objHighScore4.Add(arrangedArrayNew2[3, 1]);
                        objHighScore4.Add(arrangedArrayNew2[3, 2]);

                        IList<Object> objHighScore5 = new List<Object>();
                        objHighScore5.Add(arrangedArrayNew2[4, 0]);
                        objHighScore5.Add(arrangedArrayNew2[4, 1]);
                        objHighScore5.Add(arrangedArrayNew2[4, 2]);


                        IList<Object> objHighScore6 = new List<Object>();
                        objHighScore6.Add(arrangedArrayNew2[5, 0]);
                        objHighScore6.Add(arrangedArrayNew2[5, 1]);
                        objHighScore6.Add(arrangedArrayNew2[5, 2]);

                        IList<Object> objHighScore7 = new List<Object>();
                        objHighScore7.Add(arrangedArrayNew2[6, 0]);
                        objHighScore7.Add(arrangedArrayNew2[6, 1]);
                        objHighScore7.Add(arrangedArrayNew2[6, 2]);

                        IList<Object> objHighScore8 = new List<Object>();
                        objHighScore8.Add(arrangedArrayNew2[7, 0]);
                        objHighScore8.Add(arrangedArrayNew2[7, 1]);
                        objHighScore8.Add(arrangedArrayNew2[7, 2]);

                        IList<Object> objHighScore9 = new List<Object>();
                        objHighScore9.Add(arrangedArrayNew2[8, 0]);
                        objHighScore9.Add(arrangedArrayNew2[8, 1]);
                        objHighScore9.Add(arrangedArrayNew2[8, 2]);


                        IList<Object> objHighScore10 = new List<Object>();
                        objHighScore10.Add(arrangedArrayNew2[9, 0]);
                        objHighScore10.Add(arrangedArrayNew2[9, 1]);
                        objHighScore10.Add(arrangedArrayNew2[9, 2]);




                        IList<IList<Object>> valuesHighSCore = new List<IList<Object>>();
                        valuesHighSCore.Add(objHighScore);
                        valuesHighSCore.Add(objHighScore2);
                        valuesHighSCore.Add(objHighScore3);
                        valuesHighSCore.Add(objHighScore4);
                        valuesHighSCore.Add(objHighScore5);

                        valuesHighSCore.Add(objHighScore6);
                        valuesHighSCore.Add(objHighScore7);
                        valuesHighSCore.Add(objHighScore8);
                        valuesHighSCore.Add(objHighScore9);
                        valuesHighSCore.Add(objHighScore10);

                        SpreadsheetsResource.ValuesResource.UpdateRequest updateRequestHighSCore = service.Spreadsheets.Values.Update(new ValueRange() { Values = valuesHighSCore }, spreadsheetId, range);
                        //updateRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.;
                        updateRequestHighSCore.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                        var responseHighSCore = updateRequestHighSCore.Execute();

                        for (int j = 1; j < (values.Count + 1); j++)
                        {

                            NameArray[j] = (string)(arrangedArrayNew2[j - 1, 0]);
                            ScoreArray[j] = (string)(arrangedArrayNew2[j - 1, 1]);
                            SchoolArray[j] = (string)(arrangedArrayNew2[j - 1, 2]);

                        }
                        //InitializeComponent();
                        RankingArray.Text = ScoreArray[0];
                        RankingArray2.Text = ScoreArray[1];
                        RankingArray3.Text = ScoreArray[2];
                        RankingArray4.Text = ScoreArray[3];
                        RankingArray5.Text = ScoreArray[4];
                        RankingArray6.Text = ScoreArray[5];

                        NamePos.Text = NameArray[0];
                        NamePos2.Text = NameArray[1];
                        NamePos3.Text = NameArray[2];
                        NamePos4.Text = NameArray[3];
                        NamePos5.Text = NameArray[4];
                        NamePos6.Text = NameArray[5];

                        SchoolPos.Text = SchoolArray[0];
                        SchoolPos2.Text = SchoolArray[1];
                        SchoolPos3.Text = SchoolArray[2];
                        SchoolPos4.Text = SchoolArray[3];
                        SchoolPos5.Text = SchoolArray[4];
                        SchoolPos6.Text = SchoolArray[5];
                    }
                    catch (Exception TrialValue)
                    {

                    }
                }
                else
                {

                }
                binding.UpdateSource();
                binding2.UpdateSource();
                binding3.UpdateSource();
                binding4.UpdateSource();
                binding5.UpdateSource();
                binding6.UpdateSource();

                Scorebinding.UpdateSource();
                Scorebinding2.UpdateSource();
                Scorebinding3.UpdateSource();
                Scorebinding4.UpdateSource();
                Scorebinding5.UpdateSource();
                Scorebinding6.UpdateSource();

                Schoolbinding.UpdateSource();
                Scorebinding2.UpdateSource();
                Scorebinding3.UpdateSource();
                Scorebinding4.UpdateSource();
                Scorebinding5.UpdateSource();
                Scorebinding6.UpdateSource();


                player1Score.Content = "";

            }
            catch (Exception ErrorCheck)
            {

            }
            RestartButton.IsEnabled = true;
            StartButton.IsEnabled = true;
            ScoreEnteryButton.IsEnabled = true;
        }




    }

}
