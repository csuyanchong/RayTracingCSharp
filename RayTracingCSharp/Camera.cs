namespace RayTracingCSharp
{
    internal class Camera
    {
        public Vector3 lowerLeftCorner;
        public Vector3 horizen;
        public Vector3 vertical;
        public Vector3 origin;

        private readonly Vector3 u;
        private readonly Vector3 v;
        private readonly Vector3 w;
        private readonly float lensRadius;
        //public Camera()
        //{
        //    lowerLeftCorner = new(-2, -1, -1);
        //    horizen = new(4, 0, 0);
        //    vertical = new(0, 2, 0);
        //    origin = new(0, 0, 0);
        //}

        public Camera(Vector3 lookFrom, Vector3 lookAt, Vector3 up, float viewFov, float widthheightRatio, float aperture, float focusDist)
        {
            float theta = MathUtil.Deg2Rad(viewFov);
            float h = MathF.Tan(theta / 2);
            float viewportHeight = 2 * h;
            float viewportWidth = widthheightRatio * viewportHeight;

            w = Vector3.Normalize(lookFrom - lookAt);
            u = Vector3.Normalize(Vector3.CrossProduct(up, w));
            v = Vector3.CrossProduct(w, u);

            origin = lookFrom;
            horizen = focusDist * viewportWidth * u;
            vertical = focusDist * viewportHeight * v;
            lowerLeftCorner = origin - horizen / 2 - vertical / 2 - w * focusDist;

            lensRadius = aperture / 2;
            //float focalLength = 1.0f;

            //origin = new(0, 0, 0);
            //horizen = new(viewportWidth, 0, 0);
            //vertical = new(0, viewportHeight, 0);
            //lowerLeftCorner = origin - horizen / 2 - vertical / 2 - new Vector3(0, 0, focalLength);
        }

        //public Camera(Vector3 lowerLeftCorner, Vector3 horizen, Vector3 vertical, Vector3 origin)
        //{
        //    this.lowerLeftCorner = lowerLeftCorner;
        //    this.horizen = horizen;
        //    this.vertical = vertical;
        //    this.origin = origin;
        //}

        public Ray GetRay(float u, float v)
        {
            Vector3 rd = lensRadius * MathUtil.RandomInUnitCircle();
            Vector3 offset = this.u * rd.X + this.v * rd.Y;

            return new Ray(origin + offset, lowerLeftCorner + u * horizen + v * vertical - origin - offset);
        }
    }
}
