using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multishot : MonoBehaviour
{
    public int projspeed;

    public GameObject regularBill;
    public float billOffset;

    public Camera cam;
    public float shootDelay;
    private bool canShoot;
    private bool alive; // tied to healthbar.cs
    private Rigidbody2D rb;

    // multishot
    public int multiShotCount;
    public float multiShotSpread; 
    public bool mutiShotSpreadFunctionOfCount;
    public float multiShotSpreadMult; // multiplies the multishot count by this to get the spread if the bool is active
    public float knockbackForce;

    public bool altFire;

    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();

        if (mutiShotSpreadFunctionOfCount) {
            // Debug.Log($"set multishot spread according to count multiplier of {multiShotSpreadMult}");
            multiShotSpread = multiShotCount * multiShotSpreadMult;
        }
    }

    void Update()
    {
        alive = GetComponent<healthbar>().alive;

        string keyBind = "mainFire";
        if (altFire) keyBind = "altFire";
        if (Input.GetButton(keyBind) && canShoot && alive && Time.timeScale > 0)
        {
            // gets position of the mouse in world space
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // shoot delay
            canShoot = false;
            StartCoroutine(shootDelayFunc());

            if (mutiShotSpreadFunctionOfCount) multiShotSpread = multiShotCount * multiShotSpreadMult;
            // create bullet
            for (float i = 0; i < multiShotCount; i++)
            {   
                Vector2 projDirection = mousePos - rb.position;   

                if (multiShotCount != 1) {
                    // adjusts the index so the bills are centered on the mouse
                    // do not compress to one line, it breaks
                    float i2 = (multiShotCount-1)%2;
                    i2 = i2 / 2 + i; 
                    i2 = i2 - multiShotCount/2;
        
                    // adjusts the angle of the shot for multishot
                    float shotAngle = multiShotSpread / (multiShotCount-1) * i2;
                    projDirection = Quaternion.AngleAxis(shotAngle, Vector3.forward) * projDirection;
                }
                projDirection = projDirection.normalized;

                Vector2 billPos = rb.transform.position + (new Vector3(projDirection.x, projDirection.y, 0) * billOffset);
                GameObject bill = Instantiate(regularBill, billPos, Quaternion.identity, rb.transform);
                float projRotation = Mathf.Atan2(projDirection.y, projDirection.x) * Mathf.Rad2Deg + 180;
                bill.transform.rotation = Quaternion.Euler(0, 0, projRotation);
                bill.GetComponent<Rigidbody2D>().velocity = projDirection * projspeed;
            }

            // knockback
            Vector2 KbDirection = mousePos - rb.position;   
            KbDirection = KbDirection.normalized;
            rb.AddForce(-KbDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator shootDelayFunc()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
