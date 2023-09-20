using System.Collections;
using System.Collections.Generic;
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
            return Lerp(handle1.Position, handle1.Handle2, handle2.Handle1, handle2.Position, t);
        }
    }

    public class SplineBehaviour : MonoBehaviour
    {
        [SerializeField] private BezierHandle[] m_handles;

        private void OnDrawGizmos()
        {
            if (m_handles == null)
                return;

            for (var i = 0; i < m_handles.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(m_handles[i].Position, 0.2f);

                if (i == 0)
                    continue;

                Gizmos.color = Color.white;
                for (var j = 0; j < 30; j++)
                {
                    Gizmos.DrawSphere(Bezier.Lerp(m_handles[i - 1], m_handles[i], (float)j / 30f), 0.2f);
                }
            }
        }
    }

    [System.Serializable]
    public struct BezierHandle
    {
        public BezierHandle(Vector3 position, Vector3 handleDelta)
        {
            m_position = position;
            m_handle1 = position - handleDelta;
            m_handle2 = position + handleDelta;
        }

        [SerializeField] private Vector3 m_position;
        [SerializeField] private Vector3 m_handle1;
        [SerializeField] private Vector3 m_handle2;

        public Vector3 Position => m_position;
        public Vector3 Handle1 => m_handle1;
        public Vector3 Handle2 => m_handle2;
    }
}
