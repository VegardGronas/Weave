using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weave
{
    [Serializable]
    public class Node
    {
        public readonly string ID = Guid.NewGuid().ToString();
        public NodeTransform NTransform { get; private set; } = new(new(100, 100), new(200, 150));
        /// <summary> Use the nodeports ID as the key /// </summary>
        public Dictionary<string, NodePort> Ports { get; private set; } = new();
        /// <summary> Called on default if run is not overridden /// </summary>
        protected void Complete() { }
        /// <summary> Node executed /// </summary>
        public virtual void Run() { Complete(); }
        /// <summary> Create ports for this node /// </summary>
        public virtual void OnCreation() { }
    }

    [Serializable]
    public class NodeTransform
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public NodeTransform(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
    }
}