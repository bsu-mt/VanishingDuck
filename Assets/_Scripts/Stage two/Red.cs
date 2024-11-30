////��ը�õĹ��ܣ������˼���-1
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Red : MonoBehaviour
//{
//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            //��ȡ����ϵ�PlayerStageCounter
//            PlayerStageCounter playerScript = collision.gameObject.GetComponent<PlayerStageCounter>();

//            if (playerScript != null)
//            {
//                //������ҵ� Stage2_count
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
    public float explosionForce = 10f; // ��ը������
    public float explosionRadius = 5f; // ��ը��Ӱ�췶Χ

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ȡ��ҵ� Rigidbody
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // ���㱬ը����
                Vector3 explosionDirection = (collision.gameObject.transform.position - transform.position).normalized;

                // ��ӱ�ը��
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

