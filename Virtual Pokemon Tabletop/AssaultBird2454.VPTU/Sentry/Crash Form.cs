using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssaultBird2454.VPTU.Sentry
{
    public partial class Crash_Form : Form
    {
        public Sentry_ExtraData ExtraData;

        public Crash_Form()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            ExtraData = new Sentry_ExtraData()
            {
                DiscordName = Discord_Name.Text
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }
    }

    public class Sentry_ExtraData
    {
        public string DiscordName { get; set; }
    }
}
