using System;
using UnityEngine;
using System.Collections;
using System.Numerics;
using DefaultNamespace;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class trajectoryScript : MonoBehaviour
{
    public Sprite dotSprite;
    private static GameObject[] dots;
    private GameObject trajectoryDots;
    private float x1, y1;
    private BoxCollider2D[] dotColliders;


    void Start()
    {
        dots = new GameObject[40];
        trajectoryDots = GameObject.Find("Trajectory Dots");
        trajectoryDots.transform.localScale =
            new Vector3(GameController._instance.Manager.PlayerManager.TrajectoryManager.initialDotSize,
                GameController._instance.Manager.PlayerManager.TrajectoryManager.initialDotSize,
                trajectoryDots.transform.localScale.z); //Initial size of trajectoryDots is applied

        for (var k = 0; k < 40; k++)
        {
            dots[k] = GameObject.Find("Dot (" + k + ")");
            if (dotSprite != null)
            {
                //If a sprite is applied to dotSprite
                dots[k].GetComponent<SpriteRenderer>().sprite = dotSprite;
                dots[k].GetComponent<SpriteRenderer>().sortingLayerName = "Buildings";
                dots[k].GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }

        for (var k = GameController._instance.Manager.PlayerManager.TrajectoryManager.numberOfDots; k < 40; k++)
        {
            // ;10				
            GameObject.Find("Dot (" + k + ")").SetActive(false);
        }


        trajectoryDots.SetActive(false);
    }


    public void trajectoryActivator(bool activator)
    {
        trajectoryDots.SetActive(activator);
    }


    public void DrawDots() //MADNESSS!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        
        for (var k = 0; k < GameController._instance.Manager.PlayerManager.TrajectoryManager.numberOfDots; k++)
        {
            x1 = GameController._instance.Manager.PlayerManager.PlayerPosition.x +
                 GameController._instance.Manager.PlayerManager.getShotForce().x * Time.fixedDeltaTime *
                 (GameController._instance.Manager.PlayerManager.TrajectoryManager.dotSeparation * k +
                  GameController._instance.Manager.PlayerManager.TrajectoryManager.dotShift);
            y1 = GameController._instance.Manager.PlayerManager.PlayerPosition.y +
                 GameController._instance.Manager.PlayerManager.getShotForce().y * Time.fixedDeltaTime *
                 (GameController._instance.Manager.PlayerManager.TrajectoryManager.dotSeparation * k +
                  GameController._instance.Manager.PlayerManager.TrajectoryManager.dotShift) -
                 (-2 * Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime *
                  (GameController._instance.Manager.PlayerManager.TrajectoryManager.dotSeparation * k +
                   GameController._instance.Manager.PlayerManager.TrajectoryManager.dotShift) *
                  (GameController._instance.Manager.PlayerManager.TrajectoryManager.dotSeparation * k +
                   GameController._instance.Manager.PlayerManager.TrajectoryManager.dotShift));


            dots[k].transform.position =
                new Vector3(x1, y1, dots[k].transform.position.z);
        }
    }


    public bool TrajectoryIsOkay() // It allows to show trajectory path.
    {
        Vector2 tempDiff = GameController._instance.Manager.PlayerManager.getBallFingerDiff();

        if (Mathf.Sqrt((tempDiff.x * tempDiff.x) + (tempDiff.y * tempDiff.y)) >
            GameController._instance.Manager.PlayerManager.TrajectoryManager.TracjectoryAppearValue)
        {
            return true;
        }


        return false;
    }
}