using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReplayController : MonoBehaviour
{
    private static ReplayController instance;

    private static bool isRecording = false;
    private static bool isLoading = false;
    private bool isPaused = false;

    private static float elapsedTime = 0;
    private float RecordingFPS = 30;

    private static DataContainer replayData;
    private int loadedFrameIndex = 0;
    private int maxloadedFrameIndex = -1;
    private int recordedFrameIndex = 0;
    private string pathCurrentFile;

    private Camera mainCamera;

    private GameObject Player;
    public GameObject replayObjects;

    public vrselfControl vrselfControlScript_Avatar;
    private vrselfControl vrselfControlScript_Replay;
    private Transform leftHand;
    private Transform rightHand;
    private Transform leftHandRenderModel;
    private Transform rightHandRenderModel;
    public Transform interactables;

    private List<Transform> interactableObjectsToSave;
    private Dictionary<GameObject, int> interactableObjectsToLoad = new Dictionary<GameObject, int>();

    public Transform replay_HandModelLeft;
    public Transform replay_HandModelRight;

    public bool enablePerspectiveChange = false;
    private GameObject proxyStudentPerspective;
    private GameObject lastPerspective_Student;
    private Transform oldParentCamera;
    private Vector3 oldPositionStudent;
    private Vector3 oldRotationStudent;

    // used for GUI editor and debugging
    [HideInInspector]
    public static string activeScene;
    [HideInInspector]
    public List<string> savedReplays_activeScene = new List<string>();
    [HideInInspector]
    public int indexActiveScene;

    private void Awake()
    {
        instance = this;

        Player = GameObject.Find("Player");
        replayObjects.transform.SetParent(Player.transform);

        if (vrselfControlScript_Avatar == null)
        {
            vrselfControlScript_Avatar = Player.GetComponentInChildren<vrselfControl>();
        }

        vrselfControlScript_Replay = replayObjects.GetComponentInChildren<vrselfControl>();
        vrselfControlScript_Replay.enabled = true;


        leftHand = TransformDeepChildExtension.FindDeepChild(Player.transform, "LeftHand");
        rightHand = TransformDeepChildExtension.FindDeepChild(Player.transform, "RightHand");
        activeScene = SceneManager.GetActiveScene().name;
        GetReplays();

        interactableObjectsToSave = new List<Transform>();

        if(interactables == null)
        {
            try
            {
                interactables = GameObject.Find("Interactables").transform;
            }
            catch
            {
                Debug.LogWarning("No interactables to be recorded have been specified. Hence, there will not be any replay of objects interacted with.");
            }
        }

        foreach (Transform interactableObject in interactables)
        {
            interactableObjectsToSave.Add(interactableObject);
        }

        proxyStudentPerspective = new GameObject();
        proxyStudentPerspective.name = "perspectiveChangeToStudent";
    }

    public static ReplayController GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        StartCoroutine(FindHandModels());
    }

    public void SendReplayFilesToSocket()
    {
        NetworkController.Handler.Respond("replay", new ReplayJSONData(SceneManager.GetActiveScene().name, savedReplays_activeScene));
    }

    private void GetReplays()
    {
        string pathFolder_activeScene = DataContainer.GetSceneFolderPath(activeScene);

        if (Directory.Exists(pathFolder_activeScene))
        {
            DirectoryInfo assetsDirectoryPath = new DirectoryInfo(pathFolder_activeScene);
            FileInfo[] assetsInfo = assetsDirectoryPath.GetFiles("*.xml", SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in assetsInfo)
            {           
                savedReplays_activeScene.Add(fileInfo.Name);
            }
        }
    }

    // will only record behaviour if recording was started
    public void RecordStudentBehaviour(BehaviourController bc, string performedBehaviour)
    {
        if (isRecording)
        {
            replayData.recordedFrames[recordedFrameIndex].savedBehaviours.Add(new Saved_Behaviour(bc.name, performedBehaviour));
        }
    }

    public void StartRecording()
    {
        isRecording = true;
        StartCoroutine(FindHandModels());

        replayData = new DataContainer();
        replayData.recordedFrames = new List<RecordedFrame>();
        replayData.sceneName = SceneManager.GetActiveScene().name;

        RecordedFrame startFrame = new RecordedFrame();

        startFrame.SetupRecordData(vrselfControlScript_Avatar, 0f, leftHandRenderModel, rightHandRenderModel);
        //RecordingOptimizer.InitializeRecOptimizer(interactableObjects);
        startFrame.SaveObjectTransforms(interactableObjectsToSave);

        replayData.recordedFrames.Add(startFrame);

        StartCoroutine(RecordAtIntervals(1 / RecordingFPS));
        ClassController.onStudentBehaviour += RecordStudentBehaviour;
    }

    public void StopRecording()
    {
        StopCoroutine(RecordAtIntervals(1 / RecordingFPS));

        string replayID = replayData.Save(SceneManager.GetActiveScene().name);
        isRecording = false;
        savedReplays_activeScene.Add(replayID + ".xml");

        ClassController.onStudentBehaviour -= RecordStudentBehaviour;
        recordedFrameIndex = 0;
    }

    IEnumerator RecordAtIntervals(float secondsBetweenFrameCapture)
    {
        while (isRecording)
        {
            yield return new WaitForSeconds(secondsBetweenFrameCapture);

            elapsedTime += secondsBetweenFrameCapture;
            RecordFrame(elapsedTime);
            recordedFrameIndex++;
        }

    }

    private void RecordFrame(float elapsedTime)
    {
        RecordedFrame currentFrame = new RecordedFrame();
        currentFrame.SetupRecordData(vrselfControlScript_Avatar, elapsedTime, leftHandRenderModel, rightHandRenderModel);
        currentFrame.SaveObjectTransforms(interactableObjectsToSave);
        replayData.recordedFrames.Add(currentFrame);
    }

    public void StopLoading()
    {
        isLoading = false;

        vrselfControlScript_Replay.enabled = false;
        replayObjects.SetActive(false);
        replay_HandModelLeft.gameObject.SetActive(false);
        replay_HandModelRight.gameObject.SetActive(false);

        StopCoroutine(LoadFrames());
    }

    public void StartLoading(string path)
    {
        replayData = new DataContainer();
        replayData = DataContainer.Load(DataContainer.GetSceneFolderPath(activeScene) + "/" + path);

        NetworkController.Handler.Respond("setReplayLength", new ReplayJSONData((int) replayData.recordedFrames[replayData.recordedFrames.Count-1].timeStamp));

        pathCurrentFile = path;
        replayObjects.SetActive(true);
        replay_HandModelLeft.gameObject.SetActive(true);
        replay_HandModelRight.gameObject.SetActive(true);

        isLoading = true;
        vrselfControlScript_Replay.enabled = true;

        PopulateInteractablesToLoad(replayData);
        maxloadedFrameIndex = replayData.recordedFrames.Count;
        InitializeInteractableTransforms(replayData.recordedFrames[0]);

        loadedFrameIndex = 0;

        StartCoroutine(LoadFrames());
    }

    public void UpdateReplayTime(int startTime)
    {

        for (int index = 0; index < replayData.recordedFrames.Count; index++)
        {
            if (replayData.recordedFrames[index].timeStamp > startTime)
            {
                loadedFrameIndex = index;
                break;
            }
        }

    }

    void PopulateInteractablesToLoad(DataContainer replayData)
    {
        interactableObjectsToLoad.Clear();

        List<Saved_Transform> savedTransforms = replayData.recordedFrames[0].savedInteractables;
        int numTransformsToLoad = savedTransforms.Count;

        // Assign the transforms that will be replayed in the scene to the indices under which they are saved in the save file
        // in order to exclude gameobjects that were recorded but are not present in the scene anymore
        for (int indexTransformInSave = 0; indexTransformInSave < numTransformsToLoad; indexTransformInSave++)
        {
            Saved_Transform currentSavedTransform = savedTransforms[indexTransformInSave];
            GameObject gameObjectInScene = interactables.transform.Find(currentSavedTransform.name_Gameobject).gameObject; // quite costly; only used on start of loading a file but maybe refactor

            if (gameObjectInScene != null)
            {
                interactableObjectsToLoad.Add(gameObjectInScene, indexTransformInSave);
            }
            else
            {
                Debug.LogWarning(currentSavedTransform.name_Gameobject + " could not be found in scene and hence cannot be loaded");
            }
        }
    }

    public void PauseLoading()
    {
        if (isLoading == true)
        {
            StopCoroutine(LoadFrames());
            isLoading = false;
            isPaused = true;
        }
    }

    public void ContinueLoading()
    {
        if (isPaused == true)
        {
            isLoading = true;
            StartCoroutine(LoadFrames());
            isPaused = false;
        }
    }

    IEnumerator LoadFrames()
    {
        float timeBetweenFrameSaves;
        float executionTime;
        System.Diagnostics.Stopwatch executionTimeStopwatch = new System.Diagnostics.Stopwatch();

        while (isLoading)
        {
            executionTimeStopwatch.Reset();
            executionTimeStopwatch.Start();
            RecordedFrame currentFrame = replayData.recordedFrames[loadedFrameIndex];

            if (loadedFrameIndex == maxloadedFrameIndex)
            {

                StopLoading();
                break;
            }

            RecordedFrame nextFrame = new RecordedFrame();

            try
            {
                nextFrame = replayData.recordedFrames[loadedFrameIndex + 1];
            }
            catch (ArgumentOutOfRangeException e)
            {
                StopLoading();
                break;
            }

            timeBetweenFrameSaves = nextFrame.timeStamp - currentFrame.timeStamp;

            vrselfControlScript_Replay.ReconstructFrame(currentFrame);
            ReplayHandMovement(currentFrame);

            ReplayInteractableObjectsMovement(nextFrame, timeBetweenFrameSaves);

            if (currentFrame.savedBehaviours.Count > 0)
            {
                foreach (Saved_Behaviour behaviour in currentFrame.savedBehaviours)
                {
                    ReplayBehaviour(behaviour.student, behaviour.performedBehaviour);
                }
            }

            executionTimeStopwatch.Stop();
            executionTime = executionTimeStopwatch.ElapsedMilliseconds / 1000f;

            yield return new WaitForSeconds(timeBetweenFrameSaves - executionTime);

            loadedFrameIndex++;
        }
        yield return null;
    }

    void InitializeInteractableTransforms(RecordedFrame startFrame)
    {
        foreach (KeyValuePair<GameObject, int> objectToLoad in interactableObjectsToLoad)
        {
            GameObject currentGameObject = objectToLoad.Key;
            currentGameObject.transform.position = startFrame.savedInteractables[objectToLoad.Value].GetReconstructedPosition();
            currentGameObject.transform.eulerAngles = startFrame.savedInteractables[objectToLoad.Value].GetReconstructedRotation();
        }
    }

    void ReplayInteractableObjectsMovement(RecordedFrame nextFrame, float timeFrame)
    {
        foreach (KeyValuePair<GameObject, int> objectToLoad in interactableObjectsToLoad)
        {
            GameObject currentGameObject = objectToLoad.Key;
            Vector3 targetPosition = nextFrame.savedInteractables[objectToLoad.Value].GetReconstructedPosition();
            Vector3 targetRotation = nextFrame.savedInteractables[objectToLoad.Value].GetReconstructedRotation();

            StartCoroutine(InterpolatBetweenFrames(currentGameObject, targetPosition, targetRotation, timeFrame));
        }
    }

    IEnumerator InterpolatBetweenFrames(GameObject obj, Vector3 targetPosition, Vector3 targetRotation, float timeFrame)
    {
        float timeElapsed = 0;

        while (timeElapsed < timeFrame)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, timeElapsed / timeFrame);
            obj.transform.eulerAngles = Vector3.Lerp(obj.transform.eulerAngles, targetRotation, timeElapsed / timeFrame);

            timeElapsed += Time.deltaTime;      
            yield return null;
        }
    }


    void ReplayHandMovement(RecordedFrame currentFrame)
    {
        if (currentFrame.leftHandRenderModel != null)
        {
            replay_HandModelLeft.position = currentFrame.leftHandRenderModel.GetReconstructedPosition();
            replay_HandModelLeft.eulerAngles = currentFrame.leftHandRenderModel.GetReconstructedRotation();
        }
        if (currentFrame.rightHandRenderModel != null)
        {
            replay_HandModelRight.position = currentFrame.rightHandRenderModel.GetReconstructedPosition();
            replay_HandModelRight.eulerAngles = currentFrame.rightHandRenderModel.GetReconstructedRotation();
        }
    }

    void ReplayBehaviour(string studentName, string performedBehaviour)
    {
        foreach (GameObject student in AllStudentAttributes.allStudents)
        {
            if (student.name == studentName)
            {
                ClassController.DisruptStudent(student, performedBehaviour);
                return;
            }
        }
    }

    // refactor here
    public void TakePoVofStudent(GameObject student)
    {
        // reset previous impersonation and movements
        if (lastPerspective_Student != null && lastPerspective_Student != student)
        {
            Destroy(lastPerspective_Student.GetComponent<vrselfControl>());
            Destroy(lastPerspective_Student.GetComponent<Animator>());

            lastPerspective_Student.transform.position = oldPositionStudent;
            lastPerspective_Student.transform.eulerAngles = oldRotationStudent;
        }
        vrselfControlScript_Avatar.gameObject.SetActive(false);

        oldPositionStudent = student.transform.position;
        oldRotationStudent = student.transform.eulerAngles;

        mainCamera = Camera.main;
        oldParentCamera = mainCamera.transform.parent;

        proxyStudentPerspective.transform.position = new Vector3(student.transform.position.x, 1.2f, student.transform.position.z);
        proxyStudentPerspective.transform.LookAt(vrselfControlScript_Avatar.transform);
        mainCamera.transform.SetParent(proxyStudentPerspective.transform, false);

        vrselfControlScript_Avatar.enabled = false;
        vrselfControl vrselfControl_Student = student.AddComponent<vrselfControl>();
        AssignVRControlVariables(vrselfControl_Student);

        Animator anim = student.GetComponent<Animator>();
        anim.runtimeAnimatorController = vrselfControlScript_Avatar.transform.GetComponent<Animator>().runtimeAnimatorController;

        // FIXME: MAKE AVATAR FACE IN SAME DIRECTION AS CAMERA       
        Transform FollowHead = mainCamera.transform.GetChild(0);

        lastPerspective_Student = student;
    }


    public void SwitchPerspective(string id)
    {
        foreach(GameObject student in AllStudentAttributes.allStudents)
        {
            if(student.name == id)
            {
                TakePoVofStudent(student);
            }
        }
    }

    public void ResetPoV()
    { 
        if (lastPerspective_Student == null)
        {
            return;
        }
        else
        {
            lastPerspective_Student.transform.position = oldPositionStudent;
            lastPerspective_Student.transform.eulerAngles = oldRotationStudent;
        }
        vrselfControlScript_Avatar.gameObject.SetActive(false);

        Destroy(lastPerspective_Student.GetComponent<vrselfControl>());
        Destroy(lastPerspective_Student.GetComponent<Animator>());

        mainCamera.transform.SetParent(oldParentCamera, false);
        vrselfControlScript_Avatar.enabled = true;
    }

    void AssignVRControlVariables(vrselfControl vrselfControl_Student)
    {
        vrselfControl_Student.ikActive = true;
        vrselfControl_Student.headHeight = vrselfControlScript_Avatar.headHeight;
        vrselfControl_Student.headWidth = vrselfControlScript_Avatar.headWidth;
        vrselfControl_Student.leftControler = vrselfControlScript_Avatar.leftControler;
        vrselfControl_Student.rightControler = vrselfControlScript_Avatar.rightControler;

        Transform lookObj = vrselfControlScript_Avatar.lookObj;
        vrselfControl_Student.lookObj = lookObj;
    }


    /*
    // only for debugging and development phase
    void OnValidate()
    {
        if (enablePerspectiveChange == false)
        {
            ResetPoV();
        }
    }
    */

    // hands are instantiated in SteamVR script --> do not want to modify SteamVR scripts to send the reference here but instead search for it till found
    // to be refactored
    IEnumerator FindHandModels()
    {
        while (leftHandRenderModel == null || rightHandRenderModel == null)
        {
            leftHandRenderModel = leftHand.transform.Find("LeftRenderModel Slim(Clone)");
            rightHandRenderModel = rightHand.transform.Find("RightRenderModel Slim(Clone)");

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(ReplayController))]
public class ReplayControllerDebug : Editor
{
    float minVal = 0;
    float maxVal = 100;

