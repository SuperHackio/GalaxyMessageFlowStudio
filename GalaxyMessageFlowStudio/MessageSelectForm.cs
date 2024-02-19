namespace GalaxyMessageFlowStudio
{
    public partial class MessageSelectForm : Form
    {
        public string SelectedLabel => Labels[SelectListBox.SelectedIndex];
        List<string> Labels;
        public MessageSelectForm(List<string> labels)
        {
            InitializeComponent();
            CenterToParent();
            Labels = labels;
            for (int i = 0; i < labels.Count; i++)
            {
                SelectListBox.Items.Add(labels[i]);
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (SelectListBox.SelectedIndex >= 0)
                DialogResult = DialogResult.OK;
            Close();
        }

        private void SelectListBox_DoubleClick(object sender, EventArgs e) => SelectButton_Click(sender, e);
    }
}
