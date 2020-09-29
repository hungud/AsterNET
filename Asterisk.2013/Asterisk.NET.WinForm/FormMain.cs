using AsterNET.Manager;
using AsterNET.Manager.Event;
using System;
using System.Diagnostics;
using System.Windows.Forms;

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
            //  manager.NewChannel += Manager_NewChannel;
            //  manager.PeerEntry += Manager_PeerEntry;
            //  manager.NewState += Manager_NewState; // => OK
            //  manager.PeerStatus += Manager_PeerStatus;
            //  manager.AgentsComplete += Manager_AgentsComplete;
            //  manager.Agents += Manager_Agents;
            //  manager.AgentRingNoAnswer += Manager_AgentRingNoAnswer;
            // manager.DeviceStateChanged += Manager_DeviceStateChanged; // => OK
            // manager.VarSet += Manager_VarSet;
            // manager.DTMF += Manager_DTMF;
            // manager.NewExten += Manager_NewExten;        

            manager.NewConnectedLine += Manager_NewConnectedLine;

            //  manager.DialEnd += Manager_DialEnd;
            //  manager.ConnectionState += Manager_ConnectionState;
            //  manager.FireAllEvents = true;
            // manager.ExtensionStatus += Manager_ExtensionStatus;
            //  manager.Hangup += Manager_Hangup;

            // manager.UnhandledEvent += new EventHandler<ManagerEvent>(manager_Events);
            try
            {
                // Uncomment next 2 line comments to Disable timeout (debug mode)
                // manager.DefaultResponseTimeout = 0;
                // manager.DefaultEventTimeout = 0;
                manager.Login();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connect\n" + ex.Message);
                manager.Logoff();
                this.Close();
            }
            btnDisconnect.Enabled = true;
        }

        private void Manager_NewConnectedLine(object sender, NewConnectedLineEvent e)
        {
            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_NewExten(object sender, NewExtenEvent e)
        {
            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });

            // KHI GOI TRONG QUEUE
            if (e.Application == "SET" &&
                e.AppData != null &&
                e.AppData.IndexOf("QAGENT=") >= 0)
            {
                if (e.Attributes["channelstate"] != null && e.Attributes["channelstate"] == "4")
                {
                    if (e.Attributes["channelstatedesc"] != null && e.Attributes["channelstatedesc"] == "Ring")
                    {
                        this.UIThread(() =>
                        {

                            this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                            this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                            this.rtLog.AppendText($"exten: {e.Attributes["exten"]}");
                            this.rtLog.AppendText($"calleridnum: {e.Attributes["calleridnum"]}");
                            this.rtLog.AppendText(System.Environment.NewLine);

                        });
                    }
                }
            }


            else if (e.Application == "")
            {

            }
        }

        private void Manager_NewChannel(object sender, NewChannelEvent e)
        {
            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_ExtensionStatus(object sender, ExtensionStatusEvent e)
        {
            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_VarSet(object sender, VarSetEvent e)
        {
            // EXIT
            Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(e));

            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_DTMF(object sender, DTMFEvent e)
        {
            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_PeerEntry(object sender, PeerEntryEvent e)
        {
            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_Hangup(object sender, HangupEvent e)
        {
            this.UIThread(() =>
            {

                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);

            });
        }

        private void Manager_DeviceStateChanged(object sender, DeviceStateChangeEvent e)
        {
            this.UIThread(() =>
            {




                this.rtLog.AppendText($"================================================================================================== " + System.Environment.NewLine);
                this.rtLog.AppendText($"{DateTime.Now} " + System.Environment.NewLine);
                this.rtLog.AppendText($"e: {Newtonsoft.Json.JsonConvert.SerializeObject(e)}");
                this.rtLog.AppendText(System.Environment.NewLine);
                var ManagerConnection = (ManagerConnection)sender;
                this.rtLog.AppendText($"ManagerConnection: {Newtonsoft.Json.JsonConvert.SerializeObject(ManagerConnection)}");


            });
        }

        private void Manager_NewState(object sender, NewStateEvent e)
        {

            this.UIThread(() =>
            {

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
