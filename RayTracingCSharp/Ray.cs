using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCSharp
{
    internal class Ray
    {
        public Vector3 origin { get; set; }
        public Vector3 direction { get; set; }

        // constructor
        public Ray()
        {
            this.origin = new Vector3(0, 0, 0);
            this.direction = new Vector3(0, 0, 1);
        }

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        // method
        public Vector3 Point_at_param(float t)
        {
            return this.origin + t * this.direction;
        }
    }
}
