namespace RayTracingCSharp
{
    internal class HittableList : Hittable
    {
        public List<Hittable> objects;

        public HittableList()
        {
            objects = new List<Hittable>();
        }

        public void Add(Hittable obj)
        {
            objects.Add(obj);
        }

        public void Clear()
        {
            objects.Clear();
        }

        public override bool Hit(Ray ray, float tMin, float tMax, HitRecord rec)
        {
            bool hitAnyThing = false;
            float tClosest = tMax;

            int length = objects.Count;
            for (int i = 0; i < length; i++)
            {
                Hittable obj = objects[i];
                if (obj.Hit(ray, tMin, tClosest, rec))
                {
                    hitAnyThing = true;
                    tClosest = rec.t;
                }
            }
            return hitAnyThing;
        }
    }
}
