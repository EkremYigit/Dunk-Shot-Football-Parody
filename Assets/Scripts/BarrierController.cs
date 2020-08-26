using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject Lv1Barrier, Lv2Barrier, Lv3Barrier;
    private GameObject barrier;
    public GameObject Player;


    void Start()
    {
        if (GameController._instance.Manager.DesignManager.Lv1Barrier != null &&
            GameController._instance.Manager.DesignManager.Lv2Barrier != null &&
            GameController._instance.Manager.DesignManager.Lv3Barrier != null)
        {
            Lv1Barrier.GetComponent<SpriteRenderer>().sprite =
                GameController._instance.Manager.DesignManager.Lv1Barrier;
            Lv2Barrier.GetComponent<SpriteRenderer>().sprite =
                GameController._instance.Manager.DesignManager.Lv2Barrier;
            Lv3Barrier.GetComponent<SpriteRenderer>().sprite =
                GameController._instance.Manager.DesignManager.Lv3Barrier;
        }
    }

    public void BarrierGeneration() //Barrier Generation of certain steps with ratio.Default Ratio %100.
    {
        var generateRatio = GameController._instance.Manager.BarrierManager.BarrierToGenerateRatio / 10;
        float value = UnityEngine.Random.Range(0, 10);

        if (generateRatio >= value)
        {
            value = UnityEngine.Random.Range(0, 10);

            if (GameController._instance.getStep() >= 5 && GameController._instance.getStep() < 9)
            {
                barrier = Instantiate(Resources.Load<GameObject>("Lv1Barrier"),
                    GameController._instance.GoalPostBlockers);
                barrier.transform.position = barrier.transform.GetComponent<Lv1Barrier>()
                    .GenerateLv1BarrierPosition(Player.transform.position,
                        GameController._instance.Manager.BarrierManager.RandomGenerateXRange,
                        GameController._instance.Manager.BarrierManager.RandomGenerateYRange);
            }

            else if (GameController._instance.getStep() >= 9 && GameController._instance.getStep() < 15)
            {
                barrier = Instantiate(Resources.Load<GameObject>("Lv2Barrier"),
                    GameController._instance.GoalPostBlockers);
                barrier.transform.position = barrier.transform.GetComponent<Lv2Barrier>()
                    .GenerateLv2BarrierPosition(Player.transform.position,
                        GameController._instance.Manager.BarrierManager.RandomGenerateXRange,
                        GameController._instance.Manager.BarrierManager.RandomGenerateYRange);
            }

            else if (GameController._instance.getStep() >= 15)
            {
                barrier = Instantiate(Resources.Load<GameObject>("Lv3Barrier"),
                    GameController._instance.GoalPostBlockers);
                barrier.transform.position = barrier.transform.GetComponent<Lv3Barrier>()
                    .GenerateLv3BarrierPosition(Player.transform.position,
                        GameController._instance.Manager.BarrierManager.RandomGenerateXRange,
                        GameController._instance.Manager.BarrierManager.RandomGenerateYRange);
            }
        }
    }
}