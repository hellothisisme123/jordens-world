using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public Rigidbody2D player;  
    public bool dynamicSize; 
    public float initialSize;
    public float scrollSpeed;
    public Vector2 minMaxSize;
    private Camera cam;
    



    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = initialSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
        
        if (dynamicSize) {
            cam.orthographicSize += -Input.mouseScrollDelta.y * scrollSpeed;
            if (cam.orthographicSize > minMaxSize.y) {
                cam.orthographicSize = minMaxSize.y;
            } else if (cam.orthographicSize < minMaxSize.x) {
                cam.orthographicSize = minMaxSize.x;
            }
        }
    }
}
