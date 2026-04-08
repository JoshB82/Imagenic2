using System.Numerics;

namespace Imagenic2.Core.Maths.Transformations;

public static partial class MathsHelper
{
    #region Interpolation

    internal static T Lerp<T>(T v1, T v2, float d) where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, float, T>
    {
        return (v2 - v1) * d + v1;
    }

    internal static T Nlerp<T>(T v1, T v2, float d) where T : IVector<T>, IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, float, T>
    {
        return (v1 + (v2 - v1) * d) * (1 / T.Magnitude(v1 + (v2 - v1) * d));
    }

    internal static T Slerp<T>(T v1, T v2, float d) where T : IVector<T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, float, T>
    {
        float theta = Acos(T.Dot(v1, v2));
        float sinTheta = Sin(theta);
        return v1 * (Sin((1 - d) * theta) / sinTheta) + v2 * (Sin(d * theta) / sinTheta);
    }

    #endregion
}