using Imagenic2.Core.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Utilities;

public partial class Node
{
    #region Translate

    public Node TranslateX(float distanceX)
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return Translate(displacement);
    }

    public Node TranslateY(float distanceY)
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return Translate(displacement);
    }

    public Node TranslateZ(float distanceZ)
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return Translate(displacement);
    }

    public Node Translate(float distanceX, float distanceY, float distanceZ)
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return Translate(displacement);
    }

    public Node Translate(Vector3D displacement)
    {
        var descendants = GetDescendantsAndThis(n => n.Content is TranslatableEntity);
        foreach (Node node in descendants)
        {
            ((TranslatableEntity)(node.Content)).Translate(displacement);
        }

        return this;
    }

    #endregion

    #region Orientate

    public Node Orientate(Orientation orientation)
    {
        var descendants = GetDescendantsAndThis(n => n.Content is OrientatedEntity);
        foreach (Node node in descendants)
        {
            ((OrientatedEntity)(node.Content)).Orientate(orientation);
        }

        return this;
    }

    #endregion

    #region Rotate

    public Node Rotate(Vector3D axis, float angle)
    {
        var descendants = GetDescendantsAndThis(n => n.Content is OrientatedEntity);
        foreach (Node node in descendants)
        {
            ((OrientatedEntity)(node.Content)).Rotate(axis, angle);
        }

        return this;
    }

    #endregion

    #region Scale

    public Node ScaleX(float scaleFactor)
    {
        Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
        return Scale(scaling);
    }

    public Node ScaleY(float scaleFactor)
    {
        Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
        return Scale(scaling);
    }

    public Node ScaleZ(float scaleFactor)
    {
        Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
        return Scale(scaling);
    }

    public Node Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ)
    {
        Vector3D scaling = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return Scale(scaling);
    }

    public Node Scale(Vector3D scaleFactor)
    {
        var descendants = GetDescendantsAndThis(n => n.Content is PhysicalEntity);
        foreach (Node node in descendants)
        {
            ((PhysicalEntity)(node.Content)).Scale(scaleFactor);
        }

        return this;
    }

    #endregion

    #region Transform

    public Node Transform([DisallowNull] Action<TransformableEntity> transformation)
    {
        var descendants = GetDescendantsAndThis(n => n.Content is TransformableEntity);
        foreach (Node node in descendants)
        {
            ((TransformableEntity)(node.Content)).Transform(transformation);
        }

        return this;
    }

    #endregion
}