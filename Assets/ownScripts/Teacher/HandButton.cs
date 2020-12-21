using UnityEngine;
using UnityEngine.SceneManagement;

public class HandButton : MonoBehaviour
{
    public string FunctionName ="";

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "LeftHand Controller" || col.name== "RightHand Controller")
        {
            loadScene(FunctionName);
        }
    }

    public void loadScene(string functionName)
    {
        switch (functionName)
        {
            case "LoadFishboneScene":
                loadFishboneScene();
                break;
            case "LoadFrontalScene":
                loadFrontalScene();
                break;
        }
    }

    public void loadFrontalScene()
    {
        Debug.Log("load Frontal Scene");
        MenuDataHolder.ChosenScene = 1;
        MenuDataHolder.repetitionCount++;
        MenuDataHolder.walkedDistance = 0;
        MenuDataHolder.MisbehaviourCount = 0;
        MenuDataHolder.MisbehaviourSolved = 0;
        MenuDataHolder.MisbehaviourSeen = 0;
        SceneManager.LoadScene(1);
    }
    public void loadFishboneScene()
    {
        MenuDataHolder.repetitionCount++;
        MenuDataHolder.ChosenScene = 2;
        MenuDataHolder.walkedDistance = 0;
        MenuDataHolder.MisbehaviourCount = 0;
        MenuDataHolder.MisbehaviourSolved = 0;
        MenuDataHolder.MisbehaviourSeen = 0;
        SceneManager.LoadScene(2);
    }
}
