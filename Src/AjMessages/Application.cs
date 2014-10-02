namespace AjMessages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Application
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Server server;

        public Server Server
        {
            get { return this.server; }
            set { this.server = value; }
        }
	
        private Dictionary<string, Node> nodes = new Dictionary<string,Node>();

        public Dictionary<string,Node>.ValueCollection Nodes {
            get
            {
                return nodes.Values;
            }
        }

        public void RegisterNode(Node node)
        {
            nodes[node.Name] = node;
            node.Application = this;
        }

        public Node GetNode(string nodename)
        {
            return nodes[nodename];
        }
    }
}
