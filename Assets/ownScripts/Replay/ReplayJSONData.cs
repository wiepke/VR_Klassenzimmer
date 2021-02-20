using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayJSONData : JsonData
{
    public string sceneName;
    public List<string> replayNames = new List<string>();
    public string currentReplay;
    public string id;
    public int startTime;

    public int replayLength;

    public ReplayJSONData(string sceneName, List<string> replayNames)
    {
        this.sceneName = sceneName;
        this.replayNames = replayNames;
    }

    public ReplayJSONData(int replayLength)
    {
        this.replayLength = replayLength;
    }
}
