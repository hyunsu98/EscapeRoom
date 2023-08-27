using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//드래그 가능한 오브젝트
//거리 계산 후 놓을 수 있음
//인벤토리 저장은 할 수 없음
//서랍이동은 가능
[RequireComponent(typeof(Rigidbody), typeof(Highlight))]
public class DragObject : MonoBehaviourPun, IObjectData
{
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

    float finalDistance;

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

    #region 플레이어에서 나를 잡았을 때 나 넘기기 (포톤 소유권은 받기)
    //잡았을 때 중력 적용 되지 않게
    //내 위치 자체를
    //using Photon.Realtime; -> Player 사용하려면 필요(다른 스크립트 이름 있으면 안됨)
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
        //내 정보 넘겨주기
        return this.gameObject;
    }
    #endregion

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

            /*Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);*/

            //이동할 위치와 카메라의 거리 구하기
            float distance = Vector3.Distance(objectGrabPointTransform.position, Camera.main.transform.position);

            //처음 거리 저장 //objectGrabPointTransform;
            Vector3 savePos = objectGrabPointTransform.position;

            //이동할 위치에서 카메라의 방향으로 거리만큼만 레이쏘기
            Debug.DrawRay(objectGrabPointTransform.position, Camera.main.transform.forward * -distance, Color.green);

            //플레이어 레이어만 제외하고 충돌 체크
            int layerMask = ((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Pickable")));
            layerMask = ~layerMask;

            //플레이어 빼고 잡은 위치랑 카메라 사이의 닿은 물체가 있는지 확인
            if (Physics.Linecast(savePos, Camera.main.transform.position, out RaycastHit hit, layerMask))
            {
                //있을때
                //finalDistance = Mathf.Clamp(hit.distance * 2, 1, 10);
                finalDistance = hit.distance * 2;

                Debug.Log($"닿은물체 {hit.collider.name} 거리는 {hit.distance}");
            }
            else
            {
                //없을때
                finalDistance = 0;
                Debug.Log($"닿지 않아야함");
            }

            //마지막 위치는 = 오브젝트 위치에서부터 카메라 방향으로 최종거리만큼 이동한 위치를 최종 위치에 저장
            //Vector3 finalPosition = objectGrabPointTransform.position + -Camera.main.transform.forward * finalDistance;
            Vector3 finalPosition = savePos + -Camera.main.transform.forward * finalDistance;

            Vector3 newPosition = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * lerpSpeed);

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
            //중력 끄기
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
