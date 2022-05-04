using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using RssSharedLibrary.Utils.Grabbers;

namespace RssGenerator
{
    internal static class Program
    {
        static async Task Main()
        {
            Directory.CreateDirectory(Configuration.SavePath);
            await AmdReddit();
            await CsGo();
            DailyDigitalDigest();
        }

        private static void DailyDigitalDigest()
        {
            var feed = DailyDigitalDigestGrabber.GetFeed("amd");
            string path = Path.Combine(Configuration.SavePath, "3dnews.ru", "amd.xml");
            Utils.WriteFeedToFile(feed, path);
        }
        
        private static async Task AmdReddit()
        {
            var feeds = await RedditGrabber.GetFeeds("AMD");

            foreach (SyndicationFeed feed in feeds)
            {
                string feedCategory = feed.Title.ToString()?.Split(" ").Last();
                string fileName = $"amd.reddit.{feedCategory}.xml";
                string path = Path.Combine(Configuration.SavePath, fileName);
                
                Utils.WriteFeedToFile(feed, path);
            }
        }

        private static async Task CsGo()
        {
            SyndicationFeed feed = await WordPressFeed.GetFeed("https://blog.counter-strike.net/wp-json/");
            string path = Path.Combine(Configuration.SavePath, "csgo.xml");
            Utils.WriteFeedToFile(feed, path);
        }
    }
}
