using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    [SerializeField]
    private GameObject evaluationObj = default;

    [SerializeField]
    private float roomScaleMinX = -3.28f;
    [SerializeField]
    private float roomScaleMaxX = 8.81f;
    [SerializeField]
    private float roomScaleMinZ = -7.85f;
    [SerializeField]
    private float roomScaleMaxZ = 14.21f;

    [SerializeField]
    private float period = 0.05f;   //how fast is every Pixel revealed
    int pixCount = 0;               //needed in update function for deleting old pixels after period

    [SerializeField]
    private int shmls = 35; // seeHowManyLastSteps


    Texture2D texture;
    float nextActionTime;

    // Start is called before the first frame update
    void Start()
    {
        nextActionTime = Time.time;
        texture = new Texture2D(256, 256);
        evaluationObj.GetComponent<Renderer>().material.mainTexture = texture;
        //Test
        if (MenuDataHolder.evaluationMap.Count < 10)
        {
            for (int i = 0; i < 400; i++)
            {
                
                 float testX = Random.Range(0, 255);
                 float testY = Random.Range(0, 255);
                 MenuDataHolder.evaluationMap.Add(new Vector2(testX, testY));
            }
        }
        /////////
        for (int i=1; i<texture.width; i++)
        {
            for (int j=1; j < texture.height; j++)
            {
                texture.SetPixel(i, j, Color.clear);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            if (pixCount > shmls)
            {
                Vector2 roomPositionOld = MenuDataHolder.evaluationMap[(pixCount - shmls) % MenuDataHolder.evaluationMap.Count];
                float pixelXOld = (roomPositionOld.x - roomScaleMinX) / (roomScaleMaxX - roomScaleMinX) * texture.width;
                float pixelYOld = (roomPositionOld.y - roomScaleMinZ) / (roomScaleMaxZ - roomScaleMinZ) * texture.height;
                texture.SetPixel(Mathf.RoundToInt(pixelXOld), Mathf.RoundToInt(pixelYOld), Color.clear);
                enlargePixel(new Vector2(pixelXOld, pixelYOld), Color.clear);
            }
            nextActionTime += period;
            Vector2 roomPosition = MenuDataHolder.evaluationMap[pixCount % MenuDataHolder.evaluationMap.Count];
            float pixelX = (roomPosition.x - roomScaleMinX) / (roomScaleMaxX - roomScaleMinX) * texture.width;
            float pixelY = (roomPosition.y - roomScaleMinZ) / (roomScaleMaxZ - roomScaleMinZ) * texture.height;
            texture.SetPixel(Mathf.RoundToInt(pixelX), Mathf.RoundToInt(pixelY), Color.red);
            enlargePixel(new Vector2(pixelX, pixelY), Color.red);
            texture.Apply();
            pixCount++;
        }
        
    }

    private void enlargePixel(Vector2 pixel, Color color)
    {
        texture.SetPixel(Mathf.RoundToInt(pixel.x + 1), Mathf.RoundToInt(pixel.y + 1), color);
        texture.SetPixel(Mathf.RoundToInt(pixel.x + 1), Mathf.RoundToInt(pixel.y - 1), color);
        texture.SetPixel(Mathf.RoundToInt(pixel.x - 1), Mathf.RoundToInt(pixel.y + 1), color);
        texture.SetPixel(Mathf.RoundToInt(pixel.x - 1), Mathf.RoundToInt(pixel.y - 1), color);
        texture.SetPixel(Mathf.RoundToInt(pixel.x + 1), Mathf.RoundToInt(pixel.y), color);
        texture.SetPixel(Mathf.RoundToInt(pixel.x), Mathf.RoundToInt(pixel.y - 1), color);
        texture.SetPixel(Mathf.RoundToInt(pixel.x - 1), Mathf.RoundToInt(pixel.y), color);
        texture.SetPixel(Mathf.RoundToInt(pixel.x), Mathf.RoundToInt(pixel.y + 1), color);
    }
}
