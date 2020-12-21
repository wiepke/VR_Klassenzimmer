using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetScatter : Scatterer
{
    // TODO: Instead of generating several game objects, generate a mesh of disjointed quads?
    public GameObject Template;
    public float ScatterRange = 1f;
    public float YDist = 0.01f;

    private void OnDrawGizmosSelected()
    {
        // TODO: Render circle instead?
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ScatterRange);
    }

    public override void Generate(int n)
    {
        while (n > 0)
        {
            n--;

            Vector3 randPos = Random.insideUnitSphere * ScatterRange;
            randPos.y = n * YDist;

            Vector3 randRot = new Vector3(0, Random.value * 360f, 0);

            Transform sheet = Instantiate(Template, transform).transform;
            sheet.localPosition = randPos;
            sheet.localEulerAngles += randRot;
        }
    }
}
