using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BarrierManager : ScriptableObject
{
    //this ScriptableObject Allows to manage whole prizes.
    public int _RatioOfBarriersLv1;
    public int _RatioOfBarriersLv2;
    public int _RatioOfBarriersLv3;
    public float BarrierToGenerateRatio;
    public float RandomGenerateXRange;
    public float RandomGenerateYRange;
}