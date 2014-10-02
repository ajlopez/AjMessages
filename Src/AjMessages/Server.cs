using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Xml;
using System.Xml.Serialization;

using AjMessages.Configuration;
using AjMessages.Channels;

namespace AjMessages
{
    public class Server
    {
        private Dictionary<string,Host> hosts = new Dictionary<string,Host>();
        private Dictionary<string,Application> applications = new Dictionary<string,Application>();
        private Random rnd = new Random();

        private AjMessagesConfig configuration;

        private static XmlSerializer sercnf;

        private bool started;

        private IChannelFactory channelfactory; 

        public IChannelFactory ChannelFactory
        {
            get { return channelfactory; }
            set { channelfactory = value; }
        }
	

        static Server()
        {
            sercnf = new XmlSerializer(typeof(AjMessagesConfig));
        }

        public void Configure(string filename)
        {
            FileStream file = new FileStream(filename,FileMode.Open,FileAccess.Read);
            AjMessagesConfig config = (AjMessagesConfig)sercnf.Deserialize(file);
            file.Close();
            Configure(config);
        }

        public void Configure(TextReader reader)
        {
            AjMessagesConfig conf = (AjMessagesConfig)sercnf.Deserialize(reader);
            Configure(conf);
        }

        public void Configure(AjMessagesConfig config)        
        {
            configuration = config;

            foreach (ApplicationConfig appconfig in configuration.Applications) {
                Application app = appconfig.GetApplication();
                app.Server = this;

                applications[app.Name] = app;
            }

            List<string> activehosts = new List<string>();

            foreach (HostConfig hostconfig in configuration.Hosts)
                if (hostconfig.Activate)
                    activehosts.Add(hostconfig.Name);

            if (activehosts.Count > 0)
            {
                CreateHosts(activehosts);
                Start();
            }
        }

        internal void Reconfigure(AjMessagesConfig conf)
        {
            foreach (ApplicationConfig appconfig in conf.Applications)
            {
                Application app = appconfig.GetApplication();
                app.Server = this;

                applications[app.Name] = app;
            }

            foreach (HostConfig hostconfig in conf.Hosts)
            {
                // If there is a previous host with same name, stop it

                if (hosts.ContainsKey(hostconfig.Name)) {
                    Host oldhost = hosts[hostconfig.Name];
                    oldhost.Stop();
                    hosts.Remove(hostconfig.Name);
                }

                List<HostConfig> toremove = new List<HostConfig>();

                // Remove host from previous configuration

                foreach (HostConfig hc in configuration.Hosts)
                {
                    if (hc.Name == hostconfig.Name)
                        toremove.Add(hc);
                }

                foreach (HostConfig hc in toremove)
                    configuration.Hosts.Remove(hc);

                // If the host has no applications, discard it

                if (hostconfig.Applications.Count == 0)
                    continue;

                // Add to current configuration host list

                configuration.Hosts.Add(hostconfig);

                // Activate the local or remote host

                Host host;

                if (hostconfig.Activate == false)
                {
                    host = CreateRemoteHost(hostconfig);
                }
                else
                {
                    host = CreateLocalHost(hostconfig);
                }

                hosts[host.Name] = host;

                host.Start();
            }
        }

        internal void Reconfigure(XmlReader reader)
        {
            AjMessagesConfig conf = (AjMessagesConfig) sercnf.Deserialize(reader);
            Reconfigure(conf);
        }

        internal void Reconfigure(TextReader reader)
        {
            AjMessagesConfig conf = (AjMessagesConfig) sercnf.Deserialize(reader);
            Reconfigure(conf);
        }

        public void Post(Message msg)
        {
            string nodefullname = MessageUtilities.GetNodeFullName(msg);

            List<Host> hostsToPost = new List<Host>();

            foreach (Host host in hosts.Values)
                if (host.HasNode(nodefullname))
                {
                    hostsToPost.Add(host);
                }

            if (hostsToPost.Count == 0)
                return;

            if (hostsToPost.Count == 1)
            {
                hostsToPost[0].Post(msg);
                return;
            }

            hostsToPost[rnd.Next(hostsToPost.Count)].Post(msg);
        }

        public void Start()
        {
            if (started)
                return;

            foreach (Host host in hosts.Values)
                host.Start();

            started = true;
        }

        public void Stop()
        {
            if (!started)
                return;

            foreach (Host host in hosts.Values)
                host.Stop();

            started = false;
        }

        IOutputChannel CreateOutputChannel(string address)
        {
            if (channelfactory == null)
                return null;
            if (address == null)
                return null;
            return channelfactory.CreateOutputChannel(address);
        }

        IInputChannel CreateInputChannel(string address, IMessageConsumer consumer)
        {
            if (channelfactory == null)
                return null;
            if (address == null)
                return null;

            return channelfactory.CreateInputChannel(address, consumer);
        }

        Host CreateLocalHost(HostConfig hostconfig)
        {
            LocalHost host = new LocalHost();

            host.Name = hostconfig.Name;
            host.Port = hostconfig.Port;
            host.Address = hostconfig.Address;
            host.InputChannel = CreateInputChannel(host.Address, host);

            foreach (HostedApplicationConfig hostedapp in hostconfig.Applications)
            {
                Application app = applications[hostedapp.Name];

                foreach (HostedNodeConfig hostednode in hostedapp.Nodes)
                {
                    Node node = app.GetNode(hostednode.Name);

                    host.RegisterNode(node);
                }
            }

            return host;
        }

        Host CreateRemoteHost(HostConfig hostconfig)
        {
            RemoteHost host = new RemoteHost();

            host.Name = hostconfig.Name;
            host.Port = hostconfig.Port;
            host.Address = hostconfig.Address;
            host.OutputChannel = CreateOutputChannel(host.Address);

            foreach (HostedApplicationConfig hostedapp in hostconfig.Applications)
            {
                Application app = new Application();
                app.Name = hostedapp.Name;

                foreach (HostedNodeConfig hostednode in hostedapp.Nodes)
                {
                    Node node = new Node();
                    node.Application = app;
                    node.Name = hostednode.Name;
                    host.RegisterNode(node);
                }
            }

            return host;
        }

        public void CreateHosts(List<string> hostnames)
        {
            foreach (HostConfig hostconfig in configuration.Hosts)
            {
                Host host;

                if (hostnames.Contains(hostconfig.Name))
                    host = CreateLocalHost(hostconfig);
                else
                    host = CreateRemoteHost(hostconfig);

                hosts[host.Name] = host;
            }
        }
    }
}
