namespace AjMessages.WebCrawler.Messages
{
    using System;
    using System.Xml.Serialization;
    
    [Serializable]
    public class DownloadTarget
    {
        private Uri target;
        private Uri referrer;
        private int depth;
        private string content;

        public DownloadTarget() { }

        public DownloadTarget(Uri target, int depth)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (depth < 1)
            {
                throw new ArgumentOutOfRangeException("depth");
            }

            this.target = target;
            this.depth = depth;
        }
      
        public Uri Target
        {
            get { return this.target; }
        }

        public string TargetAddress
        {
            get { return Uri.EscapeUriString(this.target.ToString()); }
            set { this.target = new Uri(value, UriKind.RelativeOrAbsolute); }
        }

        public Uri Referrer
        {
            get { return this.referrer; }
            set { this.referrer = value; }
        }

        public string ReferrerAddress
        {
            get
            {
                if (this.referrer == null)
                {
                    return null;
                }

                return Uri.EscapeUriString(this.referrer.ToString());
            }

            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.referrer = new Uri(value, UriKind.RelativeOrAbsolute);
                }
            }
        }
        
        public int Depth
        {
            get { return this.depth; }
            set { this.depth = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }        
    }
}
