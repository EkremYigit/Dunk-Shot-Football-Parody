using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardPole : MonoBehaviour
{
    private BoxCollider2D col;


    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

     void OnCollisionEnter2D(Collision2D other)
    {

        
            if (other.transform.CompareTag("Player"))
            {

                transform.parent.gameObject.GetComponent<GoalPostController>().BackwardPole = true;

                PlayerController._instance.BallOnTheNet = true;
                transform.parent.gameObject.transform.GetComponent<GoalPost>().OnMouseDragging();

            }
        
    }
}
