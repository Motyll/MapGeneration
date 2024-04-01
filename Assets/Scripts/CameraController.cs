using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    
    public float movementTime;
    public float movementSpeed;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    void Start(){
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update(){
        HandleMovementInput();
        HandleMouseInput();
    }

    void HandleMouseInput(){
        if(Input.mouseScrollDelta.y != 0){
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        if(Input.GetMouseButtonDown(2)){
            Plane plane = new Plane(Vector3.forward, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry)){
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if(Input.GetMouseButton(2)){
            Plane plane = new Plane(Vector3.forward, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry)){
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }

    void HandleMovementInput(){
        if(Input.GetKey(KeyCode.W)){
            newPosition += (transform.up * movementSpeed);
        }
        if(Input.GetKey(KeyCode.S)){
            newPosition += (transform.up * -movementSpeed);
        }
        if(Input.GetKey(KeyCode.D)){
            newPosition += (transform.right * -movementSpeed);
        }
        if(Input.GetKey(KeyCode.A)){
            newPosition += (transform.right * movementSpeed);
        }

        if(Input.GetKey(KeyCode.Q)){
            newRotation *= Quaternion.Euler(Vector3.forward * rotationAmount);
        }
        if(Input.GetKey(KeyCode.E)){
            newRotation *= Quaternion.Euler(Vector3.forward * -rotationAmount);
        }
        // if(Input.GetKey(KeyCode.T)){
        //     newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        // }
        // if(Input.GetKey(KeyCode.Y)){
        //     newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        // }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        
        newZoom = newZoom.z >= 50 ? newZoom : new Vector3 (0, -50, 50);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);;
    }
}
