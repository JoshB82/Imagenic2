using Imagenic2.Core.Enums;
using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities;

public abstract class OrientatedEntity : TranslatableEntity
{
    #region Fields and Properties

    private Matrix4x4 rotationMatrix = Matrix4x4.Identity;
    protected Quaternion rotationQuaternion = Quaternion.Identity;

    private Orientation worldOrientation = Orientation.ModelOrientation;
    public virtual Orientation WorldOrientation
    {
        get => worldOrientation;
        set
        {
            if (value == worldOrientation) return;
            worldOrientation = value;
            rotationQuaternion = Orientation.ExtractRotation(worldOrientation.DirectionForward, worldOrientation.DirectionUp, worldOrientation.DirectionRight);

            UpdateRotationMatrix();
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    protected OrientatedEntity(Vector3D worldOrigin, Orientation worldOrientation) : base(worldOrigin)
    {
        WorldOrientation = worldOrientation;
    }

    #endregion

    #region Methods

    public override OrientatedEntity ShallowCopy() => (OrientatedEntity)MemberwiseClone();
    public override TranslatableEntity DeepCopy()
    {
        var orientatedEntity = base.DeepCopy();
        return orientatedEntity;
    }

    private void UpdateRotationMatrix()
    {
        rotationMatrix = Transform.QuaternionToMatrix(rotationQuaternion);

        UpdateModelToWorldMatrix();
    }

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();
        ModelToWorld *= rotationMatrix;
    }

    #endregion
}