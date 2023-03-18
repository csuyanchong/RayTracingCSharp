namespace RayTracingCSharp
{
    internal class Camera
    {
        public Vector3 lowerLeftCorner;
        public Vector3 horizen;
        public Vector3 vertical;
        public Vector3 origin;

        public Camera()
        {
            lowerLeftCorner = new(-2, -1, -1);
            horizen = new(4, 0, 0);
            vertical = new(0, 2, 0);
            origin = new(0, 0, 0);
        }

        public Camera(Vector3 lowerLeftCorner, Vector3 horizen, Vector3 vertical, Vector3 origin)
        {
            this.lowerLeftCorner = lowerLeftCorner;
            this.horizen = horizen;
            this.vertical = vertical;
            this.origin = origin;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(origin, lowerLeftCorner + u * horizen + v * vertical);
        }
    }
}
