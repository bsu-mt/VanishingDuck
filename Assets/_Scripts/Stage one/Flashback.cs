using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ű�����Ϊ�Ҳ����ñ༭�ƶ�����·����д�ģ����������ʹ�����ֱ����·���ƶ�����һ���������ֵ�Ч��
//ϣ��ʵ�ֵ�Ч���ǣ����ƶ���������ʱ���ӻ�ͻȻ������ӽ������ֲ������ƶ������ǰ���Ļ�԰�Ȼ����ת����ʧ����

public class Flashback : MonoBehaviour
{
    public Transform playerCamera; //�������������ҽ�ɫ��
    public float xThreshold = 30.55f; //���ߣ���������߾ͻᴥ��
    public float speed = 15f; //����ٶȣ����Ը���
    private Vector3 firstTarget = new Vector3(3.4f, -0.06f, -1.8f); //��һ���յ�λ��
    private Vector3 secondTarget = new Vector3(3.4f, -0.06f, -24f); //����λ��
    private bool triggered = false;

    private AudioSource audioSource;
    public AudioClip flashBackClip; // �ֶ�������Ƶ�ļ�

    void Start()
    {
        // ȷ����ȡ�� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ���� AudioSource
        if (flashBackClip != null)
        {
            audioSource.clip = flashBackClip;
            audioSource.playOnAwake = false; // ��ֹ�Զ�����
        }

    }

    void Update()
    {
        //���������Ƿ񴩹�����
        if (!triggered && playerCamera.position.x < xThreshold)
        {
            triggered = true;
            StartCoroutine(MoveToTargets());
        }
    }

    private System.Collections.IEnumerator MoveToTargets()
    {
        //��̵��յ�
        yield return StartCoroutine(MoveToPosition(firstTarget));

        PlayFlashBackAudio();

        //��ת��ʧ
        yield return StartCoroutine(MoveToPosition(secondTarget));
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            yield return null; 
        }
    }

    public void PlayFlashBackAudio()
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
