//��νű��������������ͷ�ϣ���¼�������stage1���ռ��������������������������Ϊ3����ҽ������͵��ڶ��׶γ���
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public int Stage1_count = 0; //��Ҽ�������
    public Vector3 teleportPosition = new Vector3(38.95341f, 0.1199989f, 200.7748f); //Ŀ�괫��λ��

    void Update()
    {
        if (Stage1_count == 3)
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        // ����Ҵ��͵�ָ��λ��
        transform.position = teleportPosition;
        Stage1_count = 0;
    }
}
