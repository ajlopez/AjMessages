using System;
using System.Collections.Generic;
using System.Text;
using AjMessages.Channels;
using AjMessages;
using Robotics.AjMessagesDssServices.Node;

namespace Robotics.AjMessagesDssServices
{
    class OutputChannel : IOutputChannel
    {
        public Node.NodeServiceOperations remoteserver;

        public void Open()
        {
            return;
        }

        public void Close()
        {
            return;
        }

        public void Send(Message msg)
        {
            ProcessMessageRequest newmsg = new ProcessMessageRequest();
            newmsg.Action = msg.Action;
            newmsg.Body = msg.Body.ToString();
            remoteserver.Post(new ProcessMessage(newmsg));
        }
    }

    class ChannelFactory : IChannelFactory
    {
        private Node.NodeService dssnode;

        public ChannelFactory(Node.NodeService node)
        {
            dssnode = node;
        }

        public IOutputChannel CreateOutputChannel(string address)
        {
            OutputChannel channel = new OutputChannel();
            channel.remoteserver = dssnode.GetServer(address);

            return channel;
        }

        public IInputChannel CreateInputChannel(string address, IMessageConsumer msgconsumer)
        {
            return null;
        }
    }
}
