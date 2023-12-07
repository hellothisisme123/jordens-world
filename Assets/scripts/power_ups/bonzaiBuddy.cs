using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bonzaiBuddy : MonoBehaviour
{
    public int projspeed;

    public GameObject bonzai;
    public float billOffset;

    public Camera cam;
    public float shootDelay;
    private bool alive; // tied to healthbar.cs
    private Rigidbody2D rb;
    
    private float shootDelayBarIndex; 
    public Image shootDelayBar;

    public bool altFire;

    void Start()
    {
        shootDelayBarIndex = shootDelay;
        rb = GetComponent<Rigidbody2D>();
    }

    public void resetShootDelay() {
        shootDelayBarIndex = shootDelay;
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
        if (Input.GetButton(keyBind) && shootDelayBarIndex >= shootDelay && alive && Time.timeScale > 0)
        {
            shootDelayBarIndex = 0;
            
            // gets position of the mouse in world space
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 projDirection = mousePos - rb.position;
            projDirection = projDirection.normalized;

            Vector2 billPos = rb.transform.position + (new Vector3(projDirection.x, projDirection.y, 0) * billOffset);
            GameObject bill = Instantiate(bonzai, billPos, Quaternion.identity, rb.transform);
            float projRotation = Mathf.Atan2(projDirection.y, projDirection.x) * Mathf.Rad2Deg + 180;
            bill.transform.rotation = Quaternion.Euler(0, 0, projRotation);
            bill.GetComponent<Rigidbody2D>().velocity = projDirection * projspeed;
        }
    }
}
