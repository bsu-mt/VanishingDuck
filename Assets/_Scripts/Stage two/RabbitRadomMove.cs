using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitRandomMove : MonoBehaviour
{
    public float moveSpeed = 10f; // 移动速度

    private Vector3 moveDirection;

    // 边界对象
    public Transform northBoundary;
    public Transform eastBoundary;
    public Transform westBoundary;
    public Transform southBoundary;

    // 边界缓冲距离，兔子靠近边界时会改变方向
    public float boundaryBuffer = 10f;

    void Start()
    {
        // 随机初始化移动方向
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void FixedUpdate()
    {
        // 更新兔子的位置
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime, Space.World);

        // 检查是否接近边界
        if (IsNearBoundary())
        {
            // 随机改变方向
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            // Debug.Log("Rabbit near boundary, changing direction!");
        }
    }

    // 检查兔子是否接近边界
    bool IsNearBoundary()
    {
        // 获取当前兔子的位置
        Vector3 position = transform.position;

        // 检查是否接近四个边界
        if (position.z > northBoundary.position.z - boundaryBuffer ||  // 接近北边界
            position.z < southBoundary.position.z + boundaryBuffer ||  // 接近南边界
            position.x > eastBoundary.position.x - boundaryBuffer ||   // 接近东边界
            position.x < westBoundary.position.x + boundaryBuffer)     // 接近西边界
        {
            return true; // 靠近边界
        }

        return false; // 未靠近边界
    }
}
