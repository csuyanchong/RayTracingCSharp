namespace RayTracingCSharp
{
    internal class Vector3Util
    {
        public const float tolerance = 1e-6f;
        /// <summary>
        /// 镜面反射，通过入射光线和法线，得到反射光线。
        /// </summary>
        /// <param name="i">入射光线</param>
        /// <param name="n">法线</param>
        /// <returns>反射光线</returns>
        public static Vector3 Reflect(Vector3 i, Vector3 n)
        {
            return i - 2 * Vector3.DotProduct(i, n) * n;
        }

        public static Vector3 Refraction(Vector3 incident, Vector3 normal, float refRatio)
        {
            float cosTheta = MathF.Min(Vector3.DotProduct(-incident, normal), 1);
            Vector3 transPerp = refRatio * (incident + cosTheta * normal);
            float magTransPerp = transPerp.Magnitude();
            Vector3 transParallel = -MathF.Sqrt(MathF.Abs(1.0f - magTransPerp * magTransPerp)) * normal;
            return transPerp + transParallel;
        }


        public static Vector3 Refraction2(Vector3 incident, Vector3 normal, float n1, float n2)
        {
            float n = n1 / n2;
            float cosI = -Vector3.DotProduct(incident, normal);
            float sinT2 = n * n * (1 - cosI * cosI);
            if (sinT2 > 1)
            {
                return null;
            }
            float cosT = MathF.Sqrt(1 - sinT2);
            return n * incident + (n * cosI - cosT) * normal;
        }

        public static float Reflectance(float cosine, float refIndex)
        {
            // Use Schlick's approximation for reflectance.
            float r0 = (1.0f - refIndex) / (1.0f + refIndex);
            r0 *= r0;
            return r0 + (1 - r0) * MathF.Pow((1.0f - cosine), 5.0f);
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
