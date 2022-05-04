using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RssSharedLibrary.Utils.Grabbers;
using RssStation.Utils;

namespace RssStation.Controllers
{
    public class DailyDigitalDigestController : Controller
    {
        public Task<ContentResult> IndexAsync(string tag)
        {
            return Task.FromResult(new ContentResult
            {
                ContentType = "application/xml",
                Content = SyndicationFeedToString.Convert(DailyDigitalDigestGrabber.GetFeed(tag)),
                StatusCode = 200
            });
        }
    }
}