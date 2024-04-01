using UnityEngine;

public class HexMapCreate : MonoBehaviour
{
    private int mapRadius;
    public GameObject hex;
    public GameObject deepWaterHex;
    public GameObject shallowWaterHex;
    public GameObject grassHex;
    public GameObject forestHex;
    public GameObject hillsHex;
    public GameObject forestHillsHex;
    public GameObject mountainHex;
    public GameObject cityHex;
    public GameObject map;
    [SerializeField] public MapSettings mapStats;

    private void OnEnable(){
        mapRadius = mapStats.radius;
        LayoutGrid();
    }

    void LayoutGrid(){
        float [,] population =  NoiseGenerator.Instance.getNoiseMap(89216308 / 2, 10000, mapStats.scale);
        float [,] forest = NoiseGenerator.Instance.getForestMap(89216308 / 3);
        float [,] water = NoiseGenerator.Instance.getWaterMap(89216308 / 4);

        for(int r = -mapRadius; r <= mapRadius; r++){
            for(int q = -mapRadius;  q <= mapRadius; q++){
                if(-r + q <= mapRadius && r - q <= mapRadius){
                    Debug.Log(water[r + mapRadius, q + mapRadius]);
                    CreateTile(r, q, 
                    (int)(water[r + mapRadius, q + mapRadius]), 
                    (int)population[r + mapRadius, q + mapRadius],
                    (int)forest[r + mapRadius, q + mapRadius]);
                }
            }
        }
    }

    public void CreateTile(int r, int q, int elevation, int population, int forest){
        //Create Tile
        GameObject tile = Instantiate(hex, GetPositionForHex(new Vector2Int(r, q)), Quaternion.identity);
        tile.name = $"Hex[{r},{q}]";
        tile.transform.parent = map.transform;

        //Seting stats for our Tile
        tile.GetComponent<TileLogic>().stats.coords_r = r;
        tile.GetComponent<TileLogic>().stats.coords_q = q;

        tile.GetComponent<TileLogic>().stats.elevation = elevation;
        
        tile.GetComponent<TileLogic>().stats.population = population;

        TerrainType terrainType = SetTerrainType(r, q, elevation, population, forest);
        tile.GetComponent<TileLogic>().stats.terrain = terrainType;
        
        if(terrainType == TerrainType.DeepWater || terrainType == TerrainType.ShallowWater) 
            tile.GetComponent<TileLogic>().stats.population = 0;
        
        //Geting terrain type
        GameObject terrain = Instantiate(GetHexStyle(terrainType), tile.transform);
        terrain.transform.position += new Vector3(0, 0, elevation > 0 ? mapStats.height * elevation/1000 - 0.4f : 0);
    }

    public TerrainType SetTerrainType(int r, int q, int elevation, int population, int forest){
        
        if(elevation < -200){
            return TerrainType.DeepWater;
        } else if(elevation <= 0){
            return TerrainType.ShallowWater;
        } else if (elevation < 100000){
            if(population > 9000) return TerrainType.SmallCityRuins;
            if(forest > 0) return TerrainType.Forest;
            return TerrainType.Grassland;
        } else if (elevation < 1500){
            if(population > 9000) return TerrainType.SmallCityRuins;
            if(forest > 0) return TerrainType.ForestHills;
            return TerrainType.Hills;
        }else{
            return TerrainType.Mountains;
        }
    }

    public GameObject GetHexStyle(TerrainType terrainType){
        switch((int)terrainType){
            case 0:
                return deepWaterHex;
            case 1:
                return shallowWaterHex;
            case 2:
                return grassHex;
            case 3:
                return forestHex;
            case 4:
                return hillsHex;
            case 5:
                return forestHillsHex;
            case 6:
                return mountainHex;
            case 7:
                return cityHex;
            case 8:
                return cityHex;
            case 9:
                return cityHex;
            case 10:
                return cityHex;
            default:
                return cityHex;
        }
    }

    public Vector3 GetPositionForHex(Vector2Int coords){
        int column = coords.x;
        int row = coords.y;

        float width;
        float height;
        float xPosition;
        float yPosition;
        float horDistance;
        float verDistance;
        float offset;

        width = 2f;
        height = Mathf.Sqrt(3);

        horDistance = width * (3f/4f);
        verDistance = height;

        offset = height / 2 * column;

        xPosition = column * horDistance;
        yPosition = (row * verDistance) - offset;

        return new Vector3(-xPosition, yPosition, 0);
    }
}
