namespace AjMessages.WebCrawler.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using AjMessages;
    using AjMessages.WebCrawler.Messages;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class Harvester : IHandler
    {
        public void Process(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            DownloadTarget target = (DownloadTarget) context.Message.Body;
            
            Console.WriteLine("[Harvester] Processing " + target.Target.ToString()); 
            
            IEnumerable<string> links = HarvestUrls(target.Content);
            foreach (string link in links)
            {
                Uri uri = new Uri(link, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri)
                {
                    uri = new Uri(target.Target, uri);
                }

                DownloadTarget newTarget = new DownloadTarget(uri, target.Depth + 1);
                newTarget.Referrer = target.Target;
                Message message = MessageUtilities.Create("WebCrawler/Controller/Resolve", newTarget);
                context.Post(message);
                
                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "Url To Harvest {0} {1}", newTarget.Depth, newTarget.TargetAddress));
            }
            
            Console.WriteLine("[Harvester] Processed " + target.Target.ToString());
        }

        private static List<string> HarvestUrls(string content)
        {
            string regexp = @"href=\s*""([^&""]*)""";

            MatchCollection matches = Regex.Matches(content, regexp);
            List<string> links = new List<string>();

            foreach (Match m in matches)
            {
                if (!links.Contains(m.Groups[1].Value))
                {
                    links.Add(m.Groups[1].Value);
                }
            }

            return links;
        }
    }
}
