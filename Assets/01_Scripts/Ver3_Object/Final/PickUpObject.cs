using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//잡을 수 있는 오브젝트
//거리계산 후 놓을 수 있음
//투명도 조절
//오른쪽 마우스 클릭 시 인벤토리 저장
//서랍이동 가능
//인벤토리 저장 시 UI 생성
public class PickUpObject : MonoBehaviourPun, IObjectData
{
    [Header("키오브젝트")]
    public bool key;

    public bool mission;

    [Header("이동속도")]
    public float lerpSpeed = 10;

    //서랍과 같이 이동하기 위한
    [Header("서랍이동")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    //서랍 안인지 밖인지
    bool ishiddenObject = false;

    //이동할위치
    Transform objectGrabPointTransform;

    //리지드 바디 필요
    Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //RPC로 내가 A객체를 잡았을 때 모든 사람한테 A객체 중력 보내기
    [PunRPC]
    public void OnOff(bool on)
    {
        if (rb != null)
        {
            rb.isKinematic = on;
        }
    }

    [PunRPC]
    //상대 유저한테도 나갔다고 알려줘야 함.
    public void OnExit(bool exit)
    {
        ishiddenObject = exit;
    }

    public GameObject PickUp(Player owner)
    {
        //객체의 주인을 매개변수값으로 바꾼다.
        //포톤 소유권 제어하기 (Takeover)
        if (photonView != null)
        {
            photonView.TransferOwnership(owner);

            //중력 제어 모든 사람에게 알려줘야 함.
            photonView.RPC(nameof(OnOff), RpcTarget.All, true);
        }
        
        //※여기서 조절 해야함!
        //가져오는 객체이기 때문에
        transform.rotation = Quaternion.identity;
        Debug.Log("아이템 획득");

        if (key)
        {
            //GameManager.instance.KeyEat(true);
            Debug.Log("키를 획득함");
        }

        return this.gameObject;
    }
    #region 잡기 / 놓기 했을 때 설정 값
    public void Grab(Transform objectGrabPointTransform)
    {
        //서랍장 따라다니기 꺼주기
        //ishiddenObject = false;
        //객체 잡기 지점을 저장
        this.objectGrabPointTransform = objectGrabPointTransform;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;

        if (photonView != null)
        {
            //중력 제어 모든 사람에게 알려줘야 함.
            photonView.RPC(nameof(OnOff), RpcTarget.All, false);
        }
    }
    #endregion

    #region 이동 방법
    //리지드 바디 이동
    private void FixedUpdate()
    {
        //이동할 위치가 있다면
        if (objectGrabPointTransform != null)
        {
            //Lerp이동 [벽뚫기 반대로 적용]
            // 1. 카메라~앞방향으로 Ray를 쏴서 부딪힌 지점과의 거리
            // 2. 카메라~objectGrabPointTransform.position와의 거리
            // 1과 2중 짧은 거리에 해당하는 //이동을

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // MovePosition은 리지드바디 보간(rigidbody interpolation)이 활성화 돼 있으면, 매 프레임 렌더링 사이에서도 자연스러운 이동을 가질 수 있음
            // 오류 생김) 값을 zero로 했을때 자꾸 움직임 
            rb.MovePosition(newPosition);
        }

        //잡으면 hiddenObject 적용하면 안됨.
        else
        {
            if (ishiddenObject)
            {
                //※ 리지드 바디 이동으로하면 흔들리는 현상도 일어남. 왜?
                transform.position = contactPlatform.transform.position - distance;
            }
        }
    }
    #endregion

    #region 서랍안에 들어갔을 때 이동
    //상자 안에 있을 시 닿은 지점을 알려주고 이동할 수 있게
    private void OnTriggerEnter(Collider other)
    {
        //충돌 했는데 그 오브젝트가 서랍같은 거면
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            if (photonView != null)
            {
                //중력 제어 모든 사람에게 알려줘야 함.

                Debug.Log("중력제거");
                photonView.RPC(nameof(OnOff), RpcTarget.All, true);
            }

            //충돌한 오브젝트의 위치와 내 위치와 같게 해라.
            Debug.Log($"서랍 안에 있는 오브젝트 {other.gameObject}");
            contactPlatform = other.gameObject;

            //내가 이해가 안가는 거 같음.
            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            ishiddenObject = true;
        }
    }

    //나가면 따라다니지 않게
    private void OnTriggerExit(Collider other)
    {
        photonView.RPC(nameof(OnExit), RpcTarget.All, false);
        //ishiddenObject = false;
    }
    #endregion 


}
