using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableLerp : MonoBehaviour , IPickable
{
    //구현 굳이? 안해도 됨
    public bool KeepWorldPosition => throw new System.NotImplementedException();

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
    }
}
