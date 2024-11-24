//这段代码的作用是雷达，雷达会追寻标记好的第一，第二，第三只兔子并根据玩家距离兔子的距离播放Audio
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSound : MonoBehaviour
{
    public AudioSource radarSound; //雷达声音源
    public Transform[] targets;   //目标物体
    private int currentTargetIndex = 0; //当前目标
    private Transform currentTarget;    
    private float minVolume = 3f;       //最小雷达音量
    private float maxVolume = 9f;       //最大雷达音量
    private float distanceThreshold1 = 20f; //声音近
    private float distanceThreshold2 = 10f; //声音中
    private float distanceThreshold3 = 5f;  //声音远

    void Start()
    {
        //检查目标
        if (targets.Length > 0)
        {
            currentTarget = targets[currentTargetIndex];
        }
    }

    void Update()
    {
        if (currentTarget == null)
        {
            //切换目标
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
            //计算距离目标的距离
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            UpdateRadarSound(distance);

            //切换目标
            if (!currentTarget.gameObject.activeInHierarchy)
            {
                currentTarget = null;
            }
        }
    }

    void UpdateRadarSound(float distance)
    {
        //根据距离调整音量
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
