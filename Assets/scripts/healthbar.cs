using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public float maxHp;
    private float hp;
    public int damagePlayerEnemyKnockback;
    public int damagePlayerPlayerKnockback;
    public bool alive;
    public Image healthFill;
    public GameObject uiManagerGO;
    private bool invinsible;
    public float iFrames; // in seconds
    public float hpDivider; // hpHealedOnRoundEnd = (maxHp / 2) + (maxHp / hp)
    public GameObject coinPrefab;
    public float coinLaunchForce;
    public int coinDropCount;

    void Start()
    {
        alive = true;
        hp = maxHp;
        invinsible = false;
    }

    public void setHp(float x) {
        hp = x;
    }

    public float getHp() {
        return hp;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Animator animator = GetComponent<Animator>();

        if (col.collider.gameObject.tag == "bullet" && gameObject.tag == "enemy")
        {
            Destroy(col.collider.gameObject);
            hp--;
        } else if (col.collider.gameObject.tag == "bonzai" && gameObject.tag == "enemy") {
            hp--;
        }

        if (gameObject.tag == "Player" && alive)
        {
            if (invinsible) {
                StartCoroutine(setInvinsible());
                return;
            } else {
                invinsible = true; // sets invisible to true if it wasn't already
                // play sound to indicate the shot was reflected by invinsibility
                // for now this isnt yet implemented
            }

            if (col.collider.gameObject.tag == "enemy" || col.collider.gameObject.tag == "egg")
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
        
        if (hp <= 0)
        {
            animator.SetTrigger("die");
            if (gameObject.tag == "enemy" && coinPrefab != null) {
                for (int i = 0; i < coinDropCount; i++)
                {
                    GameObject spawnedCoin = Instantiate(coinPrefab, gameObject.transform.position, quaternion.identity);
                    Vector2 randomVector = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
                    spawnedCoin.GetComponent<Rigidbody2D>().AddForce(randomVector * coinLaunchForce, ForceMode2D.Impulse);    
                }
            }
        }
    }

    void Update() {
        if (gameObject.tag == "Player") {
            healthFill.fillAmount = hp / maxHp;
        }
    }

    private IEnumerator setInvinsible() {
        yield return new WaitForSeconds(iFrames);
        invinsible = false;
    }

    public void explosionAnimationStart(Animation a)
    {
        if (gameObject.tag != "Player")
        {
            foreach (var cc2d in gameObject.GetComponents<CircleCollider2D>())
            {
                Destroy(cc2d);
            }
        }
        alive = false;
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
            ui_manager uiManagerScript = uiManagerGO.GetComponent<ui_manager>();
            uiManagerScript.playerDeath();
        }
    }
}
