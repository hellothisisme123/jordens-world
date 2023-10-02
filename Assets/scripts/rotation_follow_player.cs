using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_follow_player : MonoBehaviour
{
    public int rotationOffset;

    public GameObject player;
    public Rigidbody2D rb;
    

    void Start() 
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = player.transform.position - rb.transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        rb.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
