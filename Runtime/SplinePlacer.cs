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
                childToPlace.localRotation = m_spline.EvaluateRotation(t, m_EvaluationMethod);
            }
        }
    }
}