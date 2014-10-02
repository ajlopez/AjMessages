using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    public abstract class Host
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string address;

        public string Address
        {
            get {
                if (address == null)
                    return "http://localhost:" + port + "/AjMessages";

                return address; 
            }
            set { address = value; }
        }

        protected Dictionary<string, Node> nodes = new Dictionary<string,Node>();

        public bool HasNode(string nodefullname)
        {
            return nodes.ContainsKey(nodefullname);
        }

        public virtual void RegisterNode(Node node)
        {
            nodes[node.FullName] = node;
        }

        protected void Log(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected void Log(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public abstract void Post(Message msg);
        public abstract void Start();
        public abstract void Stop();
    }
}
