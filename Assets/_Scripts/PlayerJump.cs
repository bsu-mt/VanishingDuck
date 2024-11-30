using UnityEngine;
using UnityEngine.XR;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f; // 跳跃力度
    private Rigidbody rb;       // 玩家刚体
    private bool isGrounded;    // 是否在地面

    void Start()
    {
        // 获取玩家的 Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on Player!");
        }
    }

    void Update()
    {
        // 检查右手手柄 A 键是否按下
        if (IsRightHandAButtonPressed() && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // 添加向上的跳跃力
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // 设置为不在地面
        Debug.Log("Player jumped!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检测是否与地面碰撞
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 碰到地面时设置为在地面上
        }
    }

    private bool IsRightHandAButtonPressed()
    {
        // 获取右手手柄
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // 检查 A 键是否按下
        if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            return isPressed;
        }

        return false;
    }
}
