using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;
using RssStation.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RssStation.Controllers
{
    public class DailyDigitalDigestController : Controller
    {
        private async Task<List<SyndicationItem>> GetElementsByTagAsync(string tag)
        {
            var items = new List<SyndicationItem>();

            #region LOAD AND PARSE PAGE
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync("https://3dnews.ru/tags/" + tag);
            var articles = document.QuerySelectorAll("div.article-entry");
            #endregion

            Parallel.ForEach(articles, (article) =>
            {
                try
                {
                    var id = article.Id.Trim();
                    var title = article.GetElementsByTagName("h1")[0].InnerHtml.Trim();
                    var content = article.GetElementsByClassName("entry-body")[0].InnerHtml.Trim();
                    var url = ((IHtmlAnchorElement)article.GetElementsByClassName("entry-header")[0]).Href.Trim();

                    #region CONSTRUCT DATE
                    Regex regex = new Regex(@"(\d{2}\.\d{2}\.\d{4}).+\[(\d{2}:\d{2})\]");
                    Match match = regex.Match(article.QuerySelector("span.entry-date").InnerHtml);
                    string dateString = match.Groups[1].Value + " " + match.Groups[2].Value;
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    string format = "dd.MM.yyyy HH:mm";
                    DateTime date = DateTime.ParseExact(dateString, format, provider);
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