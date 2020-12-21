using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Closed Caption Database is an asset that map given AudioClip to a list of lines & time used by the CCManager
/// </summary>
[CreateAssetMenu(fileName = "CCDatabase", menuName = "ClosedCaption/CCDatabase")]
public class CCDatabase : ScriptableObject
{
    [System.Serializable]
    public class Timestamp
    {
        public float StartSecond;
        public string Text;
    }
    
    [System.Serializable]
    public class Entry
    {
        public AudioClip clip = null;
        public Timestamp[] Lines = new Timestamp[0];
    }

    public Entry[] DatabaseEntries = new Entry[0];

    Dictionary<AudioClip, Entry> m_AudioToEntryMap;

    public void BuildMap()
    {
        m_AudioToEntryMap = new Dictionary<AudioClip, Entry>();
        for (int i = 0; i < DatabaseEntries.Length; ++i)
        {
            m_AudioToEntryMap.Add(DatabaseEntries[i].clip, DatabaseEntries[i]);
        }
    }

    public string GetTextEntry(AudioClip clip, float time)
    {
        Entry entry;
        if (m_AudioToEntryMap.TryGetValue(clip, out entry))
        {
            int count = entry.Lines.Length;
            for (int i = 0; i < count; ++i)
            {
                if (i == count - 1 || (entry.Lines[i].StartSecond < time && entry.Lines[i + 1].StartSecond > time))
                {
                    return entry.Lines[i].Text;
                }
            }
        }

        return "CLOSED_CAPTION_MISSING";
    }
}
