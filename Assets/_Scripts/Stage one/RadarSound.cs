//��δ�����������״�״��׷Ѱ��Ǻõĵ�һ���ڶ�������ֻ���Ӳ�������Ҿ������ӵľ��벥��Audio
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSound : MonoBehaviour
{
    public AudioSource radarSound; //�״�����Դ
    public Transform[] targets;   //Ŀ������
    private int currentTargetIndex = 0; //��ǰĿ��
    private Transform currentTarget;    
    private float minVolume = 3f;       //��С�״�����
    private float maxVolume = 9f;       //����״�����
    private float distanceThreshold1 = 20f; //������
    private float distanceThreshold2 = 10f; //������
    private float distanceThreshold3 = 5f;  //����Զ

    void Start()
    {
        //���Ŀ��
        if (targets.Length > 0)
        {
            currentTarget = targets[currentTargetIndex];
        }
    }

    void Update()
    {
        if (currentTarget == null)
        {
            //�л�Ŀ��
            if (currentTargetIndex + 1 < targets.Length)
            {
                currentTargetIndex++;
                currentTarget = targets[currentTargetIndex];
            }
            else
            {
                radarSound.volume = 0;
                return;
            }
        }

        if (currentTarget != null)
        {
            //�������Ŀ��ľ���
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            UpdateRadarSound(distance);

            //�л�Ŀ��
            if (!currentTarget.gameObject.activeInHierarchy)
            {
                currentTarget = null;
            }
        }
    }

    void UpdateRadarSound(float distance)
    {
        //���ݾ����������
        if (distance > distanceThreshold1)
        {
            radarSound.volume = minVolume;
        }
        else if (distance <= distanceThreshold1 && distance > distanceThreshold2)
        {
            radarSound.volume = Mathf.Lerp(minVolume, maxVolume / 2, (distanceThreshold1 - distance) / (distanceThreshold1 - distanceThreshold2));
        }
        else if (distance <= distanceThreshold2 && distance > distanceThreshold3)
        {
            radarSound.volume = Mathf.Lerp(maxVolume / 2, maxVolume, (distanceThreshold2 - distance) / (distanceThreshold2 - distanceThreshold3));
        }
        else if (distance <= distanceThreshold3)
        {
            radarSound.volume = maxVolume;
        }
    }
}
