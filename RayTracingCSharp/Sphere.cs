namespace RayTracingCSharp
{
    internal class Sphere : Hittable
    {
        public Vector3 Center { get; set; }
        public float Radius { get; set; }
        public Material material;
        public Sphere(Vector3 center, float r, Material material)
        {
            this.Center = center;
            this.Radius = r;
            this.material = material;
        }

        public override bool Hit(Ray ray, float tMin, float tMax, HitRecord rec)
        {
            Vector3 origin = ray.Origin;
            Vector3 dir = ray.Direction;
            Vector3 AC = origin - Center;

            float a = Vector3.DotProduct(dir, dir);
            float b = 2 * Vector3.DotProduct(dir, AC);
            float c = Vector3.DotProduct(AC, AC) - Radius * Radius;

            float discriminant = b * b - 4 * a * c;
            if (discriminant > 0)
            {
                float temp = (-b - MathF.Sqrt(discriminant)) / (2 * a);
                if (temp < tMax && temp > tMin)
                {
                    rec.t = temp;
                    rec.point = ray.Point_at_param(temp);
                    Vector3 outwardNormal = (rec.point - Center) / Radius;
                    rec.SetFaceNormal(ray, outwardNormal);
                    rec.mat = material;
                    return true;
                }

                temp = (-b + MathF.Sqrt(discriminant)) / (2 * a);
                if (temp < tMax && temp > tMin)
                {
                    rec.t = temp;
                    rec.point = ray.Point_at_param(temp);
                    Vector3 outwardNormal = (rec.point - Center) / Radius;
                    rec.SetFaceNormal(ray, outwardNormal);
                    rec.mat = material;
                    return true;
                }
            }
            return false;
        }
    }
}
