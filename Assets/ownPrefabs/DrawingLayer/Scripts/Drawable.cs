using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles interactions with a canvas layer. Interacting objects should be on the Interactable layer
/// while the canvas should be on the Drawable layer.
/// </summary>
[RequireComponent(typeof(CanvasLayer))]
public class Drawable : MonoBehaviour
{
    private CanvasLayer cl;
    
    // Track active utensils to avoid runtime draw calls
    private Dictionary<GameObject, DrawingUtensil> utensils;

    void Start()
    {
        utensils = new Dictionary<GameObject, DrawingUtensil>();
        cl = GetComponent<CanvasLayer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var du = collision.gameObject.GetComponent<DrawingUtensil>();
        du.LastPos = CalcPosition(collision.contacts[0].point);
        utensils.Add(collision.gameObject, du);
    }

    private void OnCollisionStay(Collision collision)
    {
        var exists = utensils.TryGetValue(collision.gameObject, out var ud);
        if (exists) ud.DrawAt(cl, CalcPosition(collision.contacts[0].point));
    }

    private void OnCollisionExit(Collision collision)
    {
        utensils.Remove(collision.gameObject);
    }


    /// <summary>
    /// Calculates the canvas position of any given world position.
    /// </summary>
    /// <param name="worldPos">A world position</param>
    /// <returns>Canvas position</returns>
    private Vector2Int CalcPosition(Vector3 worldPos)
    {
        // Intended for quad base => [-0.5,0.5]^3 => transform to [0, 1]^3
        Vector3 local = transform.InverseTransformPoint(worldPos) + new Vector3(0.5f, 0.5f, 0);
        return new Vector2Int((int)(cl.CanvasSize.x * local.x), (int)(cl.CanvasSize.y * local.y));
    }
}