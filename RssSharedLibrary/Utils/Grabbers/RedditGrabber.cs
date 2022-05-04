using RedditSharp;
using RedditSharp.Things;
using Reddit = RssGenerator.Cfg.Reddit;

namespace RssSharedLibrary.Utils.Grabbers
{
    class RedditGrabber
    {
        public static Listing<Post> GetNewSubRedditPosts(string subRedditName)
        {
            var webAgent = new BotWebAgent(
                Reddit.Login,
                Reddit.Password,
                Reddit.Id,
                Reddit.Secret,
                Reddit.RedirectUri
            );
            var reddit = new RedditSharp.Reddit(webAgent, false);
            var subReddit = reddit.GetSubreddit(subRedditName);

            return subReddit.New;
        }
    }
}