using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float animLenght;
    private Animator anim;

    private float Yaxis;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.speed = anim.speed * animLenght;
    }


    public void PositionProjectile(int Yindex)
    {
        if (GameController._instance.getStep() > 13) Yaxis = 2.4f;
        else Yaxis = 0;

        var tempPosition = new Vector3(GameController._instance.Manager.Projectiles.Xaxis,
            PlayerController._instance.transform.position.y + Yaxis +
            GameController._instance.Manager.ObjectAnimationManager.Yaxis + 1f + 1.3f * Yindex, 1);
        transform.position = tempPosition;
    }


// Update is called once per frame
}