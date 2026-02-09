namespace Imagenic2.Core.Entities;

public sealed class Cone : Mesh
{
        #region Fields and Properties
    
        private float radius, height;
    
        public float Radius
        {
            get => radius;
            set
            {
                radius = value;
                Scaling = new Vector3D(radius, height, radius);
            }
        }
        public float Height
        {
            get => height;
            set
            {
                height = value;
                Scaling = new Vector3D(radius, height, radius);
            }
        }
    
        #endregion
    
        #region Constructors
    
        public Cone(Vector3D worldOrigin, Orientation worldOrientation, float height, float radius, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateConeStructure(resolution))
        {
            Radius = radius;
            Height = height;
        }
    
        #endregion
    
        #region Methods
    
        public override Cone ShallowCopy() => (Cone)MemberwiseClone();
        public override Cone DeepCopy()
        {
            var cone = (Cone)base.DeepCopy();
            cone.radius = radius;
            cone.height = height;
            return cone;
        }

        #endregion
}