using UnityEngine;

public class ClosePanel : MonoBehaviour
{

    public GameObject panel; // �������

    void Start()
    {
        
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    
}
