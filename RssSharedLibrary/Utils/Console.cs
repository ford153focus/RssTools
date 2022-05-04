using System;

namespace RssSharedLibrary.Utils
{
    static class Console
    {
        public static void WriteHr()
        {
            for (int i = 0; i < 80; i++)
            {
                System.Console.Write("=");
            }
            System.Console.WriteLine();
        }

    }
}