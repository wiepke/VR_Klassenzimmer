using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

[RequireComponent(typeof(UnityWaiter))]
[RequireComponent(typeof(SocketEventHandler))]
public class NetworkController : MonoBehaviour {
    public AmbientSound AmbientController;

    private UnityWaiter waiter;

    public static SocketEventHandler Handler { get; private set; }

    // Use this for initialization
    void Start()
    {
        waiter = GetComponent<UnityWaiter>();
        Handler = GetComponent<SocketEventHandler>();
        ImpulseController.SetupImpulseController(waiter);
        Handler.Events.RegisterCallback(
            "bootstrap",
            json => Handler.Respond("bootstrap", AllStudentAttributes.ClassToJson())
        );
        Handler.Events.RegisterCallback(
            "behave",
            json => ClassController.DisruptClass(JsonUtility.FromJson<Disruption>(json))
        );
        Handler.Events.RegisterCallback(
            "ambientChange",
            json => AmbientController.SoundLevel(JsonUtility.FromJson<AmbientChange>(json))
        );
        Handler.Events.RegisterCallback(
            "impulseGiven",
            json => ImpulseController.HandleImpulseNetworkRequest(JsonUtility.FromJson<ImpulseGiven>(json))
        );
        Handler.Events.RegisterCallback(
            "themeChange",
            json => ConfigLoader.HandleThemeChange(JsonUtility.FromJson<ThemeChange>(json).theme.ToString())
        );


        // replay controller
        // TO DO: Reduce amount of callbacks
        Handler.Events.RegisterCallback(
            "requestReplays",
            json => ReplayController.GetInstance().SendReplayFilesToSocket()
        );
        Handler.Events.RegisterCallback(
            "startRecording",
            json => ReplayController.GetInstance().StartRecording()
        );
        Handler.Events.RegisterCallback(
            "stopRecording",
            json => ReplayController.GetInstance().StopRecording()
        );
        Handler.Events.RegisterCallback(
            "startLoading",
            json => ReplayController.GetInstance().StartLoading(JsonUtility.FromJson<ReplayJSONData>(json).currentReplay)
        );
        Handler.Events.RegisterCallback(
            "stopLoading",
            json => ReplayController.GetInstance().StopLoading()
        );
        Handler.Events.RegisterCallback(
            "pauseLoading",
            json => ReplayController.GetInstance().PauseLoading()
        );
        Handler.Events.RegisterCallback(
            "continueLoading",
            json => ReplayController.GetInstance().ContinueLoading()
        );
        Handler.Events.RegisterCallback(
            "switchPerspective",
            json => ReplayController.GetInstance().SwitchPerspective(JsonUtility.FromJson<ReplayJSONData>(json).id)
        );
        Handler.Events.RegisterCallback(
            "resetPerspective",
            json => ReplayController.GetInstance().ResetPoV()
        );
        Handler.Events.RegisterCallback(
            "updateReplayTime",
            json => ReplayController.GetInstance().UpdateReplayTime(JsonUtility.FromJson<ReplayJSONData>(json).startTime)
        );


        //StartCoroutine(StartClient()); //Does not work, since cmd is closing instantaniously
    }

    IEnumerator StartClient()
    {
        try
        {
            string path = Environment.CurrentDirectory + @"\Assets\StreamingAssets\website-control~\startCoach.sh";
            Process process =  Process.Start(path);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
        yield return null;
    }
}