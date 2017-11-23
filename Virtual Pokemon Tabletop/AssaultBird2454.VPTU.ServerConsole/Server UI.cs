using AssaultBird2454.VPTU.BattleManager.Battle_Instance;
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

        public List<Server> Servers { get; private set; }

        #region Base
        public Server_UI()
        {
            InitializeComponent();
            Settings_Load();
        }
        private void Server_UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings_Save();
        }
        #endregion

        public void Settings_Save()
        {

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

                lvi.BackColor = Color.Red;

                lvi.Tag = new VPTU.Server.Instances.ServerInstance(sv.SaveFile, new VPTU.Server.Class.Logging.Console_Logger(true));
                List_Servers.Items.Add(lvi);
            }
            #endregion
        }

        private void Group_Controls_Create_Click(object sender, EventArgs e)
        {

        }

        private void Group_Controls_Start_Click(object sender, EventArgs e)
        {
            ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).StartServerInstance();
        }

        private void Group_Controls_Stop_Click(object sender, EventArgs e)
        {
            ((VPTU.Server.Instances.ServerInstance)(List_Servers.SelectedItems[0].Tag)).StopServerInstance();
        }
    }
}
