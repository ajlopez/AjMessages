using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using AjMessages.Channels;

namespace AjMessages
{
    delegate void MessageProcessDelegate(Message msg);

    public class LocalHost : Host, IMessageConsumer
    {	
        private bool running;

        private IInputChannel inputchannel;

        public IInputChannel InputChannel
        {
            get { return inputchannel; }
            set { inputchannel = value; }
        }
	
        public override void Start()
        {
            running = true;

            if (inputchannel != null)
                inputchannel.Open();

            foreach (Node node in nodes.Values)
                node.Start();

            Log(String.Format("Local Host {0} started",Name));
        }

        public override void Stop()
        {
            running = false;

            foreach (Node node in nodes.Values)
                node.Stop();

            if (inputchannel != null)
            {
                inputchannel.Close();
                inputchannel = null;
            }

            Log(String.Format("Local Host {0} stopped", Name));
        }

        public override void RegisterNode(Node node)
        {
            base.RegisterNode(node);
            node.Activate();
        }

        public override void Post(Message msg)
        {
            if (!running)
                return;

            msg = MessageUtilities.Clone(msg);

            ThreadPool.QueueUserWorkItem(new WaitCallback(Process), msg);
        }

        private void Process(Object obj)
        {
            if (!running)
                return;

            Message msg = (Message)obj;

            Node node;
            try
            {
                string nodefullname = MessageUtilities.GetNodeFullName(msg);
                Console.WriteLine("Processing " + nodefullname);

            if (!nodes.TryGetValue(nodefullname,out node))
                return;

            Action action = node.GetAction(MessageUtilities.GetAction(msg));

                action.Process(msg);
            }
            catch (Exception ex)
            {
                Log(ex.Message + ": "+ex.StackTrace);
            }
        }

        #region IMessageConsumer Members

        public void Process(Message msg)
        {
            Post(msg);
        }

        #endregion
    }
}
