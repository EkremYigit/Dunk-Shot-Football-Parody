using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.SceneManagement;
public class ammoController : MonoBehaviour
{
    // if player hit the ammo game is over.


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            other.gameObject.transform.GetComponent<Animator>().SetBool("Explode",true);
            other.gameObject.SetActive(false);
            other.gameObject.transform.GetComponent<Rigidbody2D>().isKinematic = false;
            Invoke("ResetGame",0.7f);
            
            
        }
    }


    void ResetGame()
    {
      
        GameController._instance.OnFail();
    }
}
