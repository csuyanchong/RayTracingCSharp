namespace RayTracingCSharp
{
    internal class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        // constructor
        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // operator overloading
        public static Vector3 operator -(Vector3 vec)
        {
            vec.X = -vec.X;
            vec.Y = -vec.Y;
            vec.Z = -vec.Z;
            return vec;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(float t, Vector3 a)
        {
            return new Vector3(t * a.X, t * a.Y, t * a.Z);
        }

        public static Vector3 operator *(Vector3 a, float t)
        {
            return new Vector3(t * a.X, t * a.Y, t * a.Z);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector3 operator /(Vector3 a, float t)
        {
            return new Vector3(a.X / t, a.Y / t, a.Z / t);
        }

        // method
        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3 Zero()
        {
            return new Vector3(1, 1, 1);
        }

        public static Vector3 One()
        {
            return new Vector3(1, 1, 1);
        }

        // util function
        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Vector3 Normalize(Vector3 vec)
        {
            return vec / vec.Magnitude();
        }

        public override string ToString()
        {
            return this.X.ToString() + ' ' + this.Y.ToString() + ' ' + this.Z.ToString();
        }

        /// <summary>
        /// 向量的点积。
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static float DotProduct(Vector3 vec1, Vector3 vec2)
        {
            float dot = vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z;
            return dot;
        }

        /// <summary>
        /// 向量的叉积。
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static Vector3 CrossProduct(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.Y * vec2.Z - vec1.Z * vec2.Y,
                vec1.Z * vec2.X - vec1.X * vec2.Z,
                vec1.X * vec2.Y - vec2.X * vec1.Y
                );
        }

        /// <summary>
        /// 返回两个向量的距离。
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return (v1 - v2).Magnitude();
        }

        /// <summary>
        /// 随机向量，x, y, z均在[0, 1)区间。
        /// </summary>
        /// <returns></returns>
        public static Vector3 Random()
        {
            return new Vector3(MathUtil.RandomFloat(), MathUtil.RandomFloat(), MathUtil.RandomFloat());
        }

        /// <summary>
        /// 随机向量，x, y, z均在[min, max)区间。
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 RandomRange(float min, float max)
        {
            return new Vector3(MathUtil.RandomFloatRange(min, max), MathUtil.RandomFloatRange(min, max), MathUtil.RandomFloatRange(min, max));
        }
    }
}
