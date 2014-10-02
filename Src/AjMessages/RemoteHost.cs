using System;
using System.Collections.Generic;
using System.Text;

using AjMessages.Channels;

namespace AjMessages
{
    class RemoteHost : Host
    {
        private IOutputChannel outputchannel;

        public IOutputChannel OutputChannel
        {
            get { return outputchannel; }
            set { outputchannel = value; }
        }

        public override void Post(Message msg)
        {
            if (outputchannel != null)
                try
                {
                    outputchannel.Send(msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ": "+ex.StackTrace);
                }
        }

        public override void Start()
        {
            if (outputchannel != null)
                outputchannel.Open();

            Log(String.Format("Remote Host {0} started", Name));
        }

        public override void Stop()
        {
            if (outputchannel != null)
            {
                outputchannel.Close();
                outputchannel = null;
            }

            Log(String.Format("Remote Host {0} stopped", Name));
        }
    }
}
