namespace AssaultBird2454.VPTU.ServerConsole
{
    partial class CreateServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_ServerID = new System.Windows.Forms.Label();
            this.Label_ServerName = new System.Windows.Forms.Label();
            this.Label_Port = new System.Windows.Forms.Label();
            this.Label_Save = new System.Windows.Forms.Label();
            this.Server_Port = new System.Windows.Forms.NumericUpDown();
            this.Server_ID = new System.Windows.Forms.TextBox();
            this.Server_Name = new System.Windows.Forms.TextBox();
            this.SaveFile_Location = new System.Windows.Forms.TextBox();
            this.Save_Select = new System.Windows.Forms.Button();
            this.Create = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Server_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_ServerID
            // 
            this.Label_ServerID.AutoSize = true;
            this.Label_ServerID.Location = new System.Drawing.Point(12, 9);
            this.Label_ServerID.Name = "Label_ServerID";
            this.Label_ServerID.Size = new System.Drawing.Size(67, 17);
            this.Label_ServerID.TabIndex = 0;
            this.Label_ServerID.Text = "Server ID";
            // 
            // Label_ServerName
            // 
            this.Label_ServerName.AutoSize = true;
            this.Label_ServerName.Location = new System.Drawing.Point(12, 37);
            this.Label_ServerName.Name = "Label_ServerName";
            this.Label_ServerName.Size = new System.Drawing.Size(91, 17);
            this.Label_ServerName.TabIndex = 1;
            this.Label_ServerName.Text = "Server Name";
            // 
            // Label_Port
            // 
            this.Label_Port.AutoSize = true;
            this.Label_Port.Location = new System.Drawing.Point(12, 64);
            this.Label_Port.Name = "Label_Port";
            this.Label_Port.Size = new System.Drawing.Size(80, 17);
            this.Label_Port.TabIndex = 2;
            this.Label_Port.Text = "Server Port";
            // 
            // Label_Save
            // 
            this.Label_Save.AutoSize = true;
            this.Label_Save.Location = new System.Drawing.Point(12, 93);
            this.Label_Save.Name = "Label_Save";
            this.Label_Save.Size = new System.Drawing.Size(66, 17);
            this.Label_Save.TabIndex = 3;
            this.Label_Save.Text = "Save File";
            // 
            // Server_Port
            // 
            this.Server_Port.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Server_Port.Location = new System.Drawing.Point(109, 62);
            this.Server_Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Server_Port.Name = "Server_Port";
            this.Server_Port.Size = new System.Drawing.Size(205, 22);
            this.Server_Port.TabIndex = 4;
            this.Server_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Server_Port.Value = new decimal(new int[] {
            25444,
            0,
            0,
            0});
            // 
            // Server_ID
            // 
            this.Server_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Server_ID.Location = new System.Drawing.Point(109, 6);
            this.Server_ID.Name = "Server_ID";
            this.Server_ID.Size = new System.Drawing.Size(205, 22);
            this.Server_ID.TabIndex = 5;
            // 
            // Server_Name
            // 
            this.Server_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Server_Name.Location = new System.Drawing.Point(109, 34);
            this.Server_Name.Name = "Server_Name";
            this.Server_Name.Size = new System.Drawing.Size(205, 22);
            this.Server_Name.TabIndex = 6;
            // 
            // SaveFile_Location
            // 
            this.SaveFile_Location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFile_Location.Location = new System.Drawing.Point(109, 90);
            this.SaveFile_Location.Name = "SaveFile_Location";
            this.SaveFile_Location.ReadOnly = true;
            this.SaveFile_Location.Size = new System.Drawing.Size(205, 22);
            this.SaveFile_Location.TabIndex = 7;
            // 
            // Save_Select
            // 
            this.Save_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Save_Select.Location = new System.Drawing.Point(320, 90);
            this.Save_Select.Name = "Save_Select";
            this.Save_Select.Size = new System.Drawing.Size(33, 23);
            this.Save_Select.TabIndex = 8;
            this.Save_Select.Text = "...";
            this.Save_Select.UseVisualStyleBackColor = true;
            this.Save_Select.Click += new System.EventHandler(this.Save_Select_Click);
            // 
            // Create
            // 
            this.Create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Create.Location = new System.Drawing.Point(4, 120);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(75, 25);
            this.Create.TabIndex = 9;
            this.Create.Text = "Create";
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(85, 120);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 25);
            this.Cancel.TabIndex = 10;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // CreateServer
            // 
            this.AcceptButton = this.Create;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(356, 152);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.Save_Select);
            this.Controls.Add(this.SaveFile_Location);
            this.Controls.Add(this.Server_Name);
            this.Controls.Add(this.Server_ID);
            this.Controls.Add(this.Server_Port);
            this.Controls.Add(this.Label_Save);
            this.Controls.Add(this.Label_Port);
            this.Controls.Add(this.Label_ServerName);
            this.Controls.Add(this.Label_ServerID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CreateServer";
            this.Text = "Create Server";
            ((System.ComponentModel.ISupportInitialize)(this.Server_Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ServerID;
        private System.Windows.Forms.Label Label_ServerName;
        private System.Windows.Forms.Label Label_Port;
        private System.Windows.Forms.Label Label_Save;
        private System.Windows.Forms.NumericUpDown Server_Port;
        private System.Windows.Forms.TextBox Server_ID;
        private System.Windows.Forms.TextBox Server_Name;
        private System.Windows.Forms.TextBox SaveFile_Location;
        private System.Windows.Forms.Button Save_Select;
        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.Button Cancel;
    }
}