using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    public int coinCount; // the amount of coins to be added when this coin is collected
    private GameObject player;
    private Rigidbody2D rb;
    public float homingForce;
    public float minDistanceForHoming;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distanceToPlayer = (transform.position - player.transform.position).magnitude;

        if (distanceToPlayer < minDistanceForHoming) {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            rb.AddForce(directionToPlayer * homingForce, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<powerUpHandler>().incrementCoins(coinCount);
            Destroy(gameObject);
        }
    }
}
