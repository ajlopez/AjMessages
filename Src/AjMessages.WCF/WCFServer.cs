using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages.Wcf
{
    public class WcfServer : Server
    {
        public WcfServer()
        {
            ChannelFactory = new ChannelFactory();
        }
    }
}
