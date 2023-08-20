using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickKey : MonoBehaviour, IUsable
{
    [field: SerializeField]
    public UnityEvent Onuse { get; private set; }

    public void Use(GameObject actor)
    {
        //미션1 클리어
        //GameManager.instance.KeyEat(true);

        //이름 없이 간편하게 호출될 수 있는 함수
        //이벤트에 할당된 메소드가 있는 경우 이 메소드를 호출.
        //먹는 소리 재생
        Onuse?.Invoke();

        //먹으면 삭제해야함.
        Destroy(gameObject);
    }
}
