using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;
using RssGenerator.Cfg;

namespace RssGenerator.Feeds
{
    class CsGo
    {
        public static void Generate()
        {
            #region LOAD PAGE
            var url = "http://blog.counter-strike.net/index.php/category/updates/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            #endregion

            #region CREATE FEED
            SyndicationFeed feed = new SyndicationFeed(
                "Counter-Strike: Global Offensive  » Updates",
                "Counter-Strike: Global Offensive (CS:GO) expands upon the team-based action gameplay that it pioneered when it launched in 1999.",
                new Uri("http://blog.counter-strike.net/index.php/category/updates/")
            );

            feed.Authors.Add(new SyndicationPerson("CSGOTeamFeedback@valvesoftware.com", "Valve Corporation", "http://counter-strike.net"));
            feed.Copyright = new TextSyndicationContent("Valve Corporation");
            feed.Description = new TextSyndicationContent("Counter-Strike: Global Offensive // Blog // Updates");
            feed.Generator = "Ford-RT // RssStation";
            feed.ImageUrl = new Uri("https://steamcdn-a.akamaihd.net/steam/apps/730/header.jpg");
            feed.Language = "en-US";
            #endregion
            
            #region PARSE POSTS FOR FEED ITEMS
            /* FEED ITEMS STORAGE */
            List<SyndicationItem> items = new List<SyndicationItem>();

            /* PARSE PAGE FOR POSTS */
            HtmlNodeCollection posts = doc.DocumentNode.SelectNodes("//div[@id='container']/div[@id='content_container']/div[@id='main_blog']/div[@id='post_container']/div[@class='inner_post']");

            foreach (HtmlNode post in posts)
            {
                #region TITLE
                String title = post.SelectSingleNode(".//h2/a").InnerText;
                Console.WriteLine(title);
                #endregion

                #region CONTENT
                String content = "";
                foreach (HtmlNode paragraph in post.SelectNodes(".//p"))
                {
                    if (!paragraph.HasClass("post_date"))
                    {
                        content += paragraph.OuterHtml;
                    }
                }
                Console.WriteLine(content);
                #endregion

                #region URL
                String itemAlternateLinkTxt = post.SelectSingleNode(".//h2/a").Attributes["href"].Value;
                Uri itemAlternateLink = new Uri(itemAlternateLinkTxt);
                Console.WriteLine(itemAlternateLinkTxt);
                #endregion
                
                #region ID
                String id = Regex.Match(itemAlternateLinkTxt, @"\/(\d+)\/$").Groups[1].Value;
                Console.WriteLine(id);
                #endregion
                
                #region PUBLISH DATE
                String lastUpdatedTimeTxt = post.SelectSingleNode(".//p[@class='post_date']").InnerText;
                lastUpdatedTimeTxt = Regex.Match(lastUpdatedTimeTxt, @"^\d{4}\.\d{2}\.\d{2}").Value;
                String[] lastUpdatedTimeArray = lastUpdatedTimeTxt.Split('.');
                DateTimeOffset lastUpdatedTime = new DateTimeOffset(
                    Int32.Parse(lastUpdatedTimeArray[0]), Int32.Parse(lastUpdatedTimeArray[1]), Int32.Parse(lastUpdatedTimeArray[2]),
                    0, 0, 0,
                    new TimeSpan(0, 0, 0)
                );
                Console.WriteLine(lastUpdatedTimeTxt);
                #endregion
                
                #region SAVE FEED ITEM
                items.Add(new SyndicationItem(
                    title, content, itemAlternateLink, id, lastUpdatedTime
                ));
                #endregion

                Utils.Utils.WriteHr();
            }
            #endregion
            
            feed.Items = items;
            XmlWriter atomWriter = XmlWriter.Create(Configuration.SavePath+Path.DirectorySeparatorChar+"csgo.xml");
            Atom10FeedFormatter atomFormatter = new Atom10FeedFormatter(feed);
            atomFormatter.WriteTo(atomWriter);
            atomWriter.Close();
        }
    }
}
