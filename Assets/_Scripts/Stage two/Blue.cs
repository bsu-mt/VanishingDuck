//晕头兔的功能，在碰撞时让玩家的视角随机旋转
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //获取玩家的摄像机
            Transform playerCamera = collision.gameObject.transform;

            if (playerCamera != null)
            {
                float randomYRotation = Random.Range(1f, 360f);

                //只旋转 Y 轴，使玩家的视角随机旋转
                playerCamera.eulerAngles = new Vector3(
                    playerCamera.eulerAngles.x,
                    randomYRotation,
                    playerCamera.eulerAngles.z
                );
            }
        }
    }
}
