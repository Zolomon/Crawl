using System;

namespace Lampa
{
    public class IO
    {
        public static void Print(string text)
        {
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
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                if (shouldColor)
                {
                    Console.ForegroundColor =
                        (ConsoleColor) int.Parse(fchar.ToString(), System.Globalization.NumberStyles.HexNumber);
                    Console.BackgroundColor =
                        (ConsoleColor) int.Parse(bchar.ToString(), System.Globalization.NumberStyles.HexNumber);
                }

                if (i < text.Length)
                        Console.Write(text[i]);
            }
        }
    }
}
