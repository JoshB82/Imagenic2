using Imagenic2.Core.Entities;
using Imagenic2.Core.Entities.Animation;
using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Utilities;

public partial class Node
{
    #region Orientate

    /// <summary>
    /// Orientates this node and all child nodes to the specified <see cref="Orientation"/>.
    /// </summary>
    /// <param name="orientation">The new <see cref="Orientation"/>.</param>
    /// <returns>This node.</returns>
    public Node Orientate(Orientation orientation)
    {
        var descendants = GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            ((OrientatedEntity?)(node.Content))?.Orientate(orientation);
        }

        return this;
    }

    #endregion

    #region Look At

    /// <summary>
    /// Orientates this node and all child nodes to look at the specified <see cref="TranslatableEntity"/>.
    /// </summary>
    /// <param name="translatableEntity">The <see cref="TranslatableEntity"/> to look at.</param>
    /// <returns>This node.</returns>
    public Node LookAt([DisallowNull] TranslatableEntity translatableEntity)
    {
        var descendants = GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            ((OrientatedEntity?)(node.Content))?.LookAt(translatableEntity);
        }

        return this;
    }

    #endregion

    #region Camera Pan

    /// <summary>
    /// Pans this node and all child nodes forward by the specified distance.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    /// <returns>This node.</returns>
    public Node PanForward(float distance)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.PanForward(distance);
        }
        return this;
    }

    /// <summary>
    /// Pans this node and all child nodes backward by the specified distance.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    /// <returns>This node.</returns>
    public Node PanBackward(float distance)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.PanBackward(distance);
        }
        return this;
    }

    /// <summary>
    /// Pans this node and all child nodes left by the specified distance.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    /// <returns>This node.</returns>
    public Node PanLeft(float distance)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.PanLeft(distance);
        }
        return this;
    }

    /// <summary>
    /// Pans this node and all child nodes right by the specified distance.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    /// <returns>This node.</returns>
    public Node PanRight(float distance)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.PanRight(distance);
        }
        return this;
    }

    /// <summary>
    /// Pans this node and all child nodes up by the specified distance.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    /// <returns>This node.</returns>
    public Node PanUp(float distance)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.PanUp(distance);
        }
        return this;
    }

    /// <summary>
    /// Pans this node and all child nodes down by the specified distance.
    /// </summary>
    /// <param name="distance">Distance to pan by.</param>
    /// <returns>This node.</returns>
    public Node PanDown(float distance)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.PanDown(distance);
        }
        return this;
    }

    #endregion

    #region Camera Rotate, Roll

    /// <summary>
    /// Rotates this node and all child nodes up by the specified angle.
    /// </summary>
    /// <param name="angle">Angle to rotate by.</param>
    /// <returns>This node.</returns>
    public Node RotateUp(float angle)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.RotateUp(angle);
        }

        return this;
    }

    /// <summary>
    /// Rotates this node and all child nodes down by the specified angle.
    /// </summary>
    /// <param name="angle">Angle to rotate by.</param>
    /// <returns>This node.</returns>
    public Node RotateDown(float angle)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.RotateDown(angle);
        }

        return this;
    }

    /// <summary>
    /// Rotates this node and all child nodes left by the specified angle.
    /// </summary>
    /// <param name="angle">Angle to rotate by.</param>
    /// <returns>This node.</returns>
    public Node RotateLeft(float angle)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.RotateLeft(angle);
        }

        return this;
    }

    /// <summary>
    /// Rotates this node and all child nodes right by the specified angle.
    /// </summary>
    /// <param name="angle">Angle to rotate by.</param>
    /// <returns>This node.</returns>
    public Node RotateRight(float angle)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.RotateRight(angle);
        }

        return this;
    }

    /// <summary>
    /// Rolls this node and all child nodes left by the specified angle.
    /// </summary>
    /// <param name="angle">Angle to rotate by.</param>
    /// <returns>This node.</returns>
    public Node RollLeft(float angle)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.RollLeft(angle);
        }

        return this;
    }

    /// <summary>
    /// Rolls this node and all child nodes right by the specified angle.
    /// </summary>
    /// <param name="angle">Angle to rotate by.</param>
    /// <returns>This node.</returns>
    public Node RollRight(float angle)
    {
        var descendants = GetDescendantsAndThisOfType<Camera>();
        foreach (Node node in descendants)
        {
            ((Camera?)(node.Content))?.RollRight(angle);
        }

        return this;
    }

    #endregion

    #region Rotate

    /// <summary>
    /// Rotates this node and all child nodes by the specified <see cref="Quaternion"/>.
    /// </summary>
    /// <param name="q">The <see cref="Quaternion"/> to rotate by.</param>
    /// <returns>This node.</returns>
    public Node Rotate(Quaternion q)
    {
        var descendants = GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            ((OrientatedEntity?)(node.Content))?.Rotate(q);
        }

        return this;
    }

    /// <summary>
    /// Rotates this node and all child nodes around the specified axis and by the specified angle.
    /// </summary>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This node.</returns>
    public Node Rotate(Vector3D axis, float angle)
    {
        var descendants = GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            ((OrientatedEntity?)(node.Content))?.Rotate(axis, angle);
        }

        return this;
    }

    #endregion

    #region Scale

    /// <summary>
    /// Scales this node and all child nodes by the specified scale factor in the x-direction.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>This node.</returns>
    public Node ScaleX(float scaleFactor)
    {
        Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
        return Scale(scaling);
    }

    /// <summary>
    /// Scales this node and all child nodes by the specified scale factor in the y-direction.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>This node.</returns>
    public Node ScaleY(float scaleFactor)
    {
        Vector3D scaling = new Vector3D(1, scaleFactor, 1);
        return Scale(scaling);
    }

    /// <summary>
    /// Scales this node and all child nodes by the specified scale factor in the z-direction.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>This node.</returns>
    public Node ScaleZ(float scaleFactor)
    {
        Vector3D scaling = new Vector3D(1, 1, scaleFactor);
        return Scale(scaling);
    }

    /// <summary>
    /// Scales this node and all child nodes by the specified scale factors for the x, y and z-directions.
    /// </summary>
    /// <param name="scaleFactorX">The factor to scale by in the x-direction.</param>
    /// <param name="scaleFactorY">The factor to scale by in the y-direction.</param>
    /// <param name="scaleFactorZ">The factor to scale by in the z-direction.</param>
    /// <returns>This node.</returns>
    public Node Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ)
    {
        Vector3D scaling = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return Scale(scaling);
    }

    /// <summary>
    /// Scales this node and all child nodes by the specified scale factor.
    /// </summary>
    /// <param name="scaleFactor">A vector representing factors to scale by in the x, y and z-directions.</param>
    /// <returns>This node.</returns>
    public Node Scale(Vector3D scaleFactor)
    {
        var descendants = GetDescendantsAndThisOfType<PhysicalEntity>();
        foreach (Node node in descendants)
        {
            ((PhysicalEntity?)(node.Content))?.Scale(scaleFactor);
        }

        return this;
    }

    #endregion

    #region Start

    /// <summary>
    /// Starts creation of an <see cref="Animation"/>.
    /// </summary>
    /// <param name="time">The time when the <see cref="Animation"/> should start.</param>
    /// <returns>An animation context.</returns>
    public AnimationContextNode Start(float time = 0)
    {
        return new AnimationContextNode(this, time);
    }

    #endregion

    #region Transform

    /// <summary>
    /// Applies a transformation to this onde and all child nodes.
    /// </summary>
    /// <param name="transformation">The transformation to be applied.</param>
    /// <returns>This node.</returns>
    public Node Transform([DisallowNull] Action<TransformableEntity> transformation)
    {
        var descendants = GetDescendantsAndThisOfType<TransformableEntity>();
        foreach (Node node in descendants)
        {
            ((TransformableEntity?)(node.Content))?.Transform(transformation);
        }

        return this;
    }

    #endregion

    #region Translate

    /// <summary>
    /// Translates this node and all child nodes by the specified distance in the x-direction.
    /// </summary>
    /// <param name="distanceX">The distance to translate by.</param>
    /// <returns>This node.</returns>
    public Node TranslateX(float distance)
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return Translate(displacement);
    }

    /// <summary>
    /// Translates this node and all child nodes by the specified distance in the y-direction.
    /// </summary>
    /// <param name="distanceY">The distance to translate by.</param>
    /// <returns>This node.</returns>
    public Node TranslateY(float distance)
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return Translate(displacement);
    }

    /// <summary>
    /// Translates this node and all child nodes by the specified distance in the z-direction.
    /// </summary>
    /// <param name="distanceZ">The distance to translate by.</param>
    /// <returns>This node.</returns>
    public Node TranslateZ(float distance)
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return Translate(displacement);
    }

    /// <summary>
    /// Translates this node and all child nodes by the specified for the x, y and z-directions.
    /// </summary>
    /// <param name="distanceX">The distance to translate by in the x-direction.</param>
    /// <param name="distanceY">The distance to translate by in the y-direction.</param>
    /// <param name="distanceZ">The distance to translate by in the z-direction.</param>
    /// <returns>This node.</returns>
    public Node Translate(float distanceX, float distanceY, float distanceZ)
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return Translate(displacement);
    }

    /// <summary>
    /// Translates this node and all child nodes by the specified displacement.
    /// </summary>
    /// <param name="displacement">A vector representing distances to translate by in the x, y and z-directions.</param>
    /// <returns>This node.</returns>
    public Node Translate(Vector3D displacement)
    {
        var descendants = GetDescendantsAndThisOfType<TranslatableEntity>();
        foreach (Node node in descendants)
        {
            ((TranslatableEntity?)(node.Content))?.Translate(displacement);
        }

        return this;
    }

    #endregion
}