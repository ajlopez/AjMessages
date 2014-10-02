using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages.Channels
{
    public interface IChannelFactory
    {
        IOutputChannel CreateOutputChannel(string address);
        IInputChannel CreateInputChannel(string address, IMessageConsumer msgconsumer);
    }
}
