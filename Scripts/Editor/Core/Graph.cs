using System.Collections.Generic;
using UnityEngine;

namespace Weave
{
    [CreateAssetMenu(menuName = "Weave/Graph")]
    public class Graph : ScriptableObject
    {
        private List<Node> nodes = new();

        public void AddNode(Node node)
        {
            if(nodes.Contains(node)) return;
            nodes.Add(node);
        }

        public List<Node> GetNodes()
        {
            List<Node> nodesCopy = new();
            nodesCopy.AddRange(nodes);
            return nodesCopy;
        }

        public bool RemoveNode(Node node)
        {
            if (!nodes.Contains(node))
            {
                Debug.LogWarning("Cannot remove, node is not registered in the graph");
                return false;
            }
            nodes.Remove(node);
            return true;
        }

        public NodePort GetPort(string portID)
        {
            foreach (Node node in nodes)
            {
                if (node.Ports.TryGetValue(portID, out var port))
                {
                    return port;
                }
            }
            return null;
        }

        public void ConnectPort(NodePort fromPort, NodePort toPort)
        {
            if(fromPort.PortType == toPort.PortType)
            {
                Debug.LogWarning("Cannot connect ports of the same type");
                return;
            }
            Connection connection = new(fromPort.Owner.ID, fromPort.ID, toPort.Owner.ID, toPort.ID);
            fromPort.Connections.Add(connection);
            toPort.Connections.Add(connection);
        }

        public void RunGraph()
        {

        }
    }
}