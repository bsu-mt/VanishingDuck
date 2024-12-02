using UnityEngine;

public class StartButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip introductionClip; // �ֶ�������Ƶ�ļ�
    public GameObject startMenuPanel; // ���� StartMenu ���
    public GameObject howToPlayPanel; // ���� HowToPlay ���

    void Start()
    {
        // ȷ����ȡ�� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ���� AudioSource
        if (introductionClip != null)
        {
            audioSource.clip = introductionClip;
            audioSource.playOnAwake = false; // ��ֹ�Զ�����
        }

        // ��� StartMenu Panel �Ƿ��
        if (startMenuPanel == null)
        {
            Debug.LogError("StartMenu Panel is not assigned in the Inspector!");
        }
    }

    public void PlayIntroductionAudio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // ������Ƶ
            Debug.Log("Playing audio: " + audioSource.clip.name);
            StartCoroutine(DisablePanelAfterAudio()); // ����Э�̼��
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }

    private System.Collections.IEnumerator DisablePanelAfterAudio()
    {
        // �ȴ���Ƶ���Ž���
        while (audioSource.isPlaying)
        {
            yield return null; // �ȴ�һ֡
        }

        // ��Ƶ������ɺ�������
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(false);
            Debug.Log("StartMenu Panel has been disabled.");
            howToPlayPanel.SetActive(true);
        }
    }
}
