using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssaultBird2454.VPTU.ServerConsole
{
    public partial class CreateServer : Form
    {
        Server_UI Parent;
        public Server CreatedServer = new Server();

        public CreateServer(Server_UI _Parent)
        {
            InitializeComponent();
            Parent = _Parent;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (Server_ID.Text == "") { Server_ID.Text = "Default"; }
            if (Server_Name.Text == "") { Server_Name.Text = "Default"; }

            if (Parent.Servers.FindAll(x => x.Server_Name == Server_Name.Text).Count >= 1
                && Parent.Servers.FindAll(x => x.Server_ID == Server_ID.Text).Count >= 1
                && Parent.Servers.FindAll(x => x.Server_Port == Server_Port.Value).Count >= 1)
            {
                DialogResult dr = MessageBox.Show("A server already exists with that ID, Name or Port!\n\nServer Not Added! Keep Creating?", "Server Exists with these settings", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Abort;
                    this.Close();
                }
            }
            else
            {
                DialogResult = DialogResult.OK;
                CreatedServer = new Server()
                {
                    Server_ID = Server_ID.Text,
                    Server_Name = Server_Name.Text,
                    Server_Port = (int)Server_Port.Value,
                    SaveFile = SaveFile_Location.Text
                };
                Close();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void Save_Select_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.Multiselect = false;
            openFile.Title = "Open Virtual PTU Save File";
            openFile.DefaultExt = ".ptu";

            DialogResult dr = openFile.ShowDialog();

            if(dr == DialogResult.OK)
            {
                SaveFile_Location.Text = openFile.FileName;
            }
            else { }
        }
    }
}
