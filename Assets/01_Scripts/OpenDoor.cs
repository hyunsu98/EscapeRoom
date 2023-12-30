using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviourPun
{

    [SerializeField] private float doorOpenAngle = 90f;
    
    [SerializeField] private float doorCloseAngle = 0f;

    //이동 속도
    [SerializeField] private float speed;

    public bool isOpen;
    public bool key;
    public bool finalKey;

    private void Update()
    {
        //미션을 위한 문 열기
        if(key) //1단계 문
        {
            if(GameManager.instance.missionTwo == true)
            {
                //Y축 회전
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
            }
        }

        else if(finalKey) //2단계 문
        {
            if (GameManager.instance.missionThree == true)
            {
                //Y축 회전
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
            }
        }

        //일반 문
        else
        {
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
    }


    [PunRPC]
    public void OpenDoorAction(int a)
    {
        isOpen = !isOpen;
    }
}
