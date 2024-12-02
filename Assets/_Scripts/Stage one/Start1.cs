//��δ����ʹ�ó����ǵ����Խ������ʱ��Ϸ�ĵ�һ�׶ν��Ὺʼ����ֻ��Ҫ������ҵ������ӻ��ƶ�����������λ��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start1 : MonoBehaviour
{
    public Transform playerCamera; 
    public float xThreshold = 2f; //�����ǰ���

    private bool triggered = false;
    public RadarSound radarSound;

    private AudioSource audioSource;
    public AudioClip stage1Clip; // �ֶ�������Ƶ�ļ�

    public GameObject stage1IntroPanel; // ���� Stage1Intro ���

    void Start()
    {
        // ȷ����ȡ�� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ���� AudioSource
        if (stage1Clip != null)
        {
            audioSource.clip = stage1Clip;
            audioSource.playOnAwake = false; // ��ֹ�Զ�����
        }

    }

    void Update()
    {
        if (!triggered && playerCamera.position.x < xThreshold)
        {
            triggered = true;
            //�趨����ص�
            MoveObject("Stage 1 rabbits", new Vector3(-8.29f, 0.0025f, -33.94f));
            MoveObject("Stage 1 rabbits 2", new Vector3(-1.232167f, 3f, 19.80322f));
            MoveObject("Stage 1 rabbits 3", new Vector3(-27.62f, 0.004f, 33.58f));
            
            // �����״�
            radarSound.enabled = true;

            // ����Stage1����
            PlayStage1Audio();

            // ��ʾStage1Intro UI
            stage1IntroPanel.SetActive(true);
        }
    }

    private void MoveObject(string tag, Vector3 targetPosition)
    {
        GameObject obj = GameObject.FindWithTag(tag);
        if (obj != null)
        {
            // �������ƶ���Ŀ��λ��
            obj.transform.position = targetPosition;
        }
    }

    public void PlayStage1Audio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // ������Ƶ
            Debug.Log("Playing audio: " + audioSource.clip.name);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}
