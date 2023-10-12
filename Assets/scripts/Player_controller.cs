using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    
    public int speed;
    public int projspeed;
    public int projLifespan;
    
    public GameObject billPrefab;
    public Sprite bill;
    public float billOffset;

    public Camera cam;
    public float shootDelay;
    public bool canShoot;
    public bool alive; // tied to healthbar.cs
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        alive = GetComponent<healthbar>().alive;

        if (alive)
        {
            rb.AddForce(new Vector2(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime));
        }

        if (Input.GetButton("Fire1") && canShoot && alive)
        {
            // gets position of the mouse in world space
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // shoot delay
            canShoot = false;
            StartCoroutine(shootDelayFunc());

            // create bullet
            Vector2 projDirection = mousePos - rb.position;
            projDirection = projDirection.normalized;
            Vector2 billPos = rb.transform.position + (new Vector3(projDirection.x, projDirection.y, 0) * billOffset);
            GameObject bill = Instantiate(billPrefab, billPos, Quaternion.identity, rb.transform);
            float projRotation = Mathf.Atan2(projDirection.y, projDirection.x) * Mathf.Rad2Deg + 180;
            bill.transform.rotation = Quaternion.Euler(0, 0, projRotation);
            bill.GetComponent<Rigidbody2D>().velocity = projDirection * projspeed;
        }

        
    }

    private IEnumerator shootDelayFunc()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}