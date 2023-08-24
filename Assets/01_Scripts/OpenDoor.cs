using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviourPun
{
    public bool isOpen;

    [SerializeField] public float doorOpenAngle = 90f;
    
    [SerializeField] public float doorCloseAngle = 0f;

    //이동 속도
    [SerializeField] private float speed;

    //저장 위치
    //private Vector3 savePos;
    Quaternion targetRotation;

    private void Update()
    {
        Debug.Log(isOpen);

        if (isOpen)
        {
            //Y축 회전
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
        }

        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, speed * Time.deltaTime);
        }
    }


    [PunRPC]
    public void OpenDoorAction(int a)
    {
        isOpen = !isOpen;
    }

    /*public void OpenDoorChek(bool val)
    {
        Debug.Log(isOpen);
        // 켜지기
        if (val)
        {
            Debug.Log("열림");
            //Y축 회전
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
        }

        //꺼지기
        else
        {
            Debug.Log("닫힘");
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, speed * Time.deltaTime);
        }
    }*/
}
