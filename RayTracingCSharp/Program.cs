namespace RayTracingCSharp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // image setting
            const int imgWidth = 800;
            const int imgHeight = 400;
            // 单个像素多重采样数
            const int samplePerPix = 20;
            // 最大反射次数
            const int maxDepth = 50;

            Console.WriteLine("P3");
            Console.WriteLine(imgWidth + " " + imgHeight);
            Console.WriteLine(255);

            // scene part
            HittableList world = new();
            // sphere
            Vector3 sphereCenter = new(0, 0, -1);
            float sphereRadius = 0.5f;
            Hittable sphere = new Sphere(sphereCenter, sphereRadius);
            world.Add(sphere);

            // plane
            Vector3 planeCenter = new(0, -100.5f, -1);
            float planeRadius = 100f;
            Hittable plane = new Sphere(planeCenter, planeRadius);
            world.Add(plane);

            // render part
            // camera
            Camera cam = new();
            Ray rayCam;

            //Vector3 pt;
            //Vector3 color;
            for (int j = imgHeight - 1; j >= 0; j--)
            {
                for (int i = 0; i < imgWidth; i++)
                {
                    // 每个像素多次采样，取平均值。
                    Vector3 color = new(0, 0, 0);
                    for (int k = 0; k < samplePerPix; k++)
                    {
                        Random random = new();
                        float rd = random.NextSingle();
                        float u = (i + rd) / imgWidth;
                        float v = (j + rd) / imgHeight;
                        rayCam = cam.GetRay(u, v);
                        color += Color(rayCam, world, maxDepth);
                    }
                    color /= samplePerPix;
                    color = new Vector3(MathF.Sqrt(color.X), MathF.Sqrt(color.Y), MathF.Sqrt(color.Z));

                    int r = (int)(color.X * 255.99f);
                    int g = (int)(color.Y * 255.99f);
                    int b = (int)(color.Z * 255.99f);

                    Console.Write(r + " " + g + " " + b + "\n");
                    //pt = new Vector3(i * 255.0f / imgWidth, 0, j * 255.0f / imgHeight);
                    //pt = new Vector3(0, 0, 255.0f * j / imgHeight);
                    //Console.Write(pt.ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        public static Vector3 Color(Ray ray, Hittable world, int depth)
        {
            HitRecord rec = new();
            if (depth <= 0)
            {
                return new Vector3(0, 0, 0);
            }

            if (world.Hit(ray, 0.001f, float.PositiveInfinity, rec))
            {
                // 递归返回碰撞物体的颜色，每次反射减少50%的能量。
                var target = rec.point + rec.normal + RandomPointInUnitSphere();
                return 0.5f *Color(new Ray(rec.point, target - rec.point), world, depth - 1);
            }
            else
            {
                // 返回蓝色渐变背景。
                Vector3 unitDir = Vector3.Normalize(ray.Direction);
                float t = 0.5f * (unitDir.Y + 1.0f);
                return (1.0f - t) * Vector3.One() + t * new Vector3(0.5f, 0.7f, 1.0f);
            }

        }

        public static float HitSphere(Vector3 center, float radius, Ray ray)
        {
            Vector3 origin = ray.Origin;
            Vector3 dir = ray.Direction;
            Vector3 AC = origin - center;

            float a = Vector3.DotProduct(dir, dir);
            float b = 2 * Vector3.DotProduct(dir, AC);
            float c = Vector3.DotProduct(AC, AC) - radius * radius;

            float discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                return -1;
            }
            return (-b - MathF.Sqrt(discriminant)) / (2 * a);
        }

        /// <summary>
        /// 返回单位球内的随机点：-1<x<1, -1<y<1, -1<z<1。
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomPointInUnitSphere()
        {
            Vector3 res;
            Random rd =  new ();
            do
            {
                res = 2 * new Vector3(rd.NextSingle(), rd.NextSingle(), rd.NextSingle()) - Vector3.One();
            } while (res.Magnitude() >= 1.0);
            return res;
        }
    }
}