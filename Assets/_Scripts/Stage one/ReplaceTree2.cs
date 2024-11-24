//�⼸��replace����Ĺ��ܶ������Ƶģ����Ƿ��ڶ�Ӧ�������ϣ���������ʧʱ���������ʥ��������Ч����δ�����ڵڶ�ֻ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTree2 : MonoBehaviour
{
    public GameObject tree2;      //ԭʼ��
    public GameObject tree1_2;    //ʥ����

    void Update()
    {
        //��δ���������Ҫ�����޸ģ���ʽΪ��������--����ReplaceTree�����ؽű���������ʧ--��ҵ�Stage1_count+1
        if (!gameObject.activeInHierarchy)
        {
            ReplaceTree(tree2, tree1_2);
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
