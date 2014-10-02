using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    public interface IHandler
    {
        void Process(Context ctx);
    }
}
