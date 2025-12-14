using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Text.RegularExpressions;

namespace GyorsHir.Model.RSS
{
    internal static class RSSParser
    {

        #region Private Fields

        private static Regex lineBreakRegex = new Regex(@"(>|$)(\W|\n|\r)+<", RegexOptions.Multiline);
        private static Regex stripFormattingRegex = new Regex(@"<[^>]*(>|$)", RegexOptions.Multiline);
        private static Regex tagWhiteSpaceRegex = new Regex(@"<(br|BR)\s{0,1}\/{0,1}>", RegexOptions.Multiline);

        #endregion

        #region Private Methods

        private static RSSChannel ParseChannel(XmlNode channel)
        {
            if (channel == null)
                return null;

            RSSChannel result = new RSSChannel();
            List<RSSItem> items = new List<RSSItem>();

            foreach (XmlNode child in channel.ChildNodes)
                switch (child.Name.ToLower())
                {
                    case "title": result.Title = child.InnerText; break;
                    case "link": try { result.Link = new Uri(child.InnerText); } catch { } break;
                    case "description": result.Description = child.InnerText; break;

                    case "item":
                    case "entry":
                        items.Add(ParseItem(child)); break;
                }

            /*if (string.IsNullOrWhiteSpace(result.Title) ||
                result.Link == null || string.IsNullOrWhiteSpace(result.Link.OriginalString))
                return null;*/

            result.Items = items.Where(i => i != null).ToList();
            return result;
        }

        private static RSSItem ParseItem(XmlNode item)
        {
            if (item == null)
                return null;

            RSSItem result = new RSSItem();

            foreach (XmlNode child in item.ChildNodes)
                switch (child.Name.ToLower())
                {
                    case "title": result.Title = child.InnerText; break;
                    case "link": try { result.Link = new Uri(child.InnerText); } catch { } break;
                    case "description": result.Description = HtmlToPlainText(child.InnerText); break;
                    case "author": result.Author = child.InnerText; break;
                    case "category": result.Category = child.InnerText; break;
                    case "guid": result.GUID = child.InnerText; break;
                    case "pubdate": try { result.PublishDate = DateTimeOffset.Parse(child.InnerText); } catch { } break;
                }

            return result;
        }

        //https://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text
        private static string HtmlToPlainText(string html)
        {
            //Decode html specific characters
            string text = HttpUtility.HtmlDecode(html);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

        #endregion

        #region Public Methods

        public static RSSChannel ParseString(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
                return null;

            XmlDocument xml = new XmlDocument();
            try
            {
                xml.LoadXml(xmlString);
            }
            catch (XmlException)
            {
                return null;
            }

            return ParseXml(xml);
        }
        public static RSSChannel ParseXml(XmlDocument xml)
        {
            if (xml == null)
                return null;

            XmlNodeList channelNodes = xml.GetElementsByTagName("channel");
            if (channelNodes.Count > 0)
                return ParseChannel(channelNodes.Item(0));

            XmlNodeList feedNodes = xml.GetElementsByTagName("feed");
            if (feedNodes.Count > 0)
                return ParseChannel(feedNodes.Item(0));

            return null;
        }

        #endregion

    }
}