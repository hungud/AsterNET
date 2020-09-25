using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AsterNET.Manager;
using AsterNET.Manager.Event;
using System.Diagnostics;

namespace AsterNET.WinForm
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private ManagerConnection manager = null;
		private void btnConnect_Click(object sender, EventArgs e)
		{
			string address = this.tbAddress.Text;
			int port = int.Parse(this.tbPort.Text);
			string user = this.tbUser.Text;
			string password = this.tbPassword.Text;

			btnConnect.Enabled = false;
			manager = new ManagerConnection(address, port, user, password);
            manager.NewState += Manager_NewState; // => OK
            //  manager.PeerStatus += Manager_PeerStatus;
            //  manager.AgentsComplete += Manager_AgentsComplete;
            //  manager.Agents += Manager_Agents;
            //  manager.AgentRingNoAnswer += Manager_AgentRingNoAnswer;
            //  manager.DeviceStateChanged += Manager_DeviceStateChanged; // => OK
            //  manager.DialEnd += Manager_DialEnd;
            //  manager.ConnectionState += Manager_ConnectionState;
            //  manager.ExtensionStatus += Manager_ExtensionStatus;
            //  manager.Hangup += Manager_Hangup;

            // manager.UnhandledEvent += new EventHandler<ManagerEvent>(manager_Events);
            try
            {
				// Uncomment next 2 line comments to Disable timeout (debug mode)
				// manager.DefaultResponseTimeout = 0;
				// manager.DefaultEventTimeout = 0;
				manager.Login();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error connect\n" + ex.Message);
				manager.Logoff();
				this.Close();
			}
			btnDisconnect.Enabled = true;
		}

        private void Manager_Hangup(object sender, HangupEvent e)
        {
            this.UIThread(() => {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_DeviceStateChanged(object sender, DeviceStateChangeEvent e)
        {
            this.UIThread(() => {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_NewState(object sender, NewStateEvent e)
        {

            this.UIThread(() => {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });

            /*
             
            if ((state == "Ringing") | (e.ChannelState == "5"))
            {
                String connectedLineNum;
                String connectedLineName;

                Dictionary<String, String> attributes = e.Attributes;

                attributes.TryGetValue("connectedlinenum", out connectedLineNum);
                attributes.TryGetValue("connectedlinename", out connectedLineName);
                // "callerID" - called phone number
                // "connectedLineNum" - calling phone number

                // CallIn. Incoming call
               
            }
            else if ((state == "Ring") | (e.ChannelState == "4"))
            {
                // CallOut. Outcoming call
            }
            else if ((state == "Up") | (e.ChannelState == "6"))
            {
                String connectedLineNum;
                String connectedLineName;

                Dictionary<String, String> attributes = e.Attributes;

                attributes.TryGetValue("connectedlinenum", out connectedLineNum);
                attributes.TryGetValue("connectedlinename", out connectedLineName);
                // "callerID" - called phone number
                // "connectedLineNum" - calling phone number

                // human lifted up the phone right now
            }
             
             
             */

        }

        void manager_Events(object sender, ManagerEvent e)
		{            
			// Debug.WriteLine("Event : " + e.GetType().Name);
        }

        private void Source_AgentCalled(object sender, AgentCalledEvent e)
        {
            throw new NotImplementedException();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
		{
			btnConnect.Enabled = true;
			if (this.manager != null)
			{
				manager.Logoff();
				this.manager = null;
			}
			btnDisconnect.Enabled = false;
		}
	}
}
