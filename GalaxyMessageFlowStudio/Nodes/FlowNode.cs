using Hack.io.MSBF;
using ST.Library.UI.NodeEditor;
using GalaxyMessageFlowStudio.NodeControls;
using Hack.io.MSBT;

namespace GalaxyMessageFlowStudio.Nodes;

public class FlowNode
{
    /// <summary>
    /// MSBF Entry Node Represent
    /// </summary>
    [STNode("")]
    public class EntryFlowNode : STNode
    {
        private string? _MsgLbl;
        [STNodeProperty("Linked Message Label", "The Message Label that will start this flowchain")]
        public string? MessageLabel
        {
            get
            {
                return _MsgLbl;
            }
            set
            {
                if (value is null)
                {
                    _MsgLbl = value;
                    Title = "Entry: " + "UNASSIGNED";
                }
                else
                {
                    _MsgLbl = value;
                    Title = "Entry: " + value;
                }

            }
        }
        public EntryFlowNode()
        {
            MessageLabel = "NewEntry000";
        }

        public EntryFlowNode(MSBF.EntryNode EN)
        {
            MessageLabel = EN.Label;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            InputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(NodeOptions.MakeNextNodeOption());
            TitleColor = Color.FromArgb(255, Color.Goldenrod);
            LetGetOptions = true;
        }
    }
    [STNode("")]
    public class MessageNode : STNode
    {
        const string NODETITLEENG = "Message: ";
        const string NOMESSAGE = NODETITLEENG + "(none)";
        const string NOMESSAGECHOSEN = "(No Message Chosen)";
        private MSBT.Message _message;
        private MSBT.Message Message
        {
            get => _message;
            set
            {
                _message = value;
                if (value is not null)
                    Title = NODETITLEENG + value.Label;
                else
                    Title = NOMESSAGE;
                MessageTextLabel.Text = value?.Content ?? NOMESSAGECHOSEN;
            }
        }
        [STNodeProperty("Do Not Touch", "Does nothing")]
        public ushort GroupID
        {
            get; set;
        }

        public STNodeButton MessageSelectButton;
        public STNodeLabel MessageTextLabel;
        public MessageNode()
        {
            Message = null;
            Title = NOMESSAGE;
            MessageTextLabel.Text = NOMESSAGECHOSEN;
        }

        public MessageNode(MSBT.Message msg, MSBF.MessageNode source)
        {
            SetMessage(msg);
            //GroupID = source.GroupID;
        }

        public MSBT.Message GetMessage() => Message;
        public void SetMessage(MSBT.Message msg)
        {
            Message = msg;
            InvokeValueChanged();
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            InputOptions.Add(NodeOptions.MakePrevNodeOption());
            InputOptions.Add(STNodeOption.Empty);
            InputOptions.Add(STNodeOption.Empty);
            InputOptions.Add(STNodeOption.Empty);
            InputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(NodeOptions.MakeNextNodeOption());
            TitleColor = Color.FromArgb(255, Color.Gray);
            LetGetOptions = true;

            MessageSelectButton = new STNodeButton();
            MessageSelectButton.Text = "Change Message";
            MessageSelectButton.BackColor = TitleColor;
            MessageSelectButton.HoverColor = Color.FromArgb(TitleColor.A, TitleColor.R / 2, TitleColor.G / 2, TitleColor.B / 2);
            MessageSelectButton.ClickColor = Color.FromArgb(TitleColor.A, TitleColor.R / 4, TitleColor.G / 4, TitleColor.B / 4);
            MessageSelectButton.DisplayRectangle = new Rectangle(10, 21, Width - 20, 20);
            Controls.Add(MessageSelectButton);

            MessageTextLabel = new STNodeLabel();
            MessageTextLabel.BackColor = BackColor;
            MessageTextLabel.ForeColor = ForeColor;
            MessageTextLabel.DisplayRectangle = new Rectangle(10, 51, Width - 20, 40);
            Controls.Add(MessageTextLabel);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            MessageSelectButton.DisplayRectangle = new Rectangle(10, 21, Width - 20, 20);
            MessageTextLabel.DisplayRectangle = new Rectangle(10, 51, Width - 20, 40);
        }
    }
    [STNode("")]
    public class ConditionNode : STNode
    {
        private MSBF.Conditions _condition;
        [STNodeProperty("Branch Condition", "The condition to check with.")]
        public MSBF.Conditions Condition
        {
            get { return _condition; }
            set
            {
                _condition = value;
                BranchConditionEnumBox.Enum = value;
                Title = "Condition: " + value.ToString();
            }
        }

        private int _param;
        [STNodeProperty("Condition Parameter", "An additional parameter for the condition.")]
        public int Parameter
        {
            get => _param;
            set
            {
                _param = value;
                ParameterTextLabel.Text = $"Paramter: {value}";
            }
        }

        private STNodeSelectEnumBox BranchConditionEnumBox;
        public STNodeLabel ParameterTextLabel;

        public ConditionNode()
        {
            Condition = MSBF.Conditions.YesNoResult;
            Parameter = 0;
        }
        public ConditionNode(MSBF.BranchNode source)
        {
            Condition = source.BranchCondition;
            Parameter = source.Parameter;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.AutoSize = false;
            this.Size = new Size(240, 142);
            InputOptions.Add(NodeOptions.MakePrevNodeOption());
            InputOptions.Add(STNodeOption.Empty);
            InputOptions.Add(STNodeOption.Empty);
            InputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(NodeOptions.MakeNextNodeOption(NodeOptions.TRUENODE));
            OutputOptions.Add(NodeOptions.MakeNextNodeOption(NodeOptions.FALSENODE));

            BranchConditionEnumBox = new STNodeSelectEnumBox();
            BranchConditionEnumBox.Enum = _condition;
            BranchConditionEnumBox.ValueChanged += (s, e) =>
            {
                _condition = (MSBF.Conditions)BranchConditionEnumBox.Enum;
                InvokeValueChanged();
            };
            BranchConditionEnumBox.DisplayRectangle = new Rectangle(10, 21, Width - 20, 18);
            Controls.Add(BranchConditionEnumBox);
            TitleColor = Color.FromArgb(255, Color.Green);
            LetGetOptions = true;

            ParameterTextLabel = new STNodeLabel();
            ParameterTextLabel.BackColor = BackColor;
            ParameterTextLabel.ForeColor = ForeColor;
            ParameterTextLabel.DisplayRectangle = new Rectangle(10, 95, Width - 20, 18);
            Controls.Add(ParameterTextLabel);
        }

        protected override void OnDrawNode(DrawingTools dt)
        {
            ParameterTextLabel.Visable = MSBF.IsUseParameter(Condition);
            base.OnDrawNode(dt);
        }
    }
    [STNode("")]
    public class EventNode : STNode
    {
        private MSBF.Events _event;
        [STNodeProperty("Branch Condition", "The condition to check with.")]
        public MSBF.Events Event
        {
            get { return _event; }
            set
            {
                _event = value;
                EventEnumBox.Enum = value;
                Title = "Event: " + value.ToString();
                Invalidate();
            }
        }
        private int _param;
        [STNodeProperty("Event Parameter", "An additional parameter for the event.")]
        public int Parameter
        {
            get => _param;
            set
            {
                _param = value;
                ParameterTextLabel.Text = $"Parameter: {value}";
                Invalidate();
            }
        }

        private STNodeSelectEnumBox EventEnumBox;
        public STNodeLabel ParameterTextLabel;

