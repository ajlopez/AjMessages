using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages.Channels
{
    public interface IMessageConsumer
    {
        void Process(Message msg);
    }
}
