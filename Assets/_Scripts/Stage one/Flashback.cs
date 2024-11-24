using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个脚本是因为我不会用编辑移动动画路径才写的，如果它不好使你可以直接用路径移动做出一个快速闪现的效果
//希望实现的效果是，在移动超过绊线时兔子会突然从玩家视角左侧出现并快速移动到玩家前方的花园里，然后左转后消失不见

public class Flashback : MonoBehaviour
{
    public Transform playerCamera; //摄像机（就是玩家角色）
    public float xThreshold = 30.55f; //绊线，超过这个线就会触发
    public float speed = 20f; //冲刺速度，可以更快
    private Vector3 firstTarget = new Vector3(3.4f, -0.06f, -1.8f); //第一个拐点位置
    private Vector3 secondTarget = new Vector3(3.4f, -0.06f, -24f); //最终位置
    private bool triggered = false; 

    void Update()
    {
        //检查摄像机是否穿过绊线
        if (!triggered && playerCamera.position.x > xThreshold)
        {
            triggered = true;
            StartCoroutine(MoveToTargets());
        }
    }

    private System.Collections.IEnumerator MoveToTargets()
    {
        //冲刺到拐点
        yield return StartCoroutine(MoveToPosition(firstTarget));

        //左转消失
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
}
