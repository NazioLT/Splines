using UnityEngine;

namespace Nazio_LT.Splines
{
    [ExecuteInEditMode]
    public class SplineBehaviour : MonoBehaviour
    {
        [SerializeField] private bool m_loop = false;

        [SerializeField] private BezierHandle[] m_handles;

        private float[] m_simplifiedDistances;
        private float m_splineLength = 0f;

        private const int PARAMETERIZATION_PRECISION = 100;

        public int CurveCount => m_loop ? m_handles.Length : m_handles.Length - 1;
        public int HandleCount => m_handles.Length;

        private float m_factor => 1f / (float)CurveCount;
        private int m_parameterization => PARAMETERIZATION_PRECISION * CurveCount;

        public BezierHandle GetHandle(int i)
        {
            int index = m_loop ? i % CurveCount : i;

            return m_handles[index];
        }

        #region Evaluate

        public Vector3 Evaluate(float t)
        {
            float clampedT = Mathf.Clamp(t, 0f, 0.9999f);
            float remapedT = RemapGlobalToLocalT(clampedT, out int curveID);

            return EvaluateCurve(curveID, remapedT);
        }

        public Vector3 EvaluateDistance(float distance)
        {
            float t = distance == m_splineLength ? 1f : SampleDistanceToT(distance);
            return Evaluate(t);
        }

        public Vector3 EvaluateUniform(float t)
        {
            return EvaluateDistance(t * m_splineLength);
        }

        #endregion

        #region Direction

        public void Direction(float t, out Vector3 forward, out Vector3 up, out Vector3 right)
        {
            forward = Forward(t).normalized;
            up = Up(t).normalized;
            right = Vector3.Cross(forward, up).normalized;
        }
        
        public void DirectionDistance(float distance, out Vector3 forward, out Vector3 up, out Vector3 right)
        {
            float t = distance == m_splineLength ? 1f : SampleDistanceToT(distance);
            Direction(t, out forward, out up, out right);
        }
        
        public void DirectionUniform(float t, out Vector3 forward, out Vector3 up, out Vector3 right)
        {
            DirectionDistance(t * m_splineLength, out forward, out up, out right);
        }
        
        #endregion

        private Vector3 Forward(float t)
        {
            float clampedT = Mathf.Clamp(t, 0f, 0.9999f);
            float remapedT = RemapGlobalToLocalT(clampedT, out int curveID);

            return Bezier.Derivative(GetHandle(curveID), GetHandle(curveID + 1), remapedT);
        }

        private Vector3 Up(float t) => Vector3.up;

        private float SampleDistanceToT(float distance)
        {
            return Bezier.CumulativeValuesToT(m_simplifiedDistances, distance);
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

        private void SimplifyCurve()
        {
            if (m_handles.Length == 0) return;

            m_simplifiedDistances = new float[m_parameterization + 1];

            float factor = 1f / (float)m_parameterization;
            Vector3 previousPoint = Evaluate(0f);
            m_simplifiedDistances[0] = 0;

            for (var i = 1; i < m_parameterization + 1; i++)
            {
                Vector3 newPoint = Evaluate(i * factor);
                float relativeDst = Vector3.Distance(previousPoint, newPoint);
                m_simplifiedDistances[i] = m_simplifiedDistances[i - 1] + relativeDst;

                previousPoint = newPoint;
            }

            m_splineLength = m_simplifiedDistances[m_parameterization];
        }

        private void Awake()
        {
            SimplifyCurve();
        }

        private void Update()
        {
            if (Application.isPlaying)
                return;

            SimplifyCurve();
        }
    }
}