using SyncNode.Models;

namespace SyncNode.Service
{
    public class ApiNodesService
    {
        private List<NodeInfo> RegisteredNodes { get; set; } = new List<NodeInfo>();

        public void AddNode(NodeInfo nodeInfo)
        {
            RegisteredNodes.Add(nodeInfo);
        }

        public bool FindNode(string ipAddress)
        {
            return RegisteredNodes.Any(x => x.IpAddress == ipAddress);
        }

        public NodeInfo? GetNextNode(string ipAddress)
        {
            var nodeInfo = RegisteredNodes.FirstOrDefault(x => x.IpAddress == ipAddress);

            if (nodeInfo == null)
                return null;

            if (RegisteredNodes.IndexOf(nodeInfo) == RegisteredNodes.Count - 1)
            {
                return RegisteredNodes[0];
            }

            return RegisteredNodes[RegisteredNodes.IndexOf(nodeInfo) + 1];
        }
    }
}
