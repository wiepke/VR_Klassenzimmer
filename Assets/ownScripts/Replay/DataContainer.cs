using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Replay_Data")]
public class DataContainer
{
    XmlSerializer serializer  = new XmlSerializer(typeof(DataContainer));

    [XmlArray("Recorded_Frames")]
    public List<RecordedFrame> recordedFrames;

    [XmlElement]
    public RecordedFrame currentFrame;

    [XmlElement]
    public string sceneName;

    private XmlDocument xDoc;
    private static string folderPath = Application.dataPath + "/" + "Replays/";

    public string Save(string sceneName)
    {
        string path;

        if(!Directory.Exists(GetSceneFolderPath(sceneName)))
        {
            Directory.CreateDirectory(GetSceneFolderPath(sceneName));
        }
        string replayID = GenerateReplayID(sceneName);
        path = GetSceneFolderPath(sceneName) + "/" + replayID + ".xml";

        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }

        return replayID;
    }

    private string GenerateReplayID(string sceneName)
    {
        string replayID = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString().Replace(":", " ");

        // if file already exists (i.e. it was saved twice within a minute or the saved time coincides and clashes with imported other replay files)
        if (File.Exists(GetSceneFolderPath(sceneName) + "/" + replayID  + ".xml"))
        {
            int suffix = 1;
            while(File.Exists(GetSceneFolderPath(sceneName) + "/" + replayID + "_" + suffix.ToString()+ ".xml"))
            {
                suffix++;
            }
            replayID = replayID + "_" + suffix.ToString();
        }
        return replayID;
    }

    public static string GetFolderPath()
    {
        return folderPath;
    }

    public static string GetSceneFolderPath(string sceneName)
    {
        return folderPath + sceneName;
    }

    public void Append_Save(string path, RecordedFrame frame)
    {
        currentFrame = frame;

        using (var stream = new FileStream(path, FileMode.Append))
        {
            serializer.Serialize(stream, currentFrame);
        }
    }

    // https://stackoverflow.com/questions/44935518/c-sharp-append-object-to-xml-file-using-serialization
    // currently not in use
    public void InsertNewFrame(RecordedFrame recFrame, string pathXML)
    {
        XmlElement frameNode = SerializeToXmlElement(recFrame, "RecordedFrame");

        XmlNode parentNode = xDoc.SelectSingleNode("Replay_Data/Recorded_Frames");

        //necessary for crossing XmlDocument contexts
        XmlNode importNode = parentNode.OwnerDocument.ImportNode(frameNode, true);

        parentNode.AppendChild(importNode);

        xDoc.Save(pathXML);
    }

    public static XmlElement SerializeToXmlElement(object o, string name)
    {
        XmlDocument doc = new XmlDocument();
        using (XmlWriter writer = doc.CreateNavigator().AppendChild())
        {
            new XmlSerializer(o.GetType()).Serialize(writer, o);
        }
        return doc.DocumentElement;
    }

    public static DataContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(DataContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as DataContainer;
        }
    }

}
