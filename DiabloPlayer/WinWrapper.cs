
/*
 * hypnodok #elitepvpers quakenet
 * 8.5.08
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;


namespace DiabloPlayer
{
    public static class WinAPI
    {
        #region function imports
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
           UIntPtr dwExtraInfo);


        private const int SRCCOPY = 0x00CC0020;
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hObject, int nXDest, int
        nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int
        nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int
        nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

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
        #endregion

        #region static methods

        /// <summary>
        /// simulates a keypress, see http://msdn2.microsoft.com/en-us/library/system.windows.forms.sendkeys(VS.71).aspx
        /// no winapi but this works just fine for me
        /// </summary>
        /// <param name="keys">the keys to press</param>
        public static void ManagedSendKeys(string keys)
        {
            SendKeys.SendWait(keys);
        }

        /// <summary>
        /// checks if the correct window is active, then send keypress
        /// </summary>
        /// <param name="keys">keys to press</param>
        /// <param name="windowName">window to send the keys to</param>
       /* public static void ManagedSendKeys(string keys, string windowName)
        {
            if (WindowActive(windowName))
            {

                ManagedSendKeys(keys);
            }
        }*/
        /// <summary>
        /// sends a keystring to a window
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="handle"></param>
        /*public static void ManagedSendKeys(string keys, IntPtr handle)
        {
            if (WindowActive(handle))
            {
                ManagedSendKeys(keys);
            }
        }*/
        /// <summary>
        /// sends a key to a window, pressing the button for x seconds
        /// </summary>
        /// <param name="key"></param>
        /// <param name="windowHandler"></param>
        /// <param name="delay"></param>
        /*public static void KeyboardEvent(Key key, IntPtr windowHandler, int delay)
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            // I had some Compile errors until I Casted the final 0 to UIntPtr like this...
            keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            Thread.Sleep(delay);
            keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
        }*/

        /// <summary>
        /// sends a key to a window, state="up" lifts it up, state="down" presses it down
        /// </summary>
        /// <param name="key"></param>
        /// <param name="windowHandler"></param>
        /// <param name="state"></param>
       /* public static void KeyboardEvent(Keys key, IntPtr windowHandler, string state)
        {
            byte bstate = 28;
            if (state == "up")
                bstate = 0x1|0x2;
            else 
                bstate = 0x1;
            keybd_event((byte)key, 0x45, bstate, (UIntPtr)0);
        }*/

        /// <summary>
        /// checks for the currently active window then simulates a mouseclick
        /// </summary>
        /// <param name="button">which button to press (left middle up)</param>
        /// <param name="windowName">the window to send to</param>
        public static void MouseClick(string button, string windowName)
        {
            if (WindowActive(windowName))
                MouseClick(button);
        }

        /// <summary>
        /// simulates a mouse click see http://pinvoke.net/default.aspx/user32/mouse_event.html?diff=y
        /// </summary>
        /// <param name="button">which button to press (left middle up)</param>
        public static void MouseClick(string button)
        {
            switch (button)
            {
                case "left":
                    mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
                    mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
                    break;
                case "right":
                    mouse_event((uint)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event((uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
                    break;
                case "middle":
                    mouse_event((uint)MouseEventFlags.MIDDLEDOWN, 0, 0, 0, 0);
                    mouse_event((uint)MouseEventFlags.MIDDLEUP, 0, 0, 0, 0);
                    break;
            }
        }

        /// <summary>
        /// sends a mouseclick to a window state=1 lifts it up state=0 presses it down
        /// </summary>
        /// <param name="button"></param>
        /// <param name="state"></param>
        public static void MouseClick(string button, int state)
        {
            switch(button.ToLower())
            {
                case "left":
                    switch (state)
                    {
                        case 1:
                            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
                            break;
                        case 0:
                            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
                            break;
                    }
                    break;
                case "right":
                    switch (state)
                    {
                        case 1:
                            mouse_event((uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
                            break;
                        case 0:
                            mouse_event((uint)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);
                            break;
                    }
                    break;
                case "middle":
                    switch (state)
                    {
                        case 1:
                            mouse_event((uint)MouseEventFlags.MIDDLEUP, 0, 0, 0, 0);
                            break;
                        case 0:
                            mouse_event((uint)MouseEventFlags.MIDDLEDOWN, 0, 0, 0, 0);
                            break;
                    }                    
                    break;
            }
        }

        /// <summary>
        /// moves the mouse
        /// </summary>
        /// <param name="x">x position to move to</param>
        /// <param name="y">y position to move to</param>
        public static void MouseMove(int x, int y)
        {
            SetCursorPos(x, y);
        }

        /// <summary>
        /// moves a window and resizes it accordingly
        /// </summary>
        /// <param name="x">x position to move to</param>
        /// <param name="y">y position to move to</param>
        /// <param name="windowName">the window to move</param>
        /// <param name="width">the window's new width</param>
        /// <param name="height">the window's new height</param>
        public static void WindowMove(int x, int y, string windowName, int width, int height)
        {
            IntPtr window = FindWindow(null, windowName);
            if (window != IntPtr.Zero)
                MoveWindow(window, x, y, width, height, true);
        }

        /// <summary>
        /// moves a window to a specified position
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="windowName">the window to be moved</param>
        public static void WindowMove(int x, int y, string windowName)
        {
            WindowMove(x, y, windowName, 800, 600);
        }

        /// <summary>
        /// checks if a specified window is currently the topmost one
        /// </summary>
        /// <param name="windowName">the window to check for</param>
        /// <returns>true if windowName machtes the topmost window, false if not</returns>
        public static bool WindowActive(string windowName)
        {
            IntPtr myHandle = FindWindow(null, windowName);
            IntPtr foreGround = GetForegroundWindow();
            if (myHandle != foreGround)
                return false;
            else
                return true;
        }

        /// <summary>
        /// checks if a handle is the active window atm
        /// </summary>
        /// <param name="myHandle"></param>
        /// <returns></returns>
        public static bool WindowActive(IntPtr myHandle)
        {
            IntPtr foreGround = GetForegroundWindow();
            if (myHandle != foreGround)
                return false;
            else
                return true;
        }

        /// <summary>
        /// makes the specified window the topmost one
        /// </summary>
        /// <param name="windowName">the window to activate</param>
        public static void WindowActivate(string windowName)
        {
            IntPtr myHandle = FindWindow(null, windowName);
            SetForegroundWindow(myHandle);
        }

        /// <summary>
        /// makes the specified window the topmost one
        /// </summary>
        /// <param name="handle">the window handle</param>
        public static void WindowActivate(IntPtr handle)
        {
            SetForegroundWindow(handle);
        }
        #endregion

        /// <summary>
        /// makes a screenshot of your current desktop and returns a bitmap
        /// </summary>
        /// <returns></returns>
        public static Bitmap CreateScreenshot()
        {
            IntPtr hWnd = GetDesktopWindow();
            IntPtr hSorceDC = GetWindowDC(hWnd);
            RECT rect = new RECT();
            GetWindowRect(hWnd, ref rect);
            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;
            IntPtr hDestDC = CreateCompatibleDC(hSorceDC);
            IntPtr hBitmap = CreateCompatibleBitmap(hSorceDC, width, height);
            IntPtr hObject = SelectObject(hDestDC, hBitmap);
            BitBlt(hDestDC, 0, 0, width, height, hSorceDC, 0, 0, SRCCOPY);
            SelectObject(hDestDC, hObject);
            DeleteDC(hDestDC);
            ReleaseDC(hWnd, hSorceDC);
            Bitmap screenshot = Bitmap.FromHbitmap(hBitmap);
            DeleteObject(hBitmap);
            return screenshot;
        }
    }

   
}