using UnityEditor;

namespace Nazio_LT.Splines.Editor
{
    [CustomEditor(typeof(SplinePlacer))]
    public class SplinePlacerEditor : UnityEditor.Editor
    {
        private SerializedProperty m_spline = null;
        private SerializedProperty m_EvaluationMethod = null;
        private SerializedProperty m_distanceBetweenElements = null;
        private SerializedProperty m_lockXRotation = null;
        private SerializedProperty m_lockYRotation = null;
        private SerializedProperty m_lockZRotation = null;
        
        private void OnEnable()
        {
            m_spline = serializedObject.FindProperty("m_spline");
            m_EvaluationMethod = serializedObject.FindProperty("m_EvaluationMethod");
            m_distanceBetweenElements = serializedObject.FindProperty("m_distanceBetweenElements");
            m_lockXRotation = serializedObject.FindProperty("m_lockXRotation");
            m_lockYRotation = serializedObject.FindProperty("m_lockYRotation");
            m_lockZRotation = serializedObject.FindProperty("m_lockZRotation");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_spline);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(m_EvaluationMethod);

            if (m_EvaluationMethod.intValue == (int)SplineEvaluationMethod.Distance)
            {
                EditorGUILayout.PropertyField(m_distanceBetweenElements);
            }

            EditorGUILayout.PropertyField(m_lockXRotation);
            EditorGUILayout.PropertyField(m_lockYRotation);
            EditorGUILayout.PropertyField(m_lockZRotation);
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}