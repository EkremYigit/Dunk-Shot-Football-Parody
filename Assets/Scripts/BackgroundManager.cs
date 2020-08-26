using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BackgroundManager : ScriptableObject
{
   
   
    public float CameraX_Offset ;
    public float CameraY_Offset;
    private Vector2 scaleVariable;


    public Vector2 ScaleVariable
    {
        get => scaleVariable;
        set => scaleVariable = value;
    }
}
