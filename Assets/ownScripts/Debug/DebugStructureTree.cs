using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DebugStructureTree : MonoBehaviour 
{

}

#if UNITY_EDITOR
[CustomEditor(typeof(DebugStructureTree))]
[CanEditMultipleObjects]
public class StructureTreeEditorTest : UnityEditor.Editor
{
    // TODO: Fix Debugging utility

    private GameObject student;
    private UnityWaiter waiter;

    private void Awake()
    {
        waiter = GameObject.Find("Controls").GetComponent<UnityWaiter>();
    }

    public override void OnInspectorGUI()
    {
        student = GameObject.Find("Fem2Dark");

        /*
        if (GUILayout.Button("NegativeImpulse", EditorStyles.miniButton))
        {
            ImpulseController.HandleImpulse("negative", waiter);
        }
        if (GUILayout.Button("PositiveImpulse", EditorStyles.miniButton))
        {
            ImpulseController.HandleImpulse("positive", waiter);
        }
        if (GUILayout.Button("Reset Or Setup", EditorStyles.miniButton))
        {
            StructureTreeHandler.LoadStructureTree("Bismarck");
        }
        DrawDefaultInspector();
        */ 
    }
}
#endif