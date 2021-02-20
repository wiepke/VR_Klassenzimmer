using System.Collections.Generic;
using UnityEngine;
public static class SpecialBehaviours
{
    public static readonly string[] _goodBehaviours = { "Idle", "Writing", "PlakatSolo", "PlakatPartner", "Impulse" };
    public static readonly string[] _pairActions = { "Hitting", "Chatting", "PlakatPartner" };
    public static readonly string[] _legAnimationSupported = { "AskQuestion", "Writing", "Eating", "RaiseArm", "Drinking", "Throwing", "Playing", "Idle" };
    public static readonly string[] _extraLongAnimation = { "LegLoop", "Idle", "WorkingPoster" }; // TODO: Remove LegLoop, has no business being here!
    public static readonly string[] _IKallowed = { "Idle", "AskQuestion", "RaiseArm", "Impulse" };

    public static readonly string[] _level0Distortion = { "Eating", "Drinking", "Staring" };
    public static readonly string[] _level1Distortion = { "Playing", "Tapping" };
    public static readonly string[] _level2Distortion = { "Hitting", "Chatting", "Throwing" };

    private static Dictionary<string, Behaviour> Dict;

    public static readonly string[] BehaviourIDs =
    {
        "Idle", "Writing", "AskQuestion", "RaiseArm", "PlakatSolo", "PlakatPartner", "Impulse",
        "Eating", "Drinking", "Staring", "Playing", "Tapping", "Hitting", "Chatting", "Throwing",
        "LegLoop", "WorkingPoster"
    };

    public static Behaviour GetBehaviour(string id)
    {
        if (Dict == null)
        {
            // Instantiate singleton dictionary
            Dict = new Dictionary<string, Behaviour>();
            foreach (string key in BehaviourIDs) Dict.Add(key, new Behaviour(key));
            foreach (string key in _goodBehaviours) Dict[key].Good = true;
            foreach (string key in _pairActions) Dict[key].Pair = true;
            foreach (string key in _legAnimationSupported) Dict[key].SupportLegs = true;
            foreach (string key in _extraLongAnimation) Dict[key].ExtraLong = true;
            foreach (string key in _level0Distortion) Dict[key].Level = 0;
            foreach (string key in _level1Distortion) Dict[key].Level = 1;
            foreach (string key in _level2Distortion) Dict[key].Level = 2;
            foreach (string key in _IKallowed) Dict[key].IK = true;
        }

        if (!Dict.TryGetValue(id, out Behaviour res))
            throw new KeyNotFoundException("No behaviour with ID " + id + " is known.");

        return res;
    }
}

[System.Serializable]
public class Behaviour
{
    public string Id;
    public bool Good = false, Pair = false, SupportLegs = false, ExtraLong = false, IK = false;
    public int Level = -1;

    public Behaviour(string id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return UnityEngine.JsonUtility.ToJson(this);
    }
}