using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [CreateAssetMenu]
public class LevelManager : ScriptableObject
{
    public int _NumOfGoalPostsST1=2; 
    public int _NumOfGoalPostsST2;
    public int _NumOfGoalPostsST3; 
    public int _NumOfGoalPostsST4;
    public int _VisibleBasket;
    public int RatioToGenerateProjectile;
    public float InitialPositionX, InitialPositionY;
    public float leftX, rightX, minY, maxY;
    public float tier4Y;
    public GameObject GoalPostPrefab;

}
