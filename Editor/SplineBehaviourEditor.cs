using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Splines.Editor
{
    [CustomEditor(typeof(SplineBehaviour))]
    public class SplineBehaviourEditor : UnityEditor.Editor
    {
        private SplineBehaviour m_splineBehaviour = null;
        
        private void OnEnable()
        {
            m_splineBehaviour = target as SplineBehaviour;
        }

        private void OnSceneGUI()
        {
            int curveCount = m_splineBehaviour.CurveCount;
            for (int i = 0; i < curveCount; i++)
            {
                Splines.BezierHandle handle1 = m_splineBehaviour.GetHandle(i);
            }
        }
    }
}
