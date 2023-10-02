using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class sub_enemy : MonoBehaviour
{

    public GameObject mother;
    public MonoScript enemy_pathfinding;
    public bool death;

    void Start()
    {
        death = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (death)
        {
            // send message to mother to remove this enemy form 
        }

    }
}
