using GalaxyMessageFlowStudio.Nodes;
using Hack.io.MSBF;
using Hack.io.MSBT;
using Hack.io.RARC;
using Hack.io.Utility;
using Hack.io.YAZ0;
using ST.Library.UI.NodeEditor;
using System.Diagnostics.CodeAnalysis;
using static GalaxyMessageFlowStudio.Nodes.FlowNode;

namespace GalaxyMessageFlowStudio;

public partial class MainForm : Form
{
    const string FILTER = "SMG2 Message Archive|*.arc";
    private readonly OpenFileDialog ofd = new() { Filter = FILTER };
    private readonly SaveFileDialog sfd = new() { Filter = FILTER };

    public string? Filename { get; set; }
    public string MSB_PairName = "";
    [AllowNull]
    public MSBT CurrentMSBT;

    public MainForm(string[] args)
    {
        InitializeComponent();
        CenterToScreen();
        FlowChartSTNodeEditor.ActiveChanged += FlowChartSTNodeEditor_ActiveChanged;
        FlowChartSTNodeEditor.NodeAdded += FlowChartSTNodeEditor_NodeAdded;
        FlowChartSTNodeEditor.NodeRemoved += FlowChartSTNodeEditor_NodeRemoved;
        FlowChartSTNodeEditor.OptionConnected += FlowChartSTNodeEditor_OptionConnected;
        FlowChartSTNodeEditor.OptionDisConnected += FlowChartSTNodeEditor_OptionConnected;

        if (args.Length != 0)
            Open(args[0]);

        FlowChartSTNodeTreeView.AddNode(typeof(EntryFlowNode));
        FlowChartSTNodeTreeView.AddNode(typeof(MessageNode));
        FlowChartSTNodeTreeView.AddNode(typeof(ConditionNode));
        FlowChartSTNodeTreeView.AddNode(typeof(EventNode));
        FlowChartSTNodeTreeView.AddNode(typeof(EndFlowNode));
    }

    public void Open(string ArchivePath)
    {
        List<(Func<Stream, bool> CheckFunc, Func<byte[], byte[]> DecodeFunction)> DecompFuncs =
        [
            (YAZ0.Check, YAZ0.Decompress)
        ];
        RARC Archive = new();
        int encodingselection = FileUtil.LoadFileWithDecompression(ArchivePath, Archive.Load, [.. DecompFuncs]);
        if (encodingselection == -1)
            FileUtil.LoadFile(ArchivePath, Archive.Load);

        if (Archive.Root is null)
            throw new IOException("Missing Archive Root!");

        string SelectedMessages = Archive.Root.Name; //TODO: Make a window that lets you pick one
        bool fail = false;
        TextInputForm tif = new("File Select", "Type the filename", (string s) => SelectedMessages = s, (string x) => fail = true, SelectedMessages);
        tif.ShowDialog();
        if (fail)
            return;

        MSB_PairName = SelectedMessages;
        FlowChartSTNodeEditor.Nodes.Clear();

        CurrentMSBT = new();


        if (Archive.ItemExists(SelectedMessages + ".msbt"))
        {
            RARC.File FoundMSBT = (RARC.File)(Archive[SelectedMessages + ".msbt"] ?? throw new InvalidOperationException());
            CurrentMSBT.Load((MemoryStream)FoundMSBT);
        }

        if (Archive.ItemExists(SelectedMessages + ".msbf"))
        {
            RARC.File FoundMSBF = (RARC.File)(Archive[SelectedMessages + ".msbf"] ?? throw new InvalidOperationException());
            OpenMSBF((MemoryStream)FoundMSBF);
        }


        Filename = ArchivePath;
    }

