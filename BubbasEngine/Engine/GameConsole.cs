using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BubbasEngine.Engine
{
    public static class GameConsole
    {
        public enum MessageType
        {
            Normal,
            Error,
            Warning,
            Important
        }

        // Size
        public static int WindowWidth
        { get { return Console.WindowWidth; } set { Console.WindowWidth = value; } }
        public static int WindowHeight
        { get { return Console.WindowHeight; } set { Console.WindowHeight = value; } }

        // Initialize
        public static void Initialize()
        {
            Console.Title = "GameConsole";
        }

        // Hide or Show Window
        public static void HideWindow()
        {
            NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), 0);
        }
        public static void ShowWindow()
        {
            NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), 5);
        }

        // Write to Console
        public static void Write(string text)
        {
            Write(text, MessageType.Normal);
        }
        public static void Write(string text, MessageType type)
        {
            switch (type)
            {
                // Normal
                case MessageType.Normal:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text);
                    break;

                // Error
                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(text);
                    break;

                // Warning
                case MessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(text);
                    break;

                // Important
                case MessageType.Important:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(text);
                    break;
            }
        }

        public static void WriteLine(string text)
        {
            WriteLine(text, MessageType.Normal);
        }
        public static void WriteLine(string text, MessageType type)
        {
            switch (type)
            {
                // Normal
                case MessageType.Normal:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(text);
                    break;

                // Error
                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(text);
                    break;

                // Warning
                case MessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(text);
                    break;

                // Important
                case MessageType.Important:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(text);
                    break;
            }
        }
    }
}
