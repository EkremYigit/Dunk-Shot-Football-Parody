using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    public float bouncyCounter;
    public bool firstClick;

    private GameObject ball;
    private Rigidbody2D ballRB;
    private Vector3 ballPos;
    private SpriteRenderer _spriteRenderer;
    private bool ballOnTheNet;
    private static GameObject Slot;
    private bool isPerfectShot;
    private bool DragClicked = false;
    private Vector3 tempBallPos;
    private Vector2 firstPosition;
    private Vector3 tempMouse;
    private Vector2 shotForce;
    private Vector2 tempForce;
    private GameObject memorySlot;
    private bool bouncyFlag;
    private static bool perfectFlag;
    private Vector2 tempVelocity;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bouncyCounter = 0;
        bouncyFlag = false;
        ball = gameObject;
        ballPos = ball.transform.position;
        GameController._instance.Manager.PlayerManager.PlayerPosition = ballPos;
        ballRB = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (GameController._instance.Manager.DesignManager.Player != null)
        {
            _spriteRenderer.sprite = GameController._instance.Manager.DesignManager.Player;
        }

        tempBallPos = ballPos;
        GameController._instance.Manager.PlayerManager.PlayerPosition = ballPos;
    }

    // Update is called once per frame
    void Update()
    {
        ballPos = ball.transform.position;
        GameController._instance.Manager.PlayerManager.PlayerPosition = ballPos;


        if (CheckAttach())
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstClick = true;
            }

            if (firstClick)
            {
                Slot.GetComponent<Animator>().SetBool("ResetDrag", false);
                DragClicked = true;
            }
        }


        if (DragClicked && (ballOnTheNet))
        {
            //  print("DragBall Giricek");
            DragBall();
        }
        else
        {
            FindObjectOfType<trajectoryScript>().trajectoryActivator(false);
        }


        if (Input.GetKeyUp(KeyCode.Mouse0) && (DragClicked || firstClick) && _instance.transform
                .GetComponent<trajectoryScript>().TrajectoryIsOkay())

        {
            if (ballOnTheNet)
            {
                //THROW SOUND EFFECT


                DragClicked = false;
                firstClick = false;
                ballRB.velocity = shotForce;
                RunThrowAnimation();
                FindObjectOfType<trajectoryScript>().trajectoryActivator(false);
                ballRB.isKinematic = false;
                isPerfectShot = true;
                ballOnTheNet = false;
                bouncyCounter = 0;
                bouncyFlag = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && firstClick)
        {
            DragClicked = false;
            firstClick = false;
            shotForce = Vector2.zero;
            Slot.GetComponent<Animator>().SetBool("ResetDrag", true);
            Slot.GetComponent<Animator>().SetBool("IsDragging", false);
        }
    }


    public void CurrentGoalPostReset()
    {
        Slot.GetComponent<Animator>().SetBool("ResetDrag", true);
        Slot.GetComponent<Animator>().SetBool("IsDragging", false);
    }


    void DragBall()
    {
        GameController._instance.Manager.PlayerManager.setBallFingerDiff(CalculateBallFingerDiff());
        tempVelocity = GameController._instance.Manager.PlayerManager.getBallFingerDiff();
        shotForce = new Vector2(
            tempVelocity.x * GameController._instance.Manager.PlayerManager.MovementManager.shootingPowerX,
            tempVelocity.y * GameController._instance.Manager.PlayerManager.MovementManager.shootingPowerY);

        GameController._instance.Manager.PlayerManager.setShotForce(shotForce);
        CheckForce();


        if (FindObjectOfType<trajectoryScript>().TrajectoryIsOkay()
        ) //it needs ballFingerDiff to calculate trajectoryDots;
        {
            FindObjectOfType<trajectoryScript>().DrawDots();


            FindObjectOfType<trajectoryScript>().trajectoryActivator(true);
        }
        else
        {
            FindObjectOfType<trajectoryScript>().trajectoryActivator(false);
        }


        RunDragAnimation();
    } //When User Touch and Drag This Function Runs.


    private void CheckForce() //Check Force to avoid unlimited shot.
    {
        float MaxX = GameController._instance.Manager.PlayerManager.MovementManager.MaxVelocityX;
        float MaxY = GameController._instance.Manager.PlayerManager.MovementManager.maxVelocityY;
        //  print("CheckForce");
        tempForce = shotForce;

        if (Mathf.Abs(tempForce.x) > MaxX)
        {
            tempForce.x = (tempForce.x < 0) ? tempForce.x = -1 * MaxX : tempForce.x = MaxX;
        }

        if (Mathf.Abs(tempForce.y) > MaxY)
        {
            tempForce.y = (tempForce.y < 0) ? tempForce.y = -1 * MaxY : tempForce.y = MaxY;
        }

        shotForce = tempForce;
        GameController._instance.Manager.PlayerManager.setShotForce(shotForce);
    }


    Vector2 CalculateBallFingerDiff()
    {
        Vector3 tempMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameController._instance.Manager.PlayerManager.setBallFingerDiff(new Vector2(ballPos.x - tempMouse.x,
            ballPos.y - tempMouse.y));

        return new Vector2(ballPos.x - tempMouse.x, ballPos.y - tempMouse.y);
    } //


    private bool CheckAttach() //Check is ball attached into basket when it is stopped
    {
        if (Slot != null)
        {
            Vector2 attachPoint =
                Slot.transform.GetChild(4).transform.position; // getchild(4) equals to attachPoint  !!;
            if (Mathf.Sqrt(transform.position.x * transform.position.x +
                           transform.position.y * transform.position.y) -
                Mathf.Sqrt(attachPoint.x * attachPoint.x +
                           attachPoint.y * attachPoint.y) >
                GameController._instance.Manager.PlayerManager.MovementManager.LeavingDistance &&
                ballOnTheNet)
            {
                ballOnTheNet = false; //    This function runs when the ball leaves  from to goal post.
            }
        }

        if (ballOnTheNet)
        {
            ballRB.velocity = Vector2.zero;
            ballRB.angularVelocity = 0;
            AttachToBall();
           
            ballRB.isKinematic = true;
            return true;
        }
        else
        {
            //// print("CheckAttach = false;");
            return false; //ball is not touching to net.;
        }
    }


    void AttachToBall()
    {
        if (ballOnTheNet)
        {
            Vector2 tempVector =
                new Vector2(Slot.transform.GetChild(4).position.x, Slot.transform.GetChild(4).position.y);

            GameController._instance.Manager.PlayerManager.PlayerPosition = tempVector;
            _instance.transform.position = tempVector;
        } //    When The Ball Hit The Backward Pole Attach To Ball into Goal Post
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoalPost")) //First connection between ball and goal post.
        {
            Slot = other.gameObject;

            if (memorySlot == null)
            {
                memorySlot = Slot;
            }
            else if (memorySlot != null)
            {
                if (bouncyFlag)
                {
                    print("Bouncy Score Will be added ");
                    bouncyFlag = false;
                    memorySlot = Slot;
                }
                else
                {
                    bouncyCounter = 0;
                    bouncyFlag = false;
                    //reset bouncy collection to avoid endless bouncy score bug.
                }
            }
        }
    }


    public void exitCollision()
    {
        //  print("On CollisionExit2D");
        ballOnTheNet = false;
    }


    public bool IsPerfectShot()
    {
        return isPerfectShot;
    }

    public void setIsPerfectShot(bool data)
    {
        isPerfectShot = data;
    }


    public Vector3 getBallDiff()
    {
        return GameController._instance.Manager.PlayerManager.getBallFingerDiff();
    }


    private void RunDragAnimation()
    {
        GameController._instance.PlayerDragging();
        GameController._instance.QuartinDrag();
    } //If user pull the goal post it runs.

    private void RunThrowAnimation()
    {
        GameController._instance.PlayerThrowing();
        GameController._instance.QuartinThrow();
    } //Throwing

    public bool BallOnTheNet
    {
        get => ballOnTheNet;
        set => ballOnTheNet = value;
    } //Some getters to use on trajectory and score


    public float BouncyCounter
    {
        get => bouncyCounter;
        set => bouncyCounter = value;
    }

    public bool BouncyFlag
    {
        get => bouncyFlag;
        set => bouncyFlag = value;
    }
}