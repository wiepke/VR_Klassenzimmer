using UnityEngine;

public class SocketEvent
{
    public string Type;
    public string Data;

    public SocketEvent(string type, string data)
    {
        Type = type; Data = data;
    }

    public T Parse<T>()
    {
        return JsonUtility.FromJson<T>(Data);
    }
}
