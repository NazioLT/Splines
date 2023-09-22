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
            if(Application.isPlaying)
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

                if (m_placeUniform)
                    childToPlace.position = m_spline.EvaluateUniform(distancePerObject * i);
                else 
                    childToPlace.position = m_spline.EvaluateDistance(m_distanceBetweenElements * i);
            }
        }
    }
}