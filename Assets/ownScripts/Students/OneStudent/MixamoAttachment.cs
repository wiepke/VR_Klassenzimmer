using UnityEngine;

public class MixamoAttachment : MonoBehaviour
{
    private Transform attachmentPointRight;
    private Transform attachmentPointLeft;

    private GameObject item;

    private bool isLefty;

    // Start is called before the first frame update
    void Start()
    {
        attachmentPointRight = transform.Find("mixamorig:Hips")
            .Find("mixamorig:Spine").GetChild(0).GetChild(0)
            .Find("mixamorig:RightShoulder").GetChild(0).GetChild(0).GetChild(0);
        

        attachmentPointLeft = transform.Find("mixamorig:Hips")
            .Find("mixamorig:Spine").GetChild(0).GetChild(0)
            .Find("mixamorig:LeftShoulder").GetChild(0).GetChild(0).GetChild(0);

        isLefty = GetComponent<Animator>().GetBool("isLeftHanded"); 
        
        if (isLefty)
        {
            var temp = attachmentPointLeft;
            attachmentPointLeft = attachmentPointRight;
            attachmentPointRight = temp;
        }
    }

    public void attachPrimary(GameObject attachment)
    {
        item = Instantiate(attachment, attachmentPointRight);
        var t  = item.transform;

        switch (item.name) 
        {
            case "Cola Can(Clone)":
                t.localPosition = new Vector3(-0.06f, 0.11f, 0.04f); // TODO right sequence?
                t.localEulerAngles = new Vector3(0, 0, 270);
                break;
            case "pen(Clone)":
                if (isLefty)
                {
                    t.localPosition = new Vector3(0.04f, 0.1f, 0.05f);
                    t.localEulerAngles = new Vector3(158, -20, 0);
                }
                else
                {
                    t.localPosition = new Vector3(-0.04f, 0.1f, 0.05f);
                    t.localEulerAngles = new Vector3(158, 20, 0);
                }
                break;
            case "Apple(Clone)":
                t.localPosition = new Vector3(-0.01f, 0.09f, 0.06f);
                break;
        }
        // Mirror items for lefties. TODO Correct axis?
        Vector3 scale = t.localScale;
        scale.y *= isLefty ? -1 : 1;
    }
    public void attachSecondary(GameObject attachment)
    {
        item = Instantiate(attachment, attachmentPointLeft);
        var t = item.transform;

        switch (item.name) 
        {
            case "Cola Can(Clone)":
                t.localPosition = new Vector3(-0.04f, 0.11f, 0.05f); // TODO right sequence?
                t.localEulerAngles = new Vector3(0, 0, 270);
                break;
            case "pen(Clone)":
                t.localPosition = new Vector3(0.21f, 0.07f, -0.1f);
                t.localEulerAngles = new Vector3(200, 10, 0);
                break;
        }

        // Mirror items for lefties. TODO Correct axis?
        Vector3 scale = t.localScale;
        scale.y *= isLefty ? -1 : 1;
    }

    public void detach()
    {
        if (item) Destroy(item);
        item = null;
    }
}