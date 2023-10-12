using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    public GameObject player;

    public float initialMoney;
    private float money;
    public float moneyMult;
    
    public int miloCost;
    public int aveahCost;
    public int wesleyCost;
    public int lillyCost;
    public int raulCost;
    public int sharpyCost;
    public int randomSpawnRange;

    public GameObject milo;
    public GameObject aveah;
    public GameObject wesley;
    public GameObject lilly;
    public GameObject raul;
    public GameObject sharpy;

    public GameObject[] enemyGameobjects;
    public int[] enemyCosts;

    public GameObject enemiesContainer;
    public float wave;

    void Start()
    {
        wave = 1;
        enemyGameobjects = new GameObject[] { milo, aveah, wesley, lilly, raul };
        enemyCosts = new int[] { miloCost, aveahCost, wesleyCost, lillyCost, raulCost };

        spawnEnemies();
    }

    void spawnEnemies()
    {
        money = initialMoney * Mathf.Pow(12.0f, wave-1);


        while (money >= 0)
        {
            int enemyPick = UnityEngine.Random.Range(0, enemyGameobjects.Length);

            if (money - enemyCosts[enemyPick] < 0)
            {
                return;
            }
            money -= enemyCosts[enemyPick];
            Vector2 enemySpawnPos = new Vector2(player.transform.position.x + UnityEngine.Random.Range(-randomSpawnRange, randomSpawnRange), player.transform.position.y + UnityEngine.Random.Range(-randomSpawnRange, randomSpawnRange)); 
            GameObject newEnemy = Instantiate(enemyGameobjects[enemyPick], enemySpawnPos, Quaternion.identity, enemiesContainer.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
