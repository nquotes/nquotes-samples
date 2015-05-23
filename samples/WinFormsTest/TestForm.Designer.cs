namespace TestExperts.WinFormsTest
{
    partial class TestForm
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
            this.symbolLabel = new System.Windows.Forms.Label();
            this.getInfoButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.localTimeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.brokerCompanyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current symbol:";
            // 
            // symbolLabel
            // 
            this.symbolLabel.AutoSize = true;
            this.symbolLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.symbolLabel.Location = new System.Drawing.Point(22, 266);
            this.symbolLabel.Name = "symbolLabel";
            this.symbolLabel.Size = new System.Drawing.Size(80, 17);
            this.symbolLabel.TabIndex = 1;
            this.symbolLabel.Text = "<unknown>";
            // 
            // getInfoButton
            // 
            this.getInfoButton.Location = new System.Drawing.Point(22, 190);
            this.getInfoButton.Name = "getInfoButton";
            this.getInfoButton.Size = new System.Drawing.Size(150, 34);
            this.getInfoButton.TabIndex = 2;
            this.getInfoButton.Text = "Get information";
            this.getInfoButton.UseVisualStyleBackColor = true;
            this.getInfoButton.Click += new System.EventHandler(this.getInfoButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Local time:";
            // 
            // localTimeLabel
            // 
            this.localTimeLabel.AutoSize = true;
            this.localTimeLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.localTimeLabel.Location = new System.Drawing.Point(22, 58);
            this.localTimeLabel.Name = "localTimeLabel";
            this.localTimeLabel.Size = new System.Drawing.Size(80, 17);
            this.localTimeLabel.TabIndex = 4;
            this.localTimeLabel.Text = "<unknown>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Status:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.statusLabel.Location = new System.Drawing.Point(22, 120);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(80, 17);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "<unknown>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Broker company:";
            // 
            // brokerCompanyLabel
            // 
            this.brokerCompanyLabel.AutoSize = true;
            this.brokerCompanyLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.brokerCompanyLabel.Location = new System.Drawing.Point(22, 330);
            this.brokerCompanyLabel.Name = "brokerCompanyLabel";
            this.brokerCompanyLabel.Size = new System.Drawing.Size(80, 17);
            this.brokerCompanyLabel.TabIndex = 8;
            this.brokerCompanyLabel.Text = "<unknown>";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 402);
            this.Controls.Add(this.brokerCompanyLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.localTimeLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.getInfoButton);
            this.Controls.Add(this.symbolLabel);
            this.Controls.Add(this.label1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label symbolLabel;
        private System.Windows.Forms.Button getInfoButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label localTimeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label brokerCompanyLabel;
    }
}