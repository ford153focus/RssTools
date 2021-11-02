using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RedditSharp;
using RedditSharp.Things;

namespace RssStation.Utils.Grabbers
{
    class RedditGrabber
    {
        public static Listing<RedditSharp.Things.Post> GetNewSubRedditPosts(string subRedditName)
        {
            var webAgent = new BotWebAgent(
                Credentials.Reddit.Login,
                Credentials.Reddit.Password,
                Credentials.Reddit.Id,
                Credentials.Reddit.Secret,
                Credentials.Reddit.RedirectURI
            );
            var reddit = new Reddit(webAgent, false);
            var subReddit = reddit.GetSubreddit(subRedditName);

            return subReddit.New;
        }
    }
}