namespace RayTracingCSharp
{
    internal class Color : Vector3
    {
        //public float R { get; set; }
        //public float G { get; set; }
        //public float B { get; set; }

        //public HitColor(float r, float g, float b)
        //{
        //    this.R = r;
        //    this.G = g;
        //    this.B = b;
        //}

        public Color(float r, float g, float b)
        {
            this.X = r;
            this.Y = g;
            this.Z = b;
        }

        public static Color FromVector3(Vector3 vec)
        {
            return new Color(vec.X, vec.Y, vec.Z);
        }

        public static new Color Random()
        {
            return new Color(MathUtil.RandomFloat(), MathUtil.RandomFloat(), MathUtil.RandomFloat());
        }

        public static new Color RandomRange(float min, float max)
        {
            return new Color(MathUtil.RandomFloatRange(min, max), MathUtil.RandomFloatRange(min, max), MathUtil.RandomFloatRange(min, max));
        }
    }
}