    public override void OnInspectorGUI()
    {
        var replayController = (ReplayController)target;

        DrawDefaultInspector();

        if (!Application.isPlaying) return;

        EditorGUILayout.Separator();

        if (GUILayout.Button("Start Recording"))
        {
            replayController.StartRecording();
            Debug.Log("Recording started");
        }
        if (GUILayout.Button("Stop Recording"))
        {
            replayController.StopRecording();
            Debug.Log("Recording stopped");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        GUIContent replayLabel = new GUIContent("Replay to load");
        replayController.indexActiveScene = EditorGUILayout.Popup(replayLabel, replayController.indexActiveScene, replayController.savedReplays_activeScene.ToArray());


        EditorGUILayout.MinMaxSlider("Replay range", ref minVal, ref maxVal, 0, 100);


        if (GUILayout.Button("Start Loading"))
        {
            replayController.StartLoading(replayController.savedReplays_activeScene[replayController.indexActiveScene]);
        }

        if (GUILayout.Button("Stop Loading"))
        {
            replayController.StopLoading();
        }

        if (GUILayout.Button("Pause Loading"))
        {
            replayController.PauseLoading();
        }
        if (GUILayout.Button("Resume Loading"))
        {
            replayController.ContinueLoading();
        }

        if (replayController.enablePerspectiveChange == true)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Through the eyes of a virtual student");
            if (EditorGUILayout.DropdownButton(new GUIContent("Select a student ..."), FocusType.Passive))
            {
                var menu = new GenericMenu();

                foreach (GameObject student in AllStudentAttributes.allStudents)
                {
                    menu.AddItem(new GUIContent(student.name), false, () => replayController.TakePoVofStudent(student));
                }

                menu.ShowAsContext();
            }
        }


    }


}
#endif
