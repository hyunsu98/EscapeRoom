using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TextureScroll : MonoBehaviour
{
    RawImage rawImage;  // RawImage 컴포넌트를 사용하기 위해 변경
    public float scrollSpeed = 0.5f;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();  // RawImage 컴포넌트를 가져옴
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 offset = new Vector2(-Time.time * scrollSpeed, 0);
        rawImage.material.SetTextureOffset("_MainTex", offset);  // RawImage의 Material의 텍스처 오프셋을 변경
    }
}