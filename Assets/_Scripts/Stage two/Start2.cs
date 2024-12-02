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
    private bool audioPlayed = false; // ��Ƶ�Ƿ��Ѿ�����
    private bool panelDisplayed = false; // ����Ƿ��Ѿ���ʾ
    private bool triggered = false; // �Ƿ��Ѿ���������

    private AudioSource audioSource;
    public AudioClip stage2IntroClip; // �ֶ�������Ƶ�ļ�
    public GameObject stage2IntroPanel; // ���� Stage2Intro ���

    void Start()
    {
        // ȷ����ȡ�� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ���� AudioSource
        if (stage2IntroClip != null)
        {
            audioSource.clip = stage2IntroClip;
            audioSource.playOnAwake = false; // ��ֹ�Զ�����
        }

        // ��� Stage2IntroPanel �Ƿ��
        if (stage2IntroPanel == null)
        {
            Debug.LogError("StartMenu Panel is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        // ����Stage2Intro��Ƶ
        if (!audioPlayed)
        {
            PlayStage2Audio();
            audioPlayed = true; // �����Ƶ�Ѳ���
        }

        // ��ʾStage2Intro���
        if (!panelDisplayed && stage2IntroPanel != null)
        {
            stage2IntroPanel.SetActive(true);
            panelDisplayed = true; // ����������ʾ
        }

        // �������Ƿ��Ѿ��رգ�����������
        if (!triggered && stage2IntroPanel != null && !stage2IntroPanel.activeSelf)
        {
            triggered = true; // �������������
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

    public void PlayStage2Audio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.volume = 1f; // ȷ���������
            audioSource.Play(); // ������Ƶ
            Debug.Log("Playing audio: " + audioSource.clip.name);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}