        public EventNode()
        {
            Event = MSBF.Events.EventFuncAndChain;
            Parameter = 0;
        }
        public EventNode(MSBF.EventNode source)
        {
            Event = source.EventType;
            Parameter = source.Parameter;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.AutoSize = false;
            this.Size = new Size(240, 118);
            InputOptions.Add(NodeOptions.MakePrevNodeOption());
            InputOptions.Add(STNodeOption.Empty);
            InputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(STNodeOption.Empty);
            OutputOptions.Add(NodeOptions.MakeNextNodeOption());

            EventEnumBox = new STNodeSelectEnumBox();
            EventEnumBox.Enum = _event;
            EventEnumBox.ValueChanged += (s, e) =>
            {
                _event = (MSBF.Events)EventEnumBox.Enum;
                InvokeValueChanged();
            };
            EventEnumBox.DisplayRectangle = new Rectangle(10, 21, Width - 20, 18);
            Controls.Add(EventEnumBox);

            LetGetOptions = true;

            ParameterTextLabel = new STNodeLabel();
            ParameterTextLabel.BackColor = BackColor;
            ParameterTextLabel.ForeColor = ForeColor;
            ParameterTextLabel.DisplayRectangle = new Rectangle(10, 71, Width - 20, 18);
            Controls.Add(ParameterTextLabel);
        }

        protected override void OnDrawNode(DrawingTools dt)
        {
            ParameterTextLabel.Visable = MSBF.IsUseParameter(Event);
            base.OnDrawNode(dt);
        }
    }

    /// <summary>
    /// This node is purely for deciding that this path has ended. DOES NOT EXIST IN MSBF AND IS ONLY HERE FOR EDITOR PURPOSES
    /// </summary>
    [STNode("")]
    public class EndFlowNode : STNode
    {
        [STNodeProperty("", "")]
        public string _
        {
            get
            {
                return "0xFFFF";
            }
            set
            {
            }
        }
        public EndFlowNode()
        {
            Title = "End of Flow";
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            InputOptions.Add(NodeOptions.MakePrevNodeOption());
            OutputOptions.Add(STNodeOption.Empty);
            TitleColor = Color.FromArgb(255, Color.DarkRed);
            LetGetOptions = true;
        }
    }
}

public static class NodeOptions
{
    public const string NEXTNODE = "Next Node";
    public const string PREVNODE = "Previous Node";
    public const string TRUENODE = "Condition True Next Node";
    public const string FALSENODE = "Condition False Next Node";
    public static STNodeOption MakeNextNodeOption(string title = NEXTNODE) => new STNodeOption(title, typeof(int), true);
    public static STNodeOption MakePrevNodeOption() => new STNodeOption(PREVNODE, typeof(int), false);

    public static ConnectionStatus Connect(this STNode StartNode, string OutputName, STNode EndNode, string InputName)
    {
        STNodeOption[] Outputs = StartNode.GetOutputOptions();
        STNodeOption[] Inputs = EndNode.GetInputOptions();
        for (int i = 0; i < Outputs.Length; i++)
        {
            if (Outputs[i]?.Text?.Equals(OutputName) ?? false)
            {
                for (int j = 0; j < Inputs.Length; j++)
                {
                    if (Inputs[j]?.Text?.Equals(InputName) ?? false)
                    {
                        return Outputs[i].ConnectOption(Inputs[j]);
                    }
                }
            }
        }
        return ConnectionStatus.Reject;
    }

    public static bool IsConnected(this STNode Node, string ConnectionName, string TargetConnectionName) => GetConnectedNode(Node, ConnectionName, TargetConnectionName) != null;

    public static STNode? GetConnectedNode(this STNode Node, string ConnectionName, string TargetConnectionName)
    {
        STNodeOption[] Outputs = Node.GetOutputOptions();
        STNodeOption[] Inputs = Node.GetInputOptions();

        for (int i = 0; i < Outputs.Length; i++)
        {
            List<STNodeOption> ConnectedOptions = Outputs[i].GetConnectedOption();
            if (Outputs[i].Text?.Equals(ConnectionName) ?? false && ConnectedOptions.Count > 0)
            {
                for (int j = 0; j < ConnectedOptions.Count; j++)
                {
                    if (ConnectedOptions[j].Text.Equals(TargetConnectionName))
                    {
                        return ConnectedOptions[j].Owner;
                    }
                }
            }
        }
        return null;
    }
}