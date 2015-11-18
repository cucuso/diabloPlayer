using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Runtime.CompilerServices;
using System.Drawing;


namespace DiabloPlayer
{
    class Program
    {
        public const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;
        public static System.Timers.Timer gameTimer;
        public static System.Timers.Timer bossTimer;
        private static ManualResetEvent mre = new ManualResetEvent(false);
        
        static void Main(string[] args)
        {

            
            while (true)
           CacheCollection();

            //GetScreenRes();

        }




        public static void GetScreenRes()
        {

            Rectangle resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Console.WriteLine(resolution.Width +" "+resolution.Height);
            //Console.ReadLine();

            double y = Convert.ToDouble(resolution.Height) * 1.111;
            double x = Convert.ToDouble(resolution.Height) * 0.158125;


            SetCursorPos(Convert.ToInt32(x), Convert.ToInt32(y));
            WinAPI.MouseClick("right");
            
        }


        public static void CacheCollection()
        {

            mre.Reset();
            gameTimer = new System.Timers.Timer();
            gameTimer.Elapsed += new ElapsedEventHandler(EndOfGame);
            gameTimer.Interval = 1400000;
            gameTimer.Enabled = true;


            bossTimer = new System.Timers.Timer();
            bossTimer.Elapsed += new ElapsedEventHandler(BossClick);
            bossTimer.Interval = 10000;
            bossTimer.Enabled = true;

           
        
      


            SetCursorPos(150, 1200);
            WinAPI.MouseClick("left");


            //foreach (char c in message)
           // WinAPI.ManagedSendKeys(c.ToString());
           // WinAPI.ManagedSendKeys("{enter}");
       

            Thread.Sleep(1500);
            WinAPI.ManagedSendKeys("{esc}");

            Thread.Sleep(1000);
            WinAPI.ManagedSendKeys("{esc}");
            
            Thread.Sleep(1000);
            SetCursorPos(350, 500);
            WinAPI.MouseClick("left");

            Thread.Sleep(9000);
            WinAPI.ManagedSendKeys("m");


            //City of Caldeum WP 
            Thread.Sleep(1000);
            SetCursorPos(900, 670);
            WinAPI.MouseClick("left");


            //Other WPs to left
            SetCursorPos(600, 670);
            WinAPI.MouseClick("left");

            SetCursorPos(540, 600);
            WinAPI.MouseClick("left");
            
            SetCursorPos(510, 620);
            WinAPI.MouseClick("left");


            /*
             Thread.Sleep(1000);
             WinAPI.ManagedSendKeys("{enter}");
             WinAPI.ManagedSendKeys("{tab}");
             WinAPI.ManagedSendKeys("{tab}");
            foreach (char c in message)
            WinAPI.ManagedSendKeys(c.ToString());

            WinAPI.ManagedSendKeys("{enter}");
            */



            mre.WaitOne();
            // MoveAround();




        }


        private static void LeaveGame()
        {

        
          
            //leave game
         
            WinAPI.ManagedSendKeys("{esc}");
            Thread.Sleep(1000);
            SetCursorPos(330, 480);
            WinAPI.MouseClick("left");
            Thread.Sleep(16000);
            


        }


        private static void EndOfGame(object source, ElapsedEventArgs e)
        {
            bossTimer.Enabled = false;
            gameTimer.Enabled = false;
            LeaveGame();
            mre.Set();



        }

        private static void BossClick(object source, ElapsedEventArgs e)
        {

            SetCursorPos(1030, 645);
            WinAPI.MouseClick("left");

            SetCursorPos(950, 500);
            WinAPI.MouseClick("right");

            SetCursorPos(750, 400);
            WinAPI.MouseClick("right");

            SetCursorPos(650, 400);
            WinAPI.MouseClick("left");
        }


        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }





    }



}
