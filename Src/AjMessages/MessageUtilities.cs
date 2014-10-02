using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    public class MessageUtilities
    {
        public static string GetNodeFullName(Message msg)
        {
            string action = msg.Action;
            int p = action.LastIndexOf('/');
            if (p < 0)
                return null;
            return action.Substring(0, p);
        }

        public static string GetAction(Message msg)
        {
            string action = msg.Action;
            int p = action.LastIndexOf('/');
            if (p < 0)
                return action;
            return action.Substring(p+1);
        }

        public static Message Create(string action, object obj)
        {
            Console.WriteLine("Create " + action);

            Message msg = new Message();
            msg.Action = action;
            msg.Body = obj;

            return msg;
        }

        public static Message Create(string action, object obj, Message message)
        {
            return Create(action, obj);
        }

        public static Message Create(string action, Message message)
        {
            return Create(action, message.Body);
        }

        public static Message Create(object obj, Message msg)
        {
            return Create(msg.Action, obj);
        }

        public static Message Clone(Message msg)
        {
            Message msg2 = new Message();
            msg2.Action = msg.Action;

            if (msg.Body is ICloneable)
                msg2.Body = ((ICloneable)msg.Body).Clone();
            else
                msg2.Body = msg.Body;

            if (msg.Headers!=null)
                foreach (string key in msg.Headers.Keys)
                    msg2.Headers[key] = msg.Headers[key];

            Console.WriteLine("Clone " + msg2.Action);

            return msg2;
        }
    }
}
