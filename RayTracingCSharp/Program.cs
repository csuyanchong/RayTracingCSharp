namespace RayTracingCSharp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // image setting
            int imgWidth = 800;
            int imgHeight = 600;

            Console.WriteLine("P3");
            Console.WriteLine(imgWidth + " " + imgHeight);
            Console.WriteLine(255);

            // render
            Vector3 lowerLeftCorner = new Vector3(-2, -1, -1);
            Vector3 horizen = new Vector3(4, 0, 0);
            Vector3 vertical = new Vector3(0, 2, 0);
            Vector3 origin = new Vector3(0, 0, 0);

            Vector3 pt;
            Vector3 color;
            for (int j = imgHeight - 1; j >= 0; j--)
            {
                for (int i = 0; i < imgWidth; i++)
                {
                    float u = i / (float)imgWidth;
                    float v = j / (float)imgHeight;

                    Ray ray = new Ray(origin, lowerLeftCorner + u * horizen + v * vertical);
                    color = Program.Color(ray);

                    int r = (int)(color.x * 255.99f);
                    int g = (int)(color.y * 255.99f);
                    int b = (int)(color.z * 255.99f);

                    Console.Write(r + " " + g + " " + b + "\n");
                    //pt = new Vector3(i * 255.0f / imgWidth, 0, j * 255.0f / imgHeight);
                    //pt = new Vector3(0, 0, 255.0f * j / imgHeight);
                    //Console.Write(pt.ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        public static Vector3 Color(Ray ray)
        {
            Vector3 unitDir = ray.direction.Normalize();
            float t = 0.5f * (unitDir.y + 1.0f);
            return (1.0f - t) * Vector3.One() + t * new Vector3(0.5f, 0.7f, 1.0f);
        }
    }
}