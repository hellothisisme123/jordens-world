using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeShot : MonoBehaviour
{
    public int projspeed;

    public GameObject chargedBill;
    public float billOffset;

    public Camera cam;
    public float shootDelay;
    public float chargeDuration;
    private bool canShoot;
    private bool alive; // tied to healthbar.cs
    private Rigidbody2D rb;
    private float chargeIndex;
    
    public bool altFire;

    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        alive = GetComponent<healthbar>().alive;

        string keyBind = "mainFire";
        if (altFire) keyBind = "altFire";

        if (Input.GetButton(keyBind) && Time.timeScale > 0) {
            chargeIndex += Time.deltaTime;
            if (chargeIndex >= chargeDuration) {
                chargeIndex = chargeDuration;
            }
        } else {
            if (canShoot && alive && Time.timeScale > 0 && chargeIndex > 0)
            {
                // gets position of the mouse in world space
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

                // shoot delay
                canShoot = false;
                StartCoroutine(shootDelayFunc());

                Vector2 projDirection = mousePos - rb.position;
                projDirection = projDirection.normalized;

                Vector2 billPos = rb.transform.position + (new Vector3(projDirection.x, projDirection.y, 0) * billOffset);
                GameObject bill = Instantiate(chargedBill, billPos, Quaternion.identity, rb.transform);
                float projRotation = Mathf.Atan2(projDirection.y, projDirection.x) * Mathf.Rad2Deg + 180;
                bill.transform.rotation = Quaternion.Euler(0, 0, projRotation);
                bill.GetComponent<Rigidbody2D>().velocity = projDirection * projspeed * chargeIndex/chargeDuration;
            }
            
            chargeIndex = 0;
        }
    }


    private IEnumerator shootDelayFunc()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
