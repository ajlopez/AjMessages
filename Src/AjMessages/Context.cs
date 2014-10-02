namespace AjMessages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Context
    {
        private bool stopped;

        public bool Stopped
        {
            get { return stopped; }
            set { stopped = value; }
        }
	
        private Message message;

        public Message Message
        {
            get { return message; }
            set { message = value; }
        }

        private Server server;

        internal Server Server
        {
            get { return server; }
            set { server = value; }
        }
	
        public void Post(Message msg)
        {
            server.Post(msg);
        }

        public void Stop()
        {
            stopped = true;
        }
    }
}
