//这段代码的作用是当玩家进入场地2时将会随机出现10只晕头兔，10只爆炸兔和3只需要抓住的真兔子并让它们在地面上乱窜
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2 : MonoBehaviour
{
    public Transform player; //玩家
    // public Vector3 targetPosition = new Vector3(38.95341f, 0.1199989f, 200.7748f); //stage2的起始位置
    public GameObject blueRabbitPrefab; //晕头兔
    public GameObject redRabbitPrefab; //爆炸兔
    public GameObject trueRabbitPrefab; // 真兔子预制体
    public GameObject stage2Ground; //Stage2-ground 平面

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

            // 动态生成兔子对象
            GameObject rabbit = Instantiate(prefab, randomPosition, Quaternion.identity);
            rabbit.tag = tag;

            // 添加 Rigidbody 和其他必要组件
            Rigidbody rb = rabbit.GetComponent<Rigidbody>();
            if (rb == null) // 如果不存在 Rigidbody，则添加
            {
                rb = rabbit.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            // 获取 RabbitRandomMove 脚本
            RabbitRandomMove rabbitMove = rabbit.GetComponent<RabbitRandomMove>();
            if (rabbitMove != null)
            {
                // 动态绑定边界对象
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
        //获取地面的位置和范围
        Vector3 groundPosition = stage2Ground.transform.position;
        Vector3 groundScale = stage2Ground.transform.localScale;

        //计算生成位置范围
        float randomX = Random.Range(groundPosition.x - spawnRange, groundPosition.x + spawnRange);
        float randomZ = Random.Range(groundPosition.z - 30 - spawnRange, groundPosition.z - 30 + spawnRange);
        float randomY = groundPosition.y;
        // Debug.Log($"GroundPositionX: {groundPosition.x}");
        // Debug.Log($"GroundPositionY: {groundPosition.y}");
        // Debug.Log($"GroundPositionZ: {groundPosition.z}");

        return new Vector3(randomX, randomY, randomZ);
    }
}