    public void OpenMSBF(Stream Strm)
    {
        MSBF Flows = new();
        Flows.Load(Strm);

        List<MSBF.NodeBase> TemporaryNodes = [];
        Flows.GetFlattenedNodes(ref TemporaryNodes);

        for (int i = 0; i < TemporaryNodes.Count; i++)
        {
            if (TemporaryNodes[i] is MSBF.EntryNode EN)
            {
                EntryFlowNode EFN = new(EN);
                FlowChartSTNodeEditor.Nodes.Add(EFN);
            }
            else if (TemporaryNodes[i] is MSBF.MessageNode MN)
            {
                if (MN.MessageIndex == 0xFFFF)
                {
                    FlowChartSTNodeEditor.Nodes.Add(new MessageNode());
                }
                else
                {
                    if (MN.MessageIndex < 0 || MN.MessageIndex > CurrentMSBT.Count)
                    {
                        MN.MessageIndex = 0;
                    }
                    FlowChartSTNodeEditor.Nodes.Add(new MessageNode(CurrentMSBT.Messages[MN.MessageIndex], MN));
                }
            }
            else if (TemporaryNodes[i] is MSBF.BranchNode BN)
            {
                FlowChartSTNodeEditor.Nodes.Add(new ConditionNode(BN));
            }
            else if (TemporaryNodes[i] is MSBF.EventNode EVN)
            {
                FlowChartSTNodeEditor.Nodes.Add(new EventNode(EVN));
            }
        }

        //Now lets connect everything
        int NodeCount = FlowChartSTNodeEditor.Nodes.Count; //Needed because nodes we don't process get added during this loop
        for (int i = 0; i < NodeCount; i++)
        {
            STNode CurrentNode = FlowChartSTNodeEditor.Nodes[i];

            switch (TemporaryNodes[i].Type)
            {
                case MSBF.NodeType.MESSAGE:
                    MSBF.MessageNode MN = (MSBF.MessageNode)TemporaryNodes[i];
                    if (MN.NextNode is null)
                    {
                        //Implement End nodes
                        EndFlowNode EFN = new();
                        FlowChartSTNodeEditor.Nodes.Add(EFN);
                        CurrentNode.Connect(NodeOptions.NEXTNODE, EFN, NodeOptions.PREVNODE);
                    }
                    else
                    {
                        CurrentNode.Connect(NodeOptions.NEXTNODE, FlowChartSTNodeEditor.Nodes[TemporaryNodes.IndexOf(MN.NextNode)], NodeOptions.PREVNODE);
                    }
                    break;
                case MSBF.NodeType.BRANCH:
                    MSBF.BranchNode BN = (MSBF.BranchNode)TemporaryNodes[i];
                    CurrentNode.Connect(NodeOptions.TRUENODE, FlowChartSTNodeEditor.Nodes[TemporaryNodes.IndexOf(BN.NextNode)], NodeOptions.PREVNODE);
                    CurrentNode.Connect(NodeOptions.FALSENODE, FlowChartSTNodeEditor.Nodes[TemporaryNodes.IndexOf(BN.NextNodeElse)], NodeOptions.PREVNODE);
                    break;
                case MSBF.NodeType.EVENT:
                    MSBF.EventNode EVN = (MSBF.EventNode)TemporaryNodes[i];
                    if (EVN.NextNode is null)
                    {
                        //Implement End nodes
                        EndFlowNode EFN = new();
                        FlowChartSTNodeEditor.Nodes.Add(EFN);
                        CurrentNode.Connect(NodeOptions.NEXTNODE, EFN, NodeOptions.PREVNODE);
                    }
                    else
                        CurrentNode.Connect(NodeOptions.NEXTNODE, FlowChartSTNodeEditor.Nodes[TemporaryNodes.IndexOf(EVN.NextNode)], NodeOptions.PREVNODE);
                    break;
                case MSBF.NodeType.ENTRY:
                    MSBF.EntryNode EN = (MSBF.EntryNode)TemporaryNodes[i];
                    CurrentNode.Connect(NodeOptions.NEXTNODE, FlowChartSTNodeEditor.Nodes[TemporaryNodes.IndexOf(EN.NextNode)], NodeOptions.PREVNODE);
                    break;
                default:
                    throw new Exception("Invalid node type!");
            }
        }
        //TODO: Place these nodes so that they don't look like a disaster!
        int LowestPointY = 0,
            XPos = 0,
            YPos = 0;

        Stack<STNode> NodeStack = new();
        //I hate that I have to go through the list 3 times :weary:
        for (int i = 0; i < FlowChartSTNodeEditor.Nodes.Count; i++)
        {
            STNode CurrentNode = FlowChartSTNodeEditor.Nodes[i];
            if (CurrentNode is EntryFlowNode EFN)
            {
                //Reset position to the left
                XPos = 0;
                YPos = LowestPointY + 100;
                PlaceNode(EFN, ref NodeStack, ref XPos, ref YPos, ref LowestPointY);
            }
        }
    }

