using System.ServiceModel.Syndication;
using Newtonsoft.Json;
using RssSharedLibrary.Models;

namespace RssSharedLibrary.Utils.Grabbers
{
    public static class YandexZenGrabber
    {
        public static async Task<SyndicationFeed> GetFeedAsync(string tag)
        {
            #region LOAD PAGE

            string url = "https://zen.yandex.ru/api/v3/launcher/more?interest_name=" + tag;
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(url);
            using HttpContent content = response.Content;
            string json = await content.ReadAsStringAsync();
            
            YandexZenResponse yandexZenResponse = JsonConvert.DeserializeObject<YandexZenResponse>(json);
            #endregion
            
            

            #region CREATE FEED
            var feed = new SyndicationFeed(
                "Yandex Zen | " + tag, 
                "Yandex Zen | " + tag, 
                new Uri("https://zen.yandex.ru/t/" + tag)
            );
            feed.Authors.Add(new SyndicationPerson("ford153focus@gmail.com"));
            feed.ImageUrl = new Uri("https://yastatic.net/s3/zen-lib/favicons3/apple-touch-icon.png");
            #endregion
            
            /* FEED ITEMS STORAGE */
            List<SyndicationItem> items = new List<SyndicationItem>();
            
            Parallel.ForEach(yandexZenResponse.items, yItem =>
            {
                try
                {
                    var item = new SyndicationItem();

                    item.Title = new TextSyndicationContent(yItem.title);
                    item.BaseUri = new Uri(yItem.link);
                    item.Id = yItem.id;
                    item.Content = new TextSyndicationContent(yItem.text);
                    item.PublishDate = DateTimeOffset.Parse(yItem.creation_time);

                    items.Add(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
            
            feed.Items = items;
            return feed; 
        }
    }
}
