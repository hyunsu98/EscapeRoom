using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TextureScroll : MonoBehaviour
{
    RawImage rawImage;  // RawImage ������Ʈ�� ����ϱ� ���� ����
    public float scrollSpeed = 0.5f;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();  // RawImage ������Ʈ�� ������
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 offset = new Vector2(-Time.time * scrollSpeed, 0);
        rawImage.material.SetTextureOffset("_MainTex", offset);  // RawImage�� Material�� �ؽ�ó �������� ����
    }
}