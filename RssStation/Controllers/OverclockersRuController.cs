using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RssSharedLibrary.Utils.Grabbers;
using RssStation.Utils;

namespace RssStation.Controllers
{
    public class OverclockersRuController : Controller
    {

        public async Task<ContentResult> IndexAsync(string url)
        {
            return new ContentResult
            {
                ContentType = "application/xml",
                Content = SyndicationFeedToString.Convert(await OverclockersRuGrabber.GetFeedAsync(url)),
                StatusCode = 200
            };
        }
    }
}