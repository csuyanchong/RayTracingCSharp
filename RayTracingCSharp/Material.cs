namespace RayTracingCSharp
{
    internal abstract class Material
    {
        public abstract bool Scatter(Ray rayIn, HitRecord rec, out Color attenuation, out Ray rayScattered);
    }

    internal class Lambertian : Material
    {
        public Color albedo;
        public Lambertian(Color albedo)
        {
            this.albedo = albedo;
        }

        public override bool Scatter(Ray rayIn, HitRecord rec, out Color attenuation, out Ray rayScattered)
        {
            var target = rec.point + rec.normal + MathUtil.RandomPointInUnitSphere();
            rayScattered = new Ray(rec.point, target - rec.point);

            //var scatterDir =  rec.normal + MathUtil.RandomPointInUnitSphere();
            //if (Vector3Util.NearlyEqual(scatterDir,Vector3.Zero()))
            //{
            //    scatterDir = rec.normal;
            //}
            //rayScattered = new Ray(rec.point, scatterDir);
            attenuation = albedo;
            return true;
        }
    }

    internal class Metal : Material
    {
        public Color albedo;
        /// <summary>
        /// 模糊参数。
        /// </summary>
        public float fuzz;
        public Metal(Color albedo, float fuzz)
        {
            this.albedo = albedo;
            this.fuzz = fuzz < 1 ? fuzz : 1;
        }

        public override bool Scatter(Ray rayIn, HitRecord rec, out Color attenuation, out Ray rayScattered)
        {
            Vector3 reflected = Vector3Util.Reflect(Vector3.Normalize(rayIn.Direction), rec.normal);
            rayScattered = new Ray(rec.point, reflected + fuzz * MathUtil.RandomPointInUnitSphere());
            attenuation = albedo;
            return (Vector3.DotProduct(rayScattered.Direction, rec.normal) > 0);
        }
    }

    internal class Dielectric : Material
    {
        /// <summary>
        /// 反射率
        /// </summary>
        public float refIndex;

        public Dielectric(float refIndex)
        {
            this.refIndex = refIndex;
        }

        public override bool Scatter(Ray rayIn, HitRecord rec, out Color attenuation, out Ray rayScattered)
        {
            attenuation = new Color(1.0f, 1.0f, 1.0f);
            float refIndRatio = rec.frontFace ? (1.0f / refIndex) : refIndex;

            Vector3 unitRayIn = Vector3.Normalize(rayIn.Direction);

            float cosTheta = MathF.Min(Vector3.DotProduct(-unitRayIn, rec.normal), 1);
            float sinTheta = MathF.Sqrt(1 - cosTheta * cosTheta);

            Vector3 scatterDir;
            if (refIndRatio * sinTheta > 1.0f || Vector3Util.Reflectance(cosTheta, refIndRatio) > MathUtil.RandomFloat())
            {
                // reflect
                scatterDir = Vector3Util.Reflect(unitRayIn, rec.normal);
            }
            else
            {
                // refraction
                scatterDir = Vector3Util.Refraction(unitRayIn, rec.normal, refIndRatio);
            }
            rayScattered = new Ray(rec.point, scatterDir);
            return true;
            //Vector3 incident = Vector3.Normalize(rayIn.Direction);
            //Vector3 normal = rec.normal;

            //Vector3 reflect = Vector3Util.Reflect(incident, normal);
            //Vector3 refract;

            //float reflectProb;
            //float cosine;
            //float refractionRatio;
            //Vector3 outwardNormal;

            //if (Vector3.DotProduct(incident, normal) > 0)
            //{
            //    outwardNormal = -rec.normal;
            //    refractionRatio = refIndex;
            //    cosine = refIndex * Vector3.DotProduct(incident, normal);
            //    refract = Vector3Util.Refraction(incident, outwardNormal, refIndex, 1.0f);
            //}
            //else
            //{
            //    outwardNormal = rec.normal;
            //    refractionRatio = 1.0f /refIndex;
            //    cosine = - refIndex * Vector3.DotProduct(incident, normal);
            //    refract = Vector3Util.Refraction(incident, outwardNormal, 1.0f, refIndex);
            //}

            //if (refract != null)
            //{
            //    reflectProb = Reflectance(cosine, refractionRatio);
            //}
            //else
            //{
            //    reflectProb = 1.0f;
            //    rayScattered = new Ray(rec.point, reflect);
            //}

            //if (MathUtil.RandomFloat() < reflectProb)
            //{
            //    rayScattered = new Ray(rec.point, reflect);
            //}
            //else
            //{
            //    rayScattered = new Ray(rec.point, refract);
            //}
            //return true;
            //if (rec.frontFace)
            //{
            //    refract = Vector3Util.Refraction(incident, normal, 1.0f, refIndex);

            //}
            //else
            //{
            //    refract = Vector3Util.Refraction(incident, normal, refIndex, 1.0f);
            //    refractionRatio = refIndex / 1.0f;
            //}

            //float cosine = MathF.Min(Vector3.DotProduct(-incident, normal), 1.0f);
            //if (refract == null || Reflectance(cosine, refractionRatio) > MathUtil.RandomFloat())
            //{
            //    // 全反射。
            //    rayScattered = new Ray(rec.point, reflect);
            //    return false;
            //}
            //rayScattered = new Ray(rec.point, refract);
            //return true;
        }
    }

}
