using System;
using System.Collections.Generic;
using System.Text;

using AjMessages;

namespace AjMessages.SampleApp.Handlers
{
    class DecrementHandler : IHandler
    {
        #region IHandler Members

        public void Process(Context ctx)
        {
            int counter = (int) ctx.Message.Body;

            Console.WriteLine("Decrementing {0}", counter);

            if (counter <= 0)
            {
                ctx.Stop();
                return;
            }

            counter--;

            //Message msg = Message.CreateMessage(MessageVersion.Soap11, "App1/Node1/Process", counter);
            //ctx.Post(msg);
            //ctx.Stop();
            ctx.Message = MessageUtilities.Create(counter, ctx.Message);
        }

        #endregion
    }
}
