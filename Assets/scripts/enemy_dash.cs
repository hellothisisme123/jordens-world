using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class enemy_dash : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private bool alive; // tied to healthbar.cs

    public float dashRange;
    public float dashSpeed;
    public float dashDelay;
    private bool canDash;
    private bool inDashRange;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        alive = GetComponent<healthbar>().alive;

        Vector2 distanceToPlayer = player.transform.position - gameObject.transform.position;
        if (distanceToPlayer.magnitude < dashRange)
        {
            inDashRange = true;
        }
        else
        {
            inDashRange = false;
        }

        if (canDash && inDashRange && alive)
        {
            // dashes
            canDash = false;
            //Debug.Log("false");
            rb.AddForce(distanceToPlayer.normalized * dashSpeed);
            StartCoroutine(allowDash());
        }
    }

    private IEnumerator allowDash()
    {
        yield return new WaitForSeconds(dashDelay);
        canDash = true;
    }
}
