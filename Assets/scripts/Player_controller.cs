using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    
    public int speed;
    public int projspeed;
    public int projLifespan;
    
    public GameObject billPrefab;
    public Sprite bill;

    public Camera cam;
    public float shootDelay;
    public bool canShoot;
    public bool alive;
    public int rollSpeed;
    public float rollDelay;
    public bool canRoll;
    public Vector2 latestDirection;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        alive = true;
        canRoll = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (alive)
        {
            rb.AddForce(new Vector2(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime));
        }

        setPrevDirection();

        if (Input.GetButton("Roll") && canRoll)
        {
            // Debug.Log("roll");
            canRoll = false;
            StartCoroutine(allowRoll());
            rb.AddForce(latestDirection * rollSpeed);
            
        }

        if (Input.GetButton("Fire1"))
        {
            // shoot delay
            if (!canShoot) return;
            canShoot = false;
            StartCoroutine(shootDelayFunc());

            // create bullet and add components
            Vector2 projDirection = mousepos - rb.transform.position;
            projDirection = projDirection.normalized;
            Vector2 billPos = rb.transform.position + (new Vector3(projDirection.x, projDirection.y, 0) *1);
            GameObject bill = Instantiate(billPrefab, billPos, Quaternion.identity);



            /*GameObject proj = new GameObject("projectile");
            Rigidbody2D projRb = proj.AddComponent<Rigidbody2D>();
            SpriteRenderer projSprite = proj.AddComponent<SpriteRenderer>();
            CircleCollider2D projCollider = proj.AddComponent<CircleCollider2D>();

            proj.tag = "bullet";
            projCollider.radius = 1.9f;
            projCollider.offset = new Vector2(-0.7f, 0);
            projSprite.sprite = bill;
            projRb.gravityScale = 0;
            proj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            projSprite.sortingLayerName = "Foreground";
            proj.transform.position = ;

            float projRotation = Mathf.Atan2(projDirection.y, projDirection.x) * Mathf.Rad2Deg + 180;
            proj.transform.rotation = Quaternion.Euler(0, 0, projRotation);

            StartCoroutine(killBill(projLifespan, proj));

            projRb.velocity = projDirection * projspeed;
            */
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

    private IEnumerator shootDelayFunc()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}