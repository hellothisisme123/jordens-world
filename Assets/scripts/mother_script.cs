using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mother_script : MonoBehaviour
{
    private GameObject player;
    private bool alive; // tied to healthbar.cs
    private Rigidbody2D rb;

    public GameObject subEnemyRef;
    public int spawnedLimit; // max limit for how many enemies that can be spawned at once
    public int swarmSize; // how many enemies are spawned at once
    public float spawnDelay; // how long until new enemies can be spawned
    public float spawnRadius; // how farm away the enemies can be spawned
    public bool randomizeSpawnRadius; // will spawn randomly within the radius rather than at the edge every time
    public int minEnemyDistanceToPlayer; // will only spawn enemies if they are at least this far away from player
    public int minDistanceToPlayer; // will only spawn enemies if this object is least this far away from player
    private bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(allowSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        alive = GetComponent<healthbar>().alive;
        Vector2 direction = player.transform.position - rb.transform.position;
        alive = GetComponent<healthbar>().alive;

        if (alive)
        {
            if (direction.magnitude >= minDistanceToPlayer)
            {
                // Debug.Log(direction.magnitude);
                if (canSpawn)
                {
                    canSpawn = false;
                    StartCoroutine(allowSpawn());
                    // spawn enemies

                    for (int i = 0; i < swarmSize; i++)
                    {
                        Vector2 spawnPos = rb.transform.position;
                        float spawnOffset;
                        float spawnAngle = Random.Range(0, 359);
                        if (randomizeSpawnRadius)
                        {
                            spawnOffset = Random.Range(0, spawnRadius);
                        }
                        else
                        {
                            spawnOffset = spawnRadius;
                        }

                        spawnPos += new Vector2(spawnOffset * Mathf.Cos(spawnAngle), spawnOffset * Mathf.Sin(spawnAngle));
                        GameObject newEnemy = Instantiate(subEnemyRef, spawnPos, Quaternion.identity, rb.transform);
                        newEnemy.tag = "enemy";
                    }
                }
            }
        }
    }

    private IEnumerator allowSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
