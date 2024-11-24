//�⼸��replace����Ĺ��ܶ������Ƶģ����Ƿ��ڶ�Ӧ�������ϣ���������ʧʱ���������ʥ��������Ч����δ�����ڵ���ֻ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTree3 : MonoBehaviour
{
    public GameObject tree3;      //ԭʼ��
    public GameObject tree1_3;    //ʥ����

    void Update()
    {
        //��δ���������Ҫ�����޸ģ���ʽΪ��������--����ReplaceTree�����ؽű���������ʧ--��ҵ�Stage1_count+1
        if (!gameObject.activeInHierarchy)
        {
            ReplaceTree(tree3, tree1_3);
            Destroy(this);
        }
    }

    private void ReplaceTree(GameObject originalTree, GameObject newTree)
    {
        if (originalTree != null && newTree != null)
        {
            //��ʥ�����ƶ���ԭʼ����λ��
            newTree.transform.position = originalTree.transform.position;
            newTree.transform.rotation = originalTree.transform.rotation;

            //ʹ������
            originalTree.SetActive(false);
            newTree.SetActive(true);
        }
    }
}
