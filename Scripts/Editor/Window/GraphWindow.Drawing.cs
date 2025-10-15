using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Weave
{
    public partial class GraphWindow : EditorWindow
    {
        private void DrawNodes()
        {
            if (graph?.GetNodes() == null) return;

            foreach (var node in graph.GetNodes())
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
                    DrawPortConnections(port);
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

            port.NTransform.Position = portPos;

            Rect portRect = new Rect(portPos.x, portPos.y, port.NTransform.Size.x, port.NTransform.Size.y);
            EditorGUI.DrawRect(portRect, Color.yellow);

            // Handle clicks on port
            if (Event.current.type == EventType.MouseDown && portRect.Contains(Event.current.mousePosition))
            {
                Debug.Log($"Clicked {port.PortType} port on {node.GetType().Name}");
                Event.current.Use();
            }
        }

        private void DrawPortConnections(NodePort port)
        {
            if (port.PortType == PortType.Input) return;
            foreach (Connection connection in port.Connections)
            {
                NodePort nextPort = graph.GetPort(connection.ToPortID);
                if (nextPort != null)
                {
                    Handles.DrawBezier(
                        port.NTransform.Position, nextPort.NTransform.Position,
                        port.NTransform.Position + Vector2.right * 50f,
                        nextPort.NTransform.Position + Vector2.left * 50f,
                        Color.white,
                        null,
                        2f
                    );
                }
            }
        }
    }
}