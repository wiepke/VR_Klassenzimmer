using System;

/// <summary>
/// Carrier type for requests to the client.
/// </summary>

[Serializable]
public class RequestJson : JsonData
{
    public string type;
    public JsonData payload;

    public RequestJson(string type, JsonData payload)
    {
        this.type = type; this.payload = payload;
    }

    public override string ToJson()
    {
        // somewhat awkward solution because JsonUtilities might be stupid
        return "{\"type\":\"" + type + "\",\"payload\":" + payload.ToJson() + "}";
    }
}
