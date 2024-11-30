using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance; // 单例模式，方便全局访问

    public int rabbitCount = 0; // 全局计数器
    public int requiredCount = 3; // 需要抓住的兔子数量
    public Start2 start2Script; // 引用 Start2 脚本

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

    public void IncrementRabbitCount()
    {
        rabbitCount++;
        Debug.Log($"Global Rabbit Count: {rabbitCount}");

        if (rabbitCount >= requiredCount && start2Script != null)
        {
            start2Script.enabled = true; // 启动第二阶段
            Debug.Log("Stage 2 started!");
        }
    }
}
