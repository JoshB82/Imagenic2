using System.Drawing;
using static Imagenic2.Core.Maths.NumberHelpers;

namespace Imagenic2.Core.Maths;

public static class Extensions
{
    public static TComparable Clamp<TComparable>(this TComparable comparable, TComparable min, TComparable max) where TComparable : IComparable<TComparable>
    {
        if (comparable.CompareTo(min) < 0)
        {
            return min;
        }
        else if (comparable.CompareTo(max) > 0)
        {
            return max;
        }
        else
        {
            return comparable;
        }
    }

    public static Vector3D ToVector3D(this Color colour) => new Vector3D(colour.R, colour.G, colour.B);

    

    

    internal static void SortByY(
            ref int x1, ref int y1, ref float z1,
            ref int x2, ref int y2, ref float z2,
            ref int x3, ref int y3, ref float z3)
    {
        // y1 highest; y3 lowest (?)
        if (y1 < y2)
        {
            Swap(ref x1, ref x2);
            Swap(ref y1, ref y2);
            Swap(ref z1, ref z2);
        }
        if (y1 < y3)
        {
            Swap(ref x1, ref x3);
            Swap(ref y1, ref y3);
            Swap(ref z1, ref z3);
        }
        if (y2 < y3)
        {
            Swap(ref x2, ref x3);
            Swap(ref y2, ref y3);
            Swap(ref z2, ref z3);
        }
    }
}