using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionForce : MonoBehaviour {
	public float force = 50;
	public float radius = 5;
	

	public void doExplosion(Vector3 position){
		transform.localPosition = position;
		StartCoroutine(waitAndExplode());
	}

	private IEnumerator waitAndExplode(){
		yield return new WaitForFixedUpdate();
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,radius);
     
		foreach(Collider2D coll in colliders){
			if(coll.GetComponent<Rigidbody2D>()&&coll.name!="hero"){
                AddExplosionForce(coll.GetComponent<Rigidbody2D>(), force, transform.position, radius);
			}
		}
	}

    private void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
	{
		var dir = (body.transform.position - explosionPosition);	
		float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        baseForce.z = 0;
		body.AddForce(baseForce);		
	}
}
