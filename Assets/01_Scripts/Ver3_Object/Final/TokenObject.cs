using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��ū�� ������ �ٷ� ���� �߰�
//��ο��� �˷��ֱ�
public class TokenObject : MonoBehaviourPun
{
    public void Sorce()
    {
        photonView.RPC(nameof(Add), RpcTarget.All, 1);
    }

    [PunRPC]
    public void Add(int i)
    {
        //���� �߰�
        GameManager.instance.UpdateToken(i);
        Destroy(gameObject);
    }
}
