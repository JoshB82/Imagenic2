using System.Drawing;

namespace Imagenic2.Core.Entities;

public sealed class TextureStyle : FaceStyle, IDisposable
{
    #region Fields and Properties

    public IEnumerable<Vector3D> Vertices { get; set; }
    public Color OutsideColour { get; set; } = Color.Black;

    public Imagenic2.Core.Images.Image Image { get; set; }

    #endregion

    #region Constructors

    public TextureStyle(Imagenic2.Core.Images.Image image)
    {
        Image = image;
    }

    public void Dispose()
    {
        Image?.Dispose();
    }

    #endregion

    #region Methods

    #endregion
}
