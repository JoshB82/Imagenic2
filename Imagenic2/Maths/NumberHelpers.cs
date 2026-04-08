using System.Numerics;

namespace Imagenic2.Core.Maths;

internal static class NumberHelpers
{
    

    internal static void Swap<T>(ref T x1, ref T x2)
    {
        T temp = x1;
        x1 = x2;
        x2 = temp;
    }

    public static float DegToRad(float deg) => deg * MathF.PI / 180;
    public static float RadToDeg(float rad) => rad * 180 / MathF.PI;
}