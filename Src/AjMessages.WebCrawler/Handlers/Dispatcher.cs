namespace AjMessages.WebCrawler.Handlers
{
    using System;

    using AjMessages;
    using AjMessages.WebCrawler.Messages;

    public class Dispatcher : IHandler
    {
        public Dispatcher() 
        {
        }

        public void Process(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            string partialUri = (string) context.Message.Body;

            Uri url;
            
            Console.WriteLine("[Dispatcher] Dispatching " + partialUri); 
            
            if (!Uri.TryCreate(partialUri, UriKind.Absolute, out url))
            {
                throw new ArgumentException("Invalid Message Format");
            }

            DownloadTarget target = new DownloadTarget(url, 1);

            Message message = MessageUtilities.Create("WebCrawler/Controller/Resolve", target);
            context.Post(message);

            Console.WriteLine("Starting Message Dispatched");
        }
    }
}

