using System.ServiceModel.Syndication;
using HtmlAgilityPack;
using RedditSharp;
using RedditSharp.Things;
using Reddit = RssSharedLibrary.Cfg.Reddit;

namespace RssSharedLibrary.Utils.Grabbers
{
    internal static class RedditGrabber
    {
        private static async Task<Listing<Post>> GetPosts(string subRedditName)
        {
            var webAgent = new BotWebAgent(
                Reddit.Login,
                Reddit.Password,
                Reddit.Id,
                Reddit.Secret,
                Reddit.RedirectUri
            );
            RedditSharp.Reddit reddit = new RedditSharp.Reddit(webAgent, false);
            Subreddit? subReddit = await reddit.GetSubredditAsync(subRedditName);

            return subReddit.GetPosts(Subreddit.Sort.New);
        }
        
        private static async Task<Dictionary<string, List<SyndicationItem>>> GrabItems(string subReddit)
        {
            var newSubRedditPosts = await GetPosts(subReddit);

            var items = new Dictionary<string, List<SyndicationItem>>();

            await foreach (var post in newSubRedditPosts)
            {
                Console.WriteLine(post.Title);

                try
                {
                    var item = new SyndicationItem
                    {
                        Id = post.Id,
                        Title = new TextSyndicationContent(post.Title),
                        Content = SyndicationContent.CreateHtmlContent(GrabPostContent(post)),
                        PublishDate = post.CreatedUTC
                    };
                    item.AddPermalink(new Uri(post.Shortlink));

                    var postType = post.LinkFlairText is not null ? post.LinkFlairText.Trim() : "Other";
                    if (!items.ContainsKey(postType))
                    {
                        items.Add(postType, new List<SyndicationItem>());
                    }

                    items[postType].Add(item);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            }

            return items;
        }

        private static string GrabPostContent(Post post)
        {
            string output = "";

            if (post.Url.Host != "www.reddit.com")
            {
                output += $"<p>via <a href='{post.Url}'>{post.Url.Host}</a></p>";
            }

            if (post.Thumbnail.IsAbsoluteUri)
            {

            }

            var web = new HtmlWeb();
            var doc = web.Load(post.Shortlink);
            var paragraph = doc.DocumentNode.SelectSingleNode("//div[@data-test-id=\"post-content\"]//div//p");
            if (paragraph != null)
            {
                foreach (var node in paragraph.ParentNode.Descendants().Where(x => x.NodeType == HtmlNodeType.Element))
                {
                    node.Attributes.RemoveAll();
                }
                RemoveComments(paragraph.ParentNode);
                output += paragraph.ParentNode.InnerHtml;
            }

            return output;
        }

        static void RemoveComments(HtmlNode node)
        {
            if (!node.HasChildNodes)
            {
                return;
            }

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].NodeType == HtmlNodeType.Comment)
                {
                    node.ChildNodes.RemoveAt(i);
                    --i;
                }
            }

            foreach (HtmlNode subNode in node.ChildNodes)
            {
                RemoveComments(subNode);
            }
        }

        public static async Task<List<SyndicationFeed>> GetFeeds(string subReddit)
        {
            var items = await GrabItems(subReddit);
            List<SyndicationFeed> feeds = new();
            
            foreach (var itemsCategory in items)
            {
                SyndicationFeed feed = new SyndicationFeed
                {
                    Description = new TextSyndicationContent(@"A subreddit dedicated to Advanced Micro Devices and its products. 
                    This subreddit is community run and does not represent AMD unless otherwise specified."),
                    Generator = "Ford-RT // RssStation",
                    ImageUrl = new Uri("https://b.thumbs.redditmedia.com/mD2HFHph0Md1vppzBWNoItU5TrAPLWbc7vNBfP3lsxA.png"),
                    Items = itemsCategory.Value,
                    Language = "en-US",
                    LastUpdatedTime = itemsCategory.Value.First().LastUpdatedTime,
                    Title = new TextSyndicationContent("AMD Subreddit :: " + itemsCategory.Key)
                };
                
                feeds.Add(feed);
            }

            return feeds;
        }
    }
}