//��δ����ʹ�ó����ǵ����Խ������ʱ��Ϸ�ĵ�һ�׶ν��Ὺʼ����ֻ��Ҫ������ҵ������ӻ��ƶ�����������λ��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public Transform playerCamera; 
    public float xThreshold = 2f; //�����ǰ���

    private bool triggered = false;

    void Update()
    {
        if (!triggered && playerCamera.position.x < xThreshold)
        {
            triggered = true;
            //�趨����ص�
            MoveObject("Stage 1 rabbits", new Vector3(-8.29f, 0.0025f, -33.94f));
            MoveObject("Stage 1 rabbits 2", new Vector3(-1.232167f, 3f, 19.80322f));
            MoveObject("Stage 1 rabbits 3", new Vector3(-27.62f, 0.004f, 33.58f));
            
            
        }
    }

    private void MoveObject(string tag, Vector3 targetPosition)
    {
        GameObject obj = GameObject.FindWithTag(tag);
        if (obj != null)
        {
            // �������ƶ���Ŀ��λ��
            obj.transform.position = targetPosition;
        }
    }
}
