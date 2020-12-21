using UnityEngine;
using System.Collections.Generic;

public class SoundLoader 
{
    private static SoundLoader _instance;

    // Singleton instance
    public static SoundLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SoundLoader();
                _instance.LoadAllClips();
            }
            return _instance;
        }
    }

    static string[] MaleMaleClips = { "Aufnahme #11", "Aufnahme #12", "Aufnahme #13" };
    static string[] MaleFemaleClips = { "Aufnahme #13" };
    static string[] MaleFemaleWorkClips = { "Situation 1" }; // this one appears in male male work as well!
    static string[] MaleMaleWorkClips = {
        "Situation 1", "Situation 2", "Situation 3", "Situation 4", "Situation 5", "Situation 6",
        "Situation 7", "Situation 8", "Situation 9"
    };
    static string[] FemaleFemaleClips = {
        "Gespraech - Bauernhof-Fuchs", "Gespraech - unnoetig", "Gespraech - Kino-BibiBlogsBerg"
    };
    static string[] FemaleFemaleWorkClips = {
        "Aufgabe - Aufgabenskizze 3", "Aufgabe - fertig-mit-Drittens", "Aufgabe - Mindmap-Aufbau",
        "Aufgabe - roter-Filzstift", "Aufgabe - Uebersicht-Fuchs", "Aufgabe - Mindmap-Anfang",
        "Aufgabe - schnell-vergleichen"
    };
    static string[] FemaleQuestionClips = {
        "noch mal erklaeren-female", "wozu brauchen wir das-female", "Anton lenkt ab-female", // "kapier ich nicht-female",
        "kann ich was essen-female", "Hunger-female"
    };

    static string[] MaleQuestionClips = {
        "kapier ich nicht-male", "frueher Schluss machen-male", "Herr Schmidt-male", "Pause machen-male"
    };

    static string[] ClassAmbientClips = {
        "stillarbeit2", "klassenraum_pause2"
    };

    static string[] ExperimentAmbientClips = {

    };

    public AudioClip AppleBite;
    public AudioClip JustBite;
    public AudioClip PunchSound;
    public AudioClip OpenSodaCan;
    public List<AudioClip> MaleFemaleDistortion = new List<AudioClip>();
    public List<AudioClip> MaleMaleDistortion = new List<AudioClip>();
    public List<AudioClip> FemaleFemaleDistortion = new List<AudioClip>();
    public List<AudioClip> MaleFemaleWork = new List<AudioClip>();
    public List<AudioClip> MaleMaleWork = new List<AudioClip>();
    public List<AudioClip> FemaleFemaleWork = new List<AudioClip>();
    public List<AudioClip> FemaleQuestions = new List<AudioClip>();
    public List<AudioClip> MaleQuestions = new List<AudioClip>();
    public List<AudioClip> ClassAmbient = new List<AudioClip>();
    public List<AudioClip> ExperimentAmbient = new List<AudioClip>();

    private void LoadClips(List<AudioClip> container, string[] sources, string path = "soundFiles")
    {
        foreach (string clip in sources) 
            container.Add(Resources.Load<AudioClip>(path + "/" + clip));
    }

    private void LoadAllClips()
    {
        LoadClips(MaleMaleDistortion, MaleMaleClips);
        LoadClips(MaleFemaleDistortion, MaleFemaleClips);
        LoadClips(MaleFemaleWork, MaleFemaleWorkClips);
        LoadClips(MaleMaleWork, MaleMaleWorkClips);
        LoadClips(FemaleFemaleDistortion, FemaleFemaleClips);
        LoadClips(FemaleFemaleWork, FemaleFemaleWorkClips);
        LoadClips(FemaleQuestions, FemaleQuestionClips);
        LoadClips(MaleQuestions, MaleQuestionClips);
        LoadClips(ClassAmbient, ClassAmbientClips);
        LoadClips(ExperimentAmbient, ExperimentAmbientClips);

        AppleBite = Resources.Load<AudioClip>("soundFiles/AppleBite");
        JustBite = Resources.Load<AudioClip>("soundFiles/Bite");

        OpenSodaCan = Resources.Load<AudioClip>("soundFiles/Soda-can-opening-sound-effect");
        PunchSound = Resources.Load<AudioClip>("soundFiles/punch");
    }
}