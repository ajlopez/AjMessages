namespace AjMessages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Action
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private IHandler handler;

        private string handlerName;

        public string HandlerName
        {
            get { return handlerName; }
            set { handlerName = value; }
        }
	
        private Node node;

        public Node Node
        {
            get { return node; }
            set { node = value; }
        }
	
        public void Process(Message msg)
        {
            // TODO: null argument
            if (handler == null)
                return;

            Context ctx = new Context();
            ctx.Message = msg;
            ctx.Server = node.Application.Server;
            handler.Process(ctx);
        }

        public void Activate()
        {
            handler = node.GetHandler(handlerName);
        }
    }
}
