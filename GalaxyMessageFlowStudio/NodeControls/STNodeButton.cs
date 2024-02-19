using ST.Library.UI.NodeEditor;

namespace GalaxyMessageFlowStudio.NodeControls;

public class STNodeButton : STNodeControl
{
    public Color HoverColor { get; set; }
    public Color ClickColor { get; set; }
    private bool m_b_enter;
    private bool m_b_down;

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        m_b_enter = true;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        m_b_enter = false;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        m_b_down = true;
        Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        m_b_down = false;
        Invalidate();
    }

    protected override void OnPaint(DrawingTools dt)
    {
        //base.OnPaint(dt);
        Graphics g = dt.Graphics;
        SolidBrush brush = dt.SolidBrush;
        brush.Color = BackColor;
        if (m_b_down)
            brush.Color = ClickColor;
        else if (m_b_enter)
            brush.Color = HoverColor;
        g.FillRectangle(brush, 0, 0, Width, Height);
        g.DrawString(Text, Font, Brushes.White, ClientRectangle, m_sf);
    }
}