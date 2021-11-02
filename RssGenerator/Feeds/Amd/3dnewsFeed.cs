using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using HtmlAgilityPack;

namespace RssGenerator.Feeds.Amd
{
    class DddFeed
    {
        public DddFeed()
        {
            #region LOAD PAGE
            var url = "https://3dnews.ru/tags/amd";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            #endregion

            #region CREATE FEED
            SyndicationFeed feed = new SyndicationFeed(
                "3DNews: Новости по тегу amd",
                "",
                new Uri("https://3dnews.ru/tags/amd")
            );

            feed.Authors.Add(new SyndicationPerson("news@3dnews.ru", "3DNews", "https://3dnews.ru/"));
            feed.Copyright = new TextSyndicationContent("3DNews");
            feed.Description = new TextSyndicationContent("3DNews: Новости по тегу amd");
            feed.Generator = "Ford-RT // RssStation";
            feed.ImageUrl = new Uri("https://3dnews.ru/assets/images/logo.png");
            feed.Language = "ru-RU";
            #endregion

            #region PARSE POSTS FOR FEED ITEMS
            // FEED ITEMS STORAGE
            List<SyndicationItem> items = new List<SyndicationItem>();
            
            #endregion
        }
    }
}