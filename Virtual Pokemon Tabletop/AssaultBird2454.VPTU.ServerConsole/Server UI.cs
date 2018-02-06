using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace AssaultBird2454.VPTU.ServerConsole
{
    public partial class Server_UI : Form
    {
        #region Base
        /// <summary>
        /// Assembly Directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        public Server_UI()
        {
            InitializeComponent();
            Settings_Load();
        }
        private void Server_UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings_Save();
        }

        public List<Server> Servers { get; private set; }
        public void Settings_Save()
        {
            #region Servers
            try
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(AssemblyDirectory + @"\Servers.json", FileMode.OpenOrCreate)))
                {
                    string Servers_String = Newtonsoft.Json.JsonConvert.SerializeObject(Servers);
                    sw.WriteLine(Servers_String);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a error saving servers to file!\nNo Servers have been saved, This means that you will need to create them again when you open up the server again...", "Error Saving Servers to File");
                MessageBox.Show(ex.ToString(), "Stack Trace");
            }
            #endregion
        }
        public void Settings_Load()
        {
            #region Servers
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream(AssemblyDirectory + @"\Servers.json", FileMode.OpenOrCreate)))
                {
                    string Servers_String = sr.ReadToEnd();
                    Servers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Server>>(Servers_String);
                }
            }
            catch
            {
                Servers = new List<Server>();
            }

            if (Servers == null) { Servers = new List<Server>(); }

            foreach (Server sv in Servers)
            {
                ListViewItem lvi = new ListViewItem(sv.Server_ID);

                ListViewSubItem svn = new ListViewSubItem();
                svn.Text = sv.Server_Name;
                lvi.SubItems.Add(svn);

                ListViewSubItem ss = new ListViewSubItem();
                ss.Text = "Offline";
                lvi.SubItems.Add(ss);

                ListViewSubItem svc = new ListViewSubItem();
                svc.Text = "0";
                lvi.SubItems.Add(svc);

                ListViewSubItem svs = new ListViewSubItem();
                svs.Text = sv.SaveFile;
                lvi.SubItems.Add(svs);

                lvi.BackColor = Color.IndianRed;

                sv.Server_Instance = new VPTU.Server.Instances.ServerInstance(new VPTU.Server.Class.Logging.Console_Logger(true, sv.Server_Name), sv.SaveFile, sv.Server_Port);
                sv.LVI = lvi;

                SetupEvents(sv);

                lvi.Tag = sv.Server_Instance;
                List_Servers.Items.Add(lvi);
            }
            #endregion
        }

        private void SetupEvents(Server sv)
        {
            sv.Server_Instance.Server.TCP_AcceptClients_Changed += new Networking.Server.TCP.TCP_AcceptClients_Handeler((Accepting) =>
            {
                ListViewItem LVI = sv.LVI;
            });
            sv.Server_Instance.Server.TCP_ClientState_Changed += new Networking.Server.TCP.TCP_ClientState_Handeler((Networking.Server.TCP.TCP_ClientNode Node, Networking.Data.Client_ConnectionStatus State) =>
            {
                ListViewItem LVI = sv.LVI;
                List_Servers.Invoke(new Action(() => LVI.SubItems[3].Text = sv.Server_Instance.Server.CurrentConnections + " / " + sv.Server_Instance.Server.MaxConnections));
            });
            //sv.Server_Instance.Server.TCP_Data_Error_Event += Server_TCP_Data_Error_Event;
            //sv.Server_Instance.Server.TCP_Data_Event += Server_TCP_Data_Event;
            sv.Server_Instance.Server.TCP_ServerState_Changed += new Networking.Server.TCP.TCP_ServerState_Handeler((Networking.Data.Server_Status State) =>
            {
                ListViewItem LVI = sv.LVI;
                Color Bcolor;
                Color Fcolor;

                if (State == Networking.Data.Server_Status.Online)
                {
                    Bcolor = Color.LightGreen;
                    Fcolor = Color.Black;
                    LVI.SubItems[3].Text = sv.Server_Instance.Server.CurrentConnections + " / " + sv.Server_Instance.Server.MaxConnections;
                }
                else if (State == Networking.Data.Server_Status.Starting)
                {
                    Bcolor = Color.Gold;
                    Fcolor = Color.Black;
                    LVI.SubItems[3].Text = sv.Server_Instance.Server.CurrentConnections + " / " + sv.Server_Instance.Server.MaxConnections;
                }
                else if (State == Networking.Data.Server_Status.Offline)
                {
                    Bcolor = Color.IndianRed;
                    Fcolor = Color.Black;
                    LVI.SubItems[3].Text = "0 / 0";
                }
                else
                {
                    Bcolor = Color.Black;
                    Fcolor = Color.White;
                }

                LVI.BackColor = Bcolor;
                LVI.ForeColor = Fcolor;
                LVI.SubItems[2].Text = State.ToString();
            });
        }
        #endregion

        private void Group_Controls_Create_Click(object sender, EventArgs e)
        {
            try
            {
                CreateServer cs = new CreateServer(this);
                DialogResult dr = cs.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    Servers.Add(cs.CreatedServer);

                    ListViewItem lvi = new ListViewItem(cs.CreatedServer.Server_ID);

                    ListViewSubItem svn = new ListViewSubItem();
                    svn.Text = cs.CreatedServer.Server_Name;
                    lvi.SubItems.Add(svn);

                    ListViewSubItem ss = new ListViewSubItem();
                    ss.Text = "Offline";
                    lvi.SubItems.Add(ss);

                    ListViewSubItem svc = new ListViewSubItem();
                    svc.Text = "0";
                    lvi.SubItems.Add(svc);

                    ListViewSubItem svs = new ListViewSubItem();
                    svs.Text = cs.CreatedServer.SaveFile;
                    lvi.SubItems.Add(svs);

                    lvi.BackColor = Color.IndianRed;

                    cs.CreatedServer.Server_Instance = new VPTU.Server.Instances.ServerInstance(new VPTU.Server.Class.Logging.Console_Logger(true, cs.Name), cs.CreatedServer.SaveFile, cs.CreatedServer.Server_Port);
                    cs.CreatedServer.LVI = lvi;

                    SetupEvents(cs.CreatedServer);

                    lvi.Tag = cs.CreatedServer.Server_Instance;
                    List_Servers.Items.Add(lvi);
                }
                else if (dr == DialogResult.Cancel)
                {
                    MessageBox.Show("Opperation Aborted!\n\nReasion: Canceled By User!", "Server Creation Canceled");
                }
                else if (dr == DialogResult.Abort)
                {
                    MessageBox.Show("Opperation Aborted!\n\nReasion: Canceled By User After Error!", "Server Creation Aborted");
                }
            }
            catch { }
        }

        private void Group_Controls_Delete_Click(object sender, EventArgs e)
        {

        }

        private void Group_Controls_Edit_Click(object sender, EventArgs e)
        {

        }

        private void Group_Controls_Start_Click(object sender, EventArgs e)
        {
            try
            {
                ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).StartServerInstance();
            }
            catch { }
        }

        private void Group_Controls_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).StopServerInstance();
            }
            catch { }
        }

        private void Group_Controls_FullReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server sv = Servers.Find(x => x.Server_Instance == ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)));

                sv.Server_Instance.StopServerInstance();

                sv.Server_Instance = new VPTU.Server.Instances.ServerInstance(new VPTU.Server.Class.Logging.Console_Logger(true), sv.SaveFile, sv.Server_Port);
                List_Servers.SelectedItems[0].Tag = sv.Server_Instance;

                sv.Server_Instance.StartServerInstance();
            }
            catch { }
        }

        private void Group_Controls_SaveReset_Click(object sender, EventArgs e)
        {
            try
            {
                ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).SaveManager.Load_SaveData();
            }
            catch { }
        }

        private void Group_Controls_NetworkReset_Click(object sender, EventArgs e)
        {

        }

        private void Group_Controls_Lock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Group_Controls_Lock.Checked)
                {
                    ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).Server.AcceptClients = false;
                }
                else
                {
                    ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).Server.AcceptClients = true;
                }
            }
            catch { }
        }

        private void Group_Controls_Save_Click(object sender, EventArgs e)
        {
            try
            {
                ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).SaveManager.Save_SaveData();
            }
            catch { }
        }

        private void List_Servers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).Server.AcceptClients)
                {
                    Group_Controls_Lock.Checked = false;
                }
                else
                {
                    Group_Controls_Lock.Checked = true;
                }
            }
            catch { Group_Controls_Lock.Checked = false; }
        }

        private void List_Servers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
