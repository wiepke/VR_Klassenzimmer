using UnityEngine;

public class breathingStudent : MonoBehaviour {
    private void randomBreathingPoint()
    {
        int rndPointI;
        float rndPointF;
        Animator animator = GetComponent<Animator>();
        float clipLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        int what = name.GetHashCode() % (int) clipLength;
        //Random.InitState(what);
        System.Random rnd = new System.Random();
        rndPointI = rnd.Next(1, (int)clipLength) * what;
        rndPointF = 1f/clipLength * (float)rndPointI;
        animator.Play("breath", 0, rndPointF);
    }
}
