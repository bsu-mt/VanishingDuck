//��ը�õĹ��ܣ������˼���-1
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Red : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //��ȡ����ϵ�PlayerStageCounter
            PlayerStageCounter playerScript = collision.gameObject.GetComponent<PlayerStageCounter>();

            if (playerScript != null)
            {
                //������ҵ� Stage2_count
                playerScript.Stage2_count -= 1;
            }
        }
    }
}
