using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;
using RssStation.Utils;

namespace RssStation.Controllers
{
    public class VideocardzController : Controller
    {
        private async Task<List<SyndicationItem>> GetElementsByTagAsync(string tag)
        {
            var items = new List<SyndicationItem>();

            #region LOAD AND PARSE PAGE
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync($"https://videocardz.com/{tag}/");
            var articles = document.QuerySelectorAll("article.omc-blog-one.omc-blog-one-50[id^='post-']");
            #endregion

            Parallel.ForEach(articles, article =>
            {
                try
                {
                    var id = article.Id.Trim();
                    var title = article.QuerySelector("h2 a").InnerHtml.Trim();
                    var content = title;
                    var url = ((IHtmlAnchorElement)article.QuerySelector("h2 a")).Href.Trim();

                    #region CONSTRUCT DATE
                    string dateString = article.QuerySelector("div").InnerHtml.Trim();
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    string format = "MMMM, ";
                    DateTime date = DateTime.ParseExact(dateString, format, provider);
                    #endregion

                    SyndicationItem item = new SyndicationItem(
                        title,
                        content,
                        new Uri(url),
                        id,
                        date
                    )
                    {
                        PublishDate = date,
                        Summary = new TextSyndicationContent(content)
                    };

                    items.Add(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            return items;
        }

        public async Task<ContentResult> IndexAsync(string tag)
        {
            #region CONSTRUCT FEED
            var feed = new SyndicationFeed("3DNews: Новости " + tag, "3DNews: Новости " + tag, new Uri("https://3dnews.ru/"));
            feed.Authors.Add(new SyndicationPerson("nivnikov@3dnews.ru"));
            feed.Authors.Add(new SyndicationPerson("ford153focus@gmail.com"));
            feed.Items = await GetElementsByTagAsync(tag);
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