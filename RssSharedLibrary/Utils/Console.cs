namespace RssSharedLibrary.Utils
{
    static class Con
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