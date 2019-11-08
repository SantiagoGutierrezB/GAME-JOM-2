using UnityEngine;

// Change the width of the viewport each time space key is pressed

public class CameraFollow : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    
    public Transform p1;
    public Transform p2;

    void Start()
    {
    cam1.rect = new Rect(0, 0, 0.5f, 1);
    cam2.rect = new Rect(0.5f, 0, 0.5f, 1);
    }
    
    void Update(){
        cam1.transform.position = new Vector3(p1.position.x,p1.position.y,-10);
        cam2.transform.position = new Vector3(p2.position.x,p2.position.y,-10);
    
    }

}