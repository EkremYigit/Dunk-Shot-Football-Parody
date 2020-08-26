using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float offset; //to hold the main camera above a bit.
    private bool Replace = false;


    private static float tempY;

    public float upperOffset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position =
            new Vector3(transform.position.x, Player.transform.position.y + offset, transform.position.z);
    }


    void Update()
    {
        if (Replace)
        {
            float incrementCoefficient = 35.0f; //fps
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, tempY, (tempY - transform.position.y) / incrementCoefficient),
                transform.position.z);
        }
    }


    public void UpdateCameraPosition(float Ycordinate) //if shot is successful,this code will be reposition to camera;
    {
        tempY = Ycordinate;
        Replace = true;
    }
}