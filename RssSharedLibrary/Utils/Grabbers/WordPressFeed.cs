using System.ServiceModel.Syndication;
using WordPressPCL;
using WordPressPCL.Models;

namespace RssSharedLibrary.Utils.Grabbers
{
    public static class WordPressFeed
    {
        public static async Task<SyndicationFeed> GetFeed(string url)
        {
            #region LOAD PAGE
            var client = new WordPressClient(url);
            var wpPosts = await client.Posts.GetAllAsync();
            var wpPages = await client.Pages.GetAllAsync();
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
            
            /* FEED ITEMS STORAGE */
            List<SyndicationItem> items = new List<SyndicationItem>();
            
            foreach (Post post in wpPosts)
            {
                var item = new SyndicationItem();
                
                #region AUTHOR
                var author = await client.Users.GetByIDAsync(post.Author);
                var syndicationPerson = new SyndicationPerson
                {
                    Email = author.Email,
                    Name = author.Name,
                    Uri = author.Url
                };
                item.Authors.Add(syndicationPerson);
                #endregion

                #region CATEGORY
                foreach (var categoryId in post.Categories)
                {
                    var categoryWp = await client.Categories.GetByIDAsync(categoryId);
                    var categoryRss = new SyndicationCategory
                    {
                        Name = categoryWp.Name
                    };
                    item.Categories.Add(categoryRss);
                }
                #endregion

                item.Content = new TextSyndicationContent(post.Content.Raw);
                item.Id = post.Id.ToString();
                item.Title = new TextSyndicationContent(post.Title.Rendered);
                item.BaseUri = new Uri(post.Link);
                item.PublishDate = post.Date;
                item.LastUpdatedTime = post.Modified;

                items.Add(item);
            }
            
            foreach (Page page in wpPages)
            {
                var item = new SyndicationItem();
                
                #region AUTHOR
                var author = await client.Users.GetByIDAsync(page.Author);
                var syndicationPerson = new SyndicationPerson
                {
                    Email = author.Email,
                    Name = author.Name,
                    Uri = author.Url
                };
                item.Authors.Add(syndicationPerson);
                #endregion

                item.Content = new TextSyndicationContent(page.Content.Raw);
                item.Id = page.Id.ToString();
                item.Title = new TextSyndicationContent(page.Title.Rendered);
                item.BaseUri = new Uri(page.Link);
                item.PublishDate = page.Date;
                item.LastUpdatedTime = page.Modified;

                items.Add(item);
            }
            
            items = items.OrderBy(o=>o.PublishDate).ToList(); //sort by date
            feed.Items = items;
            return feed; 
        }
    }
}
