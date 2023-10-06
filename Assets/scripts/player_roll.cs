using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_roll : MonoBehaviour
{
    private bool alive; // tied to healthbar.cs
    public int rollSpeed;
    public float rollDelay;
    private bool canRoll;
    private Vector2 latestDirection;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        canRoll = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        alive = GetComponent<healthbar>().alive;

        setPrevDirection();

        if (Input.GetButton("Roll") && canRoll && alive)
        {
            canRoll = false;
            StartCoroutine(allowRoll());
            rb.AddForce(latestDirection * rollSpeed, ForceMode2D.Impulse);     
        }

        if (!canRoll) {
            
        }
    }

    private void setPrevDirection()
    {
        if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
        {
            latestDirection.x = -1f;
            latestDirection.y = 0;
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
        {
            latestDirection.x = -1f;
            latestDirection.y = 1f;
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
        {
            latestDirection.x = -1f;
            latestDirection.y = -1f;
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0)
        {
            latestDirection.x = 1f;
            latestDirection.y = 0;
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
        {
            latestDirection.x = 1f;
            latestDirection.y = 1f;
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
        {
            latestDirection.x = 1f;
            latestDirection.y = -1f;
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0)
        {
            latestDirection.x = 0;
            latestDirection.y = -1f;
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0)
        {
            latestDirection.x = 0;
            latestDirection.y = 1f;
        }
    }

    private IEnumerator allowRoll()
    {
        yield return new WaitForSeconds(rollDelay);
        canRoll = true;
    }
}
