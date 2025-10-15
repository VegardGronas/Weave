using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

namespace Weave
{
    public partial class GraphWindow : EditorWindow
    {
        private Graph graph;
        private Node selectedNode;
        private bool isDraggingNode;
        private Vector2 dragOffset;

        public static void Open(Graph data)
        {
            // Create or focus existing window
            var window = GetWindow<GraphWindow>();
            window.titleContent = new GUIContent("Weave Graph");
            window.SetGraph(data);
            window.Show();
        }

        private void SetGraph(Graph data)
        {
            graph = data;
        }

        private void OnGUI()
        {
            if (graph == null)
            {
                EditorGUILayout.HelpBox("No graph assigned. Open a graph to edit.", MessageType.Info);
                return;
            }

            EditorGUILayout.LabelField($"Editing Graph: {graph.name}", EditorStyles.boldLabel);
            // Here you’ll draw your graph editor UI (nodes, ports, etc.)

            HandleInput(Event.current);

            DrawNodes();

            DrawPortConnections();
        }

        private void AddNodeOfType(Type type, Vector2 position)
        {
            // Create a new instance of the selected Node type
            Node node = (Node)Activator.CreateInstance(type);
            node.NTransform.Position = position;
            node.OnCreation();
            graph.Nodes.Add(node);
        }

        private Node GetNodeAtPosition(Vector2 pos)
        {
            foreach (var node in graph.Nodes)
            {
                Rect rect = new(node.NTransform.Position, node.NTransform.Size);
                if (rect.Contains(pos))
                    return node;
            }
            return null;
        }

        /// <summary>
        /// Rememeber to delete and remove all connections
        /// </summary>
        private void DelteNode()
        {
            if(selectedNode != null)
            {
                if(graph.Nodes.Contains(selectedNode))
                {
                    graph.Nodes.Remove(selectedNode);
                    selectedNode = null;
                }
            }
        }
    }
}