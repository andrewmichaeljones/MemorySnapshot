using System.Collections;
using UnityEngine.Profiling.Memory.Experimental;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;

namespace MemorySnapshot
{
    public class MemorySnapshotWindow : EditorWindow
    {
        private EditorCoroutine _editorCoroutine;

        private int _numberOfSnapshots = 10;
        private float _intervalSeconds = 60.0f;
        private string _outputFileLocation = @"C:\";

        private int _currentSnapshot;

        [MenuItem("Tools / Memory Snapshot")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(t: typeof(MemorySnapshotWindow), utility: false, title: "Memory Snapshot");
        }

        private void StartTakingSnapshots()
        {
            _currentSnapshot = 0;
            _editorCoroutine = EditorCoroutineUtility.StartCoroutine(SnapshotCoroutine(), this);
        }

        private IEnumerator SnapshotCoroutine()
        {
            while (_currentSnapshot < _numberOfSnapshots)
            {
                _currentSnapshot++;

                var snapshotfilename = Path.Combine(_outputFileLocation, $"Snapshot-{DateTime.Now.Ticks}.snap");
                MemoryProfiler.TakeSnapshot(snapshotfilename, (s, b) => { }, CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects | CaptureFlags.NativeAllocations);

                yield return new EditorWaitForSeconds(_intervalSeconds);
            }

            StopTakingSnapshots();
        }

        private void StopTakingSnapshots()
        {
            if (_editorCoroutine == null)
                return;

            EditorCoroutineUtility.StopCoroutine(_editorCoroutine);
        }

        private void OnDisable()
        {
            StopTakingSnapshots();
        }

        private void OnGUI()
        {
            _numberOfSnapshots = EditorGUILayout.IntField("Number of snapshots: ", _numberOfSnapshots);
            _intervalSeconds = EditorGUILayout.FloatField("Interval in seconds: ", _intervalSeconds);
            _outputFileLocation = EditorGUILayout.TextField("Output location: ", _outputFileLocation);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Take Snapshots"))
            {
                StartTakingSnapshots();
            }
            if (GUILayout.Button("Cancel"))
            {
                StopTakingSnapshots();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.LabelField($"Progress: {_currentSnapshot}/{_numberOfSnapshots}");
        }
    }
}