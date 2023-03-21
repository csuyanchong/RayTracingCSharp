namespace RayTracingCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // image setting
            const float aspectRatio = 2 / 1;
            const int imgWidth = 800;
            const int imgHeight = (int)(imgWidth / aspectRatio);
            // 单个像素多重采样数
            const int samplePerPix = 100;
            // 最大反射次数
            const int maxDepth = 50;

            Console.WriteLine("P3");
            Console.WriteLine(imgWidth + " " + imgHeight);
            Console.WriteLine(255);

            // scene part
            //HittableList world = new();

            //float radius = MathF.Cos(MathF.PI / 4);
            //Material matLeft = new Lambertian(new(0, 0, 1f));
            //Material matRight = new Lambertian(new(1f, 0, 0));
            //world.Add(new Sphere(new(-radius, 0.0f, -1f), radius, matLeft));
            //world.Add(new Sphere(new(radius, 0.0f, -1f), radius, matRight));

            //Material matGround = new Lambertian(new(0.8f, 0.8f, 0.0f));
            //Material matCenter = new Lambertian(new(0.1f, 0.2f, 0.5f));
            //Material matLeft = new Dielectric(1.5f);
            //Material matRight = new Metal(new(0.8f, 0.6f, 0.2f), 0.0f);

            //world.Add(new Sphere(new(0.0f, -100.5f, -1f), 100f, matGround));
            //world.Add(new Sphere(new(0.0f, 0.0f, -1f), 0.5f, matCenter));
            //world.Add(new Sphere(new(-1f, 0.0f, -1f), 0.5f, matLeft));
            //world.Add(new Sphere(new(-1f, 0.0f, -1f), -0.45f, matLeft));
            //world.Add(new Sphere(new(1f, 0.0f, -1f), 0.5f, matRight));

            HittableList world = GenerateRandomScene();

            // render part
            // camera
            Vector3 lookFrom = new(13f, 2f, 3f);
            Vector3 lookAt = new(0, 0, 0f);
            Vector3 up = new(0, 1, 0);
            float fov = 20f;
            float aperture = 0.1f;
            float distToFocus = 10;
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
                        color += HitColor(rayCam, world, maxDepth);
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

        public static Vector3 HitColor(Ray ray, Hittable world, int depth)
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
                    return attenuation * HitColor(rayScattered, world, depth - 1);
                }
                return new Vector3(0.0f, 0.0f, 0.0f);
            }

            // 返回蓝色渐变背景。
            Vector3 unitDir = Vector3.Normalize(ray.Direction);
            float t = 0.5f * (unitDir.Y + 1.0f);
            return (1.0f - t) * Vector3.One() + t * new Vector3(0.5f, 0.7f, 1.0f);

        }

        /// <summary>
        /// 创建随机场景。
        /// </summary>
        /// <returns></returns>
        public static HittableList GenerateRandomScene()
        {
            HittableList world = new();

            var groundMat = new Lambertian(new Color(0.5f, 0.5f, 0.5f));
            world.Add(new Sphere(new Vector3(0, -1000, 0), 1000, groundMat));

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    var chooseMat = MathUtil.RandomFloat();
                    Vector3 center = new(i + 0.9f * MathUtil.RandomFloat(), 0.2f, j + 0.9f * MathUtil.RandomFloat());
                    if (Vector3.Distance(center, new Vector3(4, 0.2f, 0)) > 0.9f)
                    {
                        Material sphereMat;

                        if (chooseMat < 0.8f)
                        {
                            // 漫反射
                            var colorRandom = Color.Random() * Color.Random();
                            Color albedo = Color.FromVector3(colorRandom);
                            sphereMat = new Lambertian(albedo);
                            world.Add(new Sphere(center, 0.2f, sphereMat));
                        }
                        else if (chooseMat < 0.95f)
                        {
                            // 金属
                            var colorRandom = Color.RandomRange(0.5f, 1);
                            var fuzz = MathUtil.RandomFloatRange(0, 0.5f);
                            sphereMat = new Metal(colorRandom, fuzz);
                            world.Add(new Sphere(center, 0.2f, sphereMat));
                        }
                        else
                        {
                            // 玻璃
                            sphereMat = new Dielectric(1.5f);
                            world.Add(new Sphere(center, 0.2f, sphereMat));
                        }
                    }
                }
            }

            var material1 = new Dielectric(1.5f);
            world.Add(new Sphere(new Vector3(0, 1, 0), 1.0f, material1));

            var material2 = new Lambertian(new Color(0.4f, 0.2f, 0.1f));
            world.Add(new Sphere(new Vector3(-4, 1, 0), 1.0f, material2));

            var material3 = new Metal(new Color(0.7f, 0.6f, 0.5f), 0);
            world.Add(new Sphere(new Vector3(4, 1, 0), 1.0f, material3));
            return world;
        }
    }
}