using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DragAndThrow : MonoBehaviour
{
    private bool drag,throwing=false;
    public Animator animator;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        if (drag) //farklı çekiş miktarlarına göre farklı animasyonlar oynatılacak.
        {
            animator.SetBool("IsDragging", true);
            
        }
        if(throwing)
        {
            animator.SetBool("IsDragging", false);
            animator.SetBool("IsThrowing", true);
            Invoke("ThrowDone", 0.11f);
        }
    }


    public void DragTheBasket()
    {
       // print("ULASMA YER 3 ALLAHIMA BİN ŞÜKÜR");
        drag = true;


    }

    public void ThrowTheBasket()
    {

        drag = false;
        throwing = true;


    }

    void ThrowDone()
    {

        throwing = false;
        animator.SetBool("IsThrowing", false);
    }
    
}
