using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class raul_egg_throw : MonoBehaviour
{
    public GameObject eggPrefab;
    public float throwDelay; // how long until they can throw again
    public float throwTime; // how long for the egg to reach the player
    private Rigidbody2D prb;
    private Rigidbody2D rb;
    private bool canThrow;
    public Vector2 minMaxThrowDistance;
    public float eggSpawnDistance; // how far away the eggs will spawn from the player

    private GameObject player;
    private Vector2 playerCurrentLocation;
    private Vector2 playerDirection; // direction to where the player will be
    private Vector2 playerVelocity;
    private Vector2 playerPredictedLocation; // prediction for where the player will be after throwtime

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        prb = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(setThrow());
    }

    void Update()
    {
        playerVelocity = prb.velocity;
        bool alivePlayer = player.GetComponent<healthbar>().alive;
        bool alive = GetComponent<healthbar>().alive;
        playerCurrentLocation = player.transform.position;
        playerPredictedLocation = playerCurrentLocation + (playerVelocity / throwTime);
        Vector2 vectorToPlayer = player.transform.position - gameObject.transform.position;
        float distanceToPlayer = vectorToPlayer.magnitude;

        if (canThrow && alive && alivePlayer && distanceToPlayer > minMaxThrowDistance.x && distanceToPlayer < minMaxThrowDistance.y)
        {
            canThrow = false;
            StartCoroutine(setThrow());

            Vector2 eggSpawnPos = rb.transform.position + (player.transform.position - gameObject.transform.position).normalized * eggSpawnDistance;
            GameObject egg = Instantiate(eggPrefab, eggSpawnPos, Quaternion.identity, rb.transform);
            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            egg_script eggScript = eggRb.GetComponent<egg_script>();

            eggScript.SetGravForce(new Vector2(0, 0));

            eggRb.AddForce(vectorToPlayer / throwTime);
        }


    }

    private IEnumerator setThrow()
    {
        yield return new WaitForSeconds(throwDelay);
        canThrow = true;
    }
}
