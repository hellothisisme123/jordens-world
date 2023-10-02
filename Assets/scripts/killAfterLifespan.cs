using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killAfterLifespan : MonoBehaviour
{
    public float lifespan;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(kill());
    }
    
    private IEnumerator kill() {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
