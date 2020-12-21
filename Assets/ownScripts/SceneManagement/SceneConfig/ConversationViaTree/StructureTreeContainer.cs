using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Xml2CSharp;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StructureTreeContainer
{
    public Graph graph = new Graph();

    public static Dictionary<string, GraphToTreeConverter.StructureTreeNode> Load(string path)
    {
        StreamReader sr = new StreamReader(path + "/Strukturmodell_Cleaned.txt");
        XmlSerializer serializer = new XmlSerializer(typeof(Graph));
        StringReader reader = new StringReader(sr.ReadToEnd());

        Graph graph = serializer.Deserialize(reader) as Graph;
        reader.Close();

        Dictionary<string, GraphToTreeConverter.StructureTreeNode> structureTreeNode = GraphToTreeConverter.convert(graph);
        return structureTreeNode;

    }
    public struct NextNode
    {
        public string idOfNode { get; }
        public ImpulseType impulseType { get;}
        public NextNode(string idOfNode, ImpulseType edgeType)
        {
            this.idOfNode = idOfNode;
            this.impulseType = edgeType;
        }
    }
}

namespace Xml2CSharp
{
    [XmlRoot(ElementName = "Fill")]
    public class Fill
    {
        [XmlAttribute(AttributeName = "color")]
        public string Color { get; set; }
    }

    [XmlRoot(ElementName = "node")]
    public class Node
    {
        [XmlElement(ElementName = "Fill")]
        public Fill Fill { get; set; }
        [XmlElement(ElementName = "NodeLabel")]
        public string NodeLabel { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "LineStyle")]
    public class LineStyle
    {
        [XmlAttribute(AttributeName = "color")]
        public string color { get; set; }
    }

    [XmlRoot(ElementName = "edge")]
    public class Edge
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }
        [XmlAttribute(AttributeName = "target")]
        public string Target { get; set; }
        [XmlElement(ElementName = "LineStyle")]
        public LineStyle LineStyle { get; set; }
    }

    [XmlRoot(ElementName = "graph")]
    public class Graph
    {
        [XmlElement(ElementName = "node")]
        public List<Node> Nodes { get; set; }
        [XmlElement(ElementName = "edge")]
        public List<Edge> Edges { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

}

