using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;


public class enemy_map_display : MonoBehaviour
{
    /*
    public GameObject[] enemies;
    public GameObject[] enemyRenderers;
    public float skipFrames; // the amount of frames to skip between renders

    private GameObject player; // set by code
    private GameObject enemyRendererPrefab; // set manually
    private int frameCount;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if (frameCount >= skipFrames) {
            frameCount = 0; // only runs once every x frames // x == skipFrames

            findEnemies();
            createRenderers();
            setEnemyPos();


        }

    }

    public void createRenderers() {
        foreach (GameObject renderer in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Destroy(renderer);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            // enemyRenderers[i] = Instantiate(enemyRendererPrefab, new Vector2(player.transform.position.x, player.transform.position.y), quaternion.identity);

            // enemyRenderers.push


            GameObject newEnemy = enemyRenderers[i];
            newEnemy.tag = "enemyRenderer";
            newEnemy.GetComponent<enemy_map_renderer>().setLockedEnemy(enemies[i]);


            // enemyRenderers.

        }
    }

    public void setEnemyPos() {
        for (int i = 0; i < enemies.Length; i++)
        {
            
        }
    }

    public void findEnemies() {
        enemies = GameObject.FindGameObjectsWithTag("enemy");

    }

    */
}