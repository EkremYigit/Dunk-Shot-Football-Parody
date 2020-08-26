using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBallAnim : MonoBehaviour
{
    private int counter = 0;
    public Animator animator;

    void Update()
    {
        if (counter == 1)
        {
            // NORMAL SUCCESSFULLY SHOT SOUND EFFECT


            counter++;
            animator.SetBool
                ("EnterAnim", true);
        }
        else
        {
            animator.SetBool
                ("EnterAnim", false);
        }
    }

    public void setCounter(int a)
    {
        counter = a;
    }
}