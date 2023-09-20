using UnityEngine;

namespace Nazio_LT.Splines
{
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
