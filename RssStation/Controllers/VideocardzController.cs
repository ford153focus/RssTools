using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RssSharedLibrary.Utils.Grabbers;
using RssStation.Utils;

namespace RssStation.Controllers
{
    public class VideocardzController : Controller
    {
        public async Task<ContentResult> IndexAsync(string tags)
        {
            SyndicationFeed feed = await WordPressFeed.GetFeed("https://blog.counter-strike.net/wp-json/");

            List<SyndicationItem> filteredItems = new();

            var tagsArray = tags.Split(",");
            
            foreach (SyndicationItem item in feed.Items)
            {
                foreach (string tag in tagsArray)
                {
                    if (item.Title.ToString().ToLower().Contains(tag))
                    {
                        filteredItems.Add(item);
                    }
                }
            }
            
            feed.Items = filteredItems;

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = SyndicationFeedToString.Convert(feed),
                StatusCode = 200
            };
        }
    }
}