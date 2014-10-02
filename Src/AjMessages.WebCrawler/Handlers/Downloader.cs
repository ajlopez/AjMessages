namespace AjMessages.WebCrawler.Handlers
{
    using System;
    using System.Globalization;
    using System.Net;

    using AjMessages;
    using AjMessages.WebCrawler.Messages;

    public class Downloader : IHandler
    {
        public void Process(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            DownloadTarget target = (DownloadTarget) context.Message.Body;
            
            Console.WriteLine("[Downloader] Processing " + target.Target.ToString());

            try
            {
                WebClient client = new WebClient();
                target.Content = client.DownloadString(target.Target);

                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL {0} downloaded", target.TargetAddress));
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine(
                   string.Format(
                   CultureInfo.InvariantCulture, 
                   "URL could not be downloaded", 
                   target.TargetAddress));

                return;
            }

            Message newMessage = MessageUtilities.Create("WebCrawler/Harvester/Harvest", target);
            context.Post(newMessage);

            Console.WriteLine("[Downloader] Processed " + target.Target.ToString());
        }
    }
}
