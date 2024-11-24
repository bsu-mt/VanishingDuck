//这段脚本挂载在玩家摄像头上，记录了玩家在stage1中收集到的兔子数量。如果兔子数量为3则玩家将被传送到第二阶段场地
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public int Stage1_count = 0; //玩家计数变量
    public Vector3 teleportPosition = new Vector3(38.95341f, 0.1199989f, 200.7748f); //目标传送位置

    void Update()
    {
        if (Stage1_count == 3)
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        // 将玩家传送到指定位置
        transform.position = teleportPosition;
        Stage1_count = 0;
    }
}
