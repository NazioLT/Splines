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
        [SerializeField] private bool m_placeUniform = true;
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

                float t = m_placeUniform ? distancePerObject * i : m_distanceBetweenElements * i;

                Vector3 position = m_placeUniform
                    ? m_spline.EvaluateUniform(t)
                    : m_spline.EvaluateDistance(t);

                Vector3 forward = Vector3.zero;
                if (m_placeUniform)
                {
                    m_spline.DirectionUniform(t, out forward, out _, out _);
                }
                else
                {
                    m_spline.DirectionDistance(t, out forward, out _, out _);
                }

                childToPlace.position = position;
                childToPlace.localRotation = Quaternion.LookRotation(forward, Vector3.up);
            }
        }
    }
}