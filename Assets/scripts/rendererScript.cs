using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rendererScript : MonoBehaviour
{
    private GameObject player; // set by code
    public float offset;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDirection = player.transform.position - gameObject.transform.parent.transform.position;
        gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y) + playerDirection.normalized * -offset;
    }
}
