using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Line : Mesh
{
    #region Fields and Properties
    
    private Vector3D startPoint, endPoint;
    
    public Vector3D StartPoint
    {
        get => startPoint;
        set
        {
            startPoint = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }
    public Vector3D EndPoint
    {
        get => endPoint;
        set
        {
            endPoint = value;
            Scaling = endPoint - startPoint;
        }
    }
    
    #endregion
    
    #region Constructors
    
    public Line(Vector3D startPoint, Vector3D endPoint) : base(startPoint, Orientation.OrientationZY, MeshStructure.lineStructure)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    #endregion
}