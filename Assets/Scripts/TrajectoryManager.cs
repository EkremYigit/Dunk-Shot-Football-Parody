using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TrajectoryManager : ScriptableObject
{
    
    public float TracjectoryAppearValue;
    public float initialDotSize;                                   //The initial size of the trajectoryDots game object
    public int numberOfDots;                                       //The number of points representing the trajectory
    public float dotSeparation;                                    //The space between the points representing the trajectory
    public float dotShift;                                         //How far the first dot is from the "ball"
   
}


