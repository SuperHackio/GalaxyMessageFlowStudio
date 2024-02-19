namespace GalaxyMessageFlowStudio
{
    partial class TextInputForm
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
            this.MessageLabel = new System.Windows.Forms.Label();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.DeclineButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MessageLabel.Location = new System.Drawing.Point(0, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.MessageLabel.Size = new System.Drawing.Size(395, 37);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.Text = "[Text goes here]";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // InputTextBox
            // 
            this.InputTextBox.Location = new System.Drawing.Point(12, 47);
            this.InputTextBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(371, 20);
            this.InputTextBox.TabIndex = 1;
            // 
            // DeclineButton
            // 
            this.DeclineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeclineButton.Location = new System.Drawing.Point(12, 76);
            this.DeclineButton.Name = "DeclineButton";
            this.DeclineButton.Size = new System.Drawing.Size(75, 23);
            this.DeclineButton.TabIndex = 2;
            this.DeclineButton.Text = "Cancel";
            this.DeclineButton.UseVisualStyleBackColor = true;
            this.DeclineButton.Click += new System.EventHandler(this.DeclineButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.Location = new System.Drawing.Point(308, 76);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // TextInputForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DeclineButton;
            this.ClientSize = new System.Drawing.Size(395, 111);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.DeclineButton);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.MessageLabel);
            this.Name = "TextInputForm";
            this.Text = "[Title]";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.Button DeclineButton;
        private System.Windows.Forms.Button OkButton;
    }
}