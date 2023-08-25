using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//토큰은 먹으면 바로 점수 추가
//모두에게 알려주기
public class TokenObject : MonoBehaviourPun
{
    public void Sorce()
    {
        photonView.RPC(nameof(Add), RpcTarget.All, 1);
    }

    [PunRPC]
    public void Add(int i)
    {
        //점수 추가
        GameManager.instance.UpdateToken(i);
        Destroy(gameObject);
    }
}
