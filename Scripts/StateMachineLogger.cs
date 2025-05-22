using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UnityBlocks.FSM
{
    public class StateMachineLogger : MonoBehaviour // TODO to interface
    {
        [SerializeField, Multiline(10)] private string output;
        [SerializeField] private int maxLines = 10;

        private readonly Queue<string> _logLines = new();

        public void Log(string value)
        {
            _logLines.Enqueue(value);

            if (_logLines.Count > maxLines)
                _logLines.Dequeue();

            output = string.Join("\n", _logLines);
        }

#if UNITY_EDITOR
        [ContextMenu("Clear Logs")]
        private void ClearLogs()
        {
            output = string.Empty;
            _logLines.Clear();
        }
#endif
    }
}