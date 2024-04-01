using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/Map")]
public class MapSettings : ScriptableObject{
    public int radius;
    public int seed;
    public float height;
    public float scale;
    public float waterLevel;
    public float waterScale;
    public int waterOctaves;
    public int forestAmmount;
    public float forestScale;
    public int forestOctaves;
    public float mountainAmmount;
    public float mountainScale;
    public int mountainOctaves;
}
