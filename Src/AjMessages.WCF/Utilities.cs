using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.ServiceModel;
using System.ServiceModel.Channels;

using System.Runtime.Serialization.Formatters.Binary;

namespace AjMessages.Wcf
{
    class Utilities
    {
        static BinaryFormatter fmt = new BinaryFormatter();

        internal static AjMessages.Message ToAjMessage(System.ServiceModel.Channels.Message msg)
        {
            MemoryStream ms = new MemoryStream(msg.GetBody<byte[]>());
            object obj = fmt.Deserialize(ms);
            AjMessages.Message newmsg = new AjMessages.Message();
            newmsg.Action = msg.Headers.Action;
            newmsg.Body = obj;

            return newmsg;
        }

        internal static System.ServiceModel.Channels.Message ToWCFMessage(AjMessages.Message msg)
        {
            MemoryStream ms = new MemoryStream();
            fmt.Serialize(ms, msg.Body);
            ms.Close();
            return CreateWCFMessage(msg.Action,ms.ToArray());
        }

        static System.ServiceModel.Channels.Message CreateWCFMessage(string action, object obj)
        {
            Console.WriteLine("Create " + action);

            return System.ServiceModel.Channels.Message.CreateMessage(MessageVersion.Soap11, action, obj);
        }
    }
}
