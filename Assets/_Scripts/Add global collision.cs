using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//README：这段代码被挂在了一个空的GameObject上以为全局所有物品添加一个物理碰撞

public class AddColliders : MonoBehaviour
{
    void Start()
    {
        //可用的更新建议:设置你不想添加碰撞体积的标签物体并无视

        // if (obj.tag == "NoCollision")
        // continue;

        //以下代码会针对场景中的所有物体
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            //添加一个物理碰撞，默认为BoxCollider
            if (obj.GetComponent<Collider>() == null)
            {
                obj.AddComponent<BoxCollider>();
            }
        }
    }
}