    public void OpenMSBT(Stream Strm)
    {
        //Use this for reloading MSBT
    }

    public void Save(string ArchivePath)
    {
        List<(Func<Stream, bool> CheckFunc, Func<byte[], byte[]> DecodeFunction)> DecompFuncs =
        [
            (YAZ0.Check, YAZ0.Decompress)
        ];
        RARC Archive = new();
        int encodingselection = FileUtil.LoadFileWithDecompression(ArchivePath, Archive.Load, [.. DecompFuncs]);
        if (encodingselection == -1)
            FileUtil.LoadFile(ArchivePath, Archive.Load);

        if (Archive.Root is null)
            throw new IOException("Missing Archive Root!");

        string SelectedMessages = Archive.Root.Name; //TODO: Make a window that lets you pick one
        bool fail = false;
        TextInputForm tif = new("File Select", "Type the filename", (string s) => SelectedMessages = s, (string x) => fail = true, SelectedMessages);
        tif.ShowDialog();
        if (fail)
            return;

        MSB_PairName = SelectedMessages;

        MSBF? newFile = MakeMSBF(CurrentMSBT, out bool MSBFError);
        if (MSBFError || newFile is null)
        {
            return;
        }

        if (newFile.Flows.Count != 0)
        {
            RARC.File file = new() { Name = SelectedMessages + ".msbf" };
            MemoryStream MS = new();
            newFile.Save(MS);
            file.Load(MS);
            Archive[SelectedMessages + ".msbf"] = file;
        }

        FileUtil.SaveFile(ArchivePath, Archive.Save);
    }



