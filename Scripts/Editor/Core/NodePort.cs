using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weave
{
    public enum PortType { Input, Output }

    [Serializable]
    public class NodePort<T> : NodePort
    {
        public new Type DataType => typeof(T);
        public NodePort(Node owner, string name, PortType portType, Type dataType) : base(owner, name, portType, dataType) { }
    }

    [Serializable]
    public class NodePort
    {
        public readonly string ID = Guid.NewGuid().ToString();
        public Node Owner;
        public string Name;
        public PortType PortType; // Input, Output
        public Type DataType; // typeof(float), typeof(string), etc.
        public List<Connection> Connections = new();
        public NodeTransform NTransform { get; private set; } = new(Vector2.zero, new(8, 8));

        public NodePort(Node owner, string name, PortType portType, Type dataType)
        {
            Owner = owner;
            Name = name;
            PortType = portType;
            DataType = dataType;
        }
    }

    [Serializable]
    public class Connection
    {
        public string FromNodeID;
        public string FromPortID;
        public string ToNodeID;
        public string ToPortID;

        public Connection(string fromNodeID, string fromPortID, string toNodeID, string toPortID) 
        { 
            FromNodeID = fromNodeID;
            FromPortID = fromPortID;
            ToNodeID = toNodeID;
            ToPortID = toPortID;
        }
    }
}