using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance; // ����ģʽ������ȫ�ַ���

    public int rabbitCount1 = 0; // Stage1������
    public int rabbitCount2 = 0; // Stage2������
    public int requiredCount = 3; // ��Ҫץס����������
    public Start2 start2Script; // ���� Start2 �ű�
    public Start3 start3Script; // ���� Start3 �ű�
    public bool isStage2 = false; // �Ƿ�Ϊ�ڶ��׶�
    public bool isStage3 = false; // �Ƿ�Ϊ�����׶�

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

    public void Stage1IncrementRabbitCount()
    {
        rabbitCount1++;
        Debug.Log($"Stage 1 Rabbit Count: {rabbitCount1}");

        if (rabbitCount1 >= requiredCount && start2Script != null)
        {
            start2Script.enabled = true; // �����ڶ��׶�
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
            start3Script.enabled = true; // ���������׶�
            isStage3 = true;
            Debug.Log("Stage 3 started!");
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
                    rb.isKinematic = true; // ���� Kinematic
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // ���� X �� Z ����ת
                    Debug.Log("Rigidbody added to XR Rig");
                }
            }
            else
            {
                Debug.LogError("XR Rig not found!");
            }


            //// �������߽�����
            //if (leftRayInteractor != null)
            //    leftRayInteractor.gameObject.SetActive(false);

            //if (rightRayInteractor != null)
            //    rightRayInteractor.gameObject.SetActive(false);

            //// ����ֱ�ӽ�����
            //if (leftDirectInteractor != null)
            //    leftDirectInteractor.gameObject.SetActive(true);

            //if (rightDirectInteractor != null)
            //    rightDirectInteractor.gameObject.SetActive(true);
        }
        //else
        //{
        //    // �ָ����߽�����
        //    if (leftRayInteractor != null)
        //        leftRayInteractor.gameObject.SetActive(true);

        //    if (rightRayInteractor != null)
        //        rightRayInteractor.gameObject.SetActive(true);

        //    // ����ֱ�ӽ�����
        //    if (leftDirectInteractor != null)
        //        leftDirectInteractor.gameObject.SetActive(false);

        //    if (rightDirectInteractor != null)
        //        rightDirectInteractor.gameObject.SetActive(false);
        //}

    }
}
