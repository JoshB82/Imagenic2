namespace Imagenic2.Core.Entities;

public sealed class Plane : Mesh
{
        #region Fields and Properties
    
        private float length, width;
    
        /// <summary>
        /// The length of the <see cref="Plane"/>.
        /// </summary>
        public float Length
        {
            get => length;
            set
            {
                length = value;
                Scaling = new Vector3D(length, width, 1);
            }
        }
        /// <summary>
        /// The width of the <see cref="Plane"/>.
        /// </summary>
        public float Width
        {
            get => width;
            set
            {
                width = value;
                Scaling = new Vector3D(length, width, 1);
            }
        }
    
        #endregion
    
        #region Constructors
    
        public Plane(Vector3D worldOrigin, Orientation worldOrientation, float length, float width) : base(worldOrigin, worldOrientation, MeshStructure.planeStructure)
        {
            Length = length;
            Width = width;
        }

    #endregion
}