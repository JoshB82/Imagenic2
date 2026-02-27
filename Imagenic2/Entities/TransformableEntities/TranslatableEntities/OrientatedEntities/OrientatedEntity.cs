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
            //if (value == worldOrientation) return;
            worldOrientation = value;
            rotationQuaternion = Orientation.ExtractRotation(worldOrientation.DirectionForward, worldOrientation.DirectionUp, worldOrientation.DirectionRight);

            UpdateRotationMatrix();
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

    private void UpdateRotationMatrix()
    {
        //Matrix4x4 directionForwardRotation = Transform.RotateBetweenVectors(Orientation.ModelDirectionForward, worldOrientation.DirectionForward);
        //Matrix4x4 directionUpRotation = Transform.RotateBetweenVectors((Vector3D)(directionForwardRotation * Orientation.ModelDirectionUp), worldOrientation.DirectionUp);

        //rotationMatrix = directionUpRotation * directionForwardRotation;
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