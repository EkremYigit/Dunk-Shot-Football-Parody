using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using DefaultNamespace;
using UnityEditor.Animations;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;

public class ObjectPoolItem
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
}

public class GameController : MonoBehaviour
{
    private static List<ObjectPoolItem> GameInvantory; // it containt all game objects.
    public static GameController _instance;
    private static GameObject prizeGenerator;
    private static GameObject tempPerfectEffect;
    private GameObject projectile;
    private GameObject projectileMemory;
    public GeneralManager Manager;
    public GameObject panel;
    public Transform Player;
    public Animator PlayerAnim;
    public Color currentColor = Color.white;
    private static int mapCounter = 0;
    private static int t1, t2, t3, t4;
    private static int bouncyCounter;
    private float NumOfBasket1, NumOfBasket2, NumOfBasket3, NumOfBasket4;
    private static GameObject tempAnim;
    private int replacemented = 0;
    private float Perfect = 0;
    private int ReferanceCounterToResume;
    private static ObjectPoolItem P_Item, P_Item1, P_Item2;
    private static ObjectPoolItem ReferancePoolCordinates;
    private GameObject Prizes;
    private float _offsetDistance;
    public Transform ProjecTileParent;
    public Transform GoalPostBlockers;

    private int _Step = 0;
    private int _animStep = 0;
    private GameObject blocker;

    public GameObject backGroundTemplate;

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

        GameInvantory = new List<ObjectPoolItem>();
        P_Item = new ObjectPoolItem();
        P_Item1 = new ObjectPoolItem();
        ReferancePoolCordinates = new ObjectPoolItem();

        _offsetDistance = _instance.Manager.LevelManager.InitialPositionY;
        transform.position =
            new Vector3(Manager.LevelManager.InitialPositionX, Manager.LevelManager.InitialPositionY, 0);


        BasketPoolInitilazer();

