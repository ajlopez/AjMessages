using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    public class HandlerDefinition
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string typename;

        public string TypeName
        {
            get { return typename; }
            set { typename = value; }
        }

        private List<HandlerDefinition> handlers = new List<HandlerDefinition>();

        public void AddHandler(HandlerDefinition handler)
        {
            handlers.Add(handler);
        }

        private List<PropertyDefinition> properties = new List<PropertyDefinition>();

        public void AddProperty(PropertyDefinition property)
        {
            properties.Add(property);
        }

        public IHandler GetHandler(Node node)
        {
            if (typename == null)
            {
                PipelineHandler handler = new PipelineHandler();

                foreach (HandlerDefinition hd in handlers)
                {
                    IHandler hdl = node.GetHandler(hd.Name);
                    Type hdlt = hdl.GetType();

                    foreach (PropertyDefinition prop in hd.properties)
                        hdlt.InvokeMember(prop.Name, System.Reflection.BindingFlags.SetProperty, null, hdl, new object[] { prop.Value });
                    
                    handler.AddHandler(hdl);
                }

                return handler;
            }

            Type type = System.Type.GetType(typename);

            IHandler h = (IHandler)Activator.CreateInstance(type);

            Type htp = h.GetType();

            foreach (PropertyDefinition prop in properties)
            {
                htp.InvokeMember(prop.Name, System.Reflection.BindingFlags.SetProperty, null, h, new object[] { prop.Value });
            }

            return h;
        }
    }

    public class PropertyDefinition
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
	
    }
}
