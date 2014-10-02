using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    class PrintMessageHandler : IHandler
    {

        #region IHandler Members

        public void Process(Context ctx)
        {
            Console.WriteLine("=========");
            Console.WriteLine(ctx.Message.ToString());
            Console.WriteLine("=========");
        }

        #endregion
    }
}
