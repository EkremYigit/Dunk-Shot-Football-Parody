using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using DefaultNamespace;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class BackgroundController : MonoBehaviour
{
    private SpriteRenderer _sp;

    private Vector3 MapScale;


    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        // _sp.sprite = GameController._instance.Manager.DesignManager.Background;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("BackGroundCheck"))
        {
            GameController._instance.AddMap();
            transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}