using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    private TextMeshProUGUI Text;
    public StudentController Student;

    // TODO: Determine front of class, automatically face accordingly?

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        SetNameTag(Student.Name);
    }

    public void SetNameTag(string name)
    {
        Text.text = name;
    }
}
