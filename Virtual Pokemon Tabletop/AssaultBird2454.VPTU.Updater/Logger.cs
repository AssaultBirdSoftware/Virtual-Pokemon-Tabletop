using System;

namespace AssaultBird2454.VPTU.Updater
{
    /// <summary>
    ///     Defines the what type of loggs can be made
    /// </summary>
    public enum Console_LogLevel
    {
        Info,
        Notice,
        Debug,
        Warning,
        Error,
        Critical
    }

    public static class Logger
    {
        public static void Write(string Message, Console_LogLevel Level)
        {
            if (Level == Console_LogLevel.Info)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Change the color
                Console.BackgroundColor = ConsoleColor.Black; // Change the color
                Console.WriteLine(DateTime.Now.ToShortDateString() + " [Info] -> " + Message); // Write the message
            }
            else if (Level == Console_LogLevel.Notice)
            {
                Console.ForegroundColor = ConsoleColor.Blue; // Change the color
                Console.BackgroundColor = ConsoleColor.Black; // Change the color
                Console.WriteLine(DateTime.Now.ToShortDateString() + " [Notice] -> " + Message); // Write the message
            }
            else if (Level == Console_LogLevel.Warning)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Change the color
                Console.BackgroundColor = ConsoleColor.Black; // Change the color
                Console.WriteLine(DateTime.Now.ToShortDateString() + " [Warning] -> " + Message); // Write the message
            }
            else if (Level == Console_LogLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red; // Change the color
                Console.BackgroundColor = ConsoleColor.Black; // Change the color
                Console.WriteLine(DateTime.Now.ToShortDateString() + " [Error] -> " + Message); // Write the message
            }
            else if (Level == Console_LogLevel.Critical)
            {
                Console.ForegroundColor = ConsoleColor.White; // Change the color
                Console.BackgroundColor = ConsoleColor.Red; // Change the color
                Console.WriteLine(DateTime.Now.ToShortDateString() + " [Critical] -> " + Message); // Write the message
            }
            else if (Level == Console_LogLevel.Debug)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta; // Change the color
                Console.BackgroundColor = ConsoleColor.Black; // Change the color
                Console.WriteLine(DateTime.Now.ToShortDateString() + " [Debug] -> " + Message); // Write the message
            }

            Console.ForegroundColor = ConsoleColor.DarkGray; // Change the color back
            Console.BackgroundColor = ConsoleColor.Black; // Change the color back
        }
    }
}