using Imagenic2.Core.Renderers;
using System.Drawing;

namespace Imagenic2.Core.Entities;

public abstract class Camera : RenderingEntity
{
    #region Fields and Parameters

    protected Buffer2D<Color> colourBuffer;
    protected Matrix4x4 ViewToWorld;
    protected Matrix4x4 ScreenToView;
    protected Matrix4x4 WindowToScreen;
    internal Matrix4x4 ScreenToWorld;

    // View Volume
    public override float ViewWidth
    {
        get => base.ViewWidth;
        set
        {
            base.ViewWidth = value;

            // Update view-to-screen matrix
            ScreenToView.m00 = ViewWidth / (2 * ZNear);

            NewRenderNeeded = true;
        }
    }
    public override float ViewHeight
    {
        get => base.ViewHeight;
        set
        {
            base.ViewHeight = value;

            // Update view-to-screen matrix
            ScreenToView.m11 = ViewHeight / (2 * ZNear);

            NewRenderNeeded = true;
        }
    }
    public override float ZNear
    {
        get => base.ZNear;
        set
        {
            base.ZNear = value;

            // Update view-to-screen matrix
            ScreenToView.m00 = ViewWidth / (2 * ZNear);
            ScreenToView.m11 = ViewHeight / (2 * ZNear);
            ScreenToView.m32 = (ZNear - ZFar) / (2 * ZNear * ZFar);
            ScreenToView.m33 = (ZNear + ZFar) / (2 * ZNear * ZFar);

            NewRenderNeeded = true;
        }
    }
    public override float ZFar
    {
        get => base.ZFar;
        set
        {
            base.ZFar = value;

            // Update view-to-screen matrix
            ScreenToView.m32 = (ZNear - ZFar) / (2 * ZNear * ZFar);
            ScreenToView.m33 = (ZNear + ZFar) / (2 * ZNear * ZFar);

            NewRenderNeeded = true;
        }
    }
    public override int RenderWidth
    {
        get => base.RenderWidth;
        set
        {
            base.RenderWidth = value;
            UpdateProperties();
        }
    }
    public override int RenderHeight
    {
        get => base.RenderHeight;
        set
        {
            base.RenderHeight = value;
            UpdateProperties();
        }
    }

    private Color renderBackgroundColour = Color.White;
    public Color RenderBackgroundColour
    {
        get => renderBackgroundColour;
        set
        {
            renderBackgroundColour = value;
            NewRenderNeeded = true;
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
        string[] iconObjData = Properties.Resources.Camera.Split(Environment.NewLine);
        Icon = new Custom(worldOrigin, directionForward, directionUp, iconObjData) { Dimension = 3 };
        Icon.ColourAllSolidFaces(Color.DarkCyan);
        Icon.Scale(5);
    }

    #endregion

    #region Methods

    internal override void CalculateModelToWorldMatrix()
    {
        base.CalculateModelToWorldMatrix();
        ViewToWorld = ModelToWorld;
    }

    internal override void UpdateProperties()
    {
        base.UpdateProperties();
        colourBuffer = new(RenderWidth, RenderHeight);
        WindowToScreen = ScreenToWindow.Inverse();

        NewRenderNeeded = true;
    }

    #endregion
}