using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    public GameObject player;

    public int money;
    public float moneyMult;
    
    public int miloCost;
    public int aveahCost;
    public int wesleyCost;
    public int lillyCost;
    public int sharpyCost;
    public int randomSpawnRange;

    public GameObject milo;
    public GameObject aveah;
    public GameObject wesley;
    public GameObject lilly;
    public GameObject sharpy;

    public GameObject[] enemyGameobjects;
    public int[] enemyCosts;

    void Start()
    {
        // enemyGameobjects = new GameObject[]{ milo, aveah, wesley, lilly, sharpy};
        // enemyCosts = new int[]{miloCost, aveahCost, wesleyCost, lillyCost, sharpyCost};

        enemyGameobjects = new GameObject[] { milo, aveah, wesley, lilly };
        enemyCosts = new int[] { miloCost, aveahCost, wesleyCost, lillyCost };

        spawnEnemies();
    }

    void spawnEnemies()
    {
        while (money >= 0)
        {
            int enemyPick = Random.Range(0, enemyGameobjects.Length);

            if (money - enemyCosts[enemyPick] < 0)
            {
                return;
            }
            money -= enemyCosts[enemyPick];
            Vector2 enemySpawnPos = new Vector2(player.transform.position.x + Random.Range(-randomSpawnRange, randomSpawnRange), player.transform.position.y + Random.Range(-randomSpawnRange, randomSpawnRange)); ; 
            GameObject newEnemy = Instantiate(enemyGameobjects[enemyPick], enemySpawnPos, Quaternion.identity);

            newEnemy.GetComponent<enemy_pathfinding>().setPlayer(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
