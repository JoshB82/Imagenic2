namespace Imagenic2.Core.Entities;

public abstract partial class Camera : RenderingEntity
{
    #region Fields and Parameters

    // Buffer
    //protected Buffer2D<Color> colourBuffer;
    
    // Matrices
    protected Matrix4x4 viewToWorld;
    protected Matrix4x4 screenToView;
    internal Matrix4x4 screenToWorld;

    // View volume
    public override float ViewWidth
    {
        get => base.ViewWidth;
        set
        {
            base.ViewWidth = value;

            // Update view-to-screen matrix
            screenToView.m00 = ViewWidth / (2 * ZNear);
        }
    }
    public override float ViewHeight
    {
        get => base.ViewHeight;
        set
        {
            base.ViewHeight = value;

            // Update view-to-screen matrix
            screenToView.m11 = ViewHeight / (2 * ZNear);
        }
    }
    public override float ZNear
    {
        get => base.ZNear;
        set
        {
            base.ZNear = value;

            // Update view-to-screen matrix
            screenToView.m00 = ViewWidth / (2 * ZNear);
            screenToView.m11 = ViewHeight / (2 * ZNear);
            screenToView.m32 = (ZNear - ZFar) / (2 * ZNear * ZFar);
            screenToView.m33 = (ZNear + ZFar) / (2 * ZNear * ZFar);
        }
    }
    public override float ZFar
    {
        get => base.ZFar;
        set
        {
            base.ZFar = value;

            // Update view-to-screen matrix
            screenToView.m32 = (ZNear - ZFar) / (2 * ZNear * ZFar);
            screenToView.m33 = (ZNear + ZFar) / (2 * ZNear * ZFar);
        }
    }

    #endregion

    #region Constructors

    public Camera(Vector3D worldOrigin,
                  Orientation worldOrientation,
                  float viewWidth,
                  float viewHeight,
                  float zNear,
                  float zFar)
        : base(worldOrigin, worldOrientation, viewWidth, viewHeight, zNear, zFar)
    {
        //string[] iconObjData = Properties.Resources.Camera.Split(Environment.NewLine);
        //Icon = new Custom(worldOrigin, directionForward, directionUp, iconObjData) { Dimension = 3 };
        //Icon.ColourAllSolidFaces(Color.DarkCyan);
        //Icon.Scale(5);
    }

    #endregion

    #region Methods

    public override Camera ShallowCopy() => (Camera)MemberwiseClone();
    public override Camera DeepCopy()
    {
        var camera = (Camera)base.DeepCopy();
        return camera;
    }

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();
        viewToWorld = ModelToWorld;
    }

    /*
    internal override void UpdateProperties()
    {
        base.UpdateProperties();
        colourBuffer = new(RenderWidth, RenderHeight);
        WindowToScreen = ScreenToWindow.Inverse();

        NewRenderNeeded = true;
    }
    */

    #endregion
}