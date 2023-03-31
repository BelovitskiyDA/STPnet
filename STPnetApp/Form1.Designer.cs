
namespace STPnetApp
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addBridgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStripLink = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripBridge = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBridgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBridgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripPort = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deletePortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripLink.SuspendLayout();
            this.contextMenuStripBridge.SuspendLayout();
            this.contextMenuStripPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBridgeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(959, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addBridgeToolStripMenuItem
            // 
            this.addBridgeToolStripMenuItem.Name = "addBridgeToolStripMenuItem";
            this.addBridgeToolStripMenuItem.Size = new System.Drawing.Size(118, 29);
            this.addBridgeToolStripMenuItem.Text = "Add Bridge";
            this.addBridgeToolStripMenuItem.Click += new System.EventHandler(this.addBridgeToolStripMenuItem_Click_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 547);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(959, 32);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(75, 25);
            this.toolStripStatusLabel1.Text = "Element";
            // 
            // contextMenuStripLink
            // 
            this.contextMenuStripLink.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripLink.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editLinkToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripLink.Name = "contextMenuStripLink";
            this.contextMenuStripLink.Size = new System.Drawing.Size(255, 68);
            // 
            // editLinkToolStripMenuItem
            // 
            this.editLinkToolStripMenuItem.Name = "editLinkToolStripMenuItem";
            this.editLinkToolStripMenuItem.Size = new System.Drawing.Size(254, 32);
            this.editLinkToolStripMenuItem.Text = "Редактировать связь";
            this.editLinkToolStripMenuItem.Click += new System.EventHandler(this.editLinkToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(254, 32);
            this.deleteToolStripMenuItem.Text = "Удалить связь";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // contextMenuStripBridge
            // 
            this.contextMenuStripBridge.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripBridge.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPortToolStripMenuItem,
            this.editBridgeToolStripMenuItem,
            this.deleteBridgeToolStripMenuItem});
            this.contextMenuStripBridge.Name = "contextMenuStripBridge";
            this.contextMenuStripBridge.Size = new System.Drawing.Size(250, 100);
            // 
            // addPortToolStripMenuItem
            // 
            this.addPortToolStripMenuItem.Name = "addPortToolStripMenuItem";
            this.addPortToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.addPortToolStripMenuItem.Text = "Добавить порт";
            this.addPortToolStripMenuItem.Click += new System.EventHandler(this.addPortToolStripMenuItem_Click);
            // 
            // editBridgeToolStripMenuItem
            // 
            this.editBridgeToolStripMenuItem.Name = "editBridgeToolStripMenuItem";
            this.editBridgeToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.editBridgeToolStripMenuItem.Text = "Редактировать мост";
            this.editBridgeToolStripMenuItem.Click += new System.EventHandler(this.editBridgeToolStripMenuItem_Click);
            // 
            // deleteBridgeToolStripMenuItem
            // 
            this.deleteBridgeToolStripMenuItem.Name = "deleteBridgeToolStripMenuItem";
            this.deleteBridgeToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.deleteBridgeToolStripMenuItem.Text = "Удалить мост";
            this.deleteBridgeToolStripMenuItem.Click += new System.EventHandler(this.deleteBridgeToolStripMenuItem_Click);
            // 
            // contextMenuStripPort
            // 
            this.contextMenuStripPort.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripPort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletePortToolStripMenuItem});
            this.contextMenuStripPort.Name = "contextMenuStripPort";
            this.contextMenuStripPort.Size = new System.Drawing.Size(241, 69);
            // 
            // deletePortToolStripMenuItem
            // 
            this.deletePortToolStripMenuItem.Name = "deletePortToolStripMenuItem";
            this.deletePortToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.deletePortToolStripMenuItem.Text = "Удалить порт";
            this.deletePortToolStripMenuItem.Click += new System.EventHandler(this.deletePortToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(959, 579);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "STPnet";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripLink.ResumeLayout(false);
            this.contextMenuStripBridge.ResumeLayout(false);
            this.contextMenuStripPort.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addBridgeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLink;
        private System.Windows.Forms.ToolStripMenuItem editLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBridge;
        private System.Windows.Forms.ToolStripMenuItem addPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBridgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteBridgeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPort;
        private System.Windows.Forms.ToolStripMenuItem deletePortToolStripMenuItem;
    }
}

