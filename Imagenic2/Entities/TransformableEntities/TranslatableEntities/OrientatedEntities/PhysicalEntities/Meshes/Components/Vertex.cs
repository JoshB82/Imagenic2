namespace Imagenic2.Core.Entities;

public struct Vertex
{
    #region Fields and Properties

    public Vector3D WorldOrigin { get; set; }

    private Vector3D? normal;
    public Vector3D? Normal
    {
        get => normal;
        set
        {
            if (value == normal) return;
            normal = value;
        }
    }

    public Vector2D? TextureCoordinates { get; set; }

    internal Vector4D transformedPosition;
    internal Vector4D viewSpacePosition;
    internal float invW;
    internal Vector2D? transformedTexturePosition;

    #endregion

    #region Constructors

    public Vertex(Vector3D worldOrigin)
    {
        WorldOrigin = worldOrigin;
    }

    public Vertex(Vector3D worldOrigin, Vector3D? normal = null, Vector2D? textureCoordinates = null)
    {
        WorldOrigin = worldOrigin;
        Normal = normal;
        TextureCoordinates = textureCoordinates;
    }

    #endregion

    #region Methods

    public readonly Vertex ShallowCopy() => (Vertex)MemberwiseClone();
    public Vertex DeepCopy()
    {
        Vertex newVertex = new Vertex()
        {
            WorldOrigin = this.WorldOrigin,
            Normal = this.normal,
            TextureCoordinates = this.TextureCoordinates,
            transformedPosition = this.transformedPosition,
            viewSpacePosition = this.viewSpacePosition,
            invW = this.invW,
            transformedTexturePosition = this.transformedTexturePosition
        };
        return newVertex;
    }

    public override string ToString() => $"WorldOrigin: {WorldOrigin}, Normal: {Normal}, Texture Co-ordinates: {TextureCoordinates}";

    #endregion
}