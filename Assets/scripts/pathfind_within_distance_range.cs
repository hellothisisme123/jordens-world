using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfind_within_distance_range : MonoBehaviour
{
    public bool raul_egg_throw_link;
    public Vector2 distance_range;
    
    private Rigidbody2D rb;
    private GameObject player;
    public float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (raul_egg_throw_link) {
            distance_range = gameObject.GetComponent<raul_egg_throw>().GetThrowRange();
        }
    }

    void Update()
    {   
        Vector2 directionToPlayer = player.transform.position - gameObject.transform.position;
        Vector2 distanceToLimits = new Vector2(directionToPlayer.magnitude - distance_range.x, distance_range.y - directionToPlayer.magnitude); // negative when outside of range

        if (distanceToLimits.x > 0) {
            rb.AddForce(Mathf.Log(distanceToLimits.x) * directionToPlayer.normalized * speed); // y=log(x)
        }

        if (distanceToLimits.y > 0) {
            rb.AddForce(Mathf.Log(distanceToLimits.y) * -directionToPlayer.normalized * speed); // y=log(x)
        }
    }
}
