//��δ�����Ҫ�Ƕ�stage2���ռ���������������һ������������һЩUI��ص����ݻ�д������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage2RabbitGrab : MonoBehaviour
{
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        gameObject.SetActive(false); // ץȡ����������

        // ֪ͨ StageManager ���Ӽ���
        if (StageManager.Instance != null)
        {
            StageManager.Instance.Stage1IncrementRabbitCount();
        }
        else
        {
            Debug.LogError("StageManager instance not found!");
        }
    }

}
