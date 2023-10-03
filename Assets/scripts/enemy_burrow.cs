using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_burrow : MonoBehaviour
{
    private bool underground;
    public float burrowStartRange;
    public float burrowEndRange;
    public int undergroundSpeed;

    private GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.transform.position - rb.transform.position;

        // sets aveah underground or above ground
        if (direction.magnitude >= burrowStartRange)
        {
            underground = true;
        }
        else if (direction.magnitude <= burrowEndRange)
        {
            underground = false;
        }

        if (underground)
        {
            gameObject.GetComponent<enemy_pathfinding>().SetSpeed(undergroundSpeed);
        }
        else
        {
            gameObject.GetComponent<enemy_pathfinding>().SetSpeed("walk");
        }
    }
}
