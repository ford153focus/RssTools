// using System;
// using Google.Apis.Services;
// using Google.Apis.YouTube.v3;

// namespace RssStation.Utils.Grabbers
// {
//     /// <summary>
//     /// YouTube Data API v3 sample: search by keyword.
//     /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
//     /// See https://developers.google.com/api-client-library/dotnet/get_started
//     ///
//     /// Set ApiKey to the API key value from the APIs & auth > Registered apps tab of
//     ///   https://cloud.google.com/console
//     /// Please ensure that you have enabled the YouTube Data API for your project.
//     /// </summary>
//     class YouTube {
//         public static void Run () {
//             var youtubeService = new YouTubeService (new BaseClientService.Initializer () {
//                 ApiKey = Credentials.Google.youtubeApiKey,
//                     ApplicationName = "vestnik.YouTube.Search"
//             });

//             var searchRequest = youtubeService.Search.List ("snippet");

//             searchRequest.Q = "##amd";
//             ExecuteRequest (searchRequest);

//             searchRequest.Q = "amd";
//             ExecuteRequest (searchRequest);

//             searchRequest.Q = "";
//             searchRequest.ChannelId = "UCHQDjDDW8w2RieO-IuqYlyg";
//             ExecuteRequest (searchRequest);

//         }

//         static void ExecuteRequest (SearchResource.ListRequest searchRequest) {
//             searchRequest.MaxResults = 50;
//             searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
//             searchRequest.Type = "video";
//             var response = searchRequest.Execute ();
//             var db = new YoutubeVideoContext ();
//             db.Database.EnsureCreated ();

//             foreach (var item in response.Items) {
//                 YoutubeVideo YtVideo = new YoutubeVideo ();
//                 YtVideo.Id = item.Id.VideoId;
//                 YtVideo.Title = item.Snippet.Title;
//                 YtVideo.Description = item.Snippet.Description;
//                 YtVideo.ChannelTitle = item.Snippet.ChannelTitle;
//                 YtVideo.PublishedAt = item.Snippet.PublishedAt;
//                 YtVideo.ThumbnailHighUrl = item.Snippet.Thumbnails.High.Url;
//                 Console.WriteLine (YtVideo.Title);
//                 db.YoutubeVideos.Add (YtVideo);
//                 try { db.SaveChanges (); } catch (System.Exception) { }
//             }
//         }
//     }
// }
