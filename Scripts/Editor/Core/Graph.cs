using System.Collections.Generic;
using UnityEngine;

namespace Weave
{
    [CreateAssetMenu(menuName = "Weave/Graph")]
    public class Graph : ScriptableObject
    {
        public List<Node> Nodes = new();
    }
}