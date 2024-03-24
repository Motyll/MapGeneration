using UnityEngine;

public class DeselectButton : MonoBehaviour{
    public void OnClick(){
        HexSelection.Instance.DeselectAll();
    }
}
