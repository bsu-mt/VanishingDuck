//��δ���������ǵ���ҽ��볡��2ʱ�����������10ֻ��ͷ�ã�10ֻ��ը�ú�3ֻ��Ҫץס�������Ӳ��������ڵ������Ҵ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2 : MonoBehaviour
{
    public Transform player; //���
    // public Vector3 targetPosition = new Vector3(38.95341f, 0.1199989f, 200.7748f); //stage2����ʼλ��
    public GameObject blueRabbitPrefab; //��ͷ��
    public GameObject redRabbitPrefab; //��ը��
    public GameObject trueRabbitPrefab; // ������Ԥ����
    public GameObject stage2Ground; //Stage2-ground ƽ��

    public int blueRabbitCount = 10; 
    public int redRabbitCount = 10; 
    public int trueRabbitCount = 3;  

    public float spawnRange = 10f; 
    private bool triggered = false; 

    void Update()
    {
        if (!triggered) //&& Vector3.Distance(player.position, targetPosition) < 0.1f)
        {
            triggered = true;
            SpawnRabbits(blueRabbitPrefab, blueRabbitCount, "Blue rabbits");
            SpawnRabbits(redRabbitPrefab, redRabbitCount, "Red rabbits");
            SpawnRabbits(trueRabbitPrefab, trueRabbitCount, "True rabbits");
        }
    }

    void SpawnRabbits(GameObject prefab, int count, string tag)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = GetRandomPositionOnGround();

            // ��̬�������Ӷ���
            GameObject rabbit = Instantiate(prefab, randomPosition, Quaternion.identity);
            rabbit.tag = tag;

            // ��� Rigidbody ��������Ҫ���
            Rigidbody rb = rabbit.GetComponent<Rigidbody>();
            if (rb == null) // ��������� Rigidbody�������
            {
                rb = rabbit.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            // ��ȡ RabbitRandomMove �ű�
            RabbitRandomMove rabbitMove = rabbit.GetComponent<RabbitRandomMove>();
            if (rabbitMove != null)
            {
                // ��̬�󶨱߽����
                rabbitMove.northBoundary = GameObject.Find("north").transform;
                rabbitMove.eastBoundary = GameObject.Find("east").transform;
                rabbitMove.westBoundary = GameObject.Find("west").transform;
                rabbitMove.southBoundary = GameObject.Find("south").transform;

                // Debug.Log($"Boundaries set for rabbit {i + 1}");
            }
            else
            {
                Debug.LogError("RabbitRandomMove script not found on the prefab!");
            }
        }
    }



    Vector3 GetRandomPositionOnGround()
    {
        //��ȡ�����λ�úͷ�Χ
        Vector3 groundPosition = stage2Ground.transform.position;
        Vector3 groundScale = stage2Ground.transform.localScale;

        //��������λ�÷�Χ
        float randomX = Random.Range(groundPosition.x - spawnRange, groundPosition.x + spawnRange);
        float randomZ = Random.Range(groundPosition.z - 30 - spawnRange, groundPosition.z - 30 + spawnRange);
        float randomY = groundPosition.y;
        // Debug.Log($"GroundPositionX: {groundPosition.x}");
        // Debug.Log($"GroundPositionY: {groundPosition.y}");
        // Debug.Log($"GroundPositionZ: {groundPosition.z}");

        return new Vector3(randomX, randomY, randomZ);
    }
}




