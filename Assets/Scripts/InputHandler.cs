using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject clickable;
    private static InputHandler _instance;
    public static InputHandler Instance { get { return _instance; } }

    void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else{
            _instance = this;
        }
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){

            if(EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            var rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if(rayHit.collider.gameObject == null){
                HexSelection.Instance.DeselectAll();
            } else {
                Debug.Log(rayHit.collider.gameObject.name);
                HexSelection.Instance.Select(rayHit.collider.gameObject);
            } 



            // RaycastHit hit;
            // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            //     HexSelection.Instance.Select(hit.collider.gameObject);
            // }
        }
    }
}
