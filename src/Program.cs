﻿using System;
using System.Text;

namespace Tetrix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrepareConsole();

            var settings = new GameSettings(args);
            var game = new Game(settings);
            game.Start();
            game.Shutdown();

            ResetConsole();
        }

        private static void PrepareConsole()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CancelKeyPress += (s, e) => ResetConsole();
        }

        private static void ResetConsole()
        {
            Console.Clear();
            Console.ResetColor();
        }
    }
}
