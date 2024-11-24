//这段代码的作用是当玩家进入场地2时将会随机出现10只晕头兔，10只爆炸兔和3只需要抓住的真兔子并让它们在地面上乱窜
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2 : MonoBehaviour
{
    public Transform player; //玩家
    public Vector3 targetPosition = new Vector3(38.95341f, 0.1199989f, 200.7748f); //stage2的起始位置
    public GameObject blueRabbitPrefab; //晕头兔
    public GameObject redRabbitPrefab; //爆炸兔
    public GameObject trueRabbitPrefab; // 真兔子预制体
    public GameObject stage2Ground; //Stage2-ground 平面

    public int blueRabbitCount = 10; 
    public int redRabbitCount = 10; 
    public int trueRabbitCount = 3;  

    public float spawnRange = 10f; 
    public float moveSpeed = 10f;  //移动速度
    private bool triggered = false; 

    void Update()
    {
        if (!triggered && Vector3.Distance(player.position, targetPosition) < 0.1f)
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
            //随机生成位置
            Vector3 randomPosition = GetRandomPositionOnGround();

            //创建乱七八糟的兔子
            GameObject rabbit = Instantiate(prefab, randomPosition, Quaternion.identity);
            rabbit.tag = tag;

            //添加随机移动脚本和物理检测
            Rigidbody rb = rabbit.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rabbit.AddComponent<RabbitRandomMove>().moveSpeed = moveSpeed;
        }
    }

    Vector3 GetRandomPositionOnGround()
    {
        //获取地面的位置和范围
        Vector3 groundPosition = stage2Ground.transform.position;
        Vector3 groundScale = stage2Ground.transform.localScale;

        //计算生成位置范围
        float randomX = Random.Range(groundPosition.x - spawnRange, groundPosition.x + spawnRange);
        float randomZ = groundPosition.z;
        float randomY = groundPosition.y;

        return new Vector3(randomX, randomY, randomZ);
    }
}

public class RabbitRandomMove : MonoBehaviour
{
    public float moveSpeed = 10f; //兔子洞移动速度

    private Vector3 moveDirection;

    void Start()
    {
        //随机初始化移动方向
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    void FixedUpdate()
    {
        //更新兔子的位置
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime, Space.World);

        //如果碰撞就换一个方向动
        if (Physics.Raycast(transform.position, moveDirection, 0.5f))
        {
            moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
        }
    }
}
