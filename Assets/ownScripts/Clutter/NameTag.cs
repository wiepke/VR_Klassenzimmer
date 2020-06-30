using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    private TextMeshPro text;
    public GameObject Student;

    // TODO: Slightly randomize name tag placement, font, etc. ?
    // TODO: Determine front of class, automatically orient accordingly?

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        SetNameTag(Student.GetComponent<StudentController>().Name);
    }

    public void SetNameTag(string name)
    {
        text.text = name;
    }
}
