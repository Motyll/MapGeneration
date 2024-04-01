using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour{
    public float scale;

    [SerializeField] public MapSettings mapStats;

    private static NoiseGenerator _instance;
    public static NoiseGenerator Instance { get { return _instance; } }

    public NoiseGenerator() : base(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public float CalculateFloat(int r, int q){
        float scale = 60f;
        int xOffset = mapStats.seed % 1000;
        int yOffset = mapStats.seed / 1000 % 1000;
        float xCoord = (float)r / 256 * scale + xOffset;
        float yCoord = (float)q / 256 * scale + yOffset;

        float value = Mathf.PerlinNoise(xCoord, yCoord);

        return value;
    }

    public float[,] getNoiseMap(int seed, int amplifier, float scale){
        float [,] map = new float[mapStats.radius * 4, mapStats.radius * 4];
        int xOffset = seed % 1000;
        int yOffset = seed / 1000 % 1000;

        for(int x = 0; x < mapStats.radius * 2 + 1; x++){
            for(int y = 0; y < mapStats.radius * 2 + 1; y++){
                float xCoord = (float)x / mapStats.radius * 2 * scale + xOffset;
                float yCoord = (float)y / mapStats.radius * 2 * scale + yOffset;
                map[x,y] = Mathf.PerlinNoise(xCoord, yCoord) * amplifier - 500;
            }
        }
        return map;
    }

    public float[,] getWaterMap(int seed){
        float [,] map = new float[mapStats.radius * 2 + 1, mapStats.radius * 2 + 1];
        int xOffset = seed % 1000;
        int yOffset = seed / 1000 % 1000;
        float scale = mapStats.scale;

        float octaveImpact = 1f;
        for(int i = 0; i < mapStats.waterOctaves; i++){
            for(int x = 0; x < mapStats.radius * 2 + 1; x++){
                for(int y = 0; y < mapStats.radius * 2 + 1; y++){
                    float xCoord = (float)x / mapStats.radius * 2 * scale + xOffset;
                    float yCoord = (float)y / mapStats.radius * 2 * scale + yOffset;
                    float val = ((Mathf.PerlinNoise(xCoord, yCoord) - 0.5f) * 2000 * octaveImpact) + mapStats.waterLevel;
                    map[x, y] += val > 0 ? val * 10 : val / 2;
                }
            }
            scale *= 2;
            octaveImpact /= 2;
        }
        
        return map;
    }

    public float[,] getForestMap(int seed){
        float [,] map = new float[mapStats.radius * 2 + 1, mapStats.radius * 2 + 1];
        int xOffset = seed % 2000;
        int yOffset = seed / 1000 % 2000;
        float scale = mapStats.forestScale;

        float octaveImpact = 1f;
        for(int i = 0; i < mapStats.forestOctaves; i++){
            for(int x = 0; x < mapStats.radius * 2 + 1; x++){
                for(int y = 0; y < mapStats.radius * 2 + 1; y++){
                    float xCoord = (float)x / mapStats.radius * 2 * scale + xOffset;
                    float yCoord = (float)y / mapStats.radius * 2 * scale + yOffset;
                    float val = (Mathf.PerlinNoise(xCoord, yCoord) - 0.5f) * 20;
                    map[x, y] += (val + mapStats.forestAmmount) * octaveImpact;
                }
            }
            scale *= 2;
            octaveImpact /= 2;
        }
        
        return map;
    }

    public float[,] getRigdeMap(int seed){
        float [,] map = new float[mapStats.radius * 2 + 1, mapStats.radius * 2 + 1];
        int xOffset = seed % 2000;
        int yOffset = seed / 1000 % 2000;
        float scale = mapStats.mountainScale;

        float octaveImpact = 1f;
        for(int i = 0; i < mapStats.mountainOctaves; i++){
            for(int x = 0; x < mapStats.radius * 2 + 1; x++){
                for(int y = 0; y < mapStats.radius * 2 + 1; y++){
                    float xCoord = (float)x / mapStats.radius * 2 * scale + xOffset;
                    float yCoord = (float)y / mapStats.radius * 2 * scale + yOffset;
                    float val = Mathf.PerlinNoise(xCoord, yCoord) - 0.5f;
                    val = -Mathf.Abs(val);
                    map[x, y] += (val + 0.1f) * octaveImpact;
                }
            }
            scale *= 2;
            octaveImpact /= 2;
        }
        
        return map;
    }

    // void Update(){
    //     Renderer renderer = GetComponent<Renderer>();
    //     renderer.material.mainTexture = GenerateTexture();
    // }
    

    // public Texture2D GenerateTexture(){
    //     Texture2D texture = new Texture2D(mapStats.width, mapStats.height);
    //     float[,] map = getNoiseMap(89216308, 1);
    //     for(int x = 0; x < mapStats.width; x++){
    //         for(int y = 0; y < mapStats.height; y++){
    //             float val = map[x, y];
    //             Color color = new Color(val, val, val);
    //             texture.SetPixel(x, y, color);
    //         }
    //     }    
    //     texture.Apply();
    //     return texture;
    // }

    // public Color CalculateColor(int r, int q){
    //     float xCoord = (float)r / 256 * scale + xOffset;
    //     float yCoord = (float)q / 256 * scale + yOffset;

    //     //Debug.Log(xCoord + ", " + yCoord);

    //     float value = Mathf.PerlinNoise(xCoord, yCoord);

    //     int t = (int)(value * 10);
    //     value = (float)t / 10;

    //     return new Color(value, value, value);
    // }

    // public static float[,] GenerateNoiseMap(int width, int height, float scale, int seed, int octaves, float persistance, float lacanarty, Vector2 offset){
    //     System.Random prng = new System.Random(seed);

    //     Vector2[] octaveOffsets = new Vector2[octaves];

    //     for(int i = 0; i < octaves; i++){
    //         float offsetX = prng.Next(-10000, 10000) + offset.x;
    //         float offsetY = prng.Next(-10000, 10000) + offset.y;

    //         octaveOffsets[i] = new Vector2(offsetX, offsetY);

    //     }

    //     if(scale == 0) scale = 0.001f;

    //     // float maxNoiseHeight = float.MinValue;
    //     // float minNoiseHeight = float.MaxValue;

    //     float halfWidth = width / 2;
    //     float halfHeight = height / 2;

    //     float [,] noiseMap = new float[width, height];

    //     for(int x = 0; x < width; x++){
    //         for(int y = 0; y < height; y++){
    //             float amplitude = 1;
    //             float frequency = 1;
    //             float noiseHeight = 0;

    //             for(int i = 0; i < octaves; i++){
    //                 float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
    //                 float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

    //                 float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

    //                 noiseHeight += perlinValue * amplitude;

    //                 amplitude *= persistance;

    //                 frequency *= lacanarty;
    //             }
    //         }
    //     }    
    // }
}
