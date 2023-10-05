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
    public Vector2 throwRange;
    public float eggSpawnDistance; // how far away the eggs will spawn from the player
    public float eggGravForceMult; // multiplier for the gravity force applied to the egg

    private GameObject player;
    private Vector2 playerCurrentLocation;
    private Vector2 playerDirection; // direction to where the player will be
    private Vector2 playerVelocity;
    private Vector2 vectorToPredictedLocation; // prediction for where the player will be after throwtime

    public Vector2 GetThrowRange() {
        return throwRange;
    }

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
        vectorToPredictedLocation = playerCurrentLocation + (playerVelocity / throwTime) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 vectorToPlayer = player.transform.position - gameObject.transform.position;
        float distanceToPlayer = vectorToPlayer.magnitude;

        if (canThrow && alive && alivePlayer && distanceToPlayer > throwRange.x && distanceToPlayer < throwRange.y)
        {
            canThrow = false;
            StartCoroutine(setThrow());

            Vector2 eggSpawnPos = rb.transform.position + (player.transform.position - gameObject.transform.position).normalized * eggSpawnDistance;
            GameObject egg = Instantiate(eggPrefab, eggSpawnPos, Quaternion.identity, rb.transform);
            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            egg_script eggScript = eggRb.GetComponent<egg_script>();

            eggScript.SetGravForce(new Vector2(0, 0));

            Vector2 launchForceX = vectorToPredictedLocation / throwTime;
            Vector2 gravForce = new Vector2(launchForceX.normalized.x * launchForceX.normalized.y, -Mathf.Abs(launchForceX.normalized.x)) * eggGravForceMult;
            Vector2 launchForceY = -gravForce * 2 * throwTime * throwTime;

            //launchForceX.
            Debug.Log($"launchForceX: {launchForceX}");
            Debug.Log($"gravForce: {gravForce}");
            Debug.Log($"launchForceY: {launchForceY}");

            eggScript.SetGravForce(gravForce);
            eggScript.SetLaunchForceY(launchForceY * 1.5f);
            /*eggRb.AddForce(launchForceX, ForceMode2D.Impulse);*/


            // eggRb.AddForce(-gravForce * eggGravForceMult * 4, ForceMode2D.Force);
        }
    }

    private IEnumerator setThrow()
    {
        yield return new WaitForSeconds(throwDelay);
        canThrow = true;
    }
}
