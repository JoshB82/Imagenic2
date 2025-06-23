using System;

namespace Imagenic2.Core.Maths;

public interface IApproximatelyEquatable<T> : IEquatable<T>
{
    public bool ApproxEquals(T? other, float epsilon);
}