using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv3Script : MonoBehaviour
{
    public Animator animator;
    private bool Run,RunPlus,RunMinus = false;// Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Run)
        {
            if (RunPlus)
            {
               // print("RUNPLUS");

                animator.SetBool("IsLv3+", true);

            }

            if (RunMinus)
            {
                //print("RUNMINUS");
                animator.SetBool("IsLv3-", true);

            }
        }
        else
        {
            animator.SetBool("IsLv3+", false);
            animator.SetBool("IsLv3-", false);
   
        }


    }

    public void RunAnimationPlus()
    {
        
        animator.SetBool("IsLv3+", true);
        RunPlus = true;
        Run = true;

    }

    public void RunAnimationMinus()
    {
        
        animator.SetBool("IsLv3-", true);
        RunMinus = true;
        Run = true;
    }
    
    public void StopAnim()
    {
        
        Run = false;
        animator.SetBool("IsLv3+", false);
        animator.SetBool("IsLv3-", false);
    }
}
