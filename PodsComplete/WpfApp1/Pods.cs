using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace WpfApp1
{
    internal class Pods
    {
        //private gameState;
        public const String RED = "0xf00";
        public const String GREEN = "0x0f0";
        public const String NOCOLOR = "0x000";

        private int score = 0;
        private long GameTime;
        private String COMPort;
        private SerialPort port;
        private brdSegment[] brdState;

        public bool isPlayer1;
        

        public Pods(String COMPort, long gameTime, bool isPlayer1)
        {
            this.COMPort = COMPort;
            this.GameTime = gameTime;
            this.isPlayer1 = isPlayer1;

            setUpCOMPort();
        }

        private void setUpCOMPort()
        {
            this.port = new SerialPort(this.COMPort);
            this.port.ReadTimeout = 500;
            this.port.BaudRate = 112500;
            this.port.Parity = Parity.None;
            this.port.StopBits = StopBits.One;
            this.port.DataBits = 8;

            this.port.DtrEnable = true;
            this.port.RtsEnable = true;
        }

        public void openPort()  {
            String rcvCommand = "";
            this.port.Open();
            while (string.Compare(rcvCommand, "\n>") != 0) rcvCommand = this.port.ReadExisting();
        }

        public void closePort() { this.port.Close(); }
        public int getScore()   { return this.score; }

        public bool[] getSWState()
        {
            String sw = "\n";
            Char[] swCharStates = new Char[9];
            bool[] swStates = new bool[9];


            while (sw.Equals("\n")) {
                //FiXME: shouldn't have to do this
                this.port.DiscardOutBuffer();
                this.port.DiscardInBuffer();
                this.port.Write("READ_SW\r\n");
                sw = this.port.ReadTo(">");
            }

            if (sw.Contains("SW") && sw.Length == 14)
            {
                sw = sw.Substring(1, sw.Length - 2);
                sw = sw.Substring(3);
                swCharStates = sw.ToCharArray();

                for (int i = 0; i < swCharStates.Length; i++) swStates[i] = swCharStates[i] == 'T' ? false : true;
            }
            else { for (int i = 0; i < swCharStates.Length; i++) swStates[i] = false; }
            return swStates;

        }

        public void nxtGameState() {
            for (int i = 0; i < this.brdState.Length; i++)
            {
                Random rndColor = new Random(Guid.NewGuid().GetHashCode());
                int iColor = rndColor.Next(1, 3);
                String color = iColor == 1 ? GREEN : RED;
                int value = color == RED ? -1 : 1;

                this.brdState[i] = new brdSegment(color, value);
            }
        }

        public void tScorechg()
        {
            this.score += 1;
            if (this.isPlayer1) { MainWindow.window.p1ScoreCnt = this.score.ToString();}
            else { MainWindow.window.p2ScoreCnt = this.score.ToString(); }

        }
        public void chgScore(bool[] SW)
        {
            for (int i = 0; i < SW.Length; i++)
            {
                if (SW[i])
                {
                    this.score += this.brdState[i].getValue();
                    if (this.isPlayer1) { MainWindow.window.p1ScoreCnt = this.score.ToString(); }
                    else { MainWindow.window.p2ScoreCnt = this.score.ToString(); }
                }
            }
        }

        public bool isPlayerReady() {
            const int centerSWIdx = 7;
            bool[] swStates = getSWState();
            return swStates[centerSWIdx];
        }
        public void startGame()
        {
            //initialize brdState
            bool[] tSW = new bool[9];
            long windowLengt = 9500000;
            bool isSWActive = false;
            NextBrdState();
            updatePHYBrd(this.brdState);
            
            long start = DateTime.Now.Ticks;
            do
            {
                //if you step of a switch within 
                long running = DateTime.Now.Ticks;
                isSWActive = false;
                while (DateTime.Now.Ticks - running < windowLengt && !isSWActive)
                {
                    tSW = getSWState();
                    foreach (bool s in tSW) { if (s) isSWActive = true; break;}
                    //Thread.Sleep(500);

                }
                //Possible Race conditions here
                //Thread.Sleep(350);
                chgScore(tSW);
                NextBrdState();
                updatePHYBrd(this.brdState);
                //tScorechg();
            } while (DateTime.Now.Ticks - start < this.GameTime);

            //Thread.Sleep(10000);
            //Turn off the lights after the game finishes
            for (int i = 0; i < this.brdState.Length; i++) this.brdState[i] = new brdSegment();
            updatePHYBrd(this.brdState);

            
            //MainWindow.window.btnStartGame.IsEnabled = true;
            MainWindow.window.showWinner(this.isPlayer1, this.getScore());

            this.score = 0;
            
            //Console.WriteLine("StillRunning");

        }

        private void updatePHYBrd(brdSegment[] brdState)
        {
            //communicate with Arduino to light up LEDS
            this.port.Write("SET_BRIGHTNESS: 255\r");
            for (int i = 0; i < brdState.Length; i++)
            {
                this.port.DiscardOutBuffer();
                this.port.DiscardInBuffer();
                //FIXME: Minor Visual Glitches
                port.Write("SET_COLOUR:" + i + ":" + NOCOLOR + "\r\n");
                Thread.Sleep(50);
                if (!brdState[i].getColor().Equals(NOCOLOR)) port.Write("SET_COLOUR:" + i + ":" + brdState[i].getColor() + "\r\n");
                
            }
            Console.WriteLine("SET_COLOUR:5:" + brdState[5].getColor() + "\r\n");
        }

        private void NextBrdState() {
            brdSegment[] brdSegment = new brdSegment[9];
            for (int i = 0; i < brdSegment.Length; i++) { brdSegment[i] = new brdSegment(); }
            brdSegment[0].setColor(GREEN);
            brdSegment[0].setValue(5);

            brdSegment[1].setColor(RED);
            brdSegment[1].setValue(-1);

            brdSegment[2].setColor(RED);
            brdSegment[2].setValue(-1);

            Random rnd = new Random();
            this.brdState = brdSegment.OrderBy(x => rnd.Next()).ToArray();

            int prvGreenIDx = -1;
            int newGreenIDx = -1;
            do
            {
                brdSegment = brdSegment.OrderBy(x => rnd.Next()).ToArray();
                for (int i = 0; i < brdSegment.Length; i++) { if (brdSegment[i].getColor().Equals(GREEN)) newGreenIDx = i; }
                for (int i = 0; i < brdSegment.Length; i++) { if (this.brdState[i].getColor().Equals(GREEN)) prvGreenIDx = i; }

            } while (newGreenIDx == prvGreenIDx);
            this.brdState = brdSegment;

        }

        private void serialWrite(String cmd) {
            //trying to prevent race conditions
            String rcvCommand = "";
            this.port.Write(cmd);
            while (string.Compare(rcvCommand, "\n>") != 0) rcvCommand = this.port.ReadExisting();
        }
    }

    internal class brdSegment
    {
        private String color;
        private int value;

        public brdSegment()
        {
            this.color = "0x000";
            this.value = 0;
        }

        public brdSegment(String color, int value)
        {
            this.color = color;
            this.value = value;
        }

        public String   getColor() { return this.color; }
        public int      getValue() { return this.value; }

        public void setColor(String cl) { this.color = cl; }
        public void setValue(int vl)    { this.value = vl; }
    }
}