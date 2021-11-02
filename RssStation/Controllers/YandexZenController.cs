using Microsoft.AspNetCore.Mvc;
using RssStation.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Json;
using Chronic;

namespace RssStation.Controllers
{
    public class YandexZenController : Controller
    {
        private List<SyndicationItem> GetElementsByTag(string tag)
        {
            var items = new List<SyndicationItem>();



            #region LOAD AND PARSE PAGE
            string jsonStr = (new WebClient()).DownloadString("https://zen.yandex.ru/api/v3/launcher/more?interest_name=" + tag);
            JsonValue jsonValue = JsonValue.Parse(jsonStr);
            JsonArray articles = (JsonArray)jsonValue["items"];
            #endregion

            Parallel.ForEach(articles, (article) =>
            {
                try
                {
                    var title = (string)article["title"];
                    var url = (string)article["link"];
                    var id = (string)article["id"];
                    var content = (string)article["text"];

                    #region CONSTRUCT DATE
                    var parser = new Chronic.Parser();
                    Span parsedObj = parser.Parse((string)article["creation_time"]);
                    var date = (DateTime)parsedObj.Start;
                    #endregion

                    SyndicationItem item = new SyndicationItem(
                        title,
                        content,
                        new Uri(url),
                        id,
                        date
                    );

                    item.PublishDate = date;
                    item.Summary = new TextSyndicationContent(content);

                    items.Add(item);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            return items;
        }

        public ContentResult Index(string tag)
        {
            #region CONSTRUCT FEED
            var feed = new SyndicationFeed("Yandex Zen | " + tag, "Yandex Zen | " + tag, new Uri("https://zen.yandex.ru/t/" + tag));
            feed.Authors.Add(new SyndicationPerson("ford153focus@gmail.com"));
            feed.Items = GetElementsByTag(tag);
            feed.ImageUrl = new Uri("https://yastatic.net/s3/zen-lib/favicons3/apple-touch-icon.png");
            #endregion

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = SyndicationFeedToString.Convert(feed),
                StatusCode = 200
            };
        }
    }
}