using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/Map")]
public class MapSettings : ScriptableObject{
    public int radius;
    public int seed;
    public int width;
    public int height;
    public int scale;
    public float heightScale;
    public int forestAmmount;
    public int forestScale;
}
