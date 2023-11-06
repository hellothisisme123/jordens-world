using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public Rigidbody2D player;  
    public bool dynamicSize;
    public float scrollSpeed;
    public Vector2 minMaxSize;
    private Camera mainCam;

    void Start() {
        if (gameObject.tag == "MainCamera") mainCam = GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, gameObject.transform.position.z);
        
        if (dynamicSize && Time.timeScale > 0) {
            mainCam.fieldOfView += -Input.mouseScrollDelta.y * scrollSpeed;
            if (mainCam.fieldOfView > minMaxSize.y) {
                mainCam.fieldOfView = minMaxSize.y;
            } else if (mainCam.fieldOfView < minMaxSize.x) {
                mainCam.fieldOfView = minMaxSize.x;
            }
        }
    }
}
