using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_map_renderer : MonoBehaviour
{

    private GameObject player; // set by code
    
    public Sprite jose;
    public Sprite milo;
    public Sprite aveah;
    public Sprite wesley;
    public Sprite lilly;
    public string activeSprite;
    public float offset;
    public GameObject lockedEnemy;
    private SpriteRenderer spriteRenderer;

    public bool canSetPos;


    


    public void setLockedEnemy(GameObject enemy) {
        lockedEnemy = enemy;
    }
    
    public void setSprite() {
        if (activeSprite == "jose") {
            spriteRenderer.sprite = jose;
        } else if (activeSprite == "milo") {
            spriteRenderer.sprite = milo;
        } else if (activeSprite == "lilly") {
            spriteRenderer.sprite = lilly;
        } else if (activeSprite == "aveah") {
            spriteRenderer.sprite = aveah;
        } else if (activeSprite == "wesley") {
            spriteRenderer.sprite = wesley;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");  
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        setSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSetPos) return;

        Vector2 playerDirection = player.transform.position - lockedEnemy.transform.position;
        gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y) + playerDirection.normalized * -offset;
    }
}
