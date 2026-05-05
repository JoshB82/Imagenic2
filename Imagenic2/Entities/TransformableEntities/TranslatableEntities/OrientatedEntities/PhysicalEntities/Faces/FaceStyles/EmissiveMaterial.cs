using System.Drawing;

namespace Imagenic2.Core.Entities;

public class EmissiveMaterial : Material
{
    #region Fields and Properties

    public Colour EmissionColour { get; set; }
    public float EmissionIntensity { get; set; }

    #endregion
}