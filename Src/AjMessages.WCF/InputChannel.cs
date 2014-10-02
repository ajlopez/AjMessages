using System;
using System.Collections.Generic;
using System.Text;

using AjMessages.Channels;
using System.ServiceModel;

namespace AjMessages.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class InputChannel : IInputChannel, IProcessMessage
    {
        private IMessageConsumer consumer;
        private string address;

        private System.ServiceModel.ServiceHost service;

        internal InputChannel(string address, IMessageConsumer consumer)
        {
            this.address = address;
            this.consumer = consumer;
            service = new System.ServiceModel.ServiceHost(this);
            BasicHttpBinding binding = new BasicHttpBinding("BasicHttpBinding_AjMessages");
            //binding.ReaderQuotas.MaxArrayLength = 2000000;
            service.AddServiceEndpoint(typeof(IProcessMessage), binding, address);
        }

        #region IInputChannel Members

        public void Open()
        {
            service.Open();
            Console.WriteLine("InputChannel {0} opened", address);
        }

        public void Close()
        {
            if (service.State == CommunicationState.Opened)
                service.Close();

            Console.WriteLine("InputChannel {0} closed", address);
        }

        #endregion

        #region IProcessMessage Members

        public void ProcessMessage(System.ServiceModel.Channels.Message msg)
        {
            consumer.Process(Utilities.ToAjMessage(msg));
        }

        #endregion
    }
}
