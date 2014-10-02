using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace AjMessages.Wcf
{
    [ServiceContract()]
    interface IProcessMessage
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessMessage(System.ServiceModel.Channels.Message msg);
    }
}
