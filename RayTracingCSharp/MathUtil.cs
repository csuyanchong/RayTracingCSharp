using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCSharp
{
    internal class MathUtil
    {

        /// <summary>
        /// 返回单位球内的随机点：-1<x<1, -1<y<1, -1<z<1。
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomPointInUnitSphere()
        {
            Vector3 res;
            Random rd = new();
            do
            {
                res = 2 * new Vector3(rd.NextSingle(), rd.NextSingle(), rd.NextSingle()) - Vector3.One();
            } while (res.Magnitude() >= 1.0);
            return res;
        }


        /// <summary>
        /// 返回单位圆内的随机点：(0.5, 0.2, 0); -1<x<1, -1<y<1, z = 0。
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomInUnitCircle()
        {
            Vector3 res;
            Random rd = new();
            do
            {
                res = 2 * new Vector3(rd.NextSingle(), rd.NextSingle(), 0) - new Vector3(1, 1, 0);
            } while (res.Magnitude() >= 1.0);
            return res;
        }

        /// <summary>
        /// 返回 [0, 1)的随机浮点数。
        /// </summary>
        /// <returns></returns>
        public static float RandomFloat()
        {
            Random rd = new();
            return rd.NextSingle();
        }

        /// <summary>
        /// 度数转弧度。
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static float Deg2Rad(float deg)
        {
            return 2 * MathF.PI * deg / 360;
        }

        /// <summary>
        /// 弧度转度数。
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static float Rad2Deg(float rad)
        {
            return rad * 360 / (2 * MathF.PI);
        }
    }
}
