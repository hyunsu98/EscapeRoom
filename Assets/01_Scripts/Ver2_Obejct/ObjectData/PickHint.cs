using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickHint : MonoBehaviourPun, IUsable //인터페이스 구현
{
    //인스펙터에 속성이 노출되어 할당할 수 있음
    [field: SerializeField]
    public UnityEvent Onuse { get; private set; }
    //public UnityEvent Onuse => throw new System.NotImplementedException();

    //음식을 먹으면 아이템 증가 //토큰
    private int healthBoost = 1;

    public void Use(GameObject actor)
    {
        //건강부스터를 증가 시킨다.
        //actor.GetComponent<PlayerPickable>().AddHealth(healthBoost);

        //모두
        //GameManager.instance.UpdateToken(1);
        photonView.RPC(nameof(Add), RpcTarget.All, 1);

        //이름 없이 간편하게 호출될 수 있는 함수
        //이벤트에 할당된 메소드가 있는 경우 이 메소드를 호출.
        //먹는 소리 재생
        //먹으면 삭제해야함.
    }

    [PunRPC]
    public void Add(int i)
    {
        GameManager.instance.UpdateToken(i);
        Onuse?.Invoke();
        Destroy(gameObject);
    }
}