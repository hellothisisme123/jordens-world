using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class chargedBillScript : MonoBehaviour
{
    public float knockbackForce;
    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // trigger for chargeShot
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.gameObject);

        if (col.gameObject.tag == "enemy" && gameObject.tag == "chargeShot") {
            Debug.Log(col.gameObject.GetComponent<healthbar>().getHp());
            
            col.gameObject.GetComponent<healthbar>().setHp(col.gameObject.GetComponent<healthbar>().getHp() - 1);
            col.GetComponent<Rigidbody2D>().AddForce(-(player.transform.position-col.gameObject.transform.position).normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
