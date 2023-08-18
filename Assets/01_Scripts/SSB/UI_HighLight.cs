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
        //이미지 컴포넌트를 가져온다
        highLight = GetComponent<Image>();
        //이미지 컴포넌트를 킨다
        highLight.enabled = true;

        //이미지 컬러의 알파값을 150으로 셋팅한다 
        Color currentColor = highLight.color; //현재값 저장
        currentColor.a = 150f / 255f; //알파값 설정
        highLight.color = currentColor;

        //highLight.color = new Color(highLight.color.r, highLight.color.g, highLight.color.b, 50f);
    }

    public void MouseOff()
    {
        //이미지 컴포넌트를 끈다
        highLight.enabled = false;
    }
}
