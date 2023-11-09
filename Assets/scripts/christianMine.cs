using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class christianMine : MonoBehaviour
{
    private ExplosionForce ef;

    void OnCollisionEnter2D(Collision2D other) {
        ef = GameObject.FindGameObjectWithTag("explosionForce").GetComponent<ExplosionForce>();
        gameObject.GetComponent<Explodable>().explode();
		ef.doExplosion(gameObject.transform.position);

    }
}
