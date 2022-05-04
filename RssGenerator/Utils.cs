using System.ServiceModel.Syndication;
using System.Xml;

namespace RssGenerator;

public class Utils
{
    public static void WriteFeedToFile(SyndicationFeed feed, string path)
    {
        XmlWriter atomWriter = XmlWriter.Create(path);
        Atom10FeedFormatter atomFormatter = new Atom10FeedFormatter(feed);
        atomFormatter.WriteTo(atomWriter);
        atomWriter.Close();
    }
}