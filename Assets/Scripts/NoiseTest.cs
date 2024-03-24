using System;
using Unity.VisualScripting;
using UnityEngine;
    
public class NoiseTest : MonoBehaviour{    

    public float scale;

    [SerializeField] public MapSettings mapStats;
    void Update(){
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }
    

    public Texture2D GenerateTexture(){
        Texture2D texture = new Texture2D(mapStats.width, mapStats.height);
        float[,] map = getNoiseMap(89216308, 1, mapStats.scale);
        for(int x = 0; x < mapStats.width; x++){
            for(int y = 0; y < mapStats.height; y++){
                float val = map[x, y];
                Color color = new Color(val, val, val);
                texture.SetPixel(x, y, color);
            }
        }    
        texture.Apply();
        return texture;
    }

    public float[,] getNoiseMap(int seed, int amplifier, int scale){
        float [,] map = new float[mapStats.width, mapStats.height];
        int xOffset = seed % 1000;
        int yOffset = seed / 1000 % 1000;

        for(int x = 0; x < mapStats.width; x++){
            for(int y = 0; y < mapStats.height; y++){
                float xCoord = (float)x / 256 * scale + xOffset;
                float yCoord = (float)y / 256 * scale + yOffset;
                map[x,y] = Mathf.PerlinNoise(xCoord, yCoord) * amplifier;
            }
        }
        return map;
    }
}