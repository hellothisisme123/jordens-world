using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_pathfinding : MonoBehaviour
{
    public GameObject player;
    public bool alive;
    private int speed;
    public int walkSpeed;
    public float randDirectionModifier;

    public float dashRange;
    public float dashSpeed;
    public float dashDelay;
    private bool canDash; 
    private bool inDashRange;

    public bool canBurrow; // whether or not the enemy can burrow underground or not
    public bool underground;
    public float burrowStartRange;
    public float burrowEndRange;
    public int undergroundSpeed;
    
    public bool spawner;
    public int spawnedLimit; // max limit for how many enemies that can be spawned at once
    public GameObject subEnemyRef;
    public int swarmSize; // how many enemies are spawned at once
    public float spawnDelay; // how long until new enemies can be spawned
    public float spawnRadius; // how farm away the enemies can be spawned
    public bool randomizeSpawnRadius; // will spawn randomly within the radius rather than at the edge every time
    public int minEnemyDistanceToPlayer; // will only spawn enemies if they are at least this far away from player
    public int minDistanceToPlayer; // will only spawn enemies if this object is least this far away from player
    [SerializeField] bool canSpawn;
    private GameObject[] subEnemies; // list of subEnemies spawned currently

    public void Start()
    {
        // hardcode this on at start of game
        canDash = true;
        player = GameObject.FindGameObjectWithTag("Player");
        speed = walkSpeed;
                    
        if (spawner) StartCoroutine(allowSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = player.transform.position - rb.transform.position;
        alive = GetComponent<healthbar>().alive;

        if (spawner)
        {
            if (direction.magnitude >= minDistanceToPlayer)
            {
                // Debug.Log(direction.magnitude);
                if (canSpawn)
                {
                    canSpawn = false;
                    StartCoroutine(allowSpawn());
                    // spawn enemies
                    
                    for (int i = 0; i < swarmSize; i++) {
                        Vector2 spawnPos = rb.transform.position;
                        //spawnPos += new Vector2(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
                        float spawnOffset;
                        float spawnAngle = Random.Range(0, 359);
                        if (randomizeSpawnRadius)
                        {
                            spawnOffset = Random.Range(0, spawnRadius);
                        }
                        else
                        {
                            spawnOffset = Random.Range(0, spawnRadius);
                        }

                        spawnPos += new Vector2(spawnOffset * Mathf.Cos(spawnAngle), spawnOffset * Mathf.Sin(spawnAngle));
                        GameObject newEnemy = Instantiate(subEnemyRef, spawnPos, Quaternion.identity);
                        newEnemy.tag = "enemy";
                    }
                }
            }
        }

        // sets aveah underground or above ground
        if (canBurrow && direction.magnitude >= burrowStartRange)
        {
            underground = true;
        } else if (direction.magnitude <= burrowEndRange)
        {
            underground = false;
        }

        if (underground)
        {
            speed = undergroundSpeed;
        } else
        {
            speed = walkSpeed;
        }

        if (alive)
        {
            rb.AddForce(new Vector2(speed * Time.deltaTime * Random.Range(1, randDirectionModifier), speed * Time.deltaTime * Random.Range(1, randDirectionModifier)) * direction.normalized);
        }

        Vector2 distanceToPlayer = player.transform.position - gameObject.transform.position;
        if (distanceToPlayer.magnitude < dashRange)
        {
            inDashRange = true;
        } else
        {
            inDashRange = false;
        }

        if (canDash && inDashRange && alive)
        {
            // dashes
            canDash = false;
            //Debug.Log("false");
            rb.AddForce(distanceToPlayer.normalized * dashSpeed);
            StartCoroutine(allowDash(dashDelay));
        }
    }

    private IEnumerator allowSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

    private IEnumerator allowDash(float sec)
    {
        yield return new WaitForSeconds(dashDelay);
        //Debug.Log("candash is true");
        canDash = true;
    }

    public void setPlayer(GameObject p)
    {
        player = p;
    }
}
