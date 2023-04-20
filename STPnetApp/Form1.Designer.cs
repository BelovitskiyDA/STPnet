
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootBridgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootPortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desPortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepByStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
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
            this.fileToolStripMenuItem,
            this.stepsToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.modelingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1178, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.newToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(69, 29);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.saveAsToolStripMenuItem.Text = "Сохранить как";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.newToolStripMenuItem.Text = "Новый";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // stepsToolStripMenuItem
            // 
            this.stepsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rootBridgeToolStripMenuItem,
            this.rootPortsToolStripMenuItem,
            this.desPortsToolStripMenuItem});
            this.stepsToolStripMenuItem.Name = "stepsToolStripMenuItem";
            this.stepsToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.stepsToolStripMenuItem.Text = "Этапы";
            // 
            // rootBridgeToolStripMenuItem
            // 
            this.rootBridgeToolStripMenuItem.Name = "rootBridgeToolStripMenuItem";
            this.rootBridgeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.rootBridgeToolStripMenuItem.Size = new System.Drawing.Size(269, 34);
            this.rootBridgeToolStripMenuItem.Text = "Root Bridge";
            this.rootBridgeToolStripMenuItem.Click += new System.EventHandler(this.rootBridgeToolStripMenuItem_Click);
            // 
            // rootPortsToolStripMenuItem
            // 
            this.rootPortsToolStripMenuItem.Name = "rootPortsToolStripMenuItem";
            this.rootPortsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.rootPortsToolStripMenuItem.Size = new System.Drawing.Size(269, 34);
            this.rootPortsToolStripMenuItem.Text = "Root Ports";
            this.rootPortsToolStripMenuItem.Click += new System.EventHandler(this.rootPortsToolStripMenuItem_Click);
            // 
            // desPortsToolStripMenuItem
            // 
            this.desPortsToolStripMenuItem.Name = "desPortsToolStripMenuItem";
            this.desPortsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.desPortsToolStripMenuItem.Size = new System.Drawing.Size(269, 34);
            this.desPortsToolStripMenuItem.Text = "Des Ports";
            this.desPortsToolStripMenuItem.Click += new System.EventHandler(this.desPortsToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(105, 29);
            this.resetToolStripMenuItem.Text = "Сбросить";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(103, 29);
            this.clearToolStripMenuItem.Text = "Очистить";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // modelingToolStripMenuItem
            // 
            this.modelingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepByStepToolStripMenuItem,
            this.nextStepToolStripMenuItem,
            this.completeToolStripMenuItem});
            this.modelingToolStripMenuItem.Name = "modelingToolStripMenuItem";
            this.modelingToolStripMenuItem.Size = new System.Drawing.Size(163, 29);
            this.modelingToolStripMenuItem.Text = "Моделирование";
            // 
            // stepByStepToolStripMenuItem
            // 
            this.stepByStepToolStripMenuItem.Name = "stepByStepToolStripMenuItem";
            this.stepByStepToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.stepByStepToolStripMenuItem.Text = "По шагам";
            this.stepByStepToolStripMenuItem.Click += new System.EventHandler(this.stepByStepToolStripMenuItem_Click);
            // 
            // nextStepToolStripMenuItem
            // 
            this.nextStepToolStripMenuItem.Name = "nextStepToolStripMenuItem";
            this.nextStepToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.nextStepToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.nextStepToolStripMenuItem.Text = "Следующий шаг";
            this.nextStepToolStripMenuItem.Click += new System.EventHandler(this.nextStepToolStripMenuItem_Click);
            // 
            // completeToolStripMenuItem
            // 
            this.completeToolStripMenuItem.Name = "completeToolStripMenuItem";
            this.completeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.completeToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.completeToolStripMenuItem.Text = "Завершить";
            this.completeToolStripMenuItem.Click += new System.EventHandler(this.completeToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 812);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1178, 32);
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
            this.contextMenuStripPort.Size = new System.Drawing.Size(193, 36);
            // 
            // deletePortToolStripMenuItem
            // 
            this.deletePortToolStripMenuItem.Name = "deletePortToolStripMenuItem";
            this.deletePortToolStripMenuItem.Size = new System.Drawing.Size(192, 32);
            this.deletePortToolStripMenuItem.Text = "Удалить порт";
            this.deletePortToolStripMenuItem.Click += new System.EventHandler(this.deletePortToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "astp";
            this.saveFileDialog1.Filter = "Astp files (*.astp)|*.astp";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "astp";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Astp files (*.astp)|*.astp";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1178, 844);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "STPnet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseWheel);
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
        private System.Windows.Forms.ToolStripMenuItem stepsToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem rootBridgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rootPortsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desPortsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem modelingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepByStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}

