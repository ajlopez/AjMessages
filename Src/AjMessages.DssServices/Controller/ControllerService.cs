//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     DSS Runtime Version: 2.0.730.3
//     CLR Runtime Version: 2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using W3C.Soap;
using ajmessagesdssservices = Robotics.AjMessagesDssServices;
using AjMessages;
using System.IO;
using Microsoft.Ccr.Adapters.WinForms;
using System.Windows.Forms;


namespace Robotics.AjMessagesDssServices.Controller
{
    
    
    /// <summary>
    /// Implementation class for AjMessages Controller Service
    /// </summary>
    [DisplayName("AjMessagesController")]
    [Description("AjMessages Controller Service")]
    [Contract(Contract.Identifier)]
    public class ControllerService : DsspServiceBase
    {
        
        /// <summary>
        /// _state
        /// </summary>
        [ServiceState()]
        private ControllerServiceState _state = new ControllerServiceState();
        
        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/ajmessages/controller", AllowMultipleInstances=false)]
        private ControllerServiceOperations mainPort = new ControllerServiceOperations();

        [Partner("Node", Contract = Node.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExistingOrCreate)]
        private Node.NodeServiceOperations nodePort = new Node.NodeServiceOperations();

        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public ControllerService(DsspServiceCreationPort creationPort) : 
                base(creationPort)
        {
        }
        
        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
			base.Start();
			// Add service specific initialization here.

            WinFormsServicePort.Post(new RunForm(StartForm));
        }

        private System.Windows.Forms.Form StartForm()
        {
            Form form = new ControllerForm(nodePort);

            Invoke(delegate()
                {
                    form.Text = form.Text + ": " + ServiceInfo.Service;
                }
            );

            return form;
        }

        private void Invoke(System.Windows.Forms.MethodInvoker mi)
        {
            WinFormsServicePort.Post(new FormInvoke(mi));
        }


    }
}