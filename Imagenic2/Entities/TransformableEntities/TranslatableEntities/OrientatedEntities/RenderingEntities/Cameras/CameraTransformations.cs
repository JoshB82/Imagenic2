using Imagenic2.Core.Enums;
using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities;

public abstract partial class Camera
{
    /// <summary>
    /// Pans the camera in the forward direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanForward(float distance)
    {
        this.Translate(WorldOrientation.DirectionForward * distance);
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    /// <summary>
    /// Pans the camera in the left direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanLeft(float distance)
    {
        this.Translate(WorldOrientation.DirectionRight * -distance);
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    /// <summary>
    /// Pans the camera in the right direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanRight(float distance)
    {
        this.Translate(WorldOrientation.DirectionRight * distance);
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    /// <summary>
    /// Pans the camera in the backward direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanBackward(float distance)
    {
        this.Translate(WorldOrientation.DirectionForward * -distance);
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    /// <summary>
    /// Pans the camera in the up direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanUp(float distance)
    {
        this.Translate(WorldOrientation.DirectionUp * distance);
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    /// <summary>
    /// Pans the camera in the down direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanDown(float distance)
    {
        this.Translate(WorldOrientation.DirectionUp * -distance);
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }

    public void RotateUp(float angle)
    {
        float semiAngle = angle / 2;
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionRight, WorldOrientation.rotation);
        Quaternion rotation = new Quaternion(Cos(semiAngle), axis * Sin(-semiAngle));
        WorldOrientation = new Orientation((rotation * WorldOrientation.rotation).Normalise());

        //Matrix4x4 transformationUp = Transform.Rotate(WorldOrientation.DirectionRight, -angle);
        //WorldOrientation = Orientation.CreateOrientationForwardUp((Vector3D)(transformationUp * WorldOrientation.DirectionForward), (Vector3D)(transformationUp * WorldOrientation.DirectionUp));
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    public void RotateLeft(float angle)
    {
        float semiAngle = angle / 2;
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, WorldOrientation.rotation);
        Quaternion rotation = new Quaternion(Cos(semiAngle), axis * Sin(-semiAngle));
        WorldOrientation = new Orientation((rotation * WorldOrientation.rotation).Normalise());

        //Matrix4x4 transformationLeft = Transform.Rotate(WorldOrientation.DirectionUp, -angle);
        //WorldOrientation = Orientation.CreateOrientationRightForward((Vector3D)(transformationLeft * WorldOrientation.DirectionRight), (Vector3D)(transformationLeft * WorldOrientation.DirectionForward));
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    public void RotateRight(float angle)
    {
        float semiAngle = angle / 2;
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, WorldOrientation.rotation);
        Quaternion rotation = new Quaternion(Cos(semiAngle), axis * Sin(semiAngle));
        WorldOrientation = new Orientation((rotation * WorldOrientation.rotation).Normalise());

        //Matrix4x4 transformationRight = Transform.Rotate(WorldOrientation.DirectionUp, angle);
        //WorldOrientation = Orientation.CreateOrientationRightForward((Vector3D)(transformationRight * WorldOrientation.DirectionRight), (Vector3D)(transformationRight * WorldOrientation.DirectionForward));
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    public void RotateDown(float angle)
    {
        float semiAngle = angle / 2;
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionRight, WorldOrientation.rotation);
        Quaternion rotation = new Quaternion(Cos(semiAngle), axis * Sin(semiAngle));
        WorldOrientation = new Orientation((rotation * WorldOrientation.rotation).Normalise());

        //Matrix4x4 transformationDown = Transform.Rotate(WorldOrientation.DirectionRight, angle);
        //WorldOrientation = Orientation.CreateOrientationForwardUp((Vector3D)(transformationDown * WorldOrientation.DirectionForward), (Vector3D)(transformationDown * WorldOrientation.DirectionUp));
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    public void RollLeft(float angle)
    {
        float semiAngle = angle / 2;
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, WorldOrientation.rotation);
        Quaternion rotation = new Quaternion(Cos(semiAngle), axis * Sin(semiAngle));
        WorldOrientation = new Orientation((rotation * WorldOrientation.rotation).Normalise());

        //Matrix4x4 transformationRollLeft = Transform.Rotate(WorldOrientation.DirectionForward, angle);
        //WorldOrientation = Orientation.CreateOrientationUpRight((Vector3D)(transformationRollLeft * WorldOrientation.DirectionUp), (Vector3D)(transformationRollLeft * WorldOrientation.DirectionRight));
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
    public void RollRight(float angle)
    {
        float semiAngle = angle / 2;
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, WorldOrientation.rotation);
        Quaternion rotation = new Quaternion(Cos(semiAngle), axis * Sin(-semiAngle));
        WorldOrientation = new Orientation((rotation * WorldOrientation.rotation).Normalise());

        //Matrix4x4 transformationRollRight = Transform.Rotate(WorldOrientation.DirectionForward, -angle);
        //WorldOrientation = Orientation.CreateOrientationUpRight((Vector3D)(transformationRollRight * WorldOrientation.DirectionUp), (Vector3D)(transformationRollRight * WorldOrientation.DirectionRight));
        InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
    }
}