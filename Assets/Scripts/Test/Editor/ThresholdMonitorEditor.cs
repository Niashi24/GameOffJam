// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(MonoBehaviour), true)]
// public class ThresholdMonitorEditor : Editor
// {
//     private ThresholdMonitor _monitor;
//     private Vector2 _thresholdRange;
//     private bool _defaultValue;

//     private void OnEnable()
//     {
//         var component = (MonoBehaviour)target;
//         _monitor = component.GetComponent<ThresholdMonitor>();
//         if (_monitor == null) return;

//         _thresholdRange = new Vector2(_monitor.LowerThreshold, _monitor.UpperThreshold);
//         _defaultValue = _monitor.Enabled;
//     }

//     public override void OnInspectorGUI()
//     {
//         if (_monitor == null) return;

//         _thresholdRange = EditorGUILayout.Vector2Field("Threshold Range", _thresholdRange);
//         _defaultValue = EditorGUILayout.Toggle("Default Value", _defaultValue);

//         if (_thresholdRange.x != _monitor.LowerThreshold || _thresholdRange.y != _monitor.UpperThreshold || _defaultValue != _monitor.Enabled)
//         {
//             _monitor.LowerThreshold = _thresholdRange.x;
//             _monitor.UpperThreshold = _thresholdRange.y;
//             _monitor.Enabled = _defaultValue;
//         }
//     }
// }
