using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HexSelection : MonoBehaviour{
    GameObject selectedTile;

    public GameObject panel;
    public Text hexName;
    public Text hexType;
    public Text hexCoords;

    private static HexSelection _instance;
    public static HexSelection Instance { get { return _instance; } }

    void Awake(){
        panel.SetActive(false);
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        selectedTile = null;
    }

    public void Select(GameObject tileToAdd){
        DeselectAll();
        selectedTile = tileToAdd;

        if (selectedTile != null){
            selectedTile.GetComponent<TileLogic>().Select();
            hexName.text = selectedTile.GetComponent<TileLogic>().stats.name; 
            hexType.text = selectedTile.GetComponent<TileLogic>().stats.terrain.ToString();
            hexCoords.text = selectedTile.GetComponent<TileLogic>().stats.coords_r.ToString()
                 + ", " + selectedTile.GetComponent<TileLogic>().stats.coords_q.ToString();
        }
        panel.SetActive(true);
    }

    public void DeselectAll(){
        if (selectedTile != null){
            selectedTile.GetComponent<TileLogic>().Deselect();
            selectedTile = null;
            panel.SetActive(false);
        }
    }
}
