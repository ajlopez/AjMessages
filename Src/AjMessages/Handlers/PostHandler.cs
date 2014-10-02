using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    class PostHandler : IHandler
    {
        private string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
	

        #region IHandler Members

        public void Process(Context ctx)
        {
            Console.WriteLine("Post to " + action);
            Message msg = MessageUtilities.Create(action, ctx.Message);
            //Message msg = Message.CreateMessage(MessageVersion.Soap11, action, ctx.Message.GetReaderAtBodyContents());

            //for (int k = 0; k < ctx.Message.Headers.Count; k++)
            //    if (ctx.Message.Headers[k].Name != "Action")
            //    {
            //        Console.WriteLine("Copying Header {0}", ctx.Message.Headers[k].ToString());
            //        msg.Headers.CopyHeaderFrom(ctx.Message, k);
            //    }

            //foreach (string key in ctx.Message.Properties.Keys)
            //{
            //    Console.WriteLine("Copying Property {0}", key);
            //    msg.Properties[key] = ctx.Message.Properties[key];
            //}

            ctx.Post(msg);
        }

        #endregion
    }
}
