//爆炸兔的功能，碰到了计数-1
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Red : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //获取玩家上的PlayerStageCounter
            PlayerStageCounter playerScript = collision.gameObject.GetComponent<PlayerStageCounter>();

            if (playerScript != null)
            {
                //减少玩家的 Stage2_count
                playerScript.Stage2_count -= 1;
            }
        }
    }
}
