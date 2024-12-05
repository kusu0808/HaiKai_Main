using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    [CustomEditor(typeof(Transform))]
    public sealed class ShowWorldValueOnTransform : Editor
    {
        private Transform _target = null;
        private static bool _isEnabled = false;

        private void OnEnable() => _target = target as Transform;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space(25);

            _isEnabled = EditorGUILayout.Toggle("詳細を表示", _isEnabled);
            if (_isEnabled is false) return;

            EditorGUILayout.Space(25);

            EditorGUILayout.LabelField("ワールド座標", EditorStyles.boldLabel);
            Vector3 newWorldPosition = EditorGUILayout.Vector3Field("Position", _target.position);
            Vector3 newWorldEulerAngles = EditorGUILayout.Vector3Field("EulerAngles", _target.eulerAngles);
            _ = EditorGUILayout.Vector4Field("Quaternion", _target.rotation.ToVector4());
            _ = EditorGUILayout.Vector3Field("Scale", _target.lossyScale);
            if (newWorldPosition != _target.position)
            {
                Undo.RecordObject(_target, "Change World Position");
                _target.position = newWorldPosition;
            }
            if (newWorldEulerAngles != _target.eulerAngles)
            {
                Undo.RecordObject(_target, "Change World EulerAngles");
                _target.eulerAngles = newWorldEulerAngles;
            }

            EditorGUILayout.Space(25);

            EditorGUILayout.LabelField("ローカル座標", EditorStyles.boldLabel);
            Vector3 newLocalPosition = EditorGUILayout.Vector3Field("Position", _target.localPosition);
            Vector3 newLocalEulerAngles = EditorGUILayout.Vector3Field("EulerAngles", _target.localEulerAngles);
            _ = EditorGUILayout.Vector4Field("Quaternion", _target.localRotation.ToVector4());
            Vector3 newLocalScale = EditorGUILayout.Vector3Field("Scale", _target.localScale);
            if (newLocalPosition != _target.localPosition)
            {
                Undo.RecordObject(_target, "Change Local Position");
                _target.localPosition = newLocalPosition;
            }
            if (newLocalEulerAngles != _target.localEulerAngles)
            {
                Undo.RecordObject(_target, "Change Local EulerAngles");
                _target.localEulerAngles = newLocalEulerAngles;
            }
            if (newLocalScale != _target.localScale)
            {
                Undo.RecordObject(_target, "Change Local Scale");
                _target.localScale = newLocalScale;
            }
        }
    }

    public static class ShowWorldValueOnTransformEx
    {
        public static Quaternion ToQuaternion(this Vector4 v) => new(v.x, v.y, v.z, v.w);
        public static Vector4 ToVector4(this Quaternion q) => new(q.x, q.y, q.z, q.w);
    }
}
