using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;


public class chestScript : MonoBehaviour
{

    public float lootScale; // scale for how good the loot is
    public string color; // red, yellow, green, blue, random,
    public float openDistance; // how far away the player must be for the chest to open

    public string itemInside; // the item inside the chest, set by code if not set by dev

    private GameObject player; // set by code    
    private Animator animator;
    private bool open;


    // Start is called before the first frame update
    void Start()
    {
        // set color
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        open = false;


        if (color == "red") {
            animator.SetTrigger("red");
        } else if (color == "yellow") {
            animator.SetTrigger("yellow");
        } else if (color == "green") {
            animator.SetTrigger("green");
        } else if (color == "blue") {
            animator.SetTrigger("blue");
        } else if (color == "random") {
            int rand = Random.Range(0, 3);
            if (rand == 0) {
                animator.SetTrigger("red");
            } else if (rand == 1) {
                animator.SetTrigger("yellow");
            } else if (rand == 2) {
                animator.SetTrigger("green");
            } else if (rand == 3) {
                animator.SetTrigger("blue");
            }
        } 

    }

    public void setPlayer(GameObject p)
    {
        player = p;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = player.transform.position - rb.transform.position;
        

        if (direction.magnitude <= openDistance && !open) {
            animator.SetTrigger("open");
            open = true;
        }
    }
}
