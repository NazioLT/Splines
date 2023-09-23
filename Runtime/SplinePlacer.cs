using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Splines
{
    [ExecuteInEditMode]
    public class SplinePlacer : MonoBehaviour
    {
        [SerializeField] private SplineBehaviour m_spline = null;

        [Space] 
        [SerializeField] private SplineEvaluationMethod m_EvaluationMethod = SplineEvaluationMethod.Normal;
        [SerializeField] private float m_distanceBetweenElements = 1f;

        [Space] 
        [SerializeField] private bool m_lockXRotation = true;
        [SerializeField] private bool m_lockYRotation = true;
        [SerializeField] private bool m_lockZRotation = true;

        private void Update()
        {
            if (Application.isPlaying)
                return;

            if (m_spline)
                HandleUpdatePlacer();
        }

        private void HandleUpdatePlacer()
        {
            int objectCount = transform.childCount;

            if (objectCount == 0)
                return;

            float distancePerObject = 1f / (float)objectCount;
            for (int i = 0; i < objectCount; i++)
            {
                Transform childToPlace = transform.GetChild(i);

                float t = m_EvaluationMethod == SplineEvaluationMethod.Distance ? m_distanceBetweenElements * i : distancePerObject * i;

                childToPlace.position = m_spline.EvaluatePoint(t, m_EvaluationMethod);
                Vector3 localEulerAngles = m_spline.EvaluateRotation(t, m_EvaluationMethod).eulerAngles;
                Vector3 lastEulerAngles = childToPlace.localEulerAngles;
                childToPlace.localEulerAngles = new Vector3(
                    m_lockXRotation ? localEulerAngles.x : lastEulerAngles.x, 
                    m_lockYRotation ? localEulerAngles.y : lastEulerAngles.y, 
                    m_lockZRotation ? localEulerAngles.z : lastEulerAngles.z);
            }
        }
    }
}