using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라 설정을 다르게...
public class LHS_ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    //객체 잡기 지점을 저장
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        objectRigidbody = GetComponentInParent<Rigidbody>();
    }

    //플레이어에서 호출할 수 있도록
    //포인트에 대한 변환을 받도록
    public void Grab(Transform objectGrabPointTransform)
    {
        //객체 잡기 지점을 저장
        this.objectGrabPointTransform = objectGrabPointTransform;
        //잡았을때 중력 꺼주기
        objectRigidbody.useGravity = false;
    }

    //놓기
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
    }
    private void Update()
    {
        //보류 ) 회전
        /*if (objectGrabPointTransform != null)
        {
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                transform.Rotate(Vector3.up, 90);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                transform.Rotate(Vector3.up, -90);
            }
        }*/
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

            objectRigidbody.MovePosition(newPosition);
        }
    }

    //땅에 닿았을때 내려가지 않을라면 보류
    //벽뚫기 반대로 하기
    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            objectRigidbody.velocity = new Vector3(objectRigidbody.velocity.x, 0, objectRigidbody.velocity.z);
        }
    }*/
}
