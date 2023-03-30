
namespace STPnetApp
{
    partial class AddLinkForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxId1 = new System.Windows.Forms.ComboBox();
            this.comboBoxN1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWeight = new System.Windows.Forms.TextBox();
            this.comboBoxN2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxId2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id";
            // 
            // comboBoxId1
            // 
            this.comboBoxId1.FormattingEnabled = true;
            this.comboBoxId1.Location = new System.Drawing.Point(54, 68);
            this.comboBoxId1.Name = "comboBoxId1";
            this.comboBoxId1.Size = new System.Drawing.Size(119, 33);
            this.comboBoxId1.TabIndex = 1;
            this.comboBoxId1.TextChanged += new System.EventHandler(this.comboBoxId1_TextChanged);
            // 
            // comboBoxN1
            // 
            this.comboBoxN1.FormattingEnabled = true;
            this.comboBoxN1.Location = new System.Drawing.Point(219, 71);
            this.comboBoxN1.Name = "comboBoxN1";
            this.comboBoxN1.Size = new System.Drawing.Size(119, 33);
            this.comboBoxN1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "N";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Weight";
            // 
            // textBoxWeight
            // 
            this.textBoxWeight.Location = new System.Drawing.Point(146, 21);
            this.textBoxWeight.Name = "textBoxWeight";
            this.textBoxWeight.Size = new System.Drawing.Size(171, 31);
            this.textBoxWeight.TabIndex = 5;
            // 
            // comboBoxN2
            // 
            this.comboBoxN2.FormattingEnabled = true;
            this.comboBoxN2.Location = new System.Drawing.Point(219, 124);
            this.comboBoxN2.Name = "comboBoxN2";
            this.comboBoxN2.Size = new System.Drawing.Size(119, 33);
            this.comboBoxN2.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "N";
            // 
            // comboBoxId2
            // 
            this.comboBoxId2.FormattingEnabled = true;
            this.comboBoxId2.Location = new System.Drawing.Point(54, 121);
            this.comboBoxId2.Name = "comboBoxId2";
            this.comboBoxId2.Size = new System.Drawing.Size(119, 33);
            this.comboBoxId2.TabIndex = 8;
            this.comboBoxId2.TextChanged += new System.EventHandler(this.comboBoxId2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Id";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(31, 194);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(155, 34);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(219, 196);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(150, 31);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // AddLinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 271);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxN2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxId2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxWeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxN1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxId1);
            this.Controls.Add(this.label1);
            this.Name = "AddLinkForm";
            this.Text = "AddLinkForm";
            this.Load += new System.EventHandler(this.LinkForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxId1;
        private System.Windows.Forms.ComboBox comboBoxN1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxWeight;
        private System.Windows.Forms.ComboBox comboBoxN2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxId2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
    }
}