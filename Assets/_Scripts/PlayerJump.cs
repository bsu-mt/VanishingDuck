using UnityEngine;
using UnityEngine.XR;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f; // ��Ծ����
    private Rigidbody rb;       // ��Ҹ���
    private bool isGrounded;    // �Ƿ��ڵ���

    void Start()
    {
        // ��ȡ��ҵ� Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on Player!");
        }
    }

    void Update()
    {
        // ��������ֱ� A ���Ƿ���
        if (IsRightHandAButtonPressed() && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // ������ϵ���Ծ��
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // ����Ϊ���ڵ���
        Debug.Log("Player jumped!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ��������ײ
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // ��������ʱ����Ϊ�ڵ�����
        }
    }

    private bool IsRightHandAButtonPressed()
    {
        // ��ȡ�����ֱ�
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // ��� A ���Ƿ���
        if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            return isPressed;
        }

        return false;
    }
}
