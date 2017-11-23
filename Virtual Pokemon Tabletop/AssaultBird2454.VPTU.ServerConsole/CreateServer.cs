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
                if (dr == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Parent.Servers.Add(new Server()
                    {
                        Server_ID = Server_ID.Text,
                        Server_Name = Server_Name.Text,
                        Server_Port = (int)Server_Port.Value,
                        SaveFile = SaveFile_Location.Text
                    });
                    this.Close();
                }
                else if (dr == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Abort;
                    this.Close();
                }
            }
        }
    }
}
