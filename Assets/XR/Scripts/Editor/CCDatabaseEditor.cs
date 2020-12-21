using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

/// <summary>
/// This will display a window to simplify editing the Closed Caption database when double clicking a CCDatabase asset
/// </summary>
public class CCDatabaseEditor : EditorWindow
{
    CCDatabase m_EditedDatabase;
    int m_EditedEntry = -1;
    Vector2 m_ScrollPosition;

    [OnOpenAsset(1)]
    public static bool HandleDblClick(int instanceID, int line)
    {
        var db = EditorUtility.InstanceIDToObject(instanceID) as CCDatabase;

        if (db != null)
        {
            var ed = GetWindow<CCDatabaseEditor>();
            ed.NewDatabase(db);
            ed.Show();
            return true;
        }
        
        return false;
    }

    void OnSelectionChange()
    {
        var db = Selection.activeObject as CCDatabase;

        if (db != null)
        {
            NewDatabase(db);
        }
    }

    void NewDatabase(CCDatabase db)
    {
        m_EditedEntry = -1;
        m_ScrollPosition = Vector3.zero;
        m_EditedDatabase = db;
    }
    
    void OnGUI()
    {
        if (Event.current.type == EventType.DragUpdated)
        {
            bool valid = false;
            foreach (var obj in DragAndDrop.objectReferences)
            {
                if (obj.GetType() == typeof(AudioClip))
                {
                    valid = true;
                    break;
                }
            }

            DragAndDrop.visualMode = valid ? DragAndDropVisualMode.Link : DragAndDropVisualMode.Rejected;
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            foreach (var obj in DragAndDrop.objectReferences)
            {
                AudioClip clip = obj as AudioClip;
                if (clip != null)
                {
                    var ent = new CCDatabase.Entry();
                    ent.clip = clip;
                    
                    Undo.RecordObject(m_EditedDatabase, "Added new Database Entry");
                    ArrayUtility.Add(ref m_EditedDatabase.DatabaseEntries, ent);
                    EditorUtility.SetDirty(m_EditedDatabase);
                }
            }
        }
        else
        {
            GUILayout.BeginHorizontal();

            m_ScrollPosition = GUILayout.BeginScrollView(m_ScrollPosition, GUILayout.Width(256));

            GUILayout.Label("Drop audio clip here to add them");

            for (int i = 0; i < m_EditedDatabase.DatabaseEntries.Length; ++i)
            {
                var entry = m_EditedDatabase.DatabaseEntries[i];

                GUI.enabled = i != m_EditedEntry;
                if (GUILayout.Button(entry.clip.name))
                {
                    m_EditedEntry = i;
                }
            }

            GUI.enabled = true;

            GUILayout.EndScrollView();

            GUILayout.BeginVertical();

            bool deleteEntry = false;
            if (m_EditedEntry != -1)
            {
                var entry = m_EditedDatabase.DatabaseEntries[m_EditedEntry];
                if (GUILayout.Button("Delete Entry"))
                {
                    deleteEntry = true;
                }

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Time", GUILayout.Width(64));
                EditorGUILayout.LabelField("Line");
                GUILayout.EndHorizontal();

                int deleteLine = -1;
                for (int i = 0; i < entry.Lines.Length; ++i)
                {
                    var line = entry.Lines[i];

                    GUILayout.BeginHorizontal();

                    EditorGUI.BeginChangeCheck();
                    
                    float startInSecond = EditorGUILayout.DelayedFloatField(line.StartSecond, GUILayout.Width(64));
                    string text = EditorGUILayout.DelayedTextField(line.Text);

                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(m_EditedDatabase, "Changed lines");
                        
                        line.StartSecond = startInSecond;
                        line.Text = text;
                        
                        Array.Sort(entry.Lines, (x, y) => x.StartSecond.CompareTo(y.StartSecond));
                        EditorUtility.SetDirty(m_EditedDatabase);
                    }

                    if (GUILayout.Button("-", GUILayout.Width(16)))
                        deleteLine = i;
                    
                    GUILayout.EndHorizontal();
                }

                if (deleteLine != -1)
                {
                    Undo.RecordObject(m_EditedDatabase, "Removed line");
                    ArrayUtility.RemoveAt(ref entry.Lines, deleteLine);
                    EditorUtility.SetDirty(m_EditedDatabase);
                }

                if (GUILayout.Button("New Line"))
                {
                    Undo.RecordObject(m_EditedDatabase, "Added lines");
                    ArrayUtility.Add(ref entry.Lines, new CCDatabase.Timestamp());
                    Array.Sort(entry.Lines, (x, y) => x.StartSecond.CompareTo(y.StartSecond));
                    EditorUtility.SetDirty(m_EditedDatabase);
                }
            }

            if (deleteEntry)
            {
                Undo.RecordObject(m_EditedDatabase, "Deleted Entry");
                ArrayUtility.RemoveAt(ref m_EditedDatabase.DatabaseEntries, m_EditedEntry);
                EditorUtility.SetDirty(m_EditedDatabase);
                m_EditedEntry = -1;
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }
    }
}
