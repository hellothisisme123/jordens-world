using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargedBillScript : MonoBehaviour
{
    // trigger for chargeShot
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.gameObject);

        if (col.gameObject.tag == "enemy" && gameObject.tag == "chargeShot") {
            Debug.Log(col.gameObject.GetComponent<healthbar>().getHp());
            
            col.gameObject.GetComponent<healthbar>().setHp(col.gameObject.GetComponent<healthbar>().getHp() - 1);
        }
    }
}
