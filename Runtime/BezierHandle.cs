using UnityEngine;

namespace Nazio_LT.Splines
{
    [System.Serializable]
    public class BezierHandle
    {
        public BezierHandle(Vector3 position, Vector3 handleDelta)
        {
            m_position = position;
            m_handleDelta1 = -handleDelta;
            m_handleDelta2 = handleDelta;
        }

        [SerializeField] private Vector3 m_position;
        [SerializeField] private Vector3 m_handleDelta1;
        [SerializeField] private Vector3 m_handleDelta2;

        [SerializeField] private bool m_locked = true;

        public Vector3 Position => m_position;
        public Vector3 HandleDelta1 => m_position + m_handleDelta1;
        public Vector3 HandleDelta2 => m_position + m_handleDelta2;

        public void UpdatePosition(Vector3 position)
        {
            m_position = position;
        }

        public void UpdateHandlePosition(bool firstHandle, Vector3 position)
        {
            Vector3 delta = position - m_position;

            if (!m_locked)
            {
                if (firstHandle)
                {
                    m_handleDelta1 = delta;
                }
                else
                {
                    m_handleDelta2 = delta;
                }

                return;
            }

            if (firstHandle)
            {
                UpdateLockedHandleDelta(ref m_handleDelta1, ref m_handleDelta2, delta);
                return;
            }

            UpdateLockedHandleDelta(ref m_handleDelta2, ref m_handleDelta1, delta);
        }

        private void UpdateLockedHandleDelta(ref Vector3 leadHandle, ref Vector3 followHandle, Vector3 delta)
        {
            leadHandle = delta;
            followHandle = -delta;
        }
    }
}