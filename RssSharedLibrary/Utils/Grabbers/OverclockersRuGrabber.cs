using System.Globalization;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Html.Dom;

namespace RssSharedLibrary.Utils.Grabbers
{
    class OverclockersRuGrabber
    {
        public static async Task<SyndicationFeed> GetFeedAsync(string url)
        {
            string tag = url.Split("/").Last();
            
            #region CREATE FEED
            SyndicationFeed feed = new SyndicationFeed();
            feed.Title = new TextSyndicationContent("Overclockers.ru / " + tag);
            feed.Description = new TextSyndicationContent("Overclockers.ru - Новости, статьи и блоги");

            feed.Authors.Add(new SyndicationPerson("mech@overclockers.ru"));
            feed.Authors.Add(new SyndicationPerson("ford153focus@gmail.com"));
            
            feed.Generator = "Ford-RT // RssTools";
            feed.ImageUrl = new Uri("https://st.overclockers.ru/images/88x31/overclockers.gif");
            feed.Language = "ru-RU";
            #endregion

            // FEED ITEMS STORAGE
            List<SyndicationItem> items = new List<SyndicationItem>();
            
            #region LOAD AND PARSE PAGE
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            var articles = document.QuerySelectorAll(".page-content div.item.news-wrap, .page-content div.item.article-wrap");
            #endregion
            
            Parallel.ForEach(articles, article =>
            {
                try
                {
                    SyndicationItem item = new SyndicationItem();

                    IHtmlAnchorElement? header = article.QuerySelector("a.header") as IHtmlAnchorElement;

                    string? title = header?.InnerHtml.Trim();
                    item.Title = new TextSyndicationContent(title);

                    string href = header.Href.Trim();
                    item.BaseUri = new Uri(href);
                    
                    item.Id = new Regex(@".+\/(\d+)\/.+").Match(href).Groups[1].Value;

                    string content = article.GetElementsByClassName("description").First().InnerHtml.Trim();
                    item.Content = new TextSyndicationContent(content);

                    #region CONSTRUCT DATE
                    DateTime date;
                    string dateString = article.QuerySelector("span.date").InnerHtml.Trim();
                    var matches = new Regex(@"^(\d{1,2}\s[а-я]+\s\d{4})(\sв\s(\d{2}:\d{2}))?").Match(dateString);
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
                    item.PublishDate = date;
                    #endregion

                    items.Add(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
            
            feed.Items = items;
            return feed;
        }
    }
}