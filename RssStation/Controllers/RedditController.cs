using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RssSharedLibrary.Utils.Grabbers;
using RssStation.Utils;

namespace RssStation.Controllers
{
    public class RedditController : Controller
    {
        public async Task<ContentResult> IndexAsync(string subReddit, string flair)
        {
            var feeds = await RedditGrabber.GetFeeds(subReddit);
            var feed = feeds.Find(f => f.Title.ToString().EndsWith(flair));

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = SyndicationFeedToString.Convert(feed),
                StatusCode = 200
            };
        }
    }
}