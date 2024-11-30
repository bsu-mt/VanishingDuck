//using Unity.XR.CoreUtils;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class ReplaceTreeOnGrab : MonoBehaviour
//{
//    public GameObject originalTree; // 原始树
//    public GameObject christmasTree; // 圣诞树

//    private XRGrabInteractable grabInteractable; // XR Grab Interactable 组件
//    private bool isTreeReplaced = false; // 防止重复触发

//    private PlayerTeleport playerTeleport; // 用于引用 PlayerTeleport 脚本

//    void Start()
//    {
//        // 获取 XR Grab Interactable 组件
//        grabInteractable = GetComponent<XRGrabInteractable>();

//        // 监听抓取事件
//        if (grabInteractable != null)
//        {
//            grabInteractable.selectEntered.AddListener(OnGrabbed); // 当抓取事件触发时调用 OnGrabbed 方法
//        }

//        // 获取场景中 XR Rig 上的 PlayerTeleport 脚本
//        GameObject xrRig = GameObject.FindObjectOfType<XROrigin>().gameObject; // 获取 XR Rig 的 GameObject
//        if (xrRig != null)
//        {
//            playerTeleport = xrRig.GetComponent<PlayerTeleport>();
//        }

//        if (playerTeleport == null)
//        {
//            Debug.LogError("PlayerTeleport script not found on the XR Rig.");
//        }
//    }

//    private void OnGrabbed(SelectEnterEventArgs args)
//    {
//        gameObject.SetActive(false); // 抓取后隐藏兔子

//        // 检查是否已经替换过树
//        if (!isTreeReplaced)
//        {
//            ReplaceTreeObject(originalTree, christmasTree);
//            isTreeReplaced = true; // 标记为已触发

//            // 增加 PlayerTeleport 的计数
//            if (playerTeleport != null)
//            {
//                playerTeleport.Stage1_count += 1;
//                Debug.Log($"Rabbit collected! Current count: {playerTeleport.Stage1_count}");
//            }
//        }
//    }

//    private void ReplaceTreeObject(GameObject original, GameObject replacement)
//    {
//        if (original != null && replacement != null)
//        {
//            // 将圣诞树移动到原始树的位置和旋转角度
//            replacement.transform.position = original.transform.position;
//            replacement.transform.rotation = original.transform.rotation;

//            // 替换逻辑
//            original.SetActive(false); // 隐藏原始树
//            replacement.SetActive(true); // 显示圣诞树
//        }
//    }

//    private void OnDestroy()
//    {
//        // 移除事件监听器（防止内存泄漏）
//        if (grabInteractable != null)
//        {
//            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
//        }
//    }
//}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReplaceTreeOnGrab : MonoBehaviour
{
    public GameObject originalTree; // 原始树
    public GameObject christmasTree; // 圣诞树

    private XRGrabInteractable grabInteractable; // XR Grab Interactable 组件
    private bool isTreeReplaced = false; // 防止重复触发

    void Start()
    {
        // 获取 XR Grab Interactable 组件
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 监听抓取事件
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed); // 当抓取事件触发时调用 OnGrabbed 方法
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        gameObject.SetActive(false); // 抓取后隐藏兔子

        // 检查是否已经替换过树
        if (!isTreeReplaced)
        {
            ReplaceTreeObject(originalTree, christmasTree);
            isTreeReplaced = true; // 标记为已触发

            // 通知 StageManager 增加计数
            if (StageManager.Instance != null)
            {
                StageManager.Instance.IncrementRabbitCount();
            }
            else
            {
                Debug.LogError("StageManager instance not found!");
            }
        }
    }

    private void ReplaceTreeObject(GameObject original, GameObject replacement)
    {
        if (original != null && replacement != null)
        {
            // 将圣诞树移动到原始树的位置和旋转角度
            replacement.transform.position = original.transform.position;
            replacement.transform.rotation = original.transform.rotation;

            // 替换逻辑
            original.SetActive(false); // 隐藏原始树
            replacement.SetActive(true); // 显示圣诞树
        }
    }

    private void OnDestroy()
    {
        // 移除事件监听器（防止内存泄漏）
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }
}
