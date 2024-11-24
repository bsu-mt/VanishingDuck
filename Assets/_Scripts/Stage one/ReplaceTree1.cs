//这几个replace代码的功能都是相似的，都是放在对应的兔子上，当兔子消失时触发树变成圣诞树的特效，这段代码放在第一只兔子上
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTree1 : MonoBehaviour
{
    public GameObject tree1;      //原始树
    public GameObject tree1_1;    //圣诞树

    void Update()
    {
        //这段触发条件需要做出修改，格式为做出交互--触发ReplaceTree，挂载脚本的兔子消失--玩家的Stage1_count+1
        if (!gameObject.activeInHierarchy)
        {
            ReplaceTree(tree1, tree1_1);
            Destroy(this)
        }
    }

    private void ReplaceTree(GameObject originalTree, GameObject newTree)
    {
        if (originalTree != null && newTree != null)
        {
            //将圣诞树移动到原始树的位置
            newTree.transform.position = originalTree.transform.position;
            newTree.transform.rotation = originalTree.transform.rotation;

            //使用新树
            originalTree.SetActive(false);
            newTree.SetActive(true);
        }
    }
}
