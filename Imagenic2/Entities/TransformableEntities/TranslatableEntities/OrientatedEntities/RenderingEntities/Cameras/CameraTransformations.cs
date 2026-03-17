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
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the left direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanLeft(float distance)
    {
        this.Translate(WorldOrientation.DirectionRight * -distance);
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the right direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanRight(float distance)
    {
        this.Translate(WorldOrientation.DirectionRight * distance);
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the backward direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanBackward(float distance)
    {
        this.Translate(WorldOrientation.DirectionForward * -distance);
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the up direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanUp(float distance)
    {
        this.Translate(WorldOrientation.DirectionUp * distance);
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the down direction.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    public void PanDown(float distance)
    {
        this.Translate(WorldOrientation.DirectionUp * -distance);
        InvokeRenderEvent(RenderUpdate.NewRender);
    }

    public void RotateUp(float angle)
    {
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionRight, rotationQuaternion);
        Quaternion rotation = Transform.QuaternionRotate(axis, -angle);
        Vector3D directionForward = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotation * rotationQuaternion);
        Vector3D directionUp = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotation * rotationQuaternion);
        rotationQuaternion = rotation*rotationQuaternion;
        WorldOrientation = Orientation.CreateOrientationForwardUp(directionForward, directionUp);

        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    public void RotateDown(float angle)
    {
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionRight, rotationQuaternion);
        Quaternion rotation = Transform.QuaternionRotate(axis, angle);
        Vector3D directionForward = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotation * rotationQuaternion);
        Vector3D directionUp = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotation * rotationQuaternion);
        rotationQuaternion = rotation * rotationQuaternion;
        WorldOrientation = Orientation.CreateOrientationForwardUp(directionForward, directionUp);
        
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    public void RotateLeft(float angle)
    {
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotationQuaternion);
        Quaternion rotation = Transform.QuaternionRotate(axis, -angle);
        Vector3D directionForward = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotation * rotationQuaternion);
        Vector3D directionUp = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotation * rotationQuaternion);
        rotationQuaternion = rotation * rotationQuaternion;
        WorldOrientation = Orientation.CreateOrientationForwardUp(directionForward, directionUp);
        
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    public void RotateRight(float angle)
    {
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotationQuaternion);
        Quaternion rotation = Transform.QuaternionRotate(axis, angle);
        Vector3D directionForward = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotation * rotationQuaternion);
        Vector3D directionUp = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotation * rotationQuaternion);
        rotationQuaternion = rotation * rotationQuaternion;
        WorldOrientation = Orientation.CreateOrientationForwardUp(directionForward, directionUp);
        
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    public void RollLeft(float angle)
    {
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotationQuaternion);
        Quaternion rotation = Transform.QuaternionRotate(axis, angle);
        Vector3D directionForward = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotation * rotationQuaternion);
        Vector3D directionUp = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotation * rotationQuaternion);
        rotationQuaternion = rotation * rotationQuaternion;
        WorldOrientation = Orientation.CreateOrientationForwardUp(directionForward, directionUp);
        
        InvokeRenderEvent(RenderUpdate.NewRender);
    }
    public void RollRight(float angle)
    {
        Vector3D axis = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotationQuaternion);
        Quaternion rotation = Transform.QuaternionRotate(axis, -angle);
        Vector3D directionForward = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionForward, rotation * rotationQuaternion);
        Vector3D directionUp = Transform.RotateVectorUsingQuaternion(Orientation.ModelDirectionUp, rotation * rotationQuaternion);
        rotationQuaternion = rotation * rotationQuaternion;
        WorldOrientation = Orientation.CreateOrientationForwardUp(directionForward, directionUp);

        InvokeRenderEvent(RenderUpdate.NewRender);
    }
}