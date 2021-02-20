using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xml2CSharp;

public static class GraphToTreeConverter
{
    public readonly struct StructureTreeNode
    {
        public StructureTreeNode(
            string nodeId,
            List<StructureTreeContainer.NextNode> nextNodes,
            string soundFileName,
            NodeType nodeType,
            Role? role,
            NoiseLevel? noiseLevelOfClass,
            WorkingMaterial? materialWorkedWith,
            bool? isSameStudentAsBefore)
        {
            this.nodeId = nodeId;
            this.role = role;
            this.nodeType = nodeType;
            this.soundFileName = soundFileName;
            this.noiseLevelOfClass = noiseLevelOfClass;
            this.nextNodes = nextNodes;
            this.isSameStudentAsBefore = isSameStudentAsBefore;
            this.materialWorkedWith = materialWorkedWith;
        }
        public string nodeId { get; }
        public NodeType nodeType { get; }

        public Role? role { get; }

        public string soundFileName { get; }

        public NoiseLevel? noiseLevelOfClass { get; }

        public WorkingMaterial? materialWorkedWith { get; }

        public bool? isSameStudentAsBefore { get; }

        public List<StructureTreeContainer.NextNode> nextNodes { get; }
    }

    public static Dictionary<string, StructureTreeNode> convert(Graph graph)
    {
        var result = new Dictionary<string, StructureTreeNode>();
        foreach (Node node in graph.Nodes)
        {
            string nodeId = node.Id;
            var nextNodes = new List<StructureTreeContainer.NextNode>();
            NodeType nodeType = SetNodeType(node);
            string soundFileName = null;
            Role? role = null;
            NoiseLevel? noiseLevelOfClass = null;
            WorkingMaterial? materialWorkedWith = null;
            bool? isSameStudentAsBefore = null;

            if (nodeType == NodeType.studentNode)
            {
                //optional
                role = Role.SingleStudent;
                noiseLevelOfClass = NoiseLevel.Calm;
                materialWorkedWith = WorkingMaterial.None;
                isSameStudentAsBefore = false;

                //necessary
                soundFileName = node.NodeLabel.Split(' ')[0];
                if (soundFileName.Contains(":"))
                {
                    soundFileName = soundFileName.Trim(':');
                }
                nextNodes = SetNextNodes(graph, node);
            }
            
            StructureTreeNode stn = new StructureTreeNode(
                    nodeId,
                    nextNodes,
                    soundFileName,
                    nodeType,
                    role,
                    noiseLevelOfClass,
                    materialWorkedWith,
                    isSameStudentAsBefore);
            result.Add(node.Id, stn);
        }

        return result;
    }

    private static List<StructureTreeContainer.NextNode> SetNextNodes(Graph graph, Node node)
    {
        var nextNodes = new List<StructureTreeContainer.NextNode>();
        foreach (Edge edgeIn in graph.Edges)
        {
            if (edgeIn.Source == node.Id)
            {
                if (SetImpulseType(edgeIn) == ImpulseType.None)
                {
                    var nextNode = new StructureTreeContainer.NextNode(edgeIn.Target, ImpulseType.None);
                    nextNodes.Add(nextNode);
                }
                else
                {
                    foreach (Edge edgeOut in graph.Edges)
                    {
                        if (edgeOut.Source == edgeIn.Target)
                        {
                            var nextNode = new StructureTreeContainer.NextNode(edgeOut.Target, SetImpulseType(edgeOut));
                            nextNodes.Add(nextNode);
                        }
                    }
                }
            }
        }
        return nextNodes;
    }

    private static ImpulseType SetImpulseType(Edge edge)
    {
        switch (edge.LineStyle.color)
        {
            case "#0000FF":
            case "#FF0000":
                return ImpulseType.Negative;
            case "#99CC00":
                return ImpulseType.Positive;
            case "#FFCC00":
                return ImpulseType.Neutral;
            case "#000000":
                return ImpulseType.None;
            default:
                return ImpulseType.EndOfLesson;
        }
    }

    private static NodeType SetNodeType(Node node)
    {
        switch (node.Fill.Color)
        {
            case "#FFFFFF":
            case "#0000FF":
                return NodeType.studentNode;
            case "#FF0000":
                return NodeType.negativeImpulse;
            case "#99CC00":
                return NodeType.positiveImpulse;
            case "#FFCC00":
                return NodeType.neutralImpulse;
            default:
                return NodeType.none;
        }
    }
}
