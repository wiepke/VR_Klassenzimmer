using UnityEngine;

public class thrower : MonoBehaviour {
    GameObject theBall;
    Transform attachmentPoint;

    public void ThrowBall()
    {
        theBall = Resources.Load<GameObject>("PaperBall2");
        attachmentPoint = transform.Find("mixamorig:Hips").transform.
            GetChild(2).    //Spine
            GetChild(0).    //Spine1
            GetChild(0).    //Spine2
            GetChild(2).    //RightShoulder
            GetChild(0).    //RightArm
            GetChild(0).    //RightForeArm
            GetChild(0).    //RightHand
            GetChild(1).    //RightHandMiddle1
            GetChild(0);    //RightHandMiddle2
        theBall = Instantiate(theBall, attachmentPoint.position, Quaternion.identity);
        theBall.GetComponent<BallScript>().ReleaseMe();
    }

}
