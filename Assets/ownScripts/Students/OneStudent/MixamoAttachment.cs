using UnityEngine;

public class MixamoAttachment : MonoBehaviour
{
    private Transform attachmentPointRight;
    private Transform attachmentPointLeft;

    private GameObject item;

    private bool IsLefty;

    // Start is called before the first frame update
    void Start()
    {
        attachmentPointRight = transform.Find("mixamorig:Hips")
            .Find("mixamorig:Spine").GetChild(0).GetChild(0)
            .Find("mixamorig:RightShoulder").GetChild(0).GetChild(0).GetChild(0);


        attachmentPointLeft = transform.Find("mixamorig:Hips")
            .Find("mixamorig:Spine").GetChild(0).GetChild(0)
            .Find("mixamorig:LeftShoulder").GetChild(0).GetChild(0).GetChild(0);

        IsLefty = GetComponent<Animator>().GetBool("isLeftHanded"); 
        
        if (IsLefty)
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
                t.localPosition = new Vector3(0.051f, 0.105f, 0.04f); // TODO right sequence?
                t.localEulerAngles = new Vector3(0, 0, 90);
                break;
            case "pen(Clone)":
                t.localPosition = new Vector3(-0.1049f, 0.1188f, 0.0196f);
                t.localEulerAngles = new Vector3(11, 230, 0);
                break;
        }

        // Mirror items for lefties. TODO Correct axis?
        //Vector3 scale = t.localScale;
        //scale.y *= IsLefty ? -1 : 1;
    }
    public void attachSecondary(GameObject attachment)
    {
        item = Instantiate(attachment, attachmentPointLeft);
        var t = item.transform;

        switch (item.name) 
        {
            case "Cola Can(Clone)":
                t.localPosition = new Vector3(-0.056f, 0.138f, 0.044f); // TODO right sequence?
                t.localEulerAngles = new Vector3(0, 0, -90);
                break;
            case "pen(Clone)":
                t.localPosition = new Vector3(0.0847f, 0.1631f, -0.0446f);
                t.localEulerAngles = new Vector3(10, 166, 0);
                break;
        }

        // Mirror items for lefties. TODO Correct axis?
        //Vector3 scale = t.localScale;
        //scale.y *= IsLefty ? -1 : 1;
    }

    public void detach()
    {
        if (item) Destroy(item);
        item = null;
    }
}