using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv4Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private bool Run = false;// Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Run)
        {
           
            animator.SetBool("IsLv4", true);
        }
        else
        {
            animator.SetBool("IsLv4", false);
         
            
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
