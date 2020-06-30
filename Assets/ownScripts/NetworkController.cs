using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp.Server;

public class NetworkController : MonoBehaviour {
    public AmbientSound AmbientController;
    private ClassController classController;

    private TcpListener Server { get; set; }

    private bool distortionRequested = false;
    private bool atmosphereChange = false;

    private String[] studentPlacesToAnimate;
    private String stoerung;
    
    private SocketEventHandler handler;
    
    // Use this for initialization
    void Start()
    {
        classController = GetComponent<ClassController>();
        handler = GetComponent<SocketEventHandler>();

        handler.Events.RegisterCallback(
            "bootstrap",
            json => handler.Respond("bootstrap", StudentController.ClassToJson())
        );
        handler.Events.RegisterCallback(
            "behave",
            json => classController.DisruptClass(JsonUtility.FromJson<Disruption>(json))
        );
        handler.Events.RegisterCallback(
            "ambientChange",
            json => AmbientController.SoundLevel(JsonUtility.FromJson<AmbientChange>(json))
        );
    }
}