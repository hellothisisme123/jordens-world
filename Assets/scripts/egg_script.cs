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
    public Vector2 launchForce;
    private float homingForceMult;
    private GameObject player;

    public float bulletKnockbackForce;

    // knockbacks egg on hit from bullet
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "bullet") {
            Destroy(col.collider.gameObject);

            Vector2 knockDirection = player.transform.position - gameObject.transform.position;
            rb.AddForce(-knockDirection.normalized * bulletKnockbackForce, ForceMode2D.Impulse);
        }
    }

    public void SetGravForce(Vector2 g)
    {
        gravForce = g;
    }

    public void SetLaunchForce(Vector2 f)
    {
        launchForce = f;
    }

    public void SetHomingForceMult(float f) {
        homingForceMult = f;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        rb.transform.Rotate(Vector3.forward * Random.Range(0, 359));

        rb.AddForce(launchForce, ForceMode2D.Impulse);


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
        rb.AddForce(gravForce, ForceMode2D.Force);
        rb.transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        
        Vector2 toPlayer = player.transform.position - gameObject.transform.position;
        Vector2 homingForce = toPlayer.normalized * homingForceMult;
        rb.AddForce(homingForce, ForceMode2D.Force);
    }
}
