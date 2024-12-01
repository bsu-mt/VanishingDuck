using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance; // 单例模式，方便全局访问

    public int rabbitCount1 = 0; // Stage1计数器
    public int rabbitCount2 = 0; // Stage2计数器
    public int requiredCount = 3; // 需要抓住的兔子数量
    public Start2 start2Script; // 引用 Start2 脚本
    public Start3 start3Script; // 引用 Start3 脚本
    public bool isStage2 = false; // 是否为第二阶段
    public bool isStage3 = false; // 是否为第三阶段

    public XRRayInteractor leftRayInteractor;  // 左手射线交互器
    public XRRayInteractor rightRayInteractor; // 右手射线交互器
    public XRDirectInteractor leftDirectInteractor;  // 左手直接交互器
    public XRDirectInteractor rightDirectInteractor; // 右手直接交互器

    void Awake()
    {
        // 实现单例
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Stage1IncrementRabbitCount()
    {
        rabbitCount1++;
        Debug.Log($"Stage 1 Rabbit Count: {rabbitCount1}");

        if (rabbitCount1 >= requiredCount && start2Script != null)
        {
            start2Script.enabled = true; // 启动第二阶段
            isStage2 = true;
            Debug.Log("Stage 2 started!");
        }
    }

    public void Stage2IncrementRabbitCount()
    {
        rabbitCount2++;
        Debug.Log($"Stage 2 Rabbit Count: {rabbitCount2}");

        if (rabbitCount2 >= requiredCount && start3Script != null)
        {
            start3Script.enabled = true; // 启动第三阶段
            isStage3 = true;
            Debug.Log("Stage 3 started!");
        }
    }

    void Update()
    {
        // 检查当前是否进入第二阶段
        if (isStage2)
        {
            // 给玩家（XR Rig) 添加 Rigidbody
            GameObject xrRig = GameObject.FindObjectOfType<XROrigin>().gameObject;
            if (xrRig != null)
            {
                Rigidbody rb = xrRig.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    // 如果不存在 Rigidbody，则添加
                    rb = xrRig.AddComponent<Rigidbody>();
                    rb.useGravity = true; // 开启重力
                    rb.isKinematic = true; // 禁用 Kinematic
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // 限制 X 和 Z 的旋转
                    Debug.Log("Rigidbody added to XR Rig");
                }
            }
            else
            {
                Debug.LogError("XR Rig not found!");
            }


            //// 禁用射线交互器
            //if (leftRayInteractor != null)
            //    leftRayInteractor.gameObject.SetActive(false);

            //if (rightRayInteractor != null)
            //    rightRayInteractor.gameObject.SetActive(false);

            //// 启用直接交互器
            //if (leftDirectInteractor != null)
            //    leftDirectInteractor.gameObject.SetActive(true);

            //if (rightDirectInteractor != null)
            //    rightDirectInteractor.gameObject.SetActive(true);
        }
        //else
        //{
        //    // 恢复射线交互器
        //    if (leftRayInteractor != null)
        //        leftRayInteractor.gameObject.SetActive(true);

        //    if (rightRayInteractor != null)
        //        rightRayInteractor.gameObject.SetActive(true);

        //    // 禁用直接交互器
        //    if (leftDirectInteractor != null)
        //        leftDirectInteractor.gameObject.SetActive(false);

        //    if (rightDirectInteractor != null)
        //        rightDirectInteractor.gameObject.SetActive(false);
        //}

    }
}
