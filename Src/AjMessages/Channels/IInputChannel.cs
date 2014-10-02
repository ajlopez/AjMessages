using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages.Channels
{
    public interface IInputChannel
    {
        void Open();
        void Close();
    }
}

