using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_pathfinding : MonoBehaviour
{
    private GameObject player;
    private bool alive; // tied to healthbar.cs
    private int speed;
    public int walkSpeed;
    public float randDirectionModifier;

    private Rigidbody2D rb;

    public void Start()
    {
        // hardcode this on at start of game
        player = GameObject.FindGameObjectWithTag("Player");
        speed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.transform.position - rb.transform.position;
        alive = GetComponent<healthbar>().alive;

        if (alive)
        {
            rb.AddForce(new Vector2(speed * Time.deltaTime * Random.Range(1, randDirectionModifier), speed * Time.deltaTime * Random.Range(1, randDirectionModifier)) * direction.normalized);
        }
    }

    public void SetSpeed(int s)
    {
        speed = s;
    }

    public void SetSpeed(string s)
    { 
        speed = walkSpeed;
    }

    public void setPlayer(GameObject p)
    {
        player = p;
    }
}
