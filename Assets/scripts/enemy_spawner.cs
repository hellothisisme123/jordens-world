using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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

    public Vector2 randomSpawnRange; // min max
    public float waveDowntime;

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
    public TextMeshProUGUI waveCount;
    public bool waveActive;
    public bool waveSent;

    void Start()
    {
        wave = 0;
        enemyGameobjects = new GameObject[] { milo, aveah, wesley, lilly, raul };
        enemyCosts = new int[] { miloCost, aveahCost, wesleyCost, lillyCost, raulCost };
        waveActive = false;
        waveSent = false;
    }

    void nextWave() {
        waveSent = true;

        wave++;
        waveCount.text = $"Round: {wave}";

        healthbar playerHealthbar = player.GetComponent<healthbar>();
        // hpHealed = (maxHp / 2) + (maxHp / hp) // more heals when low, less heals when high
        playerHealthbar.setHp(Mathf.Clamp((playerHealthbar.maxHp / playerHealthbar.hpDivider) + (playerHealthbar.maxHp / playerHealthbar.getHp()) + playerHealthbar.getHp(), 0, playerHealthbar.maxHp));


        StartCoroutine(spawnEnemiesDelay());
    }

    // adds a downtime until the enemies are spawned 
    private IEnumerator spawnEnemiesDelay()
    {
        yield return new WaitForSeconds(waveDowntime);
        waveActive = true;
        spawnEnemies();
    }

    void spawnEnemies() {
        money = initialMoney + Mathf.Ceil(Mathf.Pow(moneyMult, wave-1));

        while (money >= 0)
        {
            int enemyPick = UnityEngine.Random.Range(0, enemyGameobjects.Length);

            if (money - enemyCosts[enemyPick] < 0)
            {
                return;
            }
            money -= enemyCosts[enemyPick];
            
            // determine spawnPos of the enemy
            float enemySpawnAngle = Mathf.Deg2Rad * UnityEngine.Random.Range(0, 359);
            float enemySpawnDistance = UnityEngine.Random.Range(randomSpawnRange.x, randomSpawnRange.y);
            Vector2 enemySpawnPos = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(Mathf.Cos(enemySpawnAngle) * enemySpawnDistance, Mathf.Sin(enemySpawnAngle) * enemySpawnDistance);
            
            GameObject newEnemy = Instantiate(enemyGameobjects[enemyPick], enemySpawnPos, Quaternion.identity, enemiesContainer.transform);
        }
    }

    void Update()
    {
        if (enemiesContainer.transform.childCount < 1 && waveActive) {
            waveActive = false;
        } 

        if (enemiesContainer.transform.childCount > 1) {
            waveSent = false;
        }

        if (!waveActive && !waveSent) {
            nextWave();
        }
    }
}
