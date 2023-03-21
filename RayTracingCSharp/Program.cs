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
            const int samplePerPix = 50;
            // 最大反射次数
            const int maxDepth = 50;

            Console.WriteLine("P3");
            Console.WriteLine(imgWidth + " " + imgHeight);
            Console.WriteLine(255);

            // scene part
            HittableList world = new();

            //float radius = MathF.Cos(MathF.PI / 4);
            //Material matLeft = new Lambertian(new(0, 0, 1f));
            //Material matRight = new Lambertian(new(1f, 0, 0));
            //world.Add(new Sphere(new(-radius, 0.0f, -1f), radius, matLeft));
            //world.Add(new Sphere(new(radius, 0.0f, -1f), radius, matRight));

            Material matGround = new Lambertian(new(0.8f, 0.8f, 0.0f));
            Material matCenter = new Lambertian(new(0.1f, 0.2f, 0.5f));
            Material matLeft = new Dielectric(1.5f);
            Material matRight = new Metal(new(0.8f, 0.6f, 0.2f), 0.0f);

            world.Add(new Sphere(new(0.0f, -100.5f, -1f), 100f, matGround));
            world.Add(new Sphere(new(0.0f, 0.0f, -1f), 0.5f, matCenter));
            world.Add(new Sphere(new(-1f, 0.0f, -1f), 0.5f, matLeft));
            world.Add(new Sphere(new(-1f, 0.0f, -1f), -0.45f, matLeft));
            world.Add(new Sphere(new(1f, 0.0f, -1f), 0.5f, matRight));

            // render part
            // camera
            Vector3 lookFrom = new(3f, 3f, 2f);
            Vector3 lookAt = new(0, 0, -1f);
            Vector3 up = new(0, 1, 0);
            float fov = 20f;
            float aspectRatio = (float)imgWidth / imgHeight;
            float aperture = 2.0f;
            float distToFocus = Vector3.Distance(lookAt, lookFrom);
            Camera cam = new(lookFrom, lookAt, up, fov, aspectRatio, aperture, distToFocus);
            Ray rayCam;

            for (int j = imgHeight - 1; j >= 0; j--)
            {
                for (int i = 0; i < imgWidth; i++)
                {
                    // 每个像素多次采样，取平均值。
                    Vector3 color = new(0.0f, 0.0f, 0.0f);
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

                    int r = Math.Clamp((int)(color.X * 255.99f), 0, 255);
                    int g = Math.Clamp((int)(color.Y * 255.99f), 0, 255);
                    int b = Math.Clamp((int)(color.Z * 255.99f), 0, 255);

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
                return new Vector3(0.0f, 0.0f, 0.0f);
            }

            if (world.Hit(ray, 0.001f, float.PositiveInfinity, rec))
            {
                if (rec.mat != null && rec.mat.Scatter(ray, rec, out Color attenuation, out Ray rayScattered))
                {
                    // 注：这里的*不是向量叉乘，而是相应分量的乘积。
                    return attenuation * Color(rayScattered, world, depth - 1);
                }
                return new Vector3(0.0f, 0.0f, 0.0f);
            }

            // 返回蓝色渐变背景。
            Vector3 unitDir = Vector3.Normalize(ray.Direction);
            float t = 0.5f * (unitDir.Y + 1.0f);
            return (1.0f - t) * Vector3.One() + t * new Vector3(0.5f, 0.7f, 1.0f);

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
            return (-b - MathF.Sqrt(discriminant)) / (2.0f * a);
        }

    }
}