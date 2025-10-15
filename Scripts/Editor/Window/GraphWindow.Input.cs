using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Weave
{
    public partial class GraphWindow : EditorWindow
    {
        private void HandleInput(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                        OnLeftMouseDown(e);
                    else if (e.button == 1)
                        OnRightMouseDown(e);
                    break;
                case EventType.MouseUp:
                    OnMouseUp(e);
                    break;
                case EventType.MouseDrag:
                    OnMouseDrag(e);
                    break;
                case EventType.KeyDown:
                    OnKeyDown(e);
                    break;
            }
        }

        private void OnLeftMouseDown(Event e)
        {
            selectedPort = GetPortAtPosition(e.mousePosition);
            if (selectedPort != null)
            {
                Debug.Log("Clicked port: " + selectedPort.Name);
                e.Use();
                return;
            }
            else
            {
                selectedPort = null;
            }

                // Check if clicked on a node
                selectedNode = GetNodeAtPosition(e.mousePosition);
            if (selectedNode != null)
            {
                dragOffset = selectedNode.NTransform.Position - e.mousePosition;
                isDraggingNode = true;
                GUI.FocusControl(null);
                e.Use();
            }
            else
            {
                // Deselect if clicked on empty space
                selectedNode = null;
            }
        }

        private void OnRightMouseDown(Event e)
        {
            GenericMenu menu = new GenericMenu();

            // Find all node types
            var nodeTypes = System.Reflection.Assembly.GetAssembly(typeof(Node))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Node)) && !t.IsAbstract);

            foreach (var type in nodeTypes)
            {
                // Get the NodeMenuAttribute, if any
                var attr = type.GetCustomAttribute<NodeMenuAttribute>();
                string menuPath = attr != null ? attr.Category : type.Name;

                menu.AddItem(new GUIContent(menuPath), false, () => AddNodeOfType(type, e.mousePosition));
            }

            menu.ShowAsContext();
            e.Use();
        }

        private void OnMouseUp(Event e)
        {
            if (selectedPort != null)
            {
                NodePort currentPort = GetPortAtPosition(e.mousePosition);
                if (currentPort != null)
                {
                    graph.ConnectPort(selectedPort, currentPort);
                }
                e.Use();
                return;
            }
            else
            {
                selectedPort = null;
            }

            if (isDraggingNode)
            {
                isDraggingNode = false;
                e.Use();
            }
        }

        private void OnMouseDrag(Event e)
        {
            if (isDraggingNode && selectedNode != null)
            {
                selectedNode.NTransform.Position = e.mousePosition + dragOffset;
                GUI.changed = true;
                e.Use();
            }
        }

        private void OnKeyDown(Event e)
        {
            if (e.keyCode == KeyCode.Delete)
            {
                DelteNode();
                e.Use();
            }
        }
    }
}