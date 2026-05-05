namespace Imagenic2.Core.Maths;

public static class ApproximateEqualityExtensions
{
    extension(float v1)
    {
        public bool ApproxEquals(float v2, float epsilon = epsilon) => Math.Abs(v1 - v2) <= epsilon;
        public bool ApproxLessThan(float v2, float epsilon = epsilon) => v1 < v2 && !ApproxEquals(v1, v2, epsilon);
        public bool ApproxMoreThan(float v2, float epsilon = epsilon) => v1 > v2 && !ApproxEquals(v1, v2, epsilon);
        public bool ApproxLessThanEquals(float v2, float epsilon = epsilon) => v1 < v2 || ApproxEquals(v1, v2, epsilon);
        public bool ApproxMoreThanEquals(float v2, float epsilon = epsilon) => v1 > v2 || ApproxEquals(v1, v2, epsilon);
    }
}