namespace GalaxyMessageFlowStudio
{
    partial class MessageSelectForm
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
            this.SelectListBox = new System.Windows.Forms.ListBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectListBox
            // 
            this.SelectListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectListBox.FormattingEnabled = true;
            this.SelectListBox.IntegralHeight = false;
            this.SelectListBox.Location = new System.Drawing.Point(0, 0);
            this.SelectListBox.Name = "SelectListBox";
            this.SelectListBox.Size = new System.Drawing.Size(266, 297);
            this.SelectListBox.TabIndex = 0;
            this.SelectListBox.DoubleClick += new System.EventHandler(this.SelectListBox_DoubleClick);
            // 
            // SelectButton
            // 
            this.SelectButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SelectButton.Location = new System.Drawing.Point(0, 297);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(266, 23);
            this.SelectButton.TabIndex = 1;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // MessageSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 320);
            this.Controls.Add(this.SelectListBox);
            this.Controls.Add(this.SelectButton);
            this.Name = "MessageSelectForm";
            this.Text = "Select a Message";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox SelectListBox;
        private System.Windows.Forms.Button SelectButton;
    }
}