using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;


public class GoalPostController : MonoBehaviour
{
    private bool sidePole;

    private bool backwardPole;

    private bool goal;

    private bool perfectPoint;

    private float animCount;


    void Start()
    {
        sidePole = false;
        backwardPole = false;
        goal = false;
    }


    void Update()
    {
        if (sidePole)
        {
            PlayerController._instance.setIsPerfectShot(false);
        }

        if (!sidePole && perfectPoint)
        {
            PlayerController._instance.setIsPerfectShot(true);
        }

        if (goal && perfectPoint && !sidePole) //it is perfect shot
        {
            if (animCount > 0)
            {
                animCount--;
                GameController._instance.Replacement();
                if (GameController._instance.getStep() != 0)
                {
                    GameController._instance.PerfectShot(1);

                    UnityEngine.Vector3 temp = new UnityEngine.Vector3(
                        PlayerController._instance.transform.position.x +
                        GameController._instance.Manager.ObjectAnimationManager4.Xaxis,
                        PlayerController._instance.transform.position.y +
                        GameController._instance.Manager.ObjectAnimationManager4.Yaxis,
                        PlayerController._instance.transform.position.z);
                    Instantiate((GameController._instance.Manager.ObjectAnimationManager4.PrefabToShow), temp,
                        Quaternion.identity);
                }
            }
            else
            {
                animCount = 0;
            }
        }
        else if (goal && sidePole && perfectPoint) //it is not a perfect shot
        {
            if (animCount > 0)
            {
                animCount--;
                GameController._instance.Replacement();


                GameController._instance.PerfectShot(0);
                GameController._instance.setPerfect(0);
                GameController._instance.InActivePerfect();
            }
            else
            {
                animCount = 0;
            }
        }
    }


    public void ContinuePerfectState()
    {
        sidePole = false;
        goal = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            goal = true;


            GameController._instance.OnSuccess(); //When Ball Enters The Goal Post It Runs.


            transform.GetComponent<EnterBallAnim>().setCounter(1);

            transform.GetComponent<EdgeCollider2D>().enabled = false;
        }
    }


    public bool PerfectPoint
    {
        get => perfectPoint;
        set => perfectPoint = value;
    }

    public bool SidePole
    {
        get => sidePole;
        set => sidePole = value;
    }

    public bool BackwardPole
    {
        get => backwardPole;
        set => backwardPole = value;
    }

    public float aanimCount
    {
        get => animCount;
        set => animCount = value;
    }
}