using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace RssStation.Utils
{
    public class SyndicationFeedToString
    {
        public static string Convert(SyndicationFeed feed)
        {
            var sw = new Utf8StringWriter();
            var xw = XmlWriter.Create(sw);

            var rssFormatter = new Rss20FeedFormatter(feed);
            rssFormatter.WriteTo(xw);
            xw.Close();
            var content = sw.ToString();

            return content;
        }
    }
}