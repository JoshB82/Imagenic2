using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers;

internal struct Tile
{
    #region Fields and Properties

    internal int startX, startY, endX, endY;

    internal List<Triangle> triangles = new List<Triangle>();

    #endregion

    #region Constructors

    public Tile(int startX, int startY, int endX, int endY)
    {
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;
    }

    #endregion
}