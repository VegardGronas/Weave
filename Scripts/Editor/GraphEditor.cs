using UnityEditor;
using UnityEngine;

namespace Weave
{
    [CustomEditor(typeof(Graph))]
    public class GraphEditor : Editor
    {
        private Graph graph;

        private void OnEnable()
        {
            graph = (Graph)target;
        }

        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Open graph window"))
            {
                GraphWindow.Open(graph);
            }
        }
    }
}