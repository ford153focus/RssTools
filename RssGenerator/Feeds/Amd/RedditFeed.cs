using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using HtmlAgilityPack;
using RedditSharp.Things;
using RssGenerator.Cfg;
using RssGenerator.Utils.Grabbers;

namespace RssGenerator.Feeds.Amd
{
    class RedditFeed
    {
        public static Dictionary<string, List<SyndicationItem>> Grab()
        {
            var newSubRedditPosts = RedditGrabber.GetNewSubRedditPosts("Amd");

            var items = new Dictionary<string, List<SyndicationItem>>();

            foreach (var post in newSubRedditPosts)
            {
                Console.WriteLine(post.Title);

                try
                {
                    var item = new SyndicationItem
                    {
                        Id = post.Id,
                        Title = new TextSyndicationContent(post.Title),
                        Content = SyndicationContent.CreateHtmlContent(GrabPostContent(post)),
                        LastUpdatedTime = post.Created,
                        PublishDate = post.Created
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

        public static void Compile()
        {
            var items = Grab();

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

                Directory.CreateDirectory(Configuration.SavePath);
                XmlWriter atomWriter = XmlWriter.Create(String.Format(
                    "{0}{1}amd.reddit.{2}.xml",
                    Configuration.SavePath,
                    Path.DirectorySeparatorChar.ToString(),
                    itemsCategory.Key.ToLower().Replace(" ", "_")
                ));
                Atom10FeedFormatter atomFormatter = new Atom10FeedFormatter(feed);
                atomFormatter.WriteTo(atomWriter);
                atomWriter.Close();
            }
        }
    }
}