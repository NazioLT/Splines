using UnityEngine;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Splines
{
    public static class Bezier
    {
        public static Vector3 Lerp(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            float tCube = Mathf.Pow(t, 3f);
            float tSquare = t * t;

            Vector3 p1P = (-tCube + 3f * tSquare - 3f * t + 1f) * p1;
            Vector3 p2P = (3f * tCube - 6f * tSquare + 3f * t) * p2;
            Vector3 p3P = (-3f * tCube + 3f * tSquare) * p3;
            Vector3 p4P = tCube * p4;

            return p1P + p2P + p3P + p4P;
        }

        public static Vector3 Lerp(BezierHandle handle1, BezierHandle handle2, float t)
        {
            return Lerp(handle1.Position, handle1.Handle2, handle2.Handle1, handle2.Position, t);
        }
        
        /// <summary>Compute squared Bezier total distance with parameterization.</summary>
        public static float CumulativeValuesToT(float[] values, float curValue)
        {
            int length = values.Length;
            float totalDst = values[length - 1];

            curValue = curValue % totalDst;

            for (var i = 0; i < length; i++)
            {
                if (!curValue.IsIn(values[i], values[i + 1])) continue;

                return NMath.Remap(curValue, values[i], values[i + 1], i / (length - 1f), (i + 1) / (length - 1f));
            }

            return 0;
        }
    }
}
