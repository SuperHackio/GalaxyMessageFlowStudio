namespace GalaxyMessageFlowStudio
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            NewToolStripMenuItem = new ToolStripMenuItem();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            SaveAsToolStripMenuItem = new ToolStripMenuItem();
            ReloadMSBTToolStripMenuItem = new ToolStripMenuItem();
            STNodeUISplitContainer = new SplitContainer();
            FlowChartSTNodeTreeView = new ST.Library.UI.NodeEditor.STNodeTreeView();
            FlowSTNodePropertyGrid = new ST.Library.UI.NodeEditor.STNodePropertyGrid();
            FlowChartSTNodeEditor = new ST.Library.UI.NodeEditor.STNodeEditor();
            NodeContextMenuStrip = new ContextMenuStrip(components);
            DeleteNodeToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            ProgramInstructionsToolStripMenuItem = new ToolStripMenuItem();
            MSBFDocumentationToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)STNodeUISplitContainer).BeginInit();
            STNodeUISplitContainer.Panel1.SuspendLayout();
            STNodeUISplitContainer.Panel2.SuspendLayout();
            STNodeUISplitContainer.SuspendLayout();
            NodeContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, ReloadMSBTToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1464, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewToolStripMenuItem, OpenToolStripMenuItem, SaveToolStripMenuItem, SaveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // NewToolStripMenuItem
            // 
            NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            NewToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            NewToolStripMenuItem.Size = new Size(186, 22);
            NewToolStripMenuItem.Text = "New";
            NewToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            OpenToolStripMenuItem.Size = new Size(186, 22);
            OpenToolStripMenuItem.Text = "Open";
            OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SaveToolStripMenuItem.Size = new Size(186, 22);
            SaveToolStripMenuItem.Text = "Save";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // SaveAsToolStripMenuItem
            // 
            SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            SaveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            SaveAsToolStripMenuItem.Size = new Size(186, 22);
            SaveAsToolStripMenuItem.Text = "Save As";
            SaveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // ReloadMSBTToolStripMenuItem
            // 
            ReloadMSBTToolStripMenuItem.Name = "ReloadMSBTToolStripMenuItem";
            ReloadMSBTToolStripMenuItem.Size = new Size(87, 20);
            ReloadMSBTToolStripMenuItem.Text = "Reload MSBT";
            ReloadMSBTToolStripMenuItem.Visible = false;
            ReloadMSBTToolStripMenuItem.Click += ReloadMSBTToolStripMenuItem_Click;
            // 
            // STNodeUISplitContainer
            // 
            STNodeUISplitContainer.BackColor = Color.Black;
            STNodeUISplitContainer.Dock = DockStyle.Left;
            STNodeUISplitContainer.FixedPanel = FixedPanel.Panel1;
            STNodeUISplitContainer.IsSplitterFixed = true;
            STNodeUISplitContainer.Location = new Point(0, 24);
            STNodeUISplitContainer.Name = "STNodeUISplitContainer";
            STNodeUISplitContainer.Orientation = Orientation.Horizontal;
            // 
            // STNodeUISplitContainer.Panel1
            // 
            STNodeUISplitContainer.Panel1.Controls.Add(FlowChartSTNodeTreeView);
            // 
            // STNodeUISplitContainer.Panel2
            // 
            STNodeUISplitContainer.Panel2.Controls.Add(FlowSTNodePropertyGrid);
            STNodeUISplitContainer.Size = new Size(251, 657);
            STNodeUISplitContainer.SplitterDistance = 264;
            STNodeUISplitContainer.TabIndex = 1;
            // 
            // FlowChartSTNodeTreeView
            // 
            FlowChartSTNodeTreeView.AllowDrop = true;
            FlowChartSTNodeTreeView.BackColor = Color.FromArgb(35, 35, 35);
            FlowChartSTNodeTreeView.Dock = DockStyle.Fill;
            FlowChartSTNodeTreeView.FolderCountColor = Color.FromArgb(40, 255, 255, 255);
            FlowChartSTNodeTreeView.ForeColor = Color.FromArgb(220, 220, 220);
            FlowChartSTNodeTreeView.ItemBackColor = Color.FromArgb(45, 45, 45);
            FlowChartSTNodeTreeView.ItemHoverColor = Color.FromArgb(50, 125, 125, 125);
            FlowChartSTNodeTreeView.Location = new Point(0, 0);
            FlowChartSTNodeTreeView.MinimumSize = new Size(100, 60);
            FlowChartSTNodeTreeView.Name = "FlowChartSTNodeTreeView";
            FlowChartSTNodeTreeView.ShowFolderCount = true;
            FlowChartSTNodeTreeView.Size = new Size(251, 264);
            FlowChartSTNodeTreeView.TabIndex = 0;
            FlowChartSTNodeTreeView.Text = "stNodeTreeView1";
            FlowChartSTNodeTreeView.TextBoxColor = Color.FromArgb(30, 30, 30);
            FlowChartSTNodeTreeView.TitleColor = Color.FromArgb(60, 60, 60);
            // 
            // FlowSTNodePropertyGrid
            // 
            FlowSTNodePropertyGrid.BackColor = Color.FromArgb(35, 35, 35);
            FlowSTNodePropertyGrid.DescriptionColor = Color.FromArgb(200, 184, 134, 11);
            FlowSTNodePropertyGrid.Dock = DockStyle.Fill;
            FlowSTNodePropertyGrid.ErrorColor = Color.FromArgb(200, 165, 42, 42);
            FlowSTNodePropertyGrid.ForeColor = Color.White;
            FlowSTNodePropertyGrid.ItemHoverColor = Color.FromArgb(50, 125, 125, 125);
            FlowSTNodePropertyGrid.ItemValueBackColor = Color.FromArgb(80, 80, 80);
            FlowSTNodePropertyGrid.Location = new Point(0, 0);
            FlowSTNodePropertyGrid.MinimumSize = new Size(120, 50);
            FlowSTNodePropertyGrid.Name = "FlowSTNodePropertyGrid";
            FlowSTNodePropertyGrid.ShowTitle = true;
            FlowSTNodePropertyGrid.Size = new Size(251, 389);
            FlowSTNodePropertyGrid.TabIndex = 0;
            FlowSTNodePropertyGrid.Text = "Node Settings";
            FlowSTNodePropertyGrid.TitleColor = Color.FromArgb(127, 0, 0, 0);
            // 
            // FlowChartSTNodeEditor
            // 
            FlowChartSTNodeEditor.AllowDrop = true;
            FlowChartSTNodeEditor.BackColor = Color.FromArgb(34, 34, 34);
            FlowChartSTNodeEditor.Curvature = 0.3F;
            FlowChartSTNodeEditor.Dock = DockStyle.Fill;
            FlowChartSTNodeEditor.Location = new Point(251, 24);
            FlowChartSTNodeEditor.LocationBackColor = Color.FromArgb(120, 0, 0, 0);
            FlowChartSTNodeEditor.MarkBackColor = Color.FromArgb(180, 0, 0, 0);
            FlowChartSTNodeEditor.MarkForeColor = Color.FromArgb(180, 0, 0, 0);
            FlowChartSTNodeEditor.MinimumSize = new Size(100, 100);
            FlowChartSTNodeEditor.Name = "FlowChartSTNodeEditor";
            FlowChartSTNodeEditor.Size = new Size(1213, 657);
            FlowChartSTNodeEditor.TabIndex = 2;
            // 
            // NodeContextMenuStrip
            // 
            NodeContextMenuStrip.Items.AddRange(new ToolStripItem[] { DeleteNodeToolStripMenuItem });
            NodeContextMenuStrip.Name = "NodeContextMenuStrip";
            NodeContextMenuStrip.Size = new Size(140, 26);
            // 
            // DeleteNodeToolStripMenuItem
            // 
            DeleteNodeToolStripMenuItem.Name = "DeleteNodeToolStripMenuItem";
            DeleteNodeToolStripMenuItem.Size = new Size(139, 22);
            DeleteNodeToolStripMenuItem.Text = "Delete Node";
            DeleteNodeToolStripMenuItem.Click += DeleteNodeToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ProgramInstructionsToolStripMenuItem, MSBFDocumentationToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // ProgramInstructionsToolStripMenuItem
            // 
            ProgramInstructionsToolStripMenuItem.Name = "ProgramInstructionsToolStripMenuItem";
            ProgramInstructionsToolStripMenuItem.Size = new Size(189, 22);
            ProgramInstructionsToolStripMenuItem.Text = "Program Instructions";
            ProgramInstructionsToolStripMenuItem.Click += ProgramInstructionsToolStripMenuItem_Click;
            // 
            // MSBFDocumentationToolStripMenuItem
            // 
            MSBFDocumentationToolStripMenuItem.Name = "MSBFDocumentationToolStripMenuItem";
            MSBFDocumentationToolStripMenuItem.Size = new Size(189, 22);
            MSBFDocumentationToolStripMenuItem.Text = "MSBF documentation";
            MSBFDocumentationToolStripMenuItem.Click += MSBFDocumentationToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(1464, 681);
            Controls.Add(FlowChartSTNodeEditor);
            Controls.Add(STNodeUISplitContainer);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1480, 720);
            Name = "MainForm";
            Text = "Galaxy Message Flow Studio";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            STNodeUISplitContainer.Panel1.ResumeLayout(false);
            STNodeUISplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)STNodeUISplitContainer).EndInit();
            STNodeUISplitContainer.ResumeLayout(false);
            NodeContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem NewToolStripMenuItem;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripMenuItem SaveAsToolStripMenuItem;
        private SplitContainer STNodeUISplitContainer;
        private ST.Library.UI.NodeEditor.STNodeTreeView FlowChartSTNodeTreeView;
        private ST.Library.UI.NodeEditor.STNodePropertyGrid FlowSTNodePropertyGrid;
        private ST.Library.UI.NodeEditor.STNodeEditor FlowChartSTNodeEditor;
        private ToolStripMenuItem ReloadMSBTToolStripMenuItem;
        private ContextMenuStrip NodeContextMenuStrip;
        private ToolStripMenuItem DeleteNodeToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem ProgramInstructionsToolStripMenuItem;
        private ToolStripMenuItem MSBFDocumentationToolStripMenuItem;
    }
}