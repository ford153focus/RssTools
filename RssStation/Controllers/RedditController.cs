using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using RssStation.Utils;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using RedditSharp;

namespace RssStation.Controllers
{
    public class RedditController : Controller
    {
        private const int PostsCount = 53;

        private static async Task<string> GrabPostContentAsync(RedditSharp.Things.Post post)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(post.Shortlink);

            var postContent = document.QuerySelector("div[data-test-id='post-content']");
            postContent.RemoveChild(postContent.FirstChild);
            postContent.RemoveChild(postContent.LastChild);

            return postContent.InnerHtml;
        }

        private List<SyndicationItem> GetElements(string subRedditName, string flair)
        {
            var items = new List<SyndicationItem>();

            #region LOAD AND PARSE PAGE
            var webAgent = new BotWebAgent(
                Credentials.Reddit.Login,
                Credentials.Reddit.Password,
                Credentials.Reddit.Id,
                Credentials.Reddit.Secret,
                Credentials.Reddit.RedirectURI
            );
            var reddit = new Reddit(webAgent, false);
            var subReddit = reddit.GetSubreddit(subRedditName);
            var NewSubRedditPosts = subReddit.New.GetListing(PostsCount);
            #endregion

            Parallel.ForEach(NewSubRedditPosts, async (post) =>
            {
                try
                {
                    var postType = (post.LinkFlairText is string) ? post.LinkFlairText.Trim() : "Other";
                    if (postType.ToLower() != flair.ToLower()) return;

                    var title = post.Title;
                    var url = post.Shortlink;
                    var id = post.Id;
                    var content = await GrabPostContentAsync(post);
                    var date = post.Created;

                    SyndicationItem item = new SyndicationItem(
                        title,
                        content,
                        new Uri(url),
                        id,
                        date
                    );

                    item.Categories.Add(new SyndicationCategory(postType));
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

        public async Task<ContentResult> IndexAsync(string subReddit, string flair)
        {
            #region LOAD AND PARSE WEB PAGE FOR TITLE AND DESC
            var url = $"https://www.reddit.com/r/{subReddit}/?f=flair_name%3A%22{flair}%22";

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            var title = document.Head.QuerySelector("title").InnerHtml;
            var description = document.Head.QuerySelector("meta[name='description']").GetAttribute("content");
            #endregion

            #region CONSTRUCT FEED
            var feed = new SyndicationFeed($"Reddit :: {subReddit} :: {flair}", title, new Uri(url));
            feed.Authors.Add(new SyndicationPerson("ford153focus@gmail.com", "Joe Ford", "https://github.com/ford153focus"));
            feed.Items = GetElements(subReddit, flair);
            feed.ImageUrl = new Uri("https://www.redditstatic.com/desktop2x/img/favicon/android-icon-192x192.png");
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