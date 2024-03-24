using UnityEngine;

[CreateAssetMenu(fileName = "Hex", menuName = "ScriptableObjects/Hex")]
public class Hex : ScriptableObject{
    public int coords_r;
    public int coords_q;
    public TerrainType terrain;
    public int elevation;
    public int population;
}

public enum TerrainType {
    DeepWater,
    ShallowWater,
    Grassland,
    Forest,
    Hills,
    ForestHills,
    Mountains,
    Wasteland,
    BigCityRuins,
    SmallCityRuins,
}