        MapInitilazer();
        GameInvantory.Add(ReferancePoolCordinates);
    }


    private void Start()
    {
        _animStep = 0;
    }


    public GameObject getObjectPoolItem(int a)
    {
        if (a >= 0 && a < P_Item.amountToPool) return P_Item.pooledObjects[a];
        else
        {
            return null;
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < P_Item.pooledObjects.Count; i++)
        {
            if (!P_Item.pooledObjects[i].activeInHierarchy)
            {
                return P_Item.pooledObjects[i];
            }
        }


        return null;
    }


    public void Replacement()
    {
        _animStep = replacemented;
        if (replacemented != 0)
        {
            if (_Step + 1 != GameInvantory[0].pooledObjects.Count)
                GameObject.Find("Main Camera").GetComponent<CameraController>()
                    .UpdateCameraPosition(GameInvantory[0].pooledObjects[_Step + 1].transform.position.y);
        }
        
        //  GameObject.Find("Barriers").GetComponent<BarrierController>().BarrierGeneration();

        ProjectileCleaner(); ///6 AGUSTOS LAST HİT.
        replacemented++;


        if (replacemented >= Manager.LevelManager._VisibleBasket)
        {
            FadeToBlack();
            StepUp();
        }


        AirPlaneAppear(); //check air planes routine
        if (_Step >= 8)
        {
            int temp2 = UnityEngine.Random.Range(0, 10);
            if (Manager.LevelManager.RatioToGenerateProjectile / 10 >= temp2)
            {
                if (getStep() >= 8 && getStep() < 14) ProjectileGeneration(1);
                if (getStep() >= 14 && getStep() < 22) ProjectileGeneration(2);
                if (getStep() >= 22) ProjectileGeneration(3);
            }
        }

        if (_Step >= 20)
        {
            GoalPostBarrierGeneration();
        }
    }


    private void StepUp()
    {
        if (GameInvantory[0].amountToPool >= Manager.LevelManager._VisibleBasket &&
            Manager.LevelManager._VisibleBasket > 1)
        {
            getObjectPoolItem(_Step).SetActive(false);
            if (_Step + Manager.LevelManager._VisibleBasket < GameInvantory[0].amountToPool)
                getObjectPoolItem(_Step + Manager.LevelManager._VisibleBasket).SetActive(true);

            _Step++;
            GameInvantory[0].pooledObjects[_Step].GetComponent<GoalPost>().OnLanding();
            //  GameInvantory[0].pooledObjects[_Step].GetComponent<EnterBallAnim>().setCounter(1);
            Invoke("animationIdler", 0.2f);
        }
    }


    public void ProjectileGeneration(int NumOf)
    {
        for (int i = 0; i < NumOf; i++)
        {
            projectile = Instantiate(_instance.Manager.Projectiles.PrefabToShow, ProjecTileParent);
            projectile.GetComponent<ProjectileScript>().PositionProjectile(i);
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }


    private void animationIdler()
    {
        GameInvantory[0].pooledObjects[_Step].GetComponent<EnterBallAnim>().setCounter(0);
        GameInvantory[0].pooledObjects[_Step].GetComponent<Lv1Script>().RunAnimation();
    }


    public void OnFail()
    {
        PlayerAnim.SetBool("Explode", false);
        Player.gameObject.SetActive(false);
        _instance.panel.SetActive(true);
    }

    public void OnSuccess() //When Ball Enter Successfully This function is Called.
    {
        ProjectileCleaner();
        BarrierCleaner();
        print("Successful Shot");
        GameObject.Find("Barriers").GetComponent<BarrierController>().BarrierGeneration();
    }


    public void OnLevelComplete()
    {
        print("Level Is Done");
    }

    public void OnResume()
    {
        _instance.panel.SetActive(false);


        Player.position = GameInvantory[0].pooledObjects[_Step].transform.position;
        Player.gameObject.SetActive(true);

        GameInvantory[0].pooledObjects[_Step].GetComponent<Animator>().SetBool("ResetGame", true);

        if (_Step + 1 < GameInvantory[0].amountToPool)
            GameInvantory[0].pooledObjects[_Step + 1].GetComponent<GoalPostController>().ContinuePerfectState();

        Player.GetComponent<SpriteRenderer>().sprite = Manager.DesignManager.Player;
        Player.GetComponent<PlayerController>().bouncyCounter = 0;
        Player.GetComponent<PlayerController>().BouncyFlag = false;
     
    }


    public void OnPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerController._instance.GetComponent<Rigidbody2D>().isKinematic = false;
    }


    public void ProjectileCleaner()
    {
        for (int i = 0; i < ProjecTileParent.childCount; i++)
        {
            Destroy(ProjecTileParent.GetChild(i).gameObject);
        }
    }

    public void BarrierCleaner()
    {
        for (int i = 0; i < GoalPostBlockers.childCount; i++)
        {
            Destroy(GoalPostBlockers.GetChild(i).gameObject);
        }
    }


    public void PerfectShot(float a)  //if a == 0 it is false to counter.
    {
        if (_Step != 0)
        {
            if (a != 0)
            {
                Perfect++;
            }
            else
            {
                Perfect = 0;
               
                return;
            }


            Vector3 tempPosition =
                GameInvantory[0].pooledObjects[_Step].transform.position; //fonksiyona yollanacakdeger
            tempAnim = Instantiate(_instance.Manager.ObjectAnimationManager.PrefabToShow, Vector3.zero,
                Quaternion.identity);
            tempAnim.GetComponent<PerfectEffectScript>().RunPerfectEffect1(tempPosition, Perfect);


            if (Perfect == 2)
            {
                if (PlayerController._instance.transform.GetChild(0).GetChild(0).gameObject.activeSelf != true)
                {
                    PlayerController._instance.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
            }

            else if (Perfect >= 3 && Perfect < 6)
            {
               
                if (PlayerController._instance.transform.GetChild(0).GetChild(1).gameObject.activeSelf != true)
                    ActivePerfect(1);
            }
            else if (Perfect >= 6 && Perfect < 9)
            {
               
                if (PlayerController._instance.transform.GetChild(0).GetChild(2).gameObject.activeSelf != true)
                    ActivePerfect(2);
            }
            else if (Perfect >= 9)
            {
               
                tempAnim.GetComponent<PerfectEffectScript>().RunPerfectEffect1(tempPosition, Perfect);
                if (PlayerController._instance.transform.GetChild(0).GetChild(3).gameObject.activeSelf != true)
                    ActivePerfect(3);
            }
        }
    }


    public void GoalPostBarrierGeneration() // if step >20
    {
        blocker = Instantiate(Manager.GoalPostBlocker.PrefabToShow, GoalPostBlockers);
        if (_Step + 1 <= GameInvantory[0].pooledObjects.Count)
        {
            Vector3 tempPosition = GameInvantory[0].pooledObjects[_Step + 1].transform.position;
            tempPosition += new Vector3(Manager.GoalPostBlocker.Xaxis, Manager.GoalPostBlocker.Yaxis);
            blocker.transform.position = tempPosition;
        }
    }


    public void ActivePerfect(int ChildIndex)
    {
        for (int i = 1; i < PlayerController._instance.transform.GetChild(0).childCount; ++i)
        {
            if (i != ChildIndex)
                PlayerController._instance.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            else
            {
                PlayerController._instance.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            } // or false
        }
    }


    public void InActivePerfect()
    {
        //PlayerController._instance.transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < PlayerController._instance.transform.GetChild(0).childCount; ++i)
        {
            PlayerController._instance.transform.GetChild(0).GetChild(i).gameObject.SetActive(false); // or false
        }
    }


    public ObjectPoolItem getEnvantory(int Index)
    {
        return GameInvantory[Index];
    }

    public void PlayerDragging()

    {
        // print("ULASMA YER 2 Player Dragging ve Step = -->"+_Step);
        GameInvantory[0].pooledObjects[_Step].GetComponent<DragAndThrow>().DragTheBasket();
    }


    public void QuartinDrag()
    {
        //  print("QuartinDrag ACTIVATED");
        GameInvantory[0].pooledObjects[_Step].GetComponent<GoalPost>().MouseSpinOn();
        //Mouse basılı oldugunda drag ediyor.
    }


    public void PlayerThrowing()
    {
        GameInvantory[0].pooledObjects[_Step].GetComponent<DragAndThrow>().ThrowTheBasket();
    }




    public void QuartinThrow() // mouse bırakıldıgında ikiside patates.
    {
        //  print("QuartinThrow");
        GameInvantory[0].pooledObjects[_Step].GetComponent<GoalPost>().OnMouseUpped();
        GameInvantory[0].pooledObjects[_Step].GetComponent<GoalPost>().MouseSpinOff();
    }


    public void FadeToBlack()
    {
        currentColor = Color.Lerp(currentColor, Color.black, (Time.deltaTime * (TotalBasket() * 3) / 40));
    }


    float TotalBasket()
    {
        return GameController._instance.Manager.LevelManager._NumOfGoalPostsST1 +
               GameController._instance.Manager.LevelManager._NumOfGoalPostsST2 +
               GameController._instance.Manager.LevelManager._NumOfGoalPostsST3
               + GameController._instance.Manager.LevelManager._NumOfGoalPostsST4;
    }


    public void setPerfect(float P)
    {
        Perfect = P;
    }

    public float getPerfect()
    {
        return this.Perfect;
    }

    public float getStep()
    {
        return _Step;
    }


    public void setAnimStep(int a)
    {
        if (a == 0)
        {
            GameInvantory[0].pooledObjects[0].transform.GetComponent<Animator>().SetBool("ResetGame", true);
        }

        _Step = a;
    }


    void MapInitilazer()
    {
        mapCounter = 0;
        P_Item2 = new ObjectPoolItem();
        P_Item2.pooledObjects = new List<GameObject>();
        int _numOfMap = GetNumberOfMap(); //return amount of background repeat for whole game;
        float Yoffset = 20.91f;
        Vector3 tempPos;
        GameObject Obj = Instantiate(backGroundTemplate, Vector3.zero, Quaternion.identity);
        //Obj.GetComponent<SpriteRenderer>().sprite = Manager.DesignManager.BackgroundInitial;
        Obj.SetActive(true);


        P_Item2.pooledObjects.Add(Obj);
        mapCounter++;
        //Yoffset = 2*Obj.transform.GetComponent<SpriteRenderer>().sprite.bounds.max.y;
        print("Yoffset Top Value " + Yoffset);
        tempPos = new Vector3(Obj.transform.position.x, Obj.transform.position.y + Yoffset, Obj.transform.position.z);


        for (int i = 0; i < _numOfMap; i++)
        {
            if (t1 > 0)
            {
                Obj = Instantiate(backGroundTemplate, tempPos, Quaternion.identity);
                //  Obj.GetComponent<SpriteRenderer>().sprite = Manager.DesignManager.BackgroundST1;
                Obj.SetActive(false);
                P_Item2.pooledObjects.Add(Obj);
                t1--;
            }
            else if (t2 > 0)
            {
                Obj = Instantiate(backGroundTemplate, tempPos, Quaternion.identity);
                // Obj.GetComponent<SpriteRenderer>().sprite = Manager.DesignManager.BackgroundST2;
                Obj.SetActive(false);
                P_Item2.pooledObjects.Add(Obj);
                t2--;
            }
            else if (t3 > 0)
            {
                Obj = Instantiate(backGroundTemplate, tempPos, Quaternion.identity);
                //  Obj.GetComponent<SpriteRenderer>().sprite = Manager.DesignManager.BackgroundST3;
                Obj.SetActive(false);
                P_Item2.pooledObjects.Add(Obj);
                t3--;
            }
            else if (t4 > 0)
            {
                Obj = Instantiate(backGroundTemplate, tempPos, Quaternion.identity);
                //  Obj.GetComponent<SpriteRenderer>().sprite = Manager.DesignManager.BackgroundST4;
                Obj.SetActive(false);
                P_Item2.pooledObjects.Add(Obj);
                t4--;
            }

            //update for next map position;
            // Yoffset = 2*Obj.transform.GetComponent<SpriteRenderer>().sprite.bounds.max.y;
            tempPos = new Vector3(Obj.transform.position.x, Obj.transform.position.y + Yoffset,
                Obj.transform.position.z);
        }


        P_Item2.pooledObjects[mapCounter].gameObject.SetActive(true);
        mapCounter++;
        GameInvantory.Add(P_Item2);
    }


    public void AddMap()
    {
        if (GameInvantory[2] != null && mapCounter < GameInvantory[2].pooledObjects.Count)
        {
            GameInvantory[2].pooledObjects[mapCounter - 2].gameObject.SetActive(false);
            GameInvantory[2].pooledObjects[mapCounter].gameObject.SetActive(true);
            mapCounter++;
        }
    }


    public void AirPlaneAppear()
    {
        if (_Step % 5 == 0 && _Step != 0)
        {
            OnLevelComplete();
            Vector3 airPos = new Vector3(0, PlayerController._instance.transform.position.y, 0);
            airPos += new Vector3(-7.62f, 2.8f, 0);

            Instantiate(Manager.AirPlane.PrefabToShow, airPos, Quaternion.identity);
        }
    }


    int GetNumberOfMap()
    {
        if (Manager.MapManager._NumOfRepeatBG1 > 0 && Manager.MapManager._NumOfRepeatBG2 > 0 &&
            Manager.MapManager._NumOfRepeatBG3 > 0 &&
            Manager.MapManager._NumOfRepeatBG4 > 0)
        {
            t1 = Manager.MapManager._NumOfRepeatBG1;
            t2 = Manager.MapManager._NumOfRepeatBG2;
            t3 = Manager.MapManager._NumOfRepeatBG3;
            t4 = Manager.MapManager._NumOfRepeatBG4;
            return t1 + t2 + t3 + t4;
        }
        else
        {
            print("One or more Map Manager Value is equal to 0 !!");
            return 0;
        }
    }


    private void BasketPoolInitilazer()
    {
        Vector3 prizePosition = new Vector3(0, 0, 0);

        //print("PoolInitilazer running.");
        P_Item.pooledObjects = new List<GameObject>();
        P_Item1.pooledObjects = new List<GameObject>();
        ReferancePoolCordinates.pooledObjects = new List<GameObject>();

        P_Item.amountToPool = Manager.LevelManager._NumOfGoalPostsST1 + Manager.LevelManager._NumOfGoalPostsST2 +
                              Manager.LevelManager._NumOfGoalPostsST3
                              + Manager.LevelManager._NumOfGoalPostsST4;

        NumOfBasket1 = Manager.LevelManager._NumOfGoalPostsST1;
        NumOfBasket2 = Manager.LevelManager._NumOfGoalPostsST2;
        NumOfBasket3 = Manager.LevelManager._NumOfGoalPostsST3;
        NumOfBasket4 = Manager.LevelManager._NumOfGoalPostsST4;
        GameObject obj = Instantiate(Manager.LevelManager.GoalPostPrefab, transform.position, Quaternion.identity);
        obj.SetActive(false);
        P_Item.pooledObjects.Add(obj);
        P_Item.pooledObjects[0].GetComponent<Lv1Script>().RunAnimation();
        NumOfBasket1--;


        float multiplier;


        for (int i = 1; i < P_Item.amountToPool; i++)
        {
            if (i % 2 == 1) multiplier = -1; //rManager.LevelManager.ight lManager.LevelManager.eft situation.
            else
            {
                multiplier = +1;
            }

            if (NumOfBasket1 > 0)
            {
                obj = Instantiate(Manager.LevelManager.GoalPostPrefab, transform.position, Quaternion.identity);
                obj.SetActive(false);
                P_Item.pooledObjects.Add(obj);

                P_Item.pooledObjects[i].GetComponent<GoalPost>().PositionGenerator(
                    Manager.LevelManager.leftX * multiplier,
                    Manager.LevelManager.rightX * multiplier, Manager.LevelManager.minY + _offsetDistance,
                    Manager.LevelManager.maxY + _offsetDistance);

                _offsetDistance = P_Item.pooledObjects[i].transform.position.y;


                P_Item.pooledObjects[i].GetComponent<Lv1Script>().RunAnimation();
                NumOfBasket1--;
            }
            else if (NumOfBasket2 > 0)
            {
                obj = Instantiate(Manager.LevelManager.GoalPostPrefab, transform.position, Quaternion.identity);

                obj.SetActive(false);
                P_Item.pooledObjects.Add(obj);

                P_Item.pooledObjects[i].GetComponent<GoalPost>().PositionGenerator(
                    Manager.LevelManager.leftX * multiplier,
                    Manager.LevelManager.rightX * multiplier, Manager.LevelManager.minY + _offsetDistance,
                    Manager.LevelManager.maxY + _offsetDistance);
                _offsetDistance = P_Item.pooledObjects[i].transform.position.y;
                P_Item.pooledObjects[i].GetComponent<Lv2Script>().RunAnimation();
                NumOfBasket2--;
            }
            else if (NumOfBasket3 > 0)
            {
                obj = Instantiate(Manager.LevelManager.GoalPostPrefab, transform.position, Quaternion.identity);
                obj.SetActive(false);
                //it makes lv2 basket.
                P_Item.pooledObjects.Add(obj);

                P_Item.pooledObjects[i].GetComponent<GoalPost>().PositionGenerator(
                    Manager.LevelManager.leftX * multiplier,
                    Manager.LevelManager.rightX * multiplier, Manager.LevelManager.minY + _offsetDistance,
                    Manager.LevelManager.maxY + _offsetDistance);
                _offsetDistance = P_Item.pooledObjects[i].transform.position.y;


                if (multiplier > 0)
                {
                    //print("Multiplier  <0 if içinie Girdi 3.seviye yaratilirken->>>>>>>>>>>>>>>>>>>");
                    P_Item.pooledObjects[i].GetComponent<Lv3Script>().RunAnimationPlus();
                }
                else
                {
                    //print("Multiplier  <0 ELSE içinie Girdi 3.seviye yaratilirken->>>>>>>>>>>>>>>>>>>");
                    P_Item.pooledObjects[i].GetComponent<Lv3Script>().RunAnimationMinus();
                }

                NumOfBasket3--;
            }

            else if (NumOfBasket4 > 0)
            {
                obj = Instantiate(Manager.LevelManager.GoalPostPrefab, transform.position, Quaternion.identity);
                obj.SetActive(false);
                //it makes lv2 basket.
                P_Item.pooledObjects.Add(obj);

                P_Item.pooledObjects[i].GetComponent<GoalPost>().PositionGenerator(
                    (Manager.LevelManager.leftX) * multiplier,
                    (1f + Manager.LevelManager.rightX) * multiplier,
                    Manager.LevelManager.minY + _offsetDistance + Manager.LevelManager.tier4Y,
                    Manager.LevelManager.maxY + _offsetDistance + Manager.LevelManager.tier4Y);
                _offsetDistance = P_Item.pooledObjects[i].transform.position.y;

                ReferancePoolCordinates.pooledObjects.Add(obj);


                P_Item.pooledObjects[i].GetComponent<Lv4Script>().RunAnimation();
                NumOfBasket4--;
            }
        }

        GameInvantory.Add(P_Item);
        GameInvantory.Add(P_Item1);


        Manager.BackgroundManager.ScaleVariable = new Vector2(
            GameInvantory[0].pooledObjects[GameInvantory[0].pooledObjects.Count - 1].transform.position.x,
            GameInvantory[0].pooledObjects[GameInvantory[0].pooledObjects.Count - 1].transform.position
                .y + 12f);

        for (var i = 0; i < Manager.LevelManager._VisibleBasket; i++) //4 baskets exist  but just 2 of them shows.
        {
            // print("Ekrana Yerleştiriliyor.");
            P_Item.pooledObjects[i].SetActive(true);
        }
    }
}