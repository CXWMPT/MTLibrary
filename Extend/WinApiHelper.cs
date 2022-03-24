using System;
using System.Runtime.InteropServices;

namespace MTLibrary
{
    public static class WinApiHelper
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();


        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOW = 5;
        public const int SW_RESTORE = 9;
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        public const int MK_LBUTTON = 0x0001;
        public const int MK_RBUTTON = 0x0002;
        public const int MK_SHIFT = 0x0004;
        public const int MK_CONTROL = 0x0008;
        public const int MK_MBUTTON = 0x0010;
        public const int MK_XBUTTON1 = 0x0020;
        public const int MK_XBUTTON2 = 0x0040;

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bVk">refer to EVKey</param>
        /// <param name="bScan">Use MapVirtualKey to convert bVK to scan code</param>
        /// <param name="dwFlags">refer to EKeyEvent</param>
        /// <param name="dwExtraInfo">set to 0</param>
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, UInt32 dwFlags, UInt32 dwExtraInfo);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="uCode">VK or scan code</param>
        /// <param name="uMapType">refer to EVKeyMapType</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "MapVirtualKey", SetLastError = true)]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", EntryPoint = "GetAsyncKeyState", SetLastError = true)]
        public static extern short GetAsyncKeyState(int vKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pbKeyState">states of all virtual keys</param>
        /// <returns>BOOL value, 0 means failed, non zero means succeeded</returns>
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState", SetLastError = true)]
        public static extern int GetKeyboardState(byte[] pbKeyState);
    }
}
