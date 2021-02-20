using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Handles rendering to a mask texture, which is in-turn used by a shader to display a overlay material.
/// </summary>
public class CanvasLayer : MonoBehaviour
{
    /// <summary>
    /// Overlay Mask, leave empty for a clear texture
    /// </summary>
    [Tooltip("Optional pre-existing mask.")]
    public Texture2D Mask = null;

    private Renderer r;
    private MaterialPropertyBlock mpb;
    private bool change;

    /// <summary>
    /// Canvas Resolution to be used in case of no pre-existing mask texture being used.
    /// </summary>
    [Tooltip("Resolution of empty canvas.")]
    public Vector2Int CanvasSize = new Vector2Int(512, 512);

    // TODO: Check performance differences, improvements
    // Potential performance improvements:
    // * use coroutines for drawing and/or applying (threadsafe?)
    // * reduce interval of update (i.e. apply only every n-th frame
    // * keep state in CPU, use update pixels instead (lessen communication with GPU)

    // TODO: Add proper color blending (removed old model as it wasn't working as intended)

    // TODO: Add fading to reduce aliasing (circular stencil, proper alpha-fading)

    // TODO: Block/Hide CanvasSize if Mask is non-null (via editor script) for usability

    void Start()
    {
        r = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        UpdateMask();
    }

    public Texture2D UpdateMask()
    {
        if (!Mask)
        {
            Mask = new Texture2D(CanvasSize.x, CanvasSize.y);
            Clear();
        }
        else
        {
            // Copy Mask from given texture (ugly, but works)
            var Copy = new Texture2D(Mask.width, Mask.height);
            Copy.SetPixels(Mask.GetPixels());
            Mask = Copy;
        }

        change = false;
        Mask.Apply();
        Mask.alphaIsTransparency = true;

        r.GetPropertyBlock(mpb);
        mpb.SetTexture("Mask", Mask);
        r.SetPropertyBlock(mpb);

        return Mask;
    }

    private void Update()
    {
        if (change)
        {
            change = false;
            Mask.Apply();
        }
    }

    void Clear()
    {
        change = true;

        for (int i = 0; i < CanvasSize.x; i++)
        {
            for (int j = 0; j < CanvasSize.y; j++)
            {
                Mask.SetPixel(i, j, new Color(1, 1, 1, 0));
            }
        }
    }

    public void DrawSquare(Vector2Int point, DrawingUtensil du)
    {
        DrawSquare(point.x, point.y, du.Size, du.DrawColor);
    }

    public void DrawSquare(int x, int y, int size, Color color)
    {
        change = true;
        for (int i = x - size / 2; i < x + size / 2; i++)
        {
            for (int j = y - size / 2; j < y + size / 2; j++)
            {
                Mask.SetPixel(i, j, color);
            }
        }
    }

    public void DrawLine(Vector2Int p1, Vector2Int p2, DrawingUtensil du)
    {
        DrawLine(p1.x, p1.y, p2.x, p2.y, du.Size, du.DrawColor);
    }

    // Simple implementation, approximates by drawing some dots between endpoints
    public void DrawLine(int x1, int y1, int x2, int y2, int size, Color color)
    {
        change = true;

        var p1 = new Vector2(x1, y1);
        var p2 = new Vector2(x2, y2);
        float dist = (p1 - p2).magnitude;
        int dots = ((int)dist / size) * 2;

        for (float i = 0; i <= dots; i++)
        {
            var v = Vector2.Lerp(p1, p2, i / (float)dots);
            DrawSquare((int)v.x, (int)v.y, size, color);
        }

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CanvasLayer)), CanEditMultipleObjects]
public class CanvasLayerEditor : Editor
{
    // TODO: Dynamically update canvas mask
    public override void OnInspectorGUI()
    {
        var cl = target as CanvasLayer;

        cl.Mask = (Texture2D)EditorGUILayout.ObjectField("Mask", cl.Mask, typeof(Texture2D), true);
        if (!cl.Mask)
        {
            cl.CanvasSize = EditorGUILayout.Vector2IntField("Canvas Size", cl.CanvasSize);
        }
    }
}
#endif
