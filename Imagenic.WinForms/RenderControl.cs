namespace Imagenic2.WinForms;

public class RenderControl : Control
{
    #region Fields and Properties

    public Bitmap renderBuffer;

    #endregion

    #region Constructors

    public RenderControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer, true);
    }

    #endregion

    #region Methods

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.DrawImage(renderBuffer, 0, 0);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {

    }

    #endregion
}