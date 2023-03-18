using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCSharp
{
    internal class Vector3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        // constructor
        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        // operator overloading
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator *(float t, Vector3 a)
        {
            return new Vector3(t * a.x, t * a.y, t * a.z);
        }

        public static Vector3 operator /(Vector3 a, float t)
        {
            return new Vector3(a.x / t, a.y / t, a.z / t);
        }

        // method
        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 One()
        {
            return new Vector3(1, 1, 1);
        }

        // util function
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector3 Normalize()
        {
            return this / this.Magnitude();
        }

        public override string ToString()
        {
            return this.x.ToString() + ' ' + this.y.ToString() + ' ' + this.z.ToString();
        }
    }
}
