using System.Numerics;

namespace Imagenic2.Core.Maths;

public interface IVector<TSelf>/* : IAdditionOperators<TSelf, TSelf, TSelf> where TSelf : IAdditionOperators<TSelf, TSelf, TSelf>*/
{
    static abstract float Dot(TSelf v1, TSelf v2);
    static abstract float Magnitude(TSelf v);
}