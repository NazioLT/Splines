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
        public int HandleCount => m_handles.Length;

        public BezierHandle GetHandle(int i)
        {
            int index = m_loop ? i % CurveCount : i;
            
            return m_handles[index];
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
            return Bezier.Lerp(GetHandle(i), GetHandle(i + 1), t);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(Evaluate(m_t), 0.2f);
        }
    }
}
