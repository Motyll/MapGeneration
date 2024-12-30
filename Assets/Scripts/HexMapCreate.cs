using UnityEngine;

public class HexMapCreate : MonoBehaviour
{
    private int mapRadius;
    public GameObject hex;
    public GameObject deepWaterHex;
    public GameObject shallowWaterHex;
    public GameObject grassHex;
    public GameObject forestHex;
    public GameObject mountainHex;
    public GameObject map;
    [SerializeField] public MapSettings mapStats;

    private void OnEnable(){
        mapRadius = mapStats.radius;
        LayoutGrid();
    }

    void LayoutGrid(){
        float [,] population =  NoiseGenerator.Instance.getNoiseMap(mapStats.seed / 2, 10000, mapStats.scale);
        float [,] forest = NoiseGenerator.Instance.getForestMap(mapStats.seed / 3);
        float [,] water = NoiseGenerator.Instance.getWaterMap(mapStats.seed / 4);
        float [,] ridge = NoiseGenerator.Instance.getRigdeMap(mapStats.seed);

        for(int r = -mapRadius; r <= mapRadius; r++){
            for(int q = -mapRadius;  q <= mapRadius; q++){
                if(-r + q <= mapRadius && r - q <= mapRadius){
                    Debug.Log(ridge[r + mapRadius, q + mapRadius]);
                    CreateTile(r, q, 
                    (int)water[r + mapRadius, q + mapRadius], 
                    (int)population[r + mapRadius, q + mapRadius],
                    (int)forest[r + mapRadius, q + mapRadius],
                    ridge[r + mapRadius, q + mapRadius]);
                }
            }
        }
    }

    public void CreateTile(int r, int q, int elevation, int population, int forest, float ridge){
        GameObject tile = Instantiate(hex, GetPositionForHex(new Vector2Int(r, q)), Quaternion.identity);
        tile.name = $"Hex[{r},{q}]";
        tile.transform.parent = map.transform;

        tile.GetComponent<TileLogic>().stats.coords_r = r;
        tile.GetComponent<TileLogic>().stats.coords_q = q;
        tile.GetComponent<TileLogic>().stats.elevation = elevation;
        tile.GetComponent<TileLogic>().stats.population = population;
        TerrainType terrainType = SetTerrainType(elevation, population, forest, ridge);
        tile.GetComponent<TileLogic>().stats.terrain = terrainType;
        
        if(terrainType == TerrainType.DeepWater || terrainType == TerrainType.ShallowWater) 
            tile.GetComponent<TileLogic>().stats.population = 0;
        
        GameObject terrain = Instantiate(GetHexStyle(terrainType), tile.transform);
        if (terrainType == TerrainType.Mountains)
            terrain.transform.position += new Vector3(0, 0, elevation > 0 ? mapStats.height * elevation/1000 - 0.4f + 1 : 1);
        else 
            terrain.transform.position += new Vector3(0, 0, elevation > 0 ? mapStats.height * elevation/1000 - 0.4f : 0);
    }

    public TerrainType SetTerrainType(int elevation, int population, int forest, float ridge){
        
        if(elevation < -200){
            return TerrainType.DeepWater;
        } else if(elevation <= 0){
            return TerrainType.ShallowWater;
        } else if (ridge < mapStats.mountainAmmount){
            if(forest > 0) return TerrainType.Forest;
            return TerrainType.Grassland;
        } else {
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
                return mountainHex;
            default:
                return grassHex;
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
