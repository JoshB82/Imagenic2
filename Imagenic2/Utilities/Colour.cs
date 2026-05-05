namespace Imagenic2.Core.Utilities.Colour;

public readonly struct Colour
{
    #region Fields and Properties

    public readonly byte R;
    public readonly byte G;
    public readonly byte B;

    #endregion

    #region Constructors

    public Colour(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    #endregion

    #region Methods

    public Vector3D ToVector3D() => new Vector3D(R, G, B);

    #endregion

    #region Set Colours

    public static readonly Colour Red = new(255, 0, 0);
    public static readonly Colour Green = new(0, 255, 0);
    public static readonly Colour Blue = new(0, 0, 255);
    public static readonly Colour Black = new(0, 0, 0);
    public static readonly Colour White = new(255, 255, 255);

    #endregion
}