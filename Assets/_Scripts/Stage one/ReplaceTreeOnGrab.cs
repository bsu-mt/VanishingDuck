//using Unity.XR.CoreUtils;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class ReplaceTreeOnGrab : MonoBehaviour
//{
//    public GameObject originalTree; // ԭʼ��
//    public GameObject christmasTree; // ʥ����

//    private XRGrabInteractable grabInteractable; // XR Grab Interactable ���
//    private bool isTreeReplaced = false; // ��ֹ�ظ�����

//    private PlayerTeleport playerTeleport; // �������� PlayerTeleport �ű�

//    void Start()
//    {
//        // ��ȡ XR Grab Interactable ���
//        grabInteractable = GetComponent<XRGrabInteractable>();

//        // ����ץȡ�¼�
//        if (grabInteractable != null)
//        {
//            grabInteractable.selectEntered.AddListener(OnGrabbed); // ��ץȡ�¼�����ʱ���� OnGrabbed ����
//        }

//        // ��ȡ������ XR Rig �ϵ� PlayerTeleport �ű�
//        GameObject xrRig = GameObject.FindObjectOfType<XROrigin>().gameObject; // ��ȡ XR Rig �� GameObject
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
//        gameObject.SetActive(false); // ץȡ����������

//        // ����Ƿ��Ѿ��滻����
//        if (!isTreeReplaced)
//        {
//            ReplaceTreeObject(originalTree, christmasTree);
//            isTreeReplaced = true; // ���Ϊ�Ѵ���

//            // ���� PlayerTeleport �ļ���
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
//            // ��ʥ�����ƶ���ԭʼ����λ�ú���ת�Ƕ�
//            replacement.transform.position = original.transform.position;
//            replacement.transform.rotation = original.transform.rotation;

//            // �滻�߼�
//            original.SetActive(false); // ����ԭʼ��
//            replacement.SetActive(true); // ��ʾʥ����
//        }
//    }

//    private void OnDestroy()
//    {
//        // �Ƴ��¼�����������ֹ�ڴ�й©��
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
    public GameObject originalTree; // ԭʼ��
    public GameObject christmasTree; // ʥ����

    private XRGrabInteractable grabInteractable; // XR Grab Interactable ���
    private bool isTreeReplaced = false; // ��ֹ�ظ�����

    void Start()
    {
        // ��ȡ XR Grab Interactable ���
        grabInteractable = GetComponent<XRGrabInteractable>();

        // ����ץȡ�¼�
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed); // ��ץȡ�¼�����ʱ���� OnGrabbed ����
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        gameObject.SetActive(false); // ץȡ����������

        // ����Ƿ��Ѿ��滻����
        if (!isTreeReplaced)
        {
            ReplaceTreeObject(originalTree, christmasTree);
            isTreeReplaced = true; // ���Ϊ�Ѵ���

            // ֪ͨ StageManager ���Ӽ���
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
            // ��ʥ�����ƶ���ԭʼ����λ�ú���ת�Ƕ�
            replacement.transform.position = original.transform.position;
            replacement.transform.rotation = original.transform.rotation;

            // �滻�߼�
            original.SetActive(false); // ����ԭʼ��
            replacement.SetActive(true); // ��ʾʥ����
        }
    }

    private void OnDestroy()
    {
        // �Ƴ��¼�����������ֹ�ڴ�й©��
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }
}
