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
        private NodePort selectedPort;
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
        }

        private void AddNodeOfType(Type type, Vector2 position)
        {
            // Create a new instance of the selected Node type
            Node node = (Node)Activator.CreateInstance(type);
            node.NTransform.Position = position;
            node.OnCreation();
            graph.AddNode(node);
        }

        private Node GetNodeAtPosition(Vector2 pos)
        {
            foreach (var node in graph.GetNodes())
            {
                Rect rect = new(node.NTransform.Position, node.NTransform.Size);
                if (rect.Contains(pos))
                    return node;
            }
            return null;
        }

        public NodePort GetPortAtPosition(Vector2 pos)
        {
            foreach(Node node in graph.GetNodes())
            {
                foreach(NodePort port in node.Ports.Values)
                {
                    Rect rect = new(port.NTransform.Position, port.NTransform.Size);
                    if(rect.Contains(pos)) 
                        return port;
                }
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
                if(graph.RemoveNode(selectedNode))
                {
                    selectedNode = null;
                }
            }
        }
    }
}