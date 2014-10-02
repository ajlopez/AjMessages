using System;
using System.Collections.Generic;
using System.Text;

using AjMessages.Channels;

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace AjMessages.Wcf
{
    class OutputChannel : AjMessages.Channels.IOutputChannel
    {
        private ChannelFactory<IProcessMessage> factory;
        private IProcessMessage service;
        private string address;

        internal OutputChannel(string address)
        {
            BasicHttpBinding binding = new BasicHttpBinding("BasicHttpBinding_AjMessages");
            //binding.ReaderQuotas.MaxArrayLength = 2000000;
            factory = new ChannelFactory<IProcessMessage>(binding, new EndpointAddress(address));
            service = factory.CreateChannel();
            this.address = address;
        }

        #region IOutputChannel Members

        public void Open()
        {
            Console.WriteLine("OutputChannel {0} opened", address);
        }

        public void Close()
        {
            Console.WriteLine("OutputChannel {0} closed", address);
        }

        public void Send(Message msg)
        {
            service.ProcessMessage(Utilities.ToWCFMessage(msg));
        }

        #endregion
    }
}
