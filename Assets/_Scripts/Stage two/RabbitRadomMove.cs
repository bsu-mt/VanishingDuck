using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitRandomMove : MonoBehaviour
{
    public float moveSpeed = 10f; // �ƶ��ٶ�

    private Vector3 moveDirection;

    // �߽����
    public Transform northBoundary;
    public Transform eastBoundary;
    public Transform westBoundary;
    public Transform southBoundary;

    // �߽绺����룬���ӿ����߽�ʱ��ı䷽��
    public float boundaryBuffer = 10f;

    void Start()
    {
        // �����ʼ���ƶ�����
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void FixedUpdate()
    {
        // �������ӵ�λ��
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime, Space.World);

        // ����Ƿ�ӽ��߽�
        if (IsNearBoundary())
        {
            // ����ı䷽��
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            // Debug.Log("Rabbit near boundary, changing direction!");
        }
    }

    // ��������Ƿ�ӽ��߽�
    bool IsNearBoundary()
    {
        // ��ȡ��ǰ���ӵ�λ��
        Vector3 position = transform.position;

        // ����Ƿ�ӽ��ĸ��߽�
        if (position.x < northBoundary.position.x + boundaryBuffer ||  // �ӽ����߽�
            position.x > southBoundary.position.x - boundaryBuffer ||  // �ӽ��ϱ߽�
            position.z > eastBoundary.position.z - boundaryBuffer ||   // �ӽ����߽�
            position.z < westBoundary.position.z + boundaryBuffer)     // �ӽ����߽�
        {
            return true; // �����߽�
        }

        return false; // δ�����߽�
    }
}
