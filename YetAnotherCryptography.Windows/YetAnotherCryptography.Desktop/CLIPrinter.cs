using System;

namespace YetAnotherCryptography.Desktop
{
    public class CLIPrinter
    {
        enum InternalMode
        {
            Default,
            Error,
            Warning
        }

        private static void InternalPrinter(string str, InternalMode internalMode)
        {
            string datetime = DateTime.Now.ToString();
            string formattedString = string.Format("[{0}] {1}: {2}", datetime, internalMode.ToString(), str);

#if DEBUG
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Error.WriteLine(formattedString);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
#else

            switch (internalMode)
            {
                case InternalMode.Default:
                    Console.WriteLine(formattedString);
                    break;

                case InternalMode.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine(formattedString);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case InternalMode.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Error.WriteLine(formattedString);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
#endif
        }

        public static void PrintError(string str)
        {
            InternalPrinter(str, InternalMode.Error);
        }

        public static void PrintWarning(string str)
        {
            InternalPrinter(str, InternalMode.Warning);
        }

        public static void Print(string str)
        {
            InternalPrinter(str, InternalMode.Default);
        }
    }
}
