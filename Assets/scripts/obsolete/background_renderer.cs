using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class background_renderer : MonoBehaviour
{
    public GameObject player;
    public Vector2 playerCoords;
    public Vector2 gridSize;
    public Vector2 tileSize;
    public Camera cam;
    public float camHeight;
    public float camWidth;
    public Sprite tileSprite;

    // Update is called once per frame
    void Update()
    {
        camHeight = cam.GetComponent<Camera>().orthographicSize;
        camWidth = cam.GetComponent<Camera>().aspect * camHeight;
        playerCoords = player.transform.position;

        GameObject[] bgTiles = GameObject.FindGameObjectsWithTag("bgTile");
        for (int i = 0; i < bgTiles.Length; i++)
        {
            Destroy(bgTiles[i]);
        }


        for (int rowi = 0; rowi < gridSize.y; rowi++)
        {
            for (int coli = 0; coli < gridSize.x; coli++)
            {
                GameObject tile = new GameObject("tile");
                tile.tag = "bgTile";
                tile.transform.localScale = new Vector3(0.25f, 0.25f, 1);
                SpriteRenderer tileSpriteRenderer = tile.AddComponent<SpriteRenderer>();
                tileSize = tileSpriteRenderer.size;
                Debug.Log(tileSpriteRenderer.size);

                tileSpriteRenderer.sprite = tileSprite;

                tile.transform.position = new Vector3(coli * tileSize.y, rowi * tileSize.x, 0);
            }
        }

    }
}
