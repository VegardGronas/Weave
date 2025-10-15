using System;
using UnityEngine;

namespace Weave
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeMenuAttribute : Attribute
    {
        public string Category;
        public NodeMenuAttribute(string category) => Category = category;
    }
}