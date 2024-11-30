using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance; // ����ģʽ������ȫ�ַ���

    public int rabbitCount = 0; // ȫ�ּ�����
    public int requiredCount = 3; // ��Ҫץס����������
    public Start2 start2Script; // ���� Start2 �ű�

    void Awake()
    {
        // ʵ�ֵ���
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
            start2Script.enabled = true; // �����ڶ��׶�
            Debug.Log("Stage 2 started!");
        }
    }
}
