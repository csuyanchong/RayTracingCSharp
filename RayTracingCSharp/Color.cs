namespace RayTracingCSharp
{
    internal class Color :Vector3
    {
        //public float R { get; set; }
        //public float G { get; set; }
        //public float B { get; set; }

        //public Color(float r, float g, float b)
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
    }
}
