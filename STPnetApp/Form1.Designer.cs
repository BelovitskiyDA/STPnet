
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonAddLink = new System.Windows.Forms.Button();
            this.listBoxLinks = new System.Windows.Forms.ListBox();
            this.buttonEditLink = new System.Windows.Forms.Button();
            this.buttonDeleteLink = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer1.Panel2.Controls.Add(this.buttonDeleteLink);
            this.splitContainer1.Panel2.Controls.Add(this.buttonEditLink);
            this.splitContainer1.Panel2.Controls.Add(this.buttonAddLink);
            this.splitContainer1.Panel2.Controls.Add(this.listBoxLinks);
            this.splitContainer1.Size = new System.Drawing.Size(959, 579);
            this.splitContainer1.SplitterDistance = 677;
            this.splitContainer1.TabIndex = 0;
            // 
            // buttonAddLink
            // 
            this.buttonAddLink.Location = new System.Drawing.Point(15, 405);
            this.buttonAddLink.Name = "buttonAddLink";
            this.buttonAddLink.Size = new System.Drawing.Size(159, 38);
            this.buttonAddLink.TabIndex = 1;
            this.buttonAddLink.Text = "Add link";
            this.buttonAddLink.UseVisualStyleBackColor = true;
            this.buttonAddLink.Click += new System.EventHandler(this.buttonAddLink_Click);
            // 
            // listBoxLinks
            // 
            this.listBoxLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLinks.FormattingEnabled = true;
            this.listBoxLinks.ItemHeight = 25;
            this.listBoxLinks.Location = new System.Drawing.Point(3, 35);
            this.listBoxLinks.Name = "listBoxLinks";
            this.listBoxLinks.Size = new System.Drawing.Size(275, 354);
            this.listBoxLinks.TabIndex = 0;
            // 
            // buttonEditLink
            // 
            this.buttonEditLink.Location = new System.Drawing.Point(15, 459);
            this.buttonEditLink.Name = "buttonEditLink";
            this.buttonEditLink.Size = new System.Drawing.Size(159, 38);
            this.buttonEditLink.TabIndex = 2;
            this.buttonEditLink.Text = "Edit link";
            this.buttonEditLink.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteLink
            // 
            this.buttonDeleteLink.Location = new System.Drawing.Point(15, 515);
            this.buttonDeleteLink.Name = "buttonDeleteLink";
            this.buttonDeleteLink.Size = new System.Drawing.Size(159, 38);
            this.buttonDeleteLink.TabIndex = 3;
            this.buttonDeleteLink.Text = "Delete link";
            this.buttonDeleteLink.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 579);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMain";
            this.Text = "STPnet";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxLinks;
        private System.Windows.Forms.Button buttonAddLink;
        private System.Windows.Forms.Button buttonDeleteLink;
        private System.Windows.Forms.Button buttonEditLink;
    }
}

