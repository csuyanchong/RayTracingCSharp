namespace RayTracingCSharp
{
    internal class Ray
    {
        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        // constructor
        public Ray()
        {
            this.Origin = new Vector3(0, 0, 0);
            this.Direction = new Vector3(0, 0, 1);
        }

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        // method
        public Vector3 Point_at_param(float t)
        {
            return this.Origin + t * this.Direction;
        }
    }
}
