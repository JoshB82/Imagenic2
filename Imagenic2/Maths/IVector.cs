namespace Imagenic2.Core.Maths;

public interface IVector<TSelf>
{
    static abstract float Dot(TSelf v1, TSelf v2);
    static abstract float Magnitude(TSelf v);
}