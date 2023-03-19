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

}
