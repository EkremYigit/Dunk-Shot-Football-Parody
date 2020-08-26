using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2Script : MonoBehaviour
{
    public Animator animator;
    private bool Run = false; // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Run)
        {
            animator.SetBool("IsLv2", true);
          
        }
        else
        {
            animator.SetBool("IsLv2", false);
            
        }
    }

    public void RunAnimation()
    {
        Run = true;
    }

    public void StopAnim()
    {

        Run = false;

    }
}