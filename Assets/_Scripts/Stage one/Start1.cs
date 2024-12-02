//这段代码的使用场景是当玩家越过绊线时游戏的第一阶段将会开始，三只需要被玩家找到的兔子会移动到藏起来的位置
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start1 : MonoBehaviour
{
    public Transform playerCamera; 
    public float xThreshold = 2f; //这里是绊线

    private bool triggered = false;
    public RadarSound radarSound;

    private AudioSource audioSource;
    public AudioClip stage1Clip; // 手动拖入音频文件

    public GameObject stage1IntroPanel; // 拖入 Stage1Intro 面板

    void Start()
    {
        // 确保获取到 AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 配置 AudioSource
        if (stage1Clip != null)
        {
            audioSource.clip = stage1Clip;
            audioSource.playOnAwake = false; // 防止自动播放
        }

    }

    void Update()
    {
        if (!triggered && playerCamera.position.x < xThreshold)
        {
            triggered = true;
            //设定藏匿地点
            MoveObject("Stage 1 rabbits", new Vector3(-8.29f, 0.0025f, -33.94f));
            MoveObject("Stage 1 rabbits 2", new Vector3(-1.232167f, 3f, 19.80322f));
            MoveObject("Stage 1 rabbits 3", new Vector3(-27.62f, 0.004f, 33.58f));
            
            // 播放雷达
            radarSound.enabled = true;

            // 播放Stage1介绍
            PlayStage1Audio();

            // 显示Stage1Intro UI
            stage1IntroPanel.SetActive(true);
        }
    }

    private void MoveObject(string tag, Vector3 targetPosition)
    {
        GameObject obj = GameObject.FindWithTag(tag);
        if (obj != null)
        {
            // 把物体移动到目标位置
            obj.transform.position = targetPosition;
        }
    }

    public void PlayStage1Audio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // 播放音频
            Debug.Log("Playing audio: " + audioSource.clip.name);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}
