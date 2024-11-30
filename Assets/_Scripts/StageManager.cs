using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance; // ����ģʽ������ȫ�ַ���

    public int rabbitCount = 0; // ȫ�ּ�����
    public int requiredCount = 3; // ��Ҫץס����������
    public Start2 start2Script; // ���� Start2 �ű�
    public bool isStage2 = false; // �Ƿ�Ϊ�ڶ��׶�

    public XRRayInteractor leftRayInteractor;  // �������߽�����
    public XRRayInteractor rightRayInteractor; // �������߽�����
    public XRDirectInteractor leftDirectInteractor;  // ����ֱ�ӽ�����
    public XRDirectInteractor rightDirectInteractor; // ����ֱ�ӽ�����

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
            isStage2 = true;
            Debug.Log("Stage 2 started!");
        }
    }

    void Update()
    {
        // ��鵱ǰ�Ƿ����ڶ��׶�
        if (isStage2)
        {
            // ����ң�XR Rig) ��� Rigidbody
            GameObject xrRig = GameObject.FindObjectOfType<XROrigin>().gameObject;
            if (xrRig != null)
            {
                Rigidbody rb = xrRig.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    // ��������� Rigidbody�������
                    rb = xrRig.AddComponent<Rigidbody>();
                    rb.useGravity = true; // ��������
                    rb.isKinematic = false; // ���� Kinematic
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // ���� X �� Z ����ת
                    Debug.Log("Rigidbody added to XR Rig");
                }
            }
            else
            {
                Debug.LogError("XR Rig not found!");
            }



            // �������߽�����
            if (leftRayInteractor != null)
                leftRayInteractor.gameObject.SetActive(false);

            if (rightRayInteractor != null)
                rightRayInteractor.gameObject.SetActive(false);

            // ����ֱ�ӽ�����
            if (leftDirectInteractor != null)
                leftDirectInteractor.gameObject.SetActive(true);

            if (rightDirectInteractor != null)
                rightDirectInteractor.gameObject.SetActive(true);
        }
        else
        {
            // �ָ����߽�����
            if (leftRayInteractor != null)
                leftRayInteractor.gameObject.SetActive(true);

            if (rightRayInteractor != null)
                rightRayInteractor.gameObject.SetActive(true);

            // ����ֱ�ӽ�����
            if (leftDirectInteractor != null)
                leftDirectInteractor.gameObject.SetActive(false);

            if (rightDirectInteractor != null)
                rightDirectInteractor.gameObject.SetActive(false);
        }

    }
}
