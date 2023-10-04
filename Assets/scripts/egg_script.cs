using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg_script : MonoBehaviour
{
    public Sprite whiteEgg;
    public Sprite brownEgg;
    public Sprite purpleEgg;
    public string chosenEgg;

    private int randVal;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public float spinSpeed;

    public Vector2 gravForce;
    
    public void SetGravForce(Vector2 g)
    {
        gravForce = g;
    }

    // Start is called before the first frame update
    void Start()
    {
        gravForce = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        rb.transform.Rotate(Vector3.forward * Random.Range(0, 359));

        randVal = Random.Range(0, 10);
        if (randVal == 0)
        {
            chosenEgg = "purple";
            spriteRenderer.sprite = purpleEgg;
        }
        else if (randVal < 6)
        {
            chosenEgg = "white";
            spriteRenderer.sprite = whiteEgg;
        } 
        else
        {
            chosenEgg = "brown";
            spriteRenderer.sprite = brownEgg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(gravForce);
        rb.transform.Rotate(Vector3.forward * spinSpeed);
    }
}
