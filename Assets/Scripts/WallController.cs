using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;


public class WallController : MonoBehaviour
{
    private Vector3 bouncyEffectPos;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //WALL HIT BOUNCY EFFECT SOUND


            Instantiate(GameController._instance.Manager.ObjectAnimationManager2.PrefabToShow, other.contacts[0].point,
                Quaternion.identity);

            bouncyEffectPos = new Vector3(
                other.transform.position.x,
                other.transform.position.y + GameController._instance.Manager.ObjectAnimationManager3.Yaxis,
                transform.position.z);

            if (other.transform.position.x > 0)
                bouncyEffectPos.x -= GameController._instance.Manager.ObjectAnimationManager3.Xaxis;
            if (other.transform.position.x <= 0)
                bouncyEffectPos.x += GameController._instance.Manager.ObjectAnimationManager3.Xaxis;


            Instantiate(GameController._instance.Manager.ObjectAnimationManager3.PrefabToShow, bouncyEffectPos,
                Quaternion.identity);


            PlayerController._instance.BouncyFlag =
                true; //when bouncy true and after slot if change  bouncy counter will be add
            PlayerController._instance.bouncyCounter++;
        }
    }
}