using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickItem : MonoBehaviour, IUsable
{
    //단일 이벤트를 사용하기 위해
    //발사도 하기 위해
    //권총 게임 오브젝트 내부에 소리 있음 켜질 수 있게
    //이펙트 효과도 실행

    [field: SerializeField]
    public UnityEvent Onuse { get; private set; }

    public void Use(GameObject actor)
    {
        //Onuse 가 null이 아니라면 Invoke 실행
        Onuse?.Invoke();
    }
}

