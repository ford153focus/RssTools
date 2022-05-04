using RssGenerator.Feeds.Amd;
using RssSharedLibrary;

namespace RssGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // CsGo.Generate();
            RedditFeed.Compile();
        }
    }
}
