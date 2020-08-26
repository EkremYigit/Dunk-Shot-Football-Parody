using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DefaultNamespace;
using TreeEditor;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Random = System.Random;

public class GoalPost : MonoBehaviour
{
    // [Header("Object Optimizing")] 


    private SpriteRenderer sp;

    private Vector2 targetPozition, relativePos, temp;
    private Quaternion rotation;

    private bool dragging, follow;

    public float GoalPostFollowSpeed = 5f;


    private Vector3 firstPosition;
    private Quaternion firstRotation;


   
    
void Update()
    {
        if (follow && dragging) //GoalPost follow the motion of mouse position.
        {
            Vector2 direction = PlayerController._instance.getBallDiff();
            float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angel, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, GoalPostFollowSpeed * Time.deltaTime);
        }
        else if (!dragging && !follow)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity,
                GoalPostFollowSpeed * Time.deltaTime*0.2f);
        }
    }


    public void OnLanding() //stop animation if ball enter the basket successfuly
    {
        GetComponent<Lv2Script>().StopAnim();
        GetComponent<Lv3Script>().StopAnim();
        GetComponent<Lv4Script>().StopAnim();
      
    }


    public void MouseSpinOn()
    {
        follow = true;
    }

    public void MouseSpinOff()
    {
        follow = false;
    }

    public void OnMouseDragging()
    {
        dragging = true;
    }

    public void OnMouseUpped()
    {
        dragging = false;
    }


    public void PositionGenerator(float leftX, float rightX, float minY, float maxY)
    {

        float newX = UnityEngine.Random.Range(leftX, rightX);
        float newY = UnityEngine.Random.Range(minY, maxY);

        transform.position = new Vector3(newX, newY, 0);
    } //this function generate different positions for each basket.
}