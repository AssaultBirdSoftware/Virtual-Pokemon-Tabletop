namespace AssaultBird2454.VPTU.ServerConsole
{
    partial class Server_UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server_UI));
            this.List_Servers = new System.Windows.Forms.ListView();
            this.Server_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Server_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Server_State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Server_Connections = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Save_File = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Group_Controls_Create = new System.Windows.Forms.Button();
            this.Group_Controls = new System.Windows.Forms.GroupBox();
            this.Group_Controls_Lock = new System.Windows.Forms.CheckBox();
            this.Group_Controls_SaveReset = new System.Windows.Forms.Button();
            this.Group_Controls_NetworkReset = new System.Windows.Forms.Button();
            this.Group_Controls_FullReset = new System.Windows.Forms.Button();
            this.Group_Controls_Save = new System.Windows.Forms.Button();
            this.Group_Controls_Stop = new System.Windows.Forms.Button();
            this.Group_Controls_Start = new System.Windows.Forms.Button();
            this.Group_Controls_Delete = new System.Windows.Forms.Button();
            this.Group_Controls_Edit = new System.Windows.Forms.Button();
            this.Group_Controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // List_Servers
            // 
            this.List_Servers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.List_Servers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Server_ID,
            this.Server_Name,
            this.Server_State,
            this.Server_Connections,
            this.Save_File});
            this.List_Servers.Location = new System.Drawing.Point(12, 12);
            this.List_Servers.Name = "List_Servers";
            this.List_Servers.Size = new System.Drawing.Size(635, 460);
            this.List_Servers.TabIndex = 0;
            this.List_Servers.UseCompatibleStateImageBehavior = false;
            this.List_Servers.View = System.Windows.Forms.View.Details;
            this.List_Servers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.List_Servers_ItemSelectionChanged);
            this.List_Servers.SelectedIndexChanged += new System.EventHandler(this.List_Servers_SelectedIndexChanged);
            // 
            // Server_ID
            // 
            this.Server_ID.Text = "Server ID";
            this.Server_ID.Width = 150;
            // 
            // Server_Name
            // 
            this.Server_Name.Text = "Server Name";
            this.Server_Name.Width = 150;
            // 
            // Server_State
            // 
            this.Server_State.Text = "Server State";
            this.Server_State.Width = 100;
            // 
            // Server_Connections
            // 
            this.Server_Connections.Text = "Connections";
            this.Server_Connections.Width = 90;
            // 
            // Save_File
            // 
            this.Save_File.Text = "Save File";
            this.Save_File.Width = 200;
            // 
            // Group_Controls_Create
            // 
            this.Group_Controls_Create.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Group_Controls_Create.Location = new System.Drawing.Point(6, 21);
            this.Group_Controls_Create.Name = "Group_Controls_Create";
            this.Group_Controls_Create.Size = new System.Drawing.Size(75, 25);
            this.Group_Controls_Create.TabIndex = 1;
            this.Group_Controls_Create.Text = "Create";
            this.Group_Controls_Create.UseVisualStyleBackColor = false;
            this.Group_Controls_Create.Click += new System.EventHandler(this.Group_Controls_Create_Click);
            // 
            // Group_Controls
            // 
            this.Group_Controls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Group_Controls.Controls.Add(this.Group_Controls_Edit);
            this.Group_Controls.Controls.Add(this.Group_Controls_Lock);
            this.Group_Controls.Controls.Add(this.Group_Controls_SaveReset);
            this.Group_Controls.Controls.Add(this.Group_Controls_NetworkReset);
            this.Group_Controls.Controls.Add(this.Group_Controls_FullReset);
            this.Group_Controls.Controls.Add(this.Group_Controls_Save);
            this.Group_Controls.Controls.Add(this.Group_Controls_Stop);
            this.Group_Controls.Controls.Add(this.Group_Controls_Start);
            this.Group_Controls.Controls.Add(this.Group_Controls_Delete);
            this.Group_Controls.Controls.Add(this.Group_Controls_Create);
            this.Group_Controls.Location = new System.Drawing.Point(653, 12);
            this.Group_Controls.Name = "Group_Controls";
            this.Group_Controls.Size = new System.Drawing.Size(174, 460);
            this.Group_Controls.TabIndex = 2;
            this.Group_Controls.TabStop = false;
            this.Group_Controls.Text = "Controls";
            // 
            // Group_Controls_Lock
            // 
            this.Group_Controls_Lock.AutoSize = true;
            this.Group_Controls_Lock.Location = new System.Drawing.Point(6, 299);
            this.Group_Controls_Lock.Name = "Group_Controls_Lock";
            this.Group_Controls_Lock.Size = new System.Drawing.Size(122, 21);
            this.Group_Controls_Lock.TabIndex = 9;
            this.Group_Controls_Lock.Text = "Server Locked";
            this.Group_Controls_Lock.UseVisualStyleBackColor = true;
            this.Group_Controls_Lock.CheckedChanged += new System.EventHandler(this.Group_Controls_Lock_CheckedChanged);
            // 
            // Group_Controls_SaveReset
            // 
            this.Group_Controls_SaveReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Group_Controls_SaveReset.Location = new System.Drawing.Point(6, 237);
            this.Group_Controls_SaveReset.Name = "Group_Controls_SaveReset";
            this.Group_Controls_SaveReset.Size = new System.Drawing.Size(156, 25);
            this.Group_Controls_SaveReset.TabIndex = 8;
            this.Group_Controls_SaveReset.Text = "Server Save Reset";
            this.Group_Controls_SaveReset.UseVisualStyleBackColor = false;
            this.Group_Controls_SaveReset.Click += new System.EventHandler(this.Group_Controls_SaveReset_Click);
            // 
            // Group_Controls_NetworkReset
            // 
            this.Group_Controls_NetworkReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Group_Controls_NetworkReset.Location = new System.Drawing.Point(6, 206);
            this.Group_Controls_NetworkReset.Name = "Group_Controls_NetworkReset";
            this.Group_Controls_NetworkReset.Size = new System.Drawing.Size(156, 25);
            this.Group_Controls_NetworkReset.TabIndex = 7;
            this.Group_Controls_NetworkReset.Text = "Server Network Reset";
            this.Group_Controls_NetworkReset.UseVisualStyleBackColor = false;
            this.Group_Controls_NetworkReset.Click += new System.EventHandler(this.Group_Controls_NetworkReset_Click);
            // 
            // Group_Controls_FullReset
            // 
            this.Group_Controls_FullReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Group_Controls_FullReset.Location = new System.Drawing.Point(6, 175);
            this.Group_Controls_FullReset.Name = "Group_Controls_FullReset";
            this.Group_Controls_FullReset.Size = new System.Drawing.Size(156, 25);
            this.Group_Controls_FullReset.TabIndex = 6;
            this.Group_Controls_FullReset.Text = "Full Server Reset";
            this.Group_Controls_FullReset.UseVisualStyleBackColor = false;
            this.Group_Controls_FullReset.Click += new System.EventHandler(this.Group_Controls_FullReset_Click);
            // 
            // Group_Controls_Save
            // 
            this.Group_Controls_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Group_Controls_Save.Location = new System.Drawing.Point(6, 268);
            this.Group_Controls_Save.Name = "Group_Controls_Save";
            this.Group_Controls_Save.Size = new System.Drawing.Size(156, 25);
            this.Group_Controls_Save.TabIndex = 5;
            this.Group_Controls_Save.Text = "Save Game";
            this.Group_Controls_Save.UseVisualStyleBackColor = false;
            this.Group_Controls_Save.Click += new System.EventHandler(this.Group_Controls_Save_Click);
            // 
            // Group_Controls_Stop
            // 
            this.Group_Controls_Stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Group_Controls_Stop.Location = new System.Drawing.Point(6, 129);
            this.Group_Controls_Stop.Name = "Group_Controls_Stop";
            this.Group_Controls_Stop.Size = new System.Drawing.Size(156, 25);
            this.Group_Controls_Stop.TabIndex = 4;
            this.Group_Controls_Stop.Text = "Stop Server";
            this.Group_Controls_Stop.UseVisualStyleBackColor = false;
            this.Group_Controls_Stop.Click += new System.EventHandler(this.Group_Controls_Stop_Click);
            // 
            // Group_Controls_Start
            // 
            this.Group_Controls_Start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Group_Controls_Start.Location = new System.Drawing.Point(6, 98);
            this.Group_Controls_Start.Name = "Group_Controls_Start";
            this.Group_Controls_Start.Size = new System.Drawing.Size(156, 25);
            this.Group_Controls_Start.TabIndex = 3;
            this.Group_Controls_Start.Text = "Start Server";
            this.Group_Controls_Start.UseVisualStyleBackColor = false;
            this.Group_Controls_Start.Click += new System.EventHandler(this.Group_Controls_Start_Click);
            // 
            // Group_Controls_Delete
            // 
            this.Group_Controls_Delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Group_Controls_Delete.Enabled = false;
            this.Group_Controls_Delete.Location = new System.Drawing.Point(87, 21);
            this.Group_Controls_Delete.Name = "Group_Controls_Delete";
            this.Group_Controls_Delete.Size = new System.Drawing.Size(75, 25);
            this.Group_Controls_Delete.TabIndex = 2;
            this.Group_Controls_Delete.Text = "Delete";
            this.Group_Controls_Delete.UseVisualStyleBackColor = false;
            this.Group_Controls_Delete.Click += new System.EventHandler(this.Group_Controls_Delete_Click);
            // 
            // Group_Controls_Edit
            // 
            this.Group_Controls_Edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Group_Controls_Edit.Enabled = false;
            this.Group_Controls_Edit.Location = new System.Drawing.Point(6, 52);
            this.Group_Controls_Edit.Name = "Group_Controls_Edit";
            this.Group_Controls_Edit.Size = new System.Drawing.Size(156, 25);
            this.Group_Controls_Edit.TabIndex = 10;
            this.Group_Controls_Edit.Text = "Edit";
            this.Group_Controls_Edit.UseVisualStyleBackColor = false;
            this.Group_Controls_Edit.Click += new System.EventHandler(this.Group_Controls_Edit_Click);
            // 
            // Server_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 484);
            this.Controls.Add(this.Group_Controls);
            this.Controls.Add(this.List_Servers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Server_UI";
            this.Text = "Server Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_UI_FormClosing);
            this.Group_Controls.ResumeLayout(false);
            this.Group_Controls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView List_Servers;
        private System.Windows.Forms.ColumnHeader Server_ID;
        private System.Windows.Forms.ColumnHeader Server_Name;
        private System.Windows.Forms.ColumnHeader Server_Connections;
        private System.Windows.Forms.ColumnHeader Save_File;
        private System.Windows.Forms.Button Group_Controls_Create;
        private System.Windows.Forms.GroupBox Group_Controls;
        private System.Windows.Forms.Button Group_Controls_Save;
        private System.Windows.Forms.Button Group_Controls_Stop;
        private System.Windows.Forms.Button Group_Controls_Start;
        private System.Windows.Forms.Button Group_Controls_Delete;
        private System.Windows.Forms.ColumnHeader Server_State;
        private System.Windows.Forms.Button Group_Controls_FullReset;
        private System.Windows.Forms.Button Group_Controls_SaveReset;
        private System.Windows.Forms.Button Group_Controls_NetworkReset;
        private System.Windows.Forms.CheckBox Group_Controls_Lock;
        private System.Windows.Forms.Button Group_Controls_Edit;
    }
}