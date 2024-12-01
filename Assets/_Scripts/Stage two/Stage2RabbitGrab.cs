//这段代码主要是对stage2中收集的兔子数量进行一个计数，还有一些UI相关的内容会写到这里
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
        gameObject.SetActive(false); // 抓取后隐藏兔子

        // 通知 StageManager 增加计数
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
