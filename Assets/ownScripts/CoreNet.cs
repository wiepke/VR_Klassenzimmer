using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

public class CoreNet : MonoBehaviour {
    TcpListener Server { get; set; }

    bool distortionRequested = false;
    bool atmosphereChange = false;
    private GameObject atmosphere;
    String[] studentPlacesToAnimate;
    String stoerung;
    DistortionTrigger trigger;
    ConfigureStudent cs;
    Thread th;
    Socket client;

    // Use this for initialization
    void Start () {
        cs = GameObject.Find("Students").GetComponent<ConfigureStudent>();
        trigger = gameObject.AddComponent<DistortionTrigger>();
         if (MenuDataHolder.ChosenScene == 0)
        {
            MenuDataHolder.ChosenScene = 1;
        }
        if (MenuDataHolder.ChosenScene == 1)
        {
            Application.OpenURL("file:///" + Application.streamingAssetsPath + "/website-control/controlLectureScene.html");
        }
        else if (MenuDataHolder.ChosenScene == 2)
        {
            Application.OpenURL("file:///" + Application.streamingAssetsPath + "/website-control/controlGroupWorkScene.html");
        }
        atmosphere = GameObject.Find("atmosphere");
        
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        Server = new TcpListener(localAddr, 20000);
        Server.Start();
        th = new Thread(new ThreadStart(startlisten));
        th.Start();  
    }

    private void OnDisable()
    {
        th.Abort();
        Server.Stop();
    }

    private void OnApplicationQuit()
    {
        try
        {
            th.Abort();
        }
        catch(Exception)
        {

        }
        Server.Stop();
    }

    private void startlisten()
    {
        
        Dictionary<string, string> map;
        Byte[] buffer = new Byte[256];
        while (true)
        {
            try
            {
                client = Server.AcceptSocket();
                if (client.Connected && !distortionRequested && !atmosphereChange)
                {
                    client.Receive(buffer);
                    map = TransformHTTPToMap(buffer);
                    client.Send(new Byte[4]);
                    string studentPlace;
                    if (map.TryGetValue("student", out studentPlace))
                    {
                        distortionRequested = true;
                        studentPlacesToAnimate = new string[studentPlace.Length / 3];
                        for (int i = 0; i < studentPlacesToAnimate.Length; i++)
                        {
                            studentPlacesToAnimate[i] = studentPlace.Substring(i * 3, 3);
                        }
                        stoerung = map["stoerung"];
                    }
                    else
                    {
                        atmosphereChange = true;
                        map.TryGetValue("noise", out stoerung);
                        Debug.Log("noise" + stoerung);
                    }
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }catch(NullReferenceException)
            {
                Debug.Log("Server already closed");
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (distortionRequested)
        {
            GameObject student = null;
            distortionRequested = false;
            if (studentPlacesToAnimate[0] == "all") {
                GameObject allStudents = GameObject.Find("classroom-scaler/Students/");
                for (int i=0; i<allStudents.gameObject.transform.childCount;i++)
                {
                    string place = allStudents.gameObject.transform.GetChild(i).name;
                    student = cs.getStudent(place);
                    trigger.SetDisturbance(student, place, stoerung);
                }
            }
            else
            {
                foreach(string place in studentPlacesToAnimate){
                    student = cs.getStudent(place);
                    trigger.SetDisturbance(student, place, stoerung);
                }
            }  
        }
        if (atmosphereChange)
        {
            atmosphereChange = false;
            if (stoerung == "up")
            {
                atmosphere.SendMessage("lessNoise", "up");
            }
            if (stoerung == "down")
                atmosphere.SendMessage("lessNoise", "down");
        }
        
    }

    Dictionary<string, string> TransformHTTPToMap(Byte[] buffer)
    {
        Dictionary<string, string> map = new Dictionary<string, string>();
        string incomingRequest = Encoding.UTF8.GetString(buffer);
        List<string> pairs = incomingRequest.Split('=', '?', '&').ToList<string>();
        pairs.Remove(" ");
        pairs.Remove("GET /");
        for (int i = 0; i < pairs.Count / 2; i = i + 2)
        {
            map.Add(pairs[i], pairs[i + 1]);
        }
        return map;
    }
}