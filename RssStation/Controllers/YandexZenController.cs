using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RssSharedLibrary.Utils.Grabbers;
using RssStation.Utils;

namespace RssStation.Controllers
{
    public class YandexZenController : Controller
    {
        public async Task<ContentResult> Index(string tag)
        {

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = SyndicationFeedToString.Convert(await YandexZenGrabber.GetFeedAsync(tag)),
                StatusCode = 200
            };
        }
    }
}