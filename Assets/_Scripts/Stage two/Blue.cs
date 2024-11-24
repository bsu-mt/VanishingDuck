//��ͷ�õĹ��ܣ�����ײʱ����ҵ��ӽ������ת
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //��ȡ��ҵ������
            Transform playerCamera = collision.gameObject.transform;

            if (playerCamera != null)
            {
                float randomYRotation = Random.Range(1f, 360f);

                //ֻ��ת Y �ᣬʹ��ҵ��ӽ������ת
                playerCamera.eulerAngles = new Vector3(
                    playerCamera.eulerAngles.x,
                    randomYRotation,
                    playerCamera.eulerAngles.z
                );
            }
        }
    }
}