    private MSBF? MakeMSBF(MSBT messages, out bool Error)
    {
        MSBF Flows = new();
        Error = false;
        //The hard part is getting these nodes setup

        //Ideally we make the file pretty by organizing the nodes in order
        //So lets first get all the flow starts in alphabetical order

        int NodeCount = FlowChartSTNodeEditor.Nodes.Count; //Don't wanna have to type this a million times :weary:

        List<MSBF.NodeBase> NewFlatNodes = [];
        List<STNode> NodesWithoutEnds = [];

        for (int i = 0; i < NodeCount; i++)
        {
            STNode Current = FlowChartSTNodeEditor.Nodes[i];

            if (Current is EndFlowNode)
                continue;

            NodesWithoutEnds.Add(Current);
        }
        NodeCount = NodesWithoutEnds.Count;
        for (int i = 0; i < NodeCount; i++)
        {
            STNode Current = NodesWithoutEnds[i];

            if (Current is EntryFlowNode STEFN)
            {
                NewFlatNodes.Add(new MSBF.EntryNode() { Label = STEFN.MessageLabel });
                continue;
            }

            if (Current is MessageNode STMN)
            {
                MSBT.Message? NodeMessage = messages.FindByLabel(STMN.GetMessage().Label);
                if (NodeMessage is null)
                {
                    //Handle null
                    return null;
                }
                NewFlatNodes.Add(new MSBF.MessageNode() { MessageIndex = messages.IndexOf(NodeMessage) });
                continue;
            }

            if (Current is ConditionNode STBN)
            {
                NewFlatNodes.Add(new MSBF.BranchNode() { BranchCondition = STBN.Condition, Parameter = (ushort)STBN.Parameter });
                continue;
            }

            if (Current is EventNode STEN)
            {
                NewFlatNodes.Add(new MSBF.EventNode() { EventType = STEN.Event, Parameter = (ushort)STEN.Parameter });
                continue;
            }
        }
        //Second pass to link everything
        for (int i = 0; i < NodeCount; i++)
        {
            STNode CurrentSTNode = NodesWithoutEnds[i];
            MSBF.NodeBase CurrentMSBFNode = NewFlatNodes[i];

            CurrentMSBFNode.NextNode = null;
            if (CurrentSTNode is ConditionNode STCN)
            {
                ushort index = GetNextNodeIndexBranch(STCN, true);
                if (index != 0xFFFF)
                    ((MSBF.BranchNode)CurrentMSBFNode).NextNode = NewFlatNodes[index];

                index = GetNextNodeIndexBranch(STCN, false);
                if (index != 0xFFFF)
                    ((MSBF.BranchNode)CurrentMSBFNode).NextNodeElse = NewFlatNodes[index];
                else
                    ((MSBF.BranchNode)CurrentMSBFNode).NextNodeElse = null;
            }
            else
            {
                ushort index = GetNextNodeIndex(CurrentSTNode);
                if (index == 0xFFFF)
                    continue;
                CurrentMSBFNode.NextNode = NewFlatNodes[index];
            }

            if (CurrentMSBFNode is MSBF.EntryNode MEN)
                Flows.Flows.Add(MEN);
        }

        return Flows;

        ushort GetNextNodeIndex(STNode CurrentNode)
        {
            if (CurrentNode.IsConnected(NodeOptions.NEXTNODE, NodeOptions.PREVNODE))
            {
                STNode NextNode = CurrentNode.GetConnectedNode(NodeOptions.NEXTNODE, NodeOptions.PREVNODE);
                return (ushort)NodesWithoutEnds.IndexOf(NextNode);
            }
            return 0xFFFF;
        }
        ushort GetNextNodeIndexBranch(STNode CurrentNode, bool IsTrue)
        {
            if (CurrentNode.IsConnected(IsTrue ? NodeOptions.TRUENODE : NodeOptions.FALSENODE, NodeOptions.PREVNODE))
            {
                STNode NextNode = CurrentNode.GetConnectedNode(IsTrue ? NodeOptions.TRUENODE : NodeOptions.FALSENODE, NodeOptions.PREVNODE);
                return (ushort)NodesWithoutEnds.IndexOf(NextNode);
            }
            return 0xFFFF;
        }
    }


    private void PlaceNode(STNode CurrentNode, ref Stack<STNode> NodeStack, ref int X, ref int Y, ref int Lowest)
    {
        if (NodeStack.Contains(CurrentNode))
            return; //Node already positioned

        STNode NextNode;
        CurrentNode.Location = new Point(X, Y);
        if (CurrentNode is not ConditionNode)
            MakeNodeCorrection(CurrentNode, NodeStack);
        if (Y + CurrentNode.Height > Lowest)
            Lowest = Y + (CurrentNode.Height);
        if (CurrentNode is EntryFlowNode EFN)
        {
            NodeStack.Push(EFN);
            if (EFN.IsConnected(NodeOptions.NEXTNODE, NodeOptions.PREVNODE))
            {
                X += EFN.Width + 100;

                NextNode = EFN.GetConnectedNode(NodeOptions.NEXTNODE, NodeOptions.PREVNODE);
                PlaceNode(NextNode, ref NodeStack, ref X, ref Y, ref Lowest);
            }
        }
        else if (CurrentNode is MessageNode MN)
        {
            NodeStack.Push(MN);
            if (MN.IsConnected(NodeOptions.NEXTNODE, NodeOptions.PREVNODE))
            {
                X += MN.Width + 100;

                NextNode = MN.GetConnectedNode(NodeOptions.NEXTNODE, NodeOptions.PREVNODE);
                PlaceNode(NextNode, ref NodeStack, ref X, ref Y, ref Lowest);
            }
        }
        else if (CurrentNode is ConditionNode CN)
        {
            CurrentNode.Location = new Point(X, Y - CN.Height / 4);
            MakeNodeCorrection(CurrentNode, NodeStack);
            NodeStack.Push(CN);
            //Handle True case first
            int xpos = X, ypos = Y;
            if (CN.IsConnected(NodeOptions.TRUENODE, NodeOptions.PREVNODE))
            {
                NextNode = CN.GetConnectedNode(NodeOptions.TRUENODE, NodeOptions.PREVNODE);
                X += CN.Width + 100;
                Y -= (CN.Height / 2) - NextNode.Height / 4;
                PlaceNode(NextNode, ref NodeStack, ref X, ref Y, ref Lowest);
            }
            X = xpos;
            Y = ypos;
            if (CN.IsConnected(NodeOptions.FALSENODE, NodeOptions.PREVNODE))
            {
                NextNode = CN.GetConnectedNode(NodeOptions.FALSENODE, NodeOptions.PREVNODE);
                X += CN.Width + 100;
                Y += (CN.Height / 2) + NextNode.Height / 4;
                PlaceNode(NextNode, ref NodeStack, ref X, ref Y, ref Lowest);
            }


        }
        else if (CurrentNode is EventNode EVN)
        {
            NodeStack.Push(EVN);
            if (EVN.IsConnected(NodeOptions.NEXTNODE, NodeOptions.PREVNODE))
            {
                X += EVN.Width + 100;

                NextNode = EVN.GetConnectedNode(NodeOptions.NEXTNODE, NodeOptions.PREVNODE);
                PlaceNode(NextNode, ref NodeStack, ref X, ref Y, ref Lowest);
            }
        }
        else if (CurrentNode is EndFlowNode EndFlowNode)
        {
            NodeStack.Push(EndFlowNode);
        }
    }

