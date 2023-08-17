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
        objectRigidbody = GetComponent<Rigidbody>();
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

    //이동
    private void FixedUpdate()
    {
        //객체가 있다면 
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            //Lerp이동
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            objectRigidbody.MovePosition(newPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("닿앗따");
            objectRigidbody.velocity = new Vector3(objectRigidbody.velocity.x, 0, objectRigidbody.velocity.z);
        }
    }
}
