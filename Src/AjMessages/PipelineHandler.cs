using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    class PipelineHandler : IHandler
    {
        private List<IHandler> handlers = new List<IHandler>();

        public void AddHandler(IHandler handler)
        {
            handlers.Add(handler);
        }

        #region IHandler Members

        public void Process(Context ctx)
        {
            foreach (IHandler handler in handlers)
            {
                if (ctx.Stopped)
                    break;

                handler.Process(ctx);
            }
        }

        #endregion
    }
}
