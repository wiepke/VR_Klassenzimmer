using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface Hook for DeskScalerEditor-related classes.
/// </summary>
public abstract class DeskPlacer : MonoBehaviour
{
    /// <summary>
    /// Prefab of a desk to instantiate.
    /// </summary>
    public GameObject DeskPrefab = null;

    public int NumberToAdd = 0;

    /// <summary>
    /// Returns where a desk with a given index is supposed to be.
    /// </summary>
    /// <param name="index">The index of the supposed desk.</param>
    /// <param name="desk">Transform of the desk to update.</param>
    public abstract void UpdateDeskTransform(int index, Transform desk);

    /// <summary>
    /// Generates a new desk for a given position index.
    /// </summary>
    /// <param name="index">Position index of the desired desk.</param>
    /// <returns>Newly instantiated desk.</returns>
    private GameObject GenerateDeskAt(int index)
    {
        GameObject desk = Instantiate(DeskPrefab, transform);
        DeskManager dm = desk.GetComponent<DeskManager>();
        if (dm != null) dm.Id = "" + index;
        UpdateDeskTransform(index, desk.transform);
        return desk;
    }

    /// <summary>
    /// Generates a number of new desks and appends them to the existing ones.
    /// </summary>
    /// <param name="n">Number of desks to instantiate.</param>
    public void GenerateDesks(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GenerateDeskAt(transform.childCount);
        }
    }

    /// <summary>
    /// Removes all desks.
    /// </summary>
    public void Clean()
    {
        // TODO: Avoid iterating twice? (Ugly, but low priority)
        List<GameObject> clearList = new List<GameObject>();
        
        foreach (Transform child in transform)
        {
            clearList.Add(child.gameObject);
        }

        foreach (GameObject child in clearList)
        {
            DestroyImmediate(child);
        }
    }

    private IEnumerator RemoveChild(GameObject child)
    {
        DestroyImmediate(child);
        yield return null;
    }

    /// <summary>
    /// Recalculates position of all desks.
    /// </summary>
    public void UpdateDesks()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            UpdateDeskTransform(i, child);
            i++;
        }
    }
}
