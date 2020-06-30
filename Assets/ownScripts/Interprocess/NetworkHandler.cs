using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp.Server;

public class NetworkHandler : MonoBehaviour {
    private ClassController classController;
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
            json => { Debug.Log("TODO"); } // AmbientSound.SoundLevel(JsonUtility.FromJson<AmbientChange>(json))
        );
    }
}