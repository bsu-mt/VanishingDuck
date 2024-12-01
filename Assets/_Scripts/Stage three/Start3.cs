using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Start3 : MonoBehaviour
{
    public Transform player; // ���λ��
    public Vector3 targetPosition; // �����׶εĴ���λ��

    public GameObject trueRabbitPrefab; // ������Ԥ����
    public GameObject reindeerPrefab; // ��������ɵ���¹Ԥ����
    public GameObject christmasTreePrefab; // ʥ����Ԥ����
    public GameObject snowEffectPrefab; // ѩ������Ч��
    public Material snowMaterial; // ѩ�ز���
    public Material buildingMaterial; // ��������

    public GameObject[] buildings; // �����еĽ�����
    public GameObject[] groundObjects; // �����еĵ���

    public float ascendHeight = 5f; // ��������߶�
    public float ascendSpeed = 1f; // ���������ٶ�
    public float transformDelay = 4f; // �ӳ�������¹��ʱ��

    public GameObject[] snowEffects; // ѩ������Ч������

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
        // 1. ��Ҵ��͵��ض�λ��
        player.position = targetPosition;

        // 2. ���ؽ׶� 2 �ĺ�������
        // ���Һ����Ӻ�������
        GameObject[] redRabbits = GameObject.FindGameObjectsWithTag("Red rabbits");
        GameObject[] blueRabbits = GameObject.FindGameObjectsWithTag("Blue rabbits");

        // �ϲ�����
        GameObject[] redAndBlueRabbits = redRabbits.Concat(blueRabbits).ToArray();

        // �������������к����Ӻ�������
        foreach (GameObject rabbit in redAndBlueRabbits)
        {
            Destroy(rabbit);
        }


        // 3. ������������ 6 ��������
        List<GameObject> floatingRabbits = new List<GameObject>();
        for (int i = 0; i < 6; i++)
        {
            // ����ÿֻ���ӵ�λ�ã�Χ��һ��ԲȦ
            float angle = i * Mathf.PI * 2f / 6; // ÿֻ���ӵĽǶȣ��ֲ���Բ����
            float radius = 2f; // ԲȦ�İ뾶
            float x = Mathf.Cos(angle) * radius; // Բ���ϵ� x ����
            float z = Mathf.Sin(angle) * radius; // Բ���ϵ� z ����

            // �������λ�õ������ӵ�λ�ã����� x ����ƫ�� 2 ��λ
            Vector3 spawnPosition = player.position + new Vector3(x - 2f, 1f, z);

            // ʵ��������
            GameObject rabbit = Instantiate(trueRabbitPrefab, spawnPosition, Quaternion.identity);
            floatingRabbits.Add(rabbit);
        }


        // 4. ��������
        foreach (GameObject rabbit in floatingRabbits)
        {
            StartCoroutine(AscendRabbit(rabbit));
        }

        // 5. �ȴ�����������ɲ�������¹
        yield return new WaitForSeconds(transformDelay);
        Vector3 reindeerPosition = player.position + Vector3.up * (ascendHeight - 2f);
        GameObject reindeer = Instantiate(reindeerPrefab, reindeerPosition, Quaternion.identity);

        // 6. ����ת����ʥ������ѩ��
        yield return new WaitForSeconds(1f); // �����һ��ʱ��۲���¹
        TransformScene();

        // 7. ����ѩ������Ч��
        EnableSnowEffects();

        // 8. ����ʥ����
        // ��ȡ�����AudioSource���
        christmasSong = GetComponent<AudioSource>();

        // ���û��AudioSource����������һ��
        if (christmasSong == null)
        {
            christmasSong = gameObject.AddComponent<AudioSource>();
        }

        // ����Ƶ������ֵ��AudioSource
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
        // 1. �滻����Ϊѩ�ز���
        foreach (GameObject ground in groundObjects)
        {
            MeshRenderer groundRenderer = ground.GetComponent<MeshRenderer>();
            if (groundRenderer != null)
            {
                Material[] materials = groundRenderer.materials; // ��ȡ��������

                // �滻ָ���Ĳ��ʲۣ����磬��һ���ۣ�
                if (materials.Length > 0)
                {
                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i] = snowMaterial; // �����в��ʲ��滻Ϊѩ�ز���
                    }
                }

                groundRenderer.materials = materials; // Ӧ���޸ĺ�Ĳ�������
            }
        }

        // 2. �滻��Ϊʥ����
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject tree in trees)
        {
            Vector3 position = tree.transform.position;
            Quaternion rotation = tree.transform.rotation;
            Destroy(tree);
            Instantiate(christmasTreePrefab, position, rotation);
        }

        // 3. �������ﻻ��ɫ
        foreach (GameObject building in buildings)
        {
            MeshRenderer buildingRenderer = building.GetComponent<MeshRenderer>();
            if (buildingRenderer != null)
            {
                Material[] materials = buildingRenderer.materials; // ��ȡ��������

                // �滻ָ���Ĳ��ʲۣ����磬��һ���ۣ�
                if (materials.Length > 0)
                {
                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i] = buildingMaterial; // �����в��ʲ��滻Ϊ��������
                    }
                }

                buildingRenderer.materials = materials; // Ӧ���޸ĺ�Ĳ�������
            }
        }

        Debug.Log("Stage 3 Scene transformed into a Christmas theme!");
    }
}
