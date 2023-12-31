using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRot : MonoBehaviourPun
{
    //누적된 회전 값
    float rotX;
    float rotY;

    //회전 속력
    [SerializeField] float rotSpeed = 200;

    //카메라 Transfrom
    [SerializeField] Transform trCam;

    float mx;
    float my;

    void Start()
    {
        //내가 생성한 Player 일때만 카메라를 활성화 하자
        if (photonView.IsMine)
        {
            //trCam.gameObject.SetActive(true);
            trCam.GetChild(0).gameObject.SetActive(true);
        }
    }

    void Update()
    {
        //내것이 아닐때 함수를 나가자
        if (photonView.IsMine == false) return;

        //마우스의 움직임따라 플레이를 좌우 회전하고
        //카메라를 위아래 회전하고 싶다.

        //1. 마우스 입력을 받자. 
        mx = Input.GetAxis("Mouse X");
        my = Input.GetAxis("Mouse Y");

        //2. 마우스의 움직임 값을 누적
        rotX += mx * rotSpeed * Time.deltaTime;
        rotY += my * rotSpeed * Time.deltaTime;

        //3. 누적된 값만큼 회전 시키자.
        transform.localEulerAngles = new Vector3(0, rotX, 0);

        //x축 회전(상하 회전)값을 -90eh ~ 90도 사이로 제한하낟.
        rotY = Mathf.Clamp(rotY, -80f, 80f);

        //상하회전은 카메라를 회전시키자.
        // Camera.main.transform.localEulerAngles = new Vector3(-rotY, 0, 0);
        trCam.localEulerAngles = new Vector3(-rotY, 0, 0);
    }
}
