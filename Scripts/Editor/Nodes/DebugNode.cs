using UnityEngine;

namespace Weave
{
    [NodeMenu("Core/Debug")]
    public class DebugNode : Node
    {
        public override void OnCreation()
        {
            NodePort inputPort = new(this, "Input", PortType.Input, null);
            NodePort outputPort = new(this, "Output", PortType.Output, null);
            Ports.Add(inputPort.ID, inputPort);
            Ports.Add(outputPort.ID, outputPort);
        }
    }
}