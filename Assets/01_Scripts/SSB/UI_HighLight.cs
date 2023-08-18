using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HighLight : MonoBehaviour
{
    // Start is called before the first frame update
    Image highLight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseOn()
    {
        //�̹��� ������Ʈ�� �����´�
        highLight = GetComponent<Image>();
        //�̹��� ������Ʈ�� Ų��
        highLight.enabled = true;

        //�̹��� �÷��� ���İ��� 150���� �����Ѵ� 
        Color currentColor = highLight.color; //���簪 ����
        currentColor.a = 150f / 255f; //���İ� ����
        highLight.color = currentColor;

        //highLight.color = new Color(highLight.color.r, highLight.color.g, highLight.color.b, 50f);
    }

    public void MouseOff()
    {
        //�̹��� ������Ʈ�� ����
        highLight.enabled = false;
    }
}
