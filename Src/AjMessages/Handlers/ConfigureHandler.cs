using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AjMessages
{
    class ConfigureHandler : IHandler
    {
        #region IHandler Members

        public void Process(Context ctx)
        {
            string conf = (string) ctx.Message.Body;
            TextReader reader = new StringReader(conf);

            ctx.Server.Reconfigure(reader);
        }

        #endregion
    }
}
