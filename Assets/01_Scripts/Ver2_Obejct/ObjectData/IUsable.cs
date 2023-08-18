using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//인터페이스 , 객체지향,  추상화
//인터페이스 : 메서드, 속성, 이벤트, 속성등을 갖지만, 이를 직접 구현하지 않고 단지 정의만 갖는다.
//추상 멤버로만 구성된 추상 Base 클래스와 개념적으로 유사
//상속 트리를 만듬

//버튼 클릭을 위한
public interface IUsable
{
    //게임오브젝트를 얻고
    void Use(GameObject actor);

    //단일 이벤트를 가질 것 (속성)
    //using UnityEngine.Events; 생성
    UnityEvent Onuse { get; }
}