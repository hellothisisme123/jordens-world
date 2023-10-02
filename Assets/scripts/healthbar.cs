using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class healthbar : MonoBehaviour
{

    public int maxHp;
    public int hp;
    public int damagePlayerEnemyKnockback;
    public int damagePlayerPlayerKnockback;
    public bool alive;
    public MonoScript playerController;



    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        playerController = gameObject.GetComponent<MonoScript>();
    }

    void Update()
    {
        //alive = playerController.alive;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Animator animator = GetComponent<Animator>();


        if (col.collider.gameObject.tag == "bullet" && gameObject.tag != "Player")
        {
            Destroy(col.collider.gameObject);
            hp--;
        }

        if (hp <= 0)
        {
            animator.SetTrigger("die");
        }

        if (gameObject.tag == "Player" && alive)
        {
            if (col.collider.gameObject.tag == "enemy")
            {
                hp--;
                animator.SetBool("playerHurt", true);
                Vector2 damageDirection = col.collider.gameObject.transform.position - gameObject.transform.position;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                gameObject.GetComponent<Rigidbody2D>().AddForce(damageDirection.normalized * -damagePlayerPlayerKnockback);

                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(damageDirection.normalized * damagePlayerEnemyKnockback);
                }
            }
        }
    }

    public void explosionAnimationEnd(Animation a)
    {
        // runs at the end of an explosion
        if (gameObject.tag != "Player")
        {
            Destroy(gameObject);
        } else
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("playerDeath");
            Debug.Log("player death");
        }
    }

    public void explosionAnimationStart(Animation a)
    {
        if (gameObject.tag != "Player")
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            gameObject.GetComponent<enemy_pathfinding>().alive = false;
        } else
        {
            gameObject.GetComponent<Player_controller>().alive = false;

        }
    }
}
