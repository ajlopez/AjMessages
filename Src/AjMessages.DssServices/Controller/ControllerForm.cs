using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Robotics.AjMessagesDssServices.Controller
{
    public partial class ControllerForm : Form
    {
        private Node.NodeServiceOperations servicePort;

        public ControllerForm(Node.NodeServiceOperations port)
        {
            InitializeComponent();
            servicePort = port;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            txtConfigurationFile.Text = ofdSelectFile.FileName;
        }

        private void btnConfigureHost_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText(txtConfigurationFile.Text);
                string configuration = reader.ReadToEnd();
                reader.Close();

                Node.ProcessMessageRequest msg = new Node.ProcessMessageRequest();
                msg.Action = "Load";
                msg.Body = configuration;

                Node.ProcessMessage proc = new Node.ProcessMessage(msg);

                servicePort.Post(proc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText(txtConfigurationFile.Text);
                string configuration = reader.ReadToEnd();
                reader.Close();

                Node.ProcessMessageRequest msg = new Node.ProcessMessageRequest();
                msg.Action = "App1/Node1/Process";
                msg.Body = 10.ToString();

                Node.ProcessMessage proc = new Node.ProcessMessage(msg);

                servicePort.Post(proc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}