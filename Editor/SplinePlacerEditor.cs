using UnityEditor;

namespace Nazio_LT.Splines.Editor
{
    [CustomEditor(typeof(SplinePlacer))]
    public class SplinePlacerEditor : UnityEditor.Editor
    {
        private SerializedProperty m_spline = null;
        private SerializedProperty m_placeUniform = null;
        private SerializedProperty m_distanceBetweenElements = null;
        
        private void OnEnable()
        {
            m_spline = serializedObject.FindProperty("m_spline");
            m_placeUniform = serializedObject.FindProperty("m_placeUniform");
            m_distanceBetweenElements = serializedObject.FindProperty("m_distanceBetweenElements");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_spline);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(m_placeUniform);

            if (!m_placeUniform.boolValue)
            {
                EditorGUILayout.PropertyField(m_distanceBetweenElements);
            }
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}