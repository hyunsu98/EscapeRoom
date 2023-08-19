using UnityEngine;

public class PickableLerp : MonoBehaviour, IPickable
{
    //구현 굳이? 안해도 됨
    public bool KeepWorldPosition { get; private set; }

    Rigidbody rb;

    //객체 잡기 지점을 저장
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (rb != null)
            rb.isKinematic = true;
        return gameObject;
    }

    public void Update()
    {
        //카메라 자식 위치 받고
    }

    //플레이어에서 호출할 수 있도록
    //포인트에 대한 변환을 받도록
    public void Grab(Transform objectGrabPointTransform)
    {
        //서랍장 따라다니기 꺼주기
        hiddenObject = false;
        //객체 잡기 지점을 저장
        this.objectGrabPointTransform = objectGrabPointTransform;
        //잡았을때 중력 꺼주기
        //rb.useGravity = false;
    }

    //놓기 -> 플레이어에서 해줌
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
    }

    //이동
    private void FixedUpdate()
    {
        //객체가 있다면 
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            //Lerp이동
            // 1. 카메라~앞방향으로 Ray를 쏴서 부딪힌 지점과의 거리
            // 2. 카메라~objectGrabPointTransform.position와의 거리
            // 1과 2중 짧은 거리에 해당하는 위치

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            rb.MovePosition(newPosition);
        }

        //잡으면 hiddenObject 적용하면 안됨.
        else
        {
            //코드 수정 필요
            if (hiddenObject)
            {
                transform.position = contactPlatform.transform.position - distance;
                Debug.Log("이동해야함");
            }
        }
    }

    //서랍과 같이 이동하기 위한 코드
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    bool hiddenObject;

    //닿은 지점을 알려주고 이동할 수 있게
    private void OnTriggerEnter(Collider other)
    {
        //충돌 했는데 그 오브젝트가 서랍같은 거면
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            //충돌한 오브젝트의 위치와 내 위치와 같게 해라.
            Debug.Log("닿앗다");
            contactPlatform = other.gameObject;

            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            hiddenObject = true;
        }
    }

    //바닥 나가면
    private void OnTriggerExit(Collider other)
    {
        hiddenObject = false;
    }
}
