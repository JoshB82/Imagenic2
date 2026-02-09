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

    public static bool ApproxEquals(this float v1, float v2, float epsilon = float.Epsilon) => Math.Abs(v1 - v2) <= epsilon;
    public static bool ApproxLessThanEquals(this float v1, float v2, float epsilon = float.Epsilon) => v1 <= v2 + epsilon;
    public static bool ApproxMoreThanEquals(this float v1, float v2, float epsilon = float.Epsilon) => v1 >= v2 - epsilon;
    public static bool ApproxLessThan(this float v1, float v2, float epsilon = float.Epsilon) => v1 < v2 + epsilon;
    public static bool ApproxMoreThan(this float v1, float v2, float epsilon = float.Epsilon) => v1 > v2 - epsilon;

    internal static int RoundToInt(this float num) => (int)(num >= 0 ? num + 0.5f : num - 0.5f);

    internal static void Swap<T>(ref T x1, ref T x2)
    {
        T temp = x1;
        x1 = x2;
        x2 = temp;
    }

}