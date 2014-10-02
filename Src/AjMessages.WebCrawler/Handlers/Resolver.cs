namespace AjMessages.WebCrawler.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using AjMessages;
    using AjMessages.WebCrawler.Messages;

    public class Resolver : IHandler
    {
        private List<Uri> downloadedAddresses;

        public Resolver() 
        {
            this.downloadedAddresses = new List<Uri>();
        }

        public void Process(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            DownloadTarget target = (DownloadTarget) context.Message.Body;

            Console.WriteLine("[Resolver] processing " + target.Target.ToString());
            
            if (target.Depth > 5)
            {                
                Console.WriteLine(
                   string.Format(CultureInfo.InvariantCulture, "URL rejected {0} by max depth", target.TargetAddress));
                return;
            }

            if ((target.Target.Scheme != Uri.UriSchemeHttp) &&
                (target.Target.Scheme != Uri.UriSchemeHttps))
            {                
                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL rejected {0}: unsupported protocol", target.TargetAddress));
                return;
            }

            if (target.Referrer != null &&
                target.Target.Host != target.Referrer.Host)
            {                
                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL rejected {0}: different host", target.TargetAddress));
                return;
            }

            lock (this.downloadedAddresses)
            {
                if (this.downloadedAddresses.Contains(target.Target))
                {                    
                    Console.WriteLine(
                        string.Format(CultureInfo.InvariantCulture, "URL rejected {0}: already downloaded", target.TargetAddress));
                }
                else
                {
                    this.downloadedAddresses.Add(target.Target);
                    
                    Message newmessage = MessageUtilities.Create("WebCrawler/Downloader/Download", target);
                    context.Post(newmessage);
                    
                    Console.WriteLine(
                        string.Format(CultureInfo.InvariantCulture, "URL accepted: {0}", target.TargetAddress));
                }
            }
        }
    }
}