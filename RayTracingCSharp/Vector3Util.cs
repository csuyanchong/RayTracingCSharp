using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCSharp
{
    internal class Vector3Util
    {
        public const float tolerance = 1e-6f;
        /// <summary>
        /// 镜面反射，通过入射光线和法线，得到反射光线。
        /// </summary>
        /// <param name="v">入射光线</param>
        /// <param name="n">法线</param>
        /// <returns>反射光线</returns>
        public static Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.DotProduct(v, n) * n;
        }

        public static bool NearlyEqual(Vector3 v1, Vector3 v2, float tolerance = tolerance)
        {
            if (MathF.Abs(v1.X - v2.X) < tolerance && MathF.Abs(v1.Y - v2.Y) < tolerance && MathF.Abs(v1.Z - v2.Z) < tolerance)
            {
                return true;
            }
            return false;
        }
    }
}
