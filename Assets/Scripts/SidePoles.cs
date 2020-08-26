using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePoles : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.parent.gameObject.GetComponent<GoalPostController>().SidePole = true;
        }
    }
}