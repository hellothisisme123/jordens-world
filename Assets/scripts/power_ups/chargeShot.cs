using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chargeShot : MonoBehaviour
{
    public int projspeed;

    public GameObject chargedBill;
    public float billOffset;

    public Camera cam;
    public float chargeDuration;
    private bool alive; // tied to healthbar.cs
    private Rigidbody2D rb;
    private float chargeIndex;
    public float minChargeMult;

    public Image shootDelayBar;
    public GameObject minChargeMarker;
    
    public bool altFire;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 2.5 is the width of the marking
        // 192.5 is the width of the fill image - the width of the marking (195 - 2.5)
        // -100 is to account for a discrepency between localPosition and the transform.position
        //   im not sure where -100 came from    
        minChargeMarker.GetComponent<RectTransform>().localPosition = new Vector3(-100f + 2.5f + (192.5f * (1-minChargeMult)), 0, 0);
    }

    void Update()
    {
        alive = GetComponent<healthbar>().alive;
        shootDelayBar.fillAmount = (chargeDuration - chargeIndex) / chargeDuration;

        string keyBind = "mainFire";
        if (altFire) keyBind = "altFire";

        if (Input.GetButton(keyBind) && Time.timeScale > 0) {
            chargeIndex += Time.deltaTime;
            if (chargeIndex >= chargeDuration) {
                chargeIndex = chargeDuration;
            }
        } else {
            if (alive && Time.timeScale > 0 && chargeIndex > chargeDuration*minChargeMult)
            {
                // gets position of the mouse in world space
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

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
}
