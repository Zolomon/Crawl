using System;

namespace Lampa
{
    public class IO
    {
        public static void Print(string text)
        {
            ConsoleColor fc = ConsoleColor.Gray;
            ConsoleColor bc = ConsoleColor.Black;
            char fchar = '0';
            char bchar = '0';
            bool shouldColor = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals('#'))
                {
                    fchar = text[++i];
                    bchar = text[++i];
                    i++;

                    shouldColor = Char.IsLetterOrDigit(fchar) && Char.IsLetterOrDigit(bchar);
                }
                else if (shouldColor && text[i].Equals('|'))
                {
                    shouldColor = false;
                    i++;
                }
                if (shouldColor)
                    Print(text[i].ToString(), (ConsoleColor)int.Parse(fchar.ToString(), System.Globalization.NumberStyles.HexNumber),
                                              (ConsoleColor)int.Parse(bchar.ToString(), System.Globalization.NumberStyles.HexNumber));
                else
                    if (i < text.Length)
                        Console.Write(text[i]);
            }
        }

        public static void Print(string text, ConsoleColor color)
        {
            Print(text, color, ConsoleColor.Black);
        }

        public static void Print(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
