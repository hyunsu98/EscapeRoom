using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//토큰은 먹으면 바로 점수 추가
//모두에게 알려주기
public class TokenObject : MonoBehaviourPun
{
    //서랍과 같이 이동하기 위한
    [Header("서랍이동")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    //서랍 안인지 밖인지
    bool ishiddenObject = false;

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

    private void FixedUpdate()
    {
        if (ishiddenObject)
        {
            //※ 리지드 바디 이동으로하면 흔들리는 현상도 일어남. 왜?
            transform.position = contactPlatform.transform.position - distance;
        }
    }

    //상자 안에 있을 시 닿은 지점을 알려주고 이동할 수 있게
    private void OnTriggerEnter(Collider other)
    {
        //충돌 했는데 그 오브젝트가 서랍같은 거면
        if (other.gameObject.CompareTag("HiddenObject"))
        {
           
            //충돌한 오브젝트의 위치와 내 위치와 같게 해라.
            Debug.Log($"서랍 안에 있는 오브젝트 {other.gameObject}");
            contactPlatform = other.gameObject;

            //내가 이해가 안가는 거 같음.
            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            ishiddenObject = true;
        }
    }
}
