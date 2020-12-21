using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this class e.g. for stresstesting/testing performance improvements as
/// both methods below are quite performance heavy (currently).
/// </summary>
[RequireComponent(typeof(CanvasLayer))]
public class DrawTest : MonoBehaviour
{
    private CanvasLayer cl;

    private Vector2Int CanvasSize { get { return cl.CanvasSize; } }

    private void Start()
    {
        cl = GetComponent<CanvasLayer>();
    }

    private void Update()
    {
        DebugLines(5);
    }

    /// <summary>
    /// Draws a set of connected lines with random colors and endpoints.
    /// </summary>
    /// <param name="num">Number of lines in the set</param>
    /// <param name="size">Thickness of the line</param>
    private void DebugLines(int num, int size = 5)
    {
        int lastX = Random.Range(0, CanvasSize.x - 1), lastY = Random.Range(0, CanvasSize.y - 1);
        for (int i = 0; i < num; i++)
        {
            int newX = Random.Range(0, CanvasSize.x - 1), newY = Random.Range(0, CanvasSize.y - 1);
            cl.DrawLine(lastX, lastY, newX, newY, size, Random.ColorHSV());
            lastX = newX; lastY = newY;
        }
    }

    /// <summary>
    /// Draws a set of points in a raster grid.
    /// </summary>
    /// <param name="w">Number of points per row.</param>
    /// <param name="h">Number of rows</param>
    /// <param name="size">Point thickness.</param>
    private void DebugPoints(int w, int h, int size = 5)
    {
        int offx = CanvasSize.x / w, offy = CanvasSize.y / h;

        for (int i = 0; i < CanvasSize.x; i += offx)
        {
            for (int j = 0; j < CanvasSize.y; j += offy)
            {
                cl.DrawSquare(i, j, size, Random.ColorHSV());
            }
        }
    }
}
