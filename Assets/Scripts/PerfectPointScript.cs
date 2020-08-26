using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class PerfectPointScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.gameObject.GetComponent<GoalPostController>().PerfectPoint = true;
            transform.GetComponent<EdgeCollider2D>().enabled = false;
            transform.parent.gameObject.GetComponent<GoalPostController>().aanimCount = 1;
            //  GameController._instance.Replacement();
        }
    }
}
