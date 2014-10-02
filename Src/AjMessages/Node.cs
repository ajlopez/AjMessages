using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using AjMessages.Channels;

namespace AjMessages
{
    public class Node
    {
        private bool activated;

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string FullName
        {
            get { return application.Name + "/" + name; }
        }

        private Application application;

        public Application Application
        {
            get { return application; }
            set { application = value; }
        }

        private Dictionary<String, HandlerDefinition> handlers = new Dictionary<string, HandlerDefinition>();

        public void RegisterHandler(HandlerDefinition handler)
        {
            handlers[handler.Name] = handler;
        }
	
        private Dictionary<string,Action> actions = new Dictionary<string,Action>();

        public void RegisterAction(Action action)
        {
            actions[action.Name] = action;
            action.Node = this;
        }

        public Action GetAction(string actionname)
        {
            return actions[actionname];
        }

        public IHandler GetHandler(string handlername)
        {
            return handlers[handlername].GetHandler(this);
        }

        public void Activate()
        {
            if (activated)
                return;

            activated = true;

            foreach (Action action in actions.Values)
                action.Activate();
        }

        public void Post(Message msg)
        {
            msg = MessageUtilities.Clone(msg);

            ThreadPool.QueueUserWorkItem(new WaitCallback(Process), msg);
        }

        private void Process(Object obj)
        {
            Message msg = (Message)obj;

            try
            {
                string nodefullname = MessageUtilities.GetNodeFullName(msg);
                Console.WriteLine("Processing Message in Node " + nodefullname);

                Action action = GetAction(MessageUtilities.GetAction(msg));

                action.Process(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ": " + ex.StackTrace);
            }
        }

        public void ProcessMessage(Message msg)
        {
            Console.WriteLine("External Message");
            Post(msg);
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
