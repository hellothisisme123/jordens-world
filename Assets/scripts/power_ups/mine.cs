using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class mine : powerUp
{
    public GameObject minePrefab;
    public float mineOffset;

    public Camera cam;
    public float shootDelay;
    private bool canShoot;
    private bool alive; // tied to healthbar.cs
    private Rigidbody2D rb;

    private float shootDelayBarIndex; 
    public Image shootDelayBar;
    
    public bool altFire;

    void Start()
    {
        canShoot = true;
        shootDelayBarIndex = shootDelay;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        alive = GetComponent<healthbar>().alive;
        shootDelayBarIndex += Time.deltaTime;
        if (shootDelayBarIndex > shootDelay) {
            shootDelayBarIndex = shootDelay;
        }
        shootDelayBar.fillAmount = shootDelayBarIndex / shootDelay;
        

        string keyBind = "mainFire";
        if (altFire) keyBind = "altFire";
        if (Input.GetButton(keyBind) && canShoot && alive && Time.timeScale > 0)
        {
            shootDelayBarIndex = 0;
        
            // gets position of the mouse in world space
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // shoot delay
            canShoot = false;
            StartCoroutine(shootDelayFunc());

            Vector2 projDirection = mousePos - rb.position;
            projDirection = projDirection.normalized;

            Vector2 billPos = rb.transform.position + (new Vector3(projDirection.x, projDirection.y, 0) * mineOffset);
            GameObject bill = Instantiate(minePrefab, billPos, Quaternion.identity, rb.transform);
        }
    }

    private IEnumerator shootDelayFunc()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
