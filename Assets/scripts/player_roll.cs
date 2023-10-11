using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class player_roll : MonoBehaviour
{
    
    private bool alive; // tied to healthbar.cs
    public int rollSpeed;
    public float rollDelay;
    private bool canRoll;
    private Vector2 latestDirection;
    private Rigidbody2D rb;

    private float rollTime;
    public Image rollImage;

    // Start is called before the first frame update
    void Start()
    {
        canRoll = true;
        rb = GetComponent<Rigidbody2D>();
        rollTime = rollDelay*1000;
    }

    // Update is called once per frame
    void Update()
    {
        alive = GetComponent<healthbar>().alive;

        setPrevDirection();

        if (Input.GetButton("Roll") && canRoll && alive)
        {
            canRoll = false;
            rollTime = 0;
            StartCoroutine(allowRoll());
            rb.AddForce(latestDirection * rollSpeed, ForceMode2D.Impulse);     
        } else {
            rollTime = Mathf.Clamp(rollTime+=1000*Time.deltaTime, 0, rollDelay*1000);
        }

        rollImage.fillAmount = rollTime / (rollDelay*1000);
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
