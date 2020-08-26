using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerfectEffectScript : MonoBehaviour
{
    private string newtext;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void RunPerfectEffect1(Vector2 Position, float number)
    {
        var tempPosition = new Vector3(
            Position.x + GameController._instance.Manager.ObjectAnimationManager.Xaxis + 0.4f,
            Position.y + GameController._instance.Manager.ObjectAnimationManager.Yaxis + 0.5f, 1);
        transform.position = tempPosition;
        newtext = "" + "+" + number;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newtext;
        anim = GetComponent<Animator>();
        anim.SetFloat("PerfectAnim1", number);

        //PERFECT SHOT EFFECT SOUND


        Invoke("destry", 2.5f);
    }


    void destry()
    {
        Destroy(gameObject);
    }
}