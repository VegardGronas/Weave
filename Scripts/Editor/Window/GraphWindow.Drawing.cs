using UnityEditor;
using UnityEngine;

namespace Weave
{
    public partial class GraphWindow : EditorWindow
    {
        private void DrawNodes()
        {
            if (graph?.Nodes == null) return;

            foreach (var node in graph.Nodes)
            {
                Rect rect = new Rect(node.NTransform.Position, node.NTransform.Size);

                // Draw background
                Handles.DrawSolidRectangleWithOutline(
                    rect,
                    selectedNode == node ? new Color(0.2f, 0.4f, 0.7f) : new Color(0.25f, 0.25f, 0.25f),
                    Color.black
                );

                // Node title
                GUI.Label(new Rect(rect.x + 8, rect.y + 4, rect.width - 16, 20), node.GetType().Name, EditorStyles.boldLabel);

                // Example ports
                foreach (var port in node.Ports.Values)
                {
                    DrawPort(node, port, rect);
                }
            }
        }

        private void DrawPort(Node node, NodePort port, Rect nodeRect)
        {
            Vector2 portPos = Vector2.zero;

            if (port.PortType == PortType.Input)
                portPos = new Vector2(nodeRect.x - 8, nodeRect.y + nodeRect.height / 2);
            else
                portPos = new Vector2(nodeRect.xMax - 4, nodeRect.y + nodeRect.height / 2);

            Rect portRect = new Rect(portPos.x, portPos.y, 8, 8);
            EditorGUI.DrawRect(portRect, Color.yellow);

            // Handle clicks on port
            if (Event.current.type == EventType.MouseDown && portRect.Contains(Event.current.mousePosition))
            {
                Debug.Log($"Clicked {port.PortType} port on {node.GetType().Name}");
                Event.current.Use();
            }
        }

        private void DrawPortConnections()
        {

        }
    }
}