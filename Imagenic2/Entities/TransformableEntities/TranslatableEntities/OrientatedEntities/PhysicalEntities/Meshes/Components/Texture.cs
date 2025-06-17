using System.Drawing;

namespace Imagenic2.Core.Entities;

public sealed class Texture
{
    #region Fields and Properties

    public IEnumerable<Vector3D> Vertices { get; set; }
    public Color OutsideColour { get; set; } = Color.Black;

    #endregion

    #region Constructors

    public Texture()
    {
        
    }

    #endregion

    #region Methods

    #endregion
}
