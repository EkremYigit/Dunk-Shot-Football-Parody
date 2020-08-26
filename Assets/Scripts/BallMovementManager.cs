using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BallMovementManager : ScriptableObject
{
    public float shootingPowerX;
    public float MaxVelocityX;
    public float maxVelocityY;   
    public float shootingPowerY;
    public float LeavingDistance=0.3f;
    public float MinSpeedToStop = 0.00333f;
    public float NetSlower;
    
}
