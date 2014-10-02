using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages.Channels
{
    public interface IOutputChannel
    {
        void Open();
        void Close();
        void Send(Message msg);
    }
}
