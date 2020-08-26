using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[CreateAssetMenu]
public class PlayerManager : ScriptableObject
{
    [SerializeField] private BallMovementManager movementManager;
    [SerializeField] private TrajectoryManager trajectoryManager;


    /// this 3 variables require for these two manager and their functional scripts.
    private Vector2 shotForce;
    private Vector2 ballFingerDiff;
    private Vector2 playerPosition;


    public GameObject ObjectToAttachPlayer;

    public BallMovementManager MovementManager
    {
        get => movementManager;
        set => movementManager = value;
    }

    public TrajectoryManager TrajectoryManager
    {
        get => trajectoryManager;
        set => trajectoryManager = value;
    }

    public Vector2 getShotForce()
    {
        return this.shotForce;
    }

    public void setShotForce(Vector2 SF)
    {
        this.shotForce = SF;
    }

    public Vector2 getBallFingerDiff()
    {
        return ballFingerDiff;
    }

    public void setBallFingerDiff(Vector2 BFD)
    {
        this.ballFingerDiff = BFD;
    }


    public Vector2 PlayerPosition
    {
        get => playerPosition;
        set => playerPosition = value;
    }
}