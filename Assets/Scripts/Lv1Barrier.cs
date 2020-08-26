using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv1Barrier : MonoBehaviour
{
    // Start is called before the first frame update
 
    public Vector3 GenerateLv1BarrierPosition(Vector3 oldPosition,float Xvalue,float Yvalue)
    {

        return new Vector3(0, oldPosition.y + UnityEngine.Random.Range(1.5f+Yvalue, 1.7f+Yvalue),
            oldPosition.z);

    }
}
