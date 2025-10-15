using UnityEngine;

namespace Weave
{
    public class StartNode : Node
    {
        public override void OnCreation()
        {
            NodePort outPort = new(this, "Start", PortType.Output, null);
            Ports.Add(outPort.ID, outPort);
        }
    }
}