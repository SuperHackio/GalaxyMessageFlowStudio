using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalaxyMessageFlowStudio
{
    public partial class TextInputForm : Form
    {
        Action<string> OnAccept, OnCancel;

        public TextInputForm(string Title, string Message, Action<string> AcceptCallback, Action<string> CancelCallback, string StartText = "")
        {
            InitializeComponent();
            CenterToParent();
            Text = Title;
            MessageLabel.Text = Message;
            InputTextBox.Text = StartText;

            OnAccept = AcceptCallback;
            OnCancel = CancelCallback;
        }

        private void DeclineButton_Click(object sender, EventArgs e)
        {
            Close();
            if (OnCancel is null)
                return;

            OnCancel(InputTextBox.Text);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
            if (OnAccept is null)
                return;

            OnAccept(InputTextBox.Text);
        }

    }
}
