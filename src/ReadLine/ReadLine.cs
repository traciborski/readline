﻿using System.Collections.Generic;

namespace System
{
    public static class ReadLine
    {
        private static KeyHandler _keyHandler;
        private static List<string> _history;

        public static Func<string, int, string[]> AutoCompletionHandler { private get; set; }

        static ReadLine()
        {
            _history = new List<string>();
        }

        public static void AddHistory(params string[] text) => _history.AddRange(text);
        public static List<string> GetHistory() => _history;
        public static void ClearHistory() => _history = new List<string>();

        public static string Read(string prompt = "")
        {
            Console.Write(prompt);
            _keyHandler = new KeyHandler(_history, AutoCompletionHandler);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                _keyHandler.Handle(keyInfo);
                keyInfo = Console.ReadKey(true);
            }

            Console.WriteLine();
            string text = _keyHandler.Text;
            _history.Add(text);

            return text;
        }
    }
}
