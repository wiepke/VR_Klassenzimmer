using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Collider))]
public class DrawingUtensil : MonoBehaviour
{
    /// <summary>
    /// Thickness of the line. Actual size depends on canvas resolution
    /// </summary>
    [Tooltip("Line Thickness")]
    public int Size = 5;

    /// <summary>
    /// Pen color, actual appearance depends on canvas layer material.
    /// </summary>
    [Tooltip("Pen Color")]
    public Color DrawColor = Color.white;

    private Renderer r;
    private MaterialPropertyBlock mpb;

    /// <summary>
    /// Track last position to interpolate between frames.
    /// </summary>
    public Vector2Int LastPos { get; set; }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Initialize component (for inter-op with editor script)
    /// </summary>
    public void Init()
    {
        r = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        UpdateColor();
    }

    /// <summary>
    /// Updates color value (for live update in editor)
    /// </summary>
    public void UpdateColor()
    {
        r.GetPropertyBlock(mpb);
        mpb.SetColor("_Color", DrawColor);
        r.SetPropertyBlock(mpb);
    }

    /// <summary>
    /// Draws onto the canvas at the intended location.
    /// Override in subclasses to specify different behaviours.
    /// </summary>
    /// <param name="cl">Canvas handle</param>
    /// <param name="at">Location</param>
    public virtual void DrawAt(CanvasLayer cl, Vector2Int at)
    {
        if ((at - LastPos).magnitude < Size)
            cl.DrawSquare(at, this);
        else
            cl.DrawLine(LastPos, at, this);
        LastPos = at;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DrawingUtensil)), CanEditMultipleObjects]
public class DrawingUtensilEditor : Editor
{
    DrawingUtensil du;
    Color prev;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!du)
        {
            du = (DrawingUtensil)target;
            du.Init();
        }

        if (du.DrawColor != prev)
        {
            du.UpdateColor();
        }
    }
}
#endif