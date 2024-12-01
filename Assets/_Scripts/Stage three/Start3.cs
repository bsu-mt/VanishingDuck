using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Start3 : MonoBehaviour
{
    public Transform player; // 玩家位置
    public Vector3 targetPosition; // 第三阶段的传送位置

    public GameObject trueRabbitPrefab; // 真兔子预制体
    public GameObject reindeerPrefab; // 合体后生成的麋鹿预制体
    public GameObject christmasTreePrefab; // 圣诞树预制体
    public GameObject snowEffectPrefab; // 雪花粒子效果
    public Material snowMaterial; // 雪地材质
    public Material buildingMaterial; // 建筑材质

    public GameObject[] buildings; // 场景中的建筑物
    public GameObject[] groundObjects; // 场景中的地面

    public float ascendHeight = 5f; // 兔子升天高度
    public float ascendSpeed = 1f; // 兔子升天速度
    public float transformDelay = 4f; // 延迟生成麋鹿的时间

    public GameObject[] snowEffects; // 雪花粒子效果对象

    public AudioClip musicClip;
    private AudioSource christmasSong;

    private bool isStage3Triggered = false;

    void Update()
    {
        if (!isStage3Triggered)
        {
            isStage3Triggered = true;
            StartCoroutine(TriggerStage3());
        }
    }

    IEnumerator TriggerStage3()
    {
        // 1. 玩家传送到特定位置
        player.position = targetPosition;

        // 2. 隐藏阶段 2 的红蓝兔子
        // 查找红兔子和蓝兔子
        GameObject[] redRabbits = GameObject.FindGameObjectsWithTag("Red rabbits");
        GameObject[] blueRabbits = GameObject.FindGameObjectsWithTag("Blue rabbits");

        // 合并数组
        GameObject[] redAndBlueRabbits = redRabbits.Concat(blueRabbits).ToArray();

        // 遍历并销毁所有红兔子和蓝兔子
        foreach (GameObject rabbit in redAndBlueRabbits)
        {
            Destroy(rabbit);
        }


        // 3. 在玩家身边生成 6 个真兔子
        List<GameObject> floatingRabbits = new List<GameObject>();
        for (int i = 0; i < 6; i++)
        {
            // 计算每只兔子的位置，围成一个圆圈
            float angle = i * Mathf.PI * 2f / 6; // 每只兔子的角度，分布在圆周上
            float radius = 2f; // 圆圈的半径
            float x = Mathf.Cos(angle) * radius; // 圆周上的 x 坐标
            float z = Mathf.Sin(angle) * radius; // 圆周上的 z 坐标

            // 基于玩家位置调整兔子的位置，并在 x 方向偏移 2 单位
            Vector3 spawnPosition = player.position + new Vector3(x - 2f, 1f, z);

            // 实例化兔子
            GameObject rabbit = Instantiate(trueRabbitPrefab, spawnPosition, Quaternion.identity);
            floatingRabbits.Add(rabbit);
        }


        // 4. 兔子升天
        foreach (GameObject rabbit in floatingRabbits)
        {
            StartCoroutine(AscendRabbit(rabbit));
        }

        // 5. 等待兔子升天完成并生成麋鹿
        yield return new WaitForSeconds(transformDelay);
        Vector3 reindeerPosition = player.position + Vector3.up * (ascendHeight - 2f);
        GameObject reindeer = Instantiate(reindeerPrefab, reindeerPosition, Quaternion.identity);

        // 6. 场景转换：圣诞树、雪地
        yield return new WaitForSeconds(1f); // 给玩家一点时间观察麋鹿
        TransformScene();

        // 7. 启动雪花粒子效果
        EnableSnowEffects();

        // 8. 播放圣诞歌
        // 获取或添加AudioSource组件
        christmasSong = GetComponent<AudioSource>();

        // 如果没有AudioSource组件，就添加一个
        if (christmasSong == null)
        {
            christmasSong = gameObject.AddComponent<AudioSource>();
        }

        // 将音频剪辑赋值给AudioSource
        christmasSong.clip = musicClip;
        christmasSong.Play();
    }

    IEnumerator AscendRabbit(GameObject rabbit)
    {
        Vector3 targetPosition = rabbit.transform.position + Vector3.up * ascendHeight;
        while (Vector3.Distance(rabbit.transform.position, targetPosition) > 0.1f)
        {
            rabbit.transform.position = Vector3.Lerp(rabbit.transform.position, targetPosition, Time.deltaTime * ascendSpeed);
            yield return null;
        }

        Destroy(rabbit);
    }
    
    void EnableSnowEffects()
    {
        foreach (GameObject snowEffect in snowEffects)
        {
            if (snowEffect != null)
            {
                snowEffect.SetActive(true);
            }
        }
        Debug.Log("Snow effects enabled!");
    }

    void TransformScene()
    {
        // 1. 替换地面为雪地材质
        foreach (GameObject ground in groundObjects)
        {
            MeshRenderer groundRenderer = ground.GetComponent<MeshRenderer>();
            if (groundRenderer != null)
            {
                Material[] materials = groundRenderer.materials; // 获取材质数组

                // 替换指定的材质槽（例如，第一个槽）
                if (materials.Length > 0)
                {
                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i] = snowMaterial; // 将所有材质槽替换为雪地材质
                    }
                }

                groundRenderer.materials = materials; // 应用修改后的材质数组
            }
        }

        // 2. 替换树为圣诞树
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject tree in trees)
        {
            Vector3 position = tree.transform.position;
            Quaternion rotation = tree.transform.rotation;
            Destroy(tree);
            Instantiate(christmasTreePrefab, position, rotation);
        }

        // 3. 给建筑物换颜色
        foreach (GameObject building in buildings)
        {
            MeshRenderer buildingRenderer = building.GetComponent<MeshRenderer>();
            if (buildingRenderer != null)
            {
                Material[] materials = buildingRenderer.materials; // 获取材质数组

                // 替换指定的材质槽（例如，第一个槽）
                if (materials.Length > 0)
                {
                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i] = buildingMaterial; // 将所有材质槽替换为建筑材质
                    }
                }

                buildingRenderer.materials = materials; // 应用修改后的材质数组
            }
        }

        Debug.Log("Stage 3 Scene transformed into a Christmas theme!");
    }
}
