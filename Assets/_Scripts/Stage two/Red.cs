////爆炸兔的功能，碰到了计数-1
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Red : MonoBehaviour
//{
//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            //获取玩家上的PlayerStageCounter
//            PlayerStageCounter playerScript = collision.gameObject.GetComponent<PlayerStageCounter>();

//            if (playerScript != null)
//            {
//                //减少玩家的 Stage2_count
//                playerScript.Stage2_count -= 1;
//            }
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
    public float explosionForce = 10f; // 爆炸的力度
    public float explosionRadius = 5f; // 爆炸的影响范围

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 获取玩家的 Rigidbody
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // 计算爆炸方向
                Vector3 explosionDirection = (collision.gameObject.transform.position - transform.position).normalized;

                // 添加爆炸力
                playerRigidbody.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                Debug.Log("Player hit by explosion! Jumping back!");
            }
            else
            {
                Debug.LogError("Player Rigidbody not found!");
            }
        }
    }
}

