using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TileLogic : MonoBehaviour{

    public GameObject highlight;

    [SerializeField] public Hex stats;

    public bool isSelected {get; set;}

    private void Awake(){
        Instantiate();
        highlight.gameObject.SetActive(false);
    }

    private void Instantiate(){
        stats = Instantiate(stats); 
    }

    public void SetCoords(int r, int q){
        stats.coords_q = q;
        stats.coords_r = r;
    }

    public void Select(){
        highlight.gameObject.SetActive(true);
        isSelected = true;
        Debug.Log(stats.coords_q + ", " + stats.coords_r);
    }
    public void Deselect(){
        highlight.gameObject.SetActive(false);
        isSelected = false;
    }

    private void OnMouseEnter(){
        if(!isSelected) highlight.gameObject.SetActive(true);
    }

    private void OnMouseExit(){
        if(!isSelected) highlight.gameObject.SetActive(false);
    }
}
