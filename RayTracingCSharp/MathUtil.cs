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

        public static float RandomFloat()
        {
            Random rd = new();
            return rd.NextSingle();
        }
    }
}
