using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    public class Message
    {
        private string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
	
        private object body;

        public object Body
        {
            get { return body; }
            set { body = value; }
        }

        private Dictionary<string,object> headers;

        public Dictionary<string,object> Headers
        {
            get { return headers; }
            set { headers = value; }
        }
	
	
    }
}
