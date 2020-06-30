using System.Collections.Generic;
using UnityEngine;

public class SocketEventHandler : MonoBehaviour
{
    public string SocketHost = "localhost";
    public int SocketPort = 10000;
    public string SocketPath = "SockServer";
    
    private SocketServer socket;

    public EventQueue<SocketEvent, string, string> Events = new EventQueue<SocketEvent, string, string>(
        e => e.Type, e => e.Data
    );

    private void Start()
    {
        socket = SocketServer.HostServer(SocketHost, SocketPort, SocketPath);
        socket.SubscribeEventHandler(this);
    }

    public void Respond(string type, JsonData response)
    {
        socket.Emit(new RequestJson(type, response));
    }

    private void Update()
    {
        Events.ProcessAll(); // TODO constrain execution somehow?
    }
}
