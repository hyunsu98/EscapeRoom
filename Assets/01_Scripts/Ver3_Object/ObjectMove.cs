using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [Header("월드위치유지")]
    public bool isKeepWorldPosition;
    [Header("획득아이템")]
    public bool isEatItem;

    [Header("이동속도")]
    public float lerpSpeed = 10;

    //이동할위치"
    Transform objectGrabPointTransform;

    //서랍과 같이 이동하기 위한
    [Header("서랍이동")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    //서랍 안인지 밖인지
    bool hiddenObject = false;

    //리지드 바디 필요
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    //잡았을 때 중력 적용 되지 않게
    //나의 위치를 알려주기
    public GameObject PickUp()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (isEatItem)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        return this.gameObject;
    }

    //이동할 위치 받기
    public void Grab(Transform objectGrabPointTransform)
    {
        //서랍장 따라다니기 꺼주기
        hiddenObject = false;
        //객체 잡기 지점을 저장
        this.objectGrabPointTransform = objectGrabPointTransform;
    }

    //놓았을 때 -> 이동할 위치 없애기
    public void Drop()
    {
        this.objectGrabPointTransform = null;

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    //리지드 바디 이동
    private void FixedUpdate()
    {
        //먹을 수 있는 아이템이 아닐 시
        if(!isEatItem)
        {
            //이동할 위치가 있다면
            if (objectGrabPointTransform != null)
            {
                //Lerp이동 [벽뚫기 반대로 적용]
                // 1. 카메라~앞방향으로 Ray를 쏴서 부딪힌 지점과의 거리
                // 2. 카메라~objectGrabPointTransform.position와의 거리
                // 1과 2중 짧은 거리에 해당하는 위치
                Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

                // MovePosition은 리지드바디 보간(rigidbody interpolation)이 활성화 돼 있으면, 매 프레임 렌더링 사이에서도 자연스러운 이동을 가질 수 있음
                rb.MovePosition(newPosition);
            }

            //잡으면 hiddenObject 적용하면 안됨.
            else
            {
                if (hiddenObject)
                {
                    //※ 리지드 바디 이동으로하면 흔들리는 현상도 일어남. 왜?
                    transform.position = contactPlatform.transform.position - distance;
                }
            }
        }
    }

    //상자 안에 있을 시 
    //닿은 지점을 알려주고 이동할 수 있게
    private void OnTriggerEnter(Collider other)
    {
        //충돌 했는데 그 오브젝트가 서랍같은 거면
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            //충돌한 오브젝트의 위치와 내 위치와 같게 해라.
            Debug.Log($"숨겨진 오브젝트 {other.gameObject}");
            contactPlatform = other.gameObject;

            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            hiddenObject = true;
        }
    }

    //나가면 따라다니지 않게
    private void OnTriggerExit(Collider other)
    {
        hiddenObject = false;
    }
}