    private void MakeNodeCorrection(STNode CurrentNode, Stack<STNode> NodeStack)
    {
        List<STNode> Nodes = [.. NodeStack];
    Retry:
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (ReferenceEquals(CurrentNode, Nodes[i]))
                continue;

            Rectangle Rect = Nodes[i].Rectangle;
            Point TL = new(CurrentNode.Left, CurrentNode.Top);
            Point TR = new(CurrentNode.Right, CurrentNode.Top);
            Point TC = new(CurrentNode.Left + (CurrentNode.Right - CurrentNode.Left), CurrentNode.Top);
            Point CL = new(CurrentNode.Left, CurrentNode.Top + (CurrentNode.Bottom - CurrentNode.Top));
            Point CR = new(CurrentNode.Right, CurrentNode.Top + (CurrentNode.Bottom - CurrentNode.Top));
            Point CC = new(CurrentNode.Left + (CurrentNode.Right - CurrentNode.Left), CurrentNode.Top + (CurrentNode.Bottom - CurrentNode.Top));
            Point BL = new(CurrentNode.Left, CurrentNode.Bottom);
            Point BR = new(CurrentNode.Right, CurrentNode.Bottom);
            Point BC = new(CurrentNode.Left + (CurrentNode.Right - CurrentNode.Left), CurrentNode.Bottom);
            if (Rect.Contains(TL) || Rect.Contains(TR) || Rect.Contains(TC) || Rect.Contains(CL) || Rect.Contains(CR) || Rect.Contains(CC) || Rect.Contains(BL) || Rect.Contains(BR) || Rect.Contains(BC))
            {
                int Offset = 30;
                CurrentNode.Location = new Point(CurrentNode.Location.X, Rect.Bottom + Offset);
                goto Retry; //Repeat this process until we are not inside any nodes
            }
        }
    }



    private void FlowChartSTNodeEditor_OptionConnected(object sender, STNodeEditorOptionEventArgs e)
    {
        switch (e.Status)
        {
            case ConnectionStatus.NoOwner:
            case ConnectionStatus.SameInputOrOutput:
            case ConnectionStatus.ErrorType:
            case ConnectionStatus.SingleOption:
            case ConnectionStatus.Exists:
            case ConnectionStatus.EmptyOption:
            case ConnectionStatus.Locked:
            case ConnectionStatus.Reject:
                FlowChartSTNodeEditor.ShowAlert("Connection Failed", Color.White, Color.Red);
                break;
            case ConnectionStatus.SameOwner:
            case ConnectionStatus.Loop:
            case ConnectionStatus.Connected:
            case ConnectionStatus.Connecting:
                FlowChartSTNodeEditor.ShowAlert("Connected", Color.White, Color.Green);
                break;
            case ConnectionStatus.DisConnected:
            case ConnectionStatus.DisConnecting:
                FlowChartSTNodeEditor.ShowAlert("Disconnected", Color.White, Color.Goldenrod);
                break;
            default:
                FlowChartSTNodeEditor.ShowAlert("UNKNOWN STATUS", Color.White, Color.Red);
                break;
        }
    }

    private void FlowChartSTNodeEditor_NodeAdded(object sender, STNodeEditorEventArgs e)
    {
        e.Node.ContextMenuStrip = NodeContextMenuStrip;
        FlowChartSTNodeEditor.ShowAlert($"Added Node \"{e.Node.Title}\"", e.Node.ForeColor, e.Node.TitleColor);

        e.Node.ValueChanged += Node_ValueChanged;

        if (e.Node is MessageNode MN)
        {
            MN.MessageSelectButton.MouseClick += MessageSelectButton_MouseClick;
        }
    }

    private void FlowChartSTNodeEditor_NodeRemoved(object sender, STNodeEditorEventArgs e)
    {
        e.Node.ValueChanged -= Node_ValueChanged;
    }

    private void FlowChartSTNodeEditor_ActiveChanged(object? sender, EventArgs e)
    {
        FlowSTNodePropertyGrid.SetNode(FlowChartSTNodeEditor.ActiveNode);
        FlowSTNodePropertyGrid.Refresh();
    }


    private void NewToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //MessageListView.Items.Clear();
        FlowChartSTNodeEditor.Nodes.Clear();
        Filename = null;
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (ofd.ShowDialog() != DialogResult.OK)
            return;

        Open(ofd.FileName);
    }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Filename is null)
        {
            SaveAsToolStripMenuItem_Click(sender, e);
            return;
        }


        Save(Filename);
    }

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //FlowChartSTNodeEditor.Hide();
        DialogResult dr = sfd.ShowDialog();
        //FlowChartSTNodeEditor.Show();
        if (dr != DialogResult.OK)
            return;

        Filename = sfd.FileName;
        //Create a new archive
        RARC newArchive = new()
        {
            Root = new RARC.Directory() { Name = Path.GetFileNameWithoutExtension(Filename) }
        };
        FileUtil.SaveFile(Filename, newArchive.Save);

        Save(Filename);
    }

    private void ReloadMSBTToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Didn't feel like implementing right now D: (2023-10-04)");
    }

    private void MessageSelectButton_MouseClick(object? sender, MouseEventArgs e)
    {
        if (FlowChartSTNodeEditor.ActiveNode is null)
            return;
        if (CurrentMSBT is null || CurrentMSBT.Count == 0)
        {
            MessageBox.Show("There are no MSBT messages to choose from!");
            return;
        }

        List<string> Labels = [];
        for (int i = 0; i < CurrentMSBT.Count; i++)
        {
            Labels.Add(CurrentMSBT.Messages[i].Label);
        }
        MessageSelectForm MSF = new(Labels);
        if (MSF.ShowDialog() == DialogResult.OK)
        {
            for (int i = 0; i < CurrentMSBT.Count; i++)
            {
                if (CurrentMSBT.Messages[i].Label.Equals(MSF.SelectedLabel))
                {
                    ((MessageNode)FlowChartSTNodeEditor.ActiveNode).SetMessage(CurrentMSBT.Messages[i]);
                }
            }
        }
    }

    private void DeleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (FlowChartSTNodeEditor.ActiveNode is null)
            return;
        FlowChartSTNodeEditor.Nodes.Remove(FlowChartSTNodeEditor.ActiveNode);
    }

    private void Node_ValueChanged(object? sender, EventArgs e)
    {
        FlowSTNodePropertyGrid.Invalidate();
    }
}