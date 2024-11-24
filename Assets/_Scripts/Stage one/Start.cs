//这段代码的使用场景是当玩家越过绊线时游戏的第一阶段将会开始，三只需要被玩家找到的兔子会移动到藏起来的位置
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public Transform playerCamera; 
    public float xThreshold = 13.88f; //这里是绊线

    private bool triggered = false;

    void Update()
    {
        if (!triggered && playerCamera.position.x > xThreshold)
        {
            triggered = true;
            //设定藏匿地点
            MoveObject("Stage 1 rabbits", new Vector3(-3.232167f, -4.768372e-07f, 21.00322f));
            MoveObject("Stage 1 rabbits 2", new Vector3(-20.62f, -4.768372e-07f, 27.58f));
            MoveObject("Stage 1 rabbits 3", new Vector3(-4.29f, -4.768372e-07f, -26.49f));
        }
    }

    private void MoveObject(string tag, Vector3 targetPosition)
    {
        GameObject obj = GameObject.FindWithTag(tag);
        if (obj != null)
        {
            把物体移动到目标位置
            obj.transform.position = targetPosition;
        }
    }
}
