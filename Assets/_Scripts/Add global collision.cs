using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//README����δ��뱻������һ���յ�GameObject����Ϊȫ��������Ʒ���һ��������ײ

public class AddColliders : MonoBehaviour
{
    // �Ƿ�ѡ isTrigger ��ѡ��
    public bool setIsTrigger = true;

    void Start()
    {
        //���õĸ��½���:�����㲻�������ײ����ı�ǩ���岢����

        // if (obj.tag == "NoCollision")
        // continue;

        //���´������Գ����е���������
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            //���һ��������ײ��Ĭ��ΪBoxCollider
            if (obj.GetComponent<Collider>() == null && obj.name != "north" && obj.name != "south" && obj.name != "west" && obj.name != "east" && obj.name != "075 Deer")
            {
                obj.AddComponent<BoxCollider>();
            }
        }
    }
}
