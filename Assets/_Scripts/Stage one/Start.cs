//��δ����ʹ�ó����ǵ����Խ������ʱ��Ϸ�ĵ�һ�׶ν��Ὺʼ����ֻ��Ҫ������ҵ������ӻ��ƶ�����������λ��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public Transform playerCamera; 
    public float xThreshold = 13.88f; //�����ǰ���

    private bool triggered = false;

    void Update()
    {
        if (!triggered && playerCamera.position.x > xThreshold)
        {
            triggered = true;
            //�趨����ص�
            MoveObject("Stage 1 rabbits", new Vector3(-3.232167f, -4.768372e-07f, 21.00322f));
            MoveObject("Stage 1 rabbits 2", new Vector3(-20.62f, -4.768372e-07f, 27.58f));
            MoveObject("Stage 1 rabbits 3", new Vector3(-4.29f, -4.768372e-07f, -26.49f));
        }
    }

    private void MoveObject(string tag, Vector3 targetPosition)
    {
        GameObject obj = GameObject.FindWithTag(tag);
        if (obj != null)
        {
            �������ƶ���Ŀ��λ��
            obj.transform.position = targetPosition;
        }
    }
}
