using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv1Script : MonoBehaviour
{
    public Animator animator;
    private bool Run=false;
    private static readonly int Lv1 = Animator.StringToHash("Lv1");

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if(Run) animator.SetBool(Lv1,true);
    }

    public void RunAnimation()
    {
        Run = true;
    }
}
