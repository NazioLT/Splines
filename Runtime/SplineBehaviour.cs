using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Splines
{
    public class SplineBehaviour : MonoBehaviour
    {
        [SerializeField] private bool m_loop = false;
        [SerializeField, Range(0f, 1f)] private float m_t = 0.5f;

        [SerializeField] private BezierHandle[] m_handles;

        public int CurveCount => m_loop ? m_handles.Length : m_handles.Length - 1;

        public BezierHandle GetHandle(int i)
        {
            return m_handles[i];
        }

        public Vector3 Evaluate(float t)
        {
            float clampedT = Mathf.Clamp(t, 0f, 0.9999f);
            float remapedT = RemapGlobalToLocalT(clampedT, out int curveID);

            return EvaluateCurve(curveID, remapedT);
        }

        private float RemapGlobalToLocalT(float t, out int curveID)
        {
            int curveCount = CurveCount;
            curveID = Mathf.FloorToInt(t * curveCount);

            float curveTLength = 1f / (float)curveCount;
            float startCurveValue = curveID * curveTLength;

            float newT = t - startCurveValue;

            return Mathf.InverseLerp(0, curveTLength, newT);
        }

        private Vector3 EvaluateCurve(int i, float t)
        {
            return Bezier.Lerp(m_handles[i], m_handles[i + 1], t);
        }

        private void OnDrawGizmos()
        {
            if (m_handles == null)
                return;

            Gizmos.color = Color.red * 0.5f;
            Gizmos.DrawSphere(Evaluate(m_t), 0.3f);

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
}
