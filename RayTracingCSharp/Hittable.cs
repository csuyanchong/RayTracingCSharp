namespace RayTracingCSharp
{
    internal class HitRecord
    {
        public Vector3 point;
        public Vector3 normal;
        public float t;
        public bool frontFace;

        public HitRecord()
        {
            this.point = new Vector3();
            this.normal = new Vector3();
            this.t = 0f;
        }

        public HitRecord(Vector3 point, Vector3 normal, float t)
        {
            this.point = point;
            this.normal = normal;
            this.t = t;
        }

        public void SetFaceNormal(Ray ray, Vector3 outwardNormal)
        {
            frontFace = Vector3.DotProduct(ray.Direction, outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }

    internal abstract class Hittable
    {
        public abstract bool Hit(Ray ray, float tMin, float tMax, HitRecord rec);
    }
}
