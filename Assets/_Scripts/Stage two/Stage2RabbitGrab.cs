//��δ�����Ҫ�Ƕ�stage2���ռ���������������һ������������һЩUI��ص����ݻ�д������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage2RabbitGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        gameObject.SetActive(false); // ץȡ����������

        // ֪ͨ StageManager ���Ӽ���
        if (StageManager.Instance != null)
        {
            StageManager.Instance.Stage2IncrementRabbitCount();
        }
        else
        {
            Debug.LogError("StageManager instance not found!");
        }
    }

}
