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

    #region Rounding

    extension(float num)
    {
        internal int RoundToInt() => (int)(num >= 0 ? num + 0.5f : num - 0.5f);
        internal byte RoundToByte() => (byte)RoundToInt(num);
    }

    #endregion
}