using System;
using HtmlAgilityPack;

namespace RssStation.Utils
{
    class Utils
    {
        public static void WriteHr()
        {
            for (int i = 0; i < 80; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

    }
}