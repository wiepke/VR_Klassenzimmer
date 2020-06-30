using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentAnimationEvents : MonoBehaviour
{
    private Animator animator;
    private StudentController sc;

    private Transform right, left, release;
    private GameObject whatToHold;

    public GameObject Projectile;
    public StateMachineBehaviour CurrentSMB;

    void Start()
    {
        animator = GetComponent<Animator>();
        sc = transform.parent.GetComponent<StudentController>();
        
        Transform spine = transform.Find("mixamorig:Hips").GetChild(2).GetChild(0).GetChild(0);

        right = spine.Find("mixamorig:RightShoulder").GetChild(0).GetChild(0).GetChild(0);
        left = spine.Find("mixamorig:LeftShoulder").GetChild(0).GetChild(0).GetChild(0);
        release = right.GetChild(1).GetChild(0);
    }

    public void Attach(bool isRight, GameObject toAttach)
    {
        Transform attachTo = isRight ? right : left;
        whatToHold = Instantiate(toAttach, attachTo);
        Transform wthTrans = whatToHold.transform;

        switch (whatToHold.name)
        {
            case "Cola Can(Cola)": // TODO Use empty attachment point object and mirroring instead?
                wthTrans.localPosition = new Vector3(0.21f, 0.07f, isRight ? 0.1f : -0.1f);
                wthTrans.localEulerAngles = new Vector3(0, 0, 90);
                break;
            case "pen(Clone)":
                wthTrans.localPosition = new Vector3(0.21f, isRight ? 0.12f : 0.07f, isRight ? -0.07f : 0.01f);
                wthTrans.localEulerAngles = new Vector3(0, 0, (isRight ? 200 : 180));
                break;
        }
    }

    public void Detach()
    {
        if (whatToHold != null) Destroy(whatToHold);
        whatToHold = null;
    }

    public void PlayAudio()
    {
        ((PlayAudioVariation)CurrentSMB)?.PlayAudio(sc.IsMale);
    }

    public void PlayClip(string clip)
    {
        ((PlayAudioClip)CurrentSMB)?.PlayClip(clip);
    }

    public void ThrowBall()
    {
        GameObject bullet = Instantiate(Projectile, release.position, Quaternion.identity);
        bullet.GetComponent<ReleaseObject>().ReleaseMe();
    }
}
