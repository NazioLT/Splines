using UnityEngine;

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
            return Lerp(handle1.Position, handle1.HandleDelta2, handle2.HandleDelta1, handle2.Position, t);
        }
    }
}
