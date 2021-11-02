using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;
using RssStation.Utils;

namespace RssStation.Controllers
{
    public class OverclockersRuController : Controller
    {
        private async Task<List<SyndicationItem>> GetElementsByTagAsync(string tag)
        {
            var items = new List<SyndicationItem>();

            #region LOAD AND PARSE PAGE
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync("https://overclockers.ru/tag/show/" + tag);
            var articles = document.QuerySelectorAll("div.item.news-wrap, div.item.article-wrap");
            #endregion

            Parallel.ForEach(articles, article =>
            {
                try
                {
                    var header = (IHtmlAnchorElement)article.QuerySelector("a.header");

                    var title = header.InnerHtml.Trim();
                    var url = header.Href.Trim();
                    var id = new Regex(@".+\/(\d+)\/.+").Match(url).Groups[1].Value;

                    var content = article.GetElementsByClassName("description")[0].InnerHtml.Trim();

                    #region CONSTRUCT DATE
                    string dateString = "";
                    var date = DateTime.Now;
                    var matches = new Regex(@"^(\d{1,2}\s[а-я]+\s\d{4})(\sв\s(\d{2}:\d{2}))?")
                                            .Match(article.QuerySelector("span.date").InnerHtml.Trim());
                    if (matches.Groups[3].Value == "")
                    {
                        dateString = matches.Groups[1].Value;
                        date = DateTime.ParseExact(dateString, "d MMMM yyyy", CultureInfo.GetCultureInfo("ru-RU"));
                    }
                    else
                    {
                        dateString = matches.Groups[1].Value + " " + matches.Groups[3].Value;
                        date = DateTime.ParseExact(dateString, "d MMMM yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"));
                    }

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
            var feed = new SyndicationFeed("Overclockers.ru / " + tag, "Overclockers.ru - Новости, статьи и блоги", new Uri("https://overclockers.ru"));
            feed.Authors.Add(new SyndicationPerson("mech@overclockers.ru"));
            feed.Authors.Add(new SyndicationPerson("ford153focus@gmail.com"));
            feed.Items = await GetElementsByTagAsync(tag);
            feed.ImageUrl = new Uri("https://st.overclockers.ru/images/88x31/overclockers.gif");
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