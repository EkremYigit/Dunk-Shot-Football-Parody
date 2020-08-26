using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    public Rigidbody2D player;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            //GAME OVER SOUND EFFECT
            GameController._instance.OnFail();
        }
    }
}