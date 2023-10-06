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
    public float eggHomingForceMult; // multiplier for the homing force applied to the egg

    private GameObject player;
    private Vector2 playerCurrentLocation;
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
        bool alivePlayer = player.GetComponent<healthbar>().alive;
        bool alive = GetComponent<healthbar>().alive;
        Vector2 vectorToPlayer = player.transform.position - gameObject.transform.position;
        float distanceToPlayer = vectorToPlayer.magnitude;

        if (canThrow && alive && alivePlayer && distanceToPlayer > throwRange.x && distanceToPlayer < throwRange.y)
        {
            canThrow = false;
            StartCoroutine(setThrow());

            playerVelocity = prb.velocity;
            playerCurrentLocation = player.transform.position;

            Vector2 eggSpawnPos = rb.transform.position + (player.transform.position - gameObject.transform.position).normalized * eggSpawnDistance;
            vectorToPredictedLocation = playerCurrentLocation + (playerVelocity / throwTime) - eggSpawnPos;
            GameObject egg = Instantiate(eggPrefab, eggSpawnPos, Quaternion.identity, rb.transform);
            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            egg_script eggScript = eggRb.GetComponent<egg_script>();

            eggScript.SetGravForce(new Vector2(0, 0));

            Vector2 launchForceX = vectorToPredictedLocation / throwTime;
            Vector2 gravForce = new Vector2(launchForceX.normalized.x * launchForceX.normalized.y, -Mathf.Abs(launchForceX.normalized.x)) * eggGravForceMult;
            Vector2 launchForceY = -gravForce * 2 * throwTime * throwTime * eggGravForceMult;

            Vector2 launchForce = launchForceX + launchForceY;
            eggScript.SetGravForce(gravForce);
            eggScript.SetLaunchForce(launchForce);
            eggScript.SetHomingForceMult(eggHomingForceMult);
        }
    }

    private IEnumerator setThrow()
    {
        yield return new WaitForSeconds(throwDelay);
        canThrow = true;
    }
}
