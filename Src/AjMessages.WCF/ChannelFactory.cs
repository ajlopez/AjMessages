using System;
using System.Collections.Generic;
using System.Text;

using AjMessages.Channels;

namespace AjMessages.Wcf
{
    class ChannelFactory : IChannelFactory
    {
        #region IChannelFactory Members

        public IOutputChannel CreateOutputChannel(string address)
        {
            return new OutputChannel(address);
        }

        public IInputChannel CreateInputChannel(string address, IMessageConsumer consumer)
        {
            return new InputChannel(address, consumer);
        }

        #endregion
    }
}
