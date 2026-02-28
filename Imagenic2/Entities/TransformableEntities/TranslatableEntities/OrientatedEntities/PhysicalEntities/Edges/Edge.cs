using System.Drawing;

namespace Imagenic2.Core.Entities;

public class Edge
{
    #region Fields and Properties

    public Vertex Vertex1 { get; set; }
    public Vertex Vertex2 { get; set; }

    internal Vector4D TransformedP1 { get; set; }
    internal Vector4D TransformedP2 { get; set; }

    public EdgeStyle EdgeStyle { get; set; } = new SolidEdgeStyle(Color.Red);

    #endregion

    #region Constructors

    public Edge(Vertex v1, Vertex v2)
    {
        Vertex1 = v1;
        Vertex2 = v2;
    }

    #endregion

    #region Methods

    #endregion
}