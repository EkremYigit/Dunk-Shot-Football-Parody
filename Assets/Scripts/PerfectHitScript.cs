using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectHitScript : MonoBehaviour
{
    private Vector3 hitEffectPos;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameController._instance.getPerfect() >= 2&& GameController._instance.getPerfect()<5)
        {
            
            hitEffectPos = other.contacts[0].point;
            PlayerController._instance.transform.GetChild(1).transform.position = other.contacts[0].point;
           
            PlayerController._instance.transform.GetChild(1).gameObject.SetActive(true);
            PlayerController._instance.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Clear();
            PlayerController._instance.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();

        }
        else if (GameController._instance.getPerfect() >= 5)
        {
            PlayerController._instance.transform.GetChild(1).gameObject.SetActive(false);
            hitEffectPos = other.contacts[0].point;
            PlayerController._instance.transform.GetChild(2).transform.position = other.contacts[0].point;
           
            PlayerController._instance.transform.GetChild(2).gameObject.SetActive(true);
            PlayerController._instance.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Clear();
            PlayerController._instance.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            PlayerController._instance.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Clear();
            PlayerController._instance.transform.GetChild(1).gameObject.SetActive(false);
            
        }
    }
}
