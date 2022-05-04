using System.ServiceModel.Syndication;
using HtmlAgilityPack;

namespace RssSharedLibrary.Utils.Grabbers
{
    class DailyDigitalDigestGrabber
    {
        public static SyndicationFeed GetFeed(string tag)
        {
            #region LOAD PAGE
            var url = $"https://3dnews.ru/tags/{tag}";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            #endregion

            #region CREATE FEED
            SyndicationFeed feed = new SyndicationFeed(
                $"3DNews: Новости по тегу {tag}",
                "",
                    new Uri($"https://3dnews.ru/tags/{tag}")
            );

            feed.Authors.Add(new SyndicationPerson("news@3dnews.ru", "3DNews", "https://3dnews.ru/"));
            feed.Copyright = new TextSyndicationContent("3DNews");
            feed.Description = new TextSyndicationContent($"3DNews: Новости по тегу {tag}");
            feed.Generator = "Ford-RT // RssTools";
            feed.ImageUrl = new Uri("https://3dnews.ru/assets/images/logo.png");
            feed.Language = "ru-RU";
            #endregion

            #region PARSE POSTS FOR FEED ITEMS
            // FEED ITEMS STORAGE
            List<SyndicationItem> items = new List<SyndicationItem>();
            
            HtmlNodeCollection posts = doc.DocumentNode.SelectNodes("/div[@id='section-content']/div[@class='article-entry']");

            foreach (HtmlNode post in posts)
            {
                #region TITLE
                String title = post.SelectSingleNode(".//h1").InnerText;
                #endregion

                #region CONTENT
                String content = post.SelectSingleNode(".//div[@class='entry-body']").InnerText;
                #endregion

                #region URL
                String itemAlternateLinkTxt = post.SelectSingleNode(".//a[@class='entry-header']").Attributes["href"].Value;
                Uri itemAlternateLink = new Uri($"https://3dnews.ru{itemAlternateLinkTxt}");
                #endregion
                
                #region ID
                string id = itemAlternateLinkTxt.Trim(new [] {'/'});
                #endregion
                
                #region PUBLISH DATE
                string lastUpdatedTimeTxt = post.SelectSingleNode(".//span[class='entry-date]/strong").InnerText;
                DateTimeOffset lastUpdatedTime = DateTimeOffset.Parse(lastUpdatedTimeTxt);
                #endregion
                
                #region SAVE FEED ITEM
                items.Add(new SyndicationItem(
                    title, content, itemAlternateLink, id, lastUpdatedTime
                ));
                #endregion

                Con.WriteHr();
            }
            #endregion
            
            feed.Items = items;
            return feed;
        }
    }
}