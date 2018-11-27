using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationProc : MonoBehaviour {

    public Texture2D map;

    public ColorToPrefab[] colorMappings;

    private void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }

        }
    }
    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                if (map.GetPixel(x - 1, y).a!=0 || map.GetPixel(x + 1, y).a != 0 || map.GetPixel(x, y+1).a != 0|| map.GetPixel(x, y-1).a != 0)
                {
                    Vector2 pos = new Vector2(x, y);
                    Instantiate(colorMapping.prefab, pos, Quaternion.identity, transform);
                }
                else
                {
                    Vector2 pos = new Vector2(x, y);
                    Instantiate(colorMapping.prefab, pos, Quaternion.identity, transform);
                }
                     
               
            } 


        }
    }
}