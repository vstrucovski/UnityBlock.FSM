using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace UnityBlocks.FSM.Logging
{
    public class EditorStateMachineLogger : MonoBehaviour, IStateMachineLogger
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