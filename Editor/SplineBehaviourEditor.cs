using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Splines.Editor
{
    [CustomEditor(typeof(SplineBehaviour))]
    public class SplineBehaviourEditor : UnityEditor.Editor
    {
        private SplineBehaviour m_splineBehaviour = null;

        private int curveSelected = int.MaxValue;

        private void OnEnable()
        {
            m_splineBehaviour = target as SplineBehaviour;
            curveSelected = int.MaxValue;
        }

        private void OnSceneGUI()
        {
            DisplayHandleButtons();
            DisplayCurve();

            if (curveSelected == int.MaxValue)
                return;

            DisplayCurrentHandle();
        }

        private void DisplayHandleButtons()
        {
            for (int i = 0; i < m_splineBehaviour.HandleCount; i++)
            {
                if (curveSelected == i)
                    continue;

                Splines.BezierHandle handle1 = m_splineBehaviour.GetHandle(i);
                bool handlePerformed = Handles.Button(handle1.Position, Quaternion.identity, 0.2f, 0.2f,
                    Handles.CubeHandleCap);

                if (handlePerformed)
                    curveSelected = i;
            }
        }

        private void DisplayCurve()
        {
            for (int i = 0; i < m_splineBehaviour.CurveCount; i++)
            {
                Splines.BezierHandle handle1 = m_splineBehaviour.GetHandle(i);
                Splines.BezierHandle handle2 = m_splineBehaviour.GetHandle(i + 1);

                Handles.DrawBezier(handle1.Position, handle2.Position, handle1.Handle2, handle2.Handle1, Color.magenta,
                    Texture2D.whiteTexture, 1f);
            }
        }

        private void DisplayCurrentHandle()
        {
            Splines.BezierHandle handle = m_splineBehaviour.GetHandle(curveSelected);
            Vector3 position = Handles.PositionHandle(handle.Position, Quaternion.identity);
            Vector3 handle1 = Handles.PositionHandle(handle.Handle1, Quaternion.identity);
            Vector3 handle2 = Handles.PositionHandle(handle.Handle2, Quaternion.identity);
            
            Handles.DrawLine(position, handle1, 1f);
            Handles.DrawLine(position, handle2);

            handle.UpdatePosition(position);

            if (Vector3.Distance(handle.Handle1, handle1) > 0.01f)
                handle.UpdateHandlePosition(true, handle1);
            else if (Vector3.Distance(handle.Handle2, handle2) > 0.01f)
                handle.UpdateHandlePosition(false, handle2);
        }
    }
}