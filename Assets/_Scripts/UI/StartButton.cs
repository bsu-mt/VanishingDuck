using UnityEngine;

public class StartButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip introductionClip; // 手动拖入音频文件
    public GameObject startMenuPanel; // 拖入 StartMenu 面板
    public GameObject howToPlayPanel; // 拖入 HowToPlay 面板

    void Start()
    {
        // 确保获取到 AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 配置 AudioSource
        if (introductionClip != null)
        {
            audioSource.clip = introductionClip;
            audioSource.playOnAwake = false; // 防止自动播放
        }

        // 检查 StartMenu Panel 是否绑定
        if (startMenuPanel == null)
        {
            Debug.LogError("StartMenu Panel is not assigned in the Inspector!");
        }
    }

    public void PlayIntroductionAudio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // 播放音频
            Debug.Log("Playing audio: " + audioSource.clip.name);
            StartCoroutine(DisablePanelAfterAudio()); // 启动协程监测
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }

    private System.Collections.IEnumerator DisablePanelAfterAudio()
    {
        // 等待音频播放结束
        while (audioSource.isPlaying)
        {
            yield return null; // 等待一帧
        }

        // 音频播放完成后禁用面板
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(false);
            Debug.Log("StartMenu Panel has been disabled.");
            howToPlayPanel.SetActive(true);
        }
    }
}
