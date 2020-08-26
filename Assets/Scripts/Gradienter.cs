using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gradienter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
     void Start()
     {
         _spriteRenderer = GetComponent<SpriteRenderer>();

     }
     private void Update()
     {
         _spriteRenderer.color = GameController._instance.currentColor;
     }

     



   
}
    
    
    
    
