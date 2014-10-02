using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AjMessages.Configuration
{
    [XmlRoot("AjMessages")]
    public class AjMessagesConfig
    {
        private List<ApplicationConfig> applications;

        [XmlElement("Application")]
        public List<ApplicationConfig> Applications
        {
            get { return applications; }
            set { applications = value; }
        }

        private List<HostConfig> hosts;

        [XmlElement("Host")]
        public List<HostConfig> Hosts
        {
            get { return hosts; }
            set { hosts = value; }
        }
	
        //public void RegisterApplications(LocalHost host)
        //{
        //    foreach (ApplicationConfig appconfig in applications)
        //        host.RegisterApplication(appconfig.GetApplication());
        //}

        //public LocalHost CreateHost(string hostname)
        //{
        //    foreach (HostConfig hostconfig in hosts)
        //        if (hostconfig.Name == hostname)
        //            return CreateHost(hostconfig);

        //    throw new ArgumentException(String.Format("Unknown host {0}", hostname));
        //}

        private Application GetApplication(string appname)
        {
            foreach (ApplicationConfig appconfig in applications)
                if (appconfig.Name == appname)
                    return appconfig.GetApplication();

            throw new Exception(String.Format("Unknown Application {0}", appname));
        }
    }

    [XmlRoot("Application")]
    public class ApplicationConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<NodeConfig> nodes;

        [XmlElement("Node")]
        public List<NodeConfig> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        public Application GetApplication()
        {
            Application app = new Application();
            app.Name = Name;

            foreach (NodeConfig nodeconf in nodes)
                app.RegisterNode(nodeconf.GetNode());

            return app;
        }
    }

    [XmlRoot("Node")]
    public class NodeConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<HandlerConfig> handlers;

        [XmlElement("Handler")]
        public List<HandlerConfig> Handlers
        {
            get { return handlers; }
            set { handlers = value; }
        }

        private List<ActionConfig> actions;

        [XmlElement("Action")]
        public List<ActionConfig> Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        public Node GetNode()
        {
            Node node = new Node();
            node.Name = Name;

            foreach (HandlerConfig handler in handlers)
                node.RegisterHandler(handler.GetHandler());

            foreach (ActionConfig action in actions)
                node.RegisterAction(action.GetAction());

            return node;
        }
    }

    [XmlRoot("Handler")]
    public class HandlerConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string typeName;

        [XmlAttribute("Type")]
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private List<HandlerConfig> handlers;

        [XmlElement("Handler")]
        public List<HandlerConfig> Handlers
        {
            get { return handlers; }
            set { handlers = value; }
        }

        private List<PropertyConfig> properties;

        [XmlElement("Property")]
        public List<PropertyConfig> Properties
        {
            get { return properties; }
            set { properties = value; }
        }

        public HandlerDefinition GetHandler()
        {
            HandlerDefinition handler = new HandlerDefinition();
            handler.Name = name;
            handler.TypeName = typeName;

            foreach (HandlerConfig hc in handlers)
                handler.AddHandler(hc.GetHandler());

            foreach (PropertyConfig pc in properties)
                handler.AddProperty(pc.GetProperty());

            return handler;
        }
    }

    [XmlRoot("Action")]
    public class ActionConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string handler;

        [XmlAttribute("Handler")]
        public string Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        private List<HandlerConfig> handlers;

        [XmlElement("Handler")]
        public List<HandlerConfig> Handlers
        {
            get { return handlers; }
            set { handlers = value; }
        }

        public Action GetAction()
        {
            Action action = new Action();
            action.Name = name;
            action.HandlerName = handler;

            return action;
        }
    }

    [XmlRoot("Property")]
    public class PropertyConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string value;

        [XmlAttribute("Value")]
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public PropertyDefinition GetProperty()
        {
            PropertyDefinition pd = new PropertyDefinition();
            pd.Name = name;
            pd.Value = value;
            return pd;
        }
    }

    [XmlRoot("Host")]
    public class HostConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;

        [XmlAttribute("Address")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
	
        private int port;

        [XmlAttribute("Port")]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private bool activate;

        [XmlAttribute("Activate")]
        public bool Activate
        {
            get { return activate; }
            set { activate = value; }
        }

        private List<HostedApplicationConfig> applications;

        [XmlElement("Application")]
        public List<HostedApplicationConfig> Applications
        {
            get { return applications; }
            set { applications = value; }
        }
    }

    [XmlRoot("Application")]
    public class HostedApplicationConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<HostedNodeConfig> nodes;

        [XmlElement("Node")]
        public List<HostedNodeConfig> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
    }

    [XmlRoot("Node")]
    public class HostedNodeConfig
    {
        private string name;

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
			
    }
}

