using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot : MonoBehaviour
{
    //������ ȸ�� ��
    float rotX;
    float rotY;

    //ȸ�� �ӷ�
    [SerializeField] float rotSpeed = 200;

    //ī�޶� Transfrom
    [SerializeField] Transform trCam;

    float mx;
    float my;

    void Start()
    {
    }

    void Update()
    {
        //���콺�� �����ӵ��� �÷��̸� �¿� ȸ���ϰ�
        //ī�޶� ���Ʒ� ȸ���ϰ� �ʹ�.

        //1. ���콺 �Է��� ����. 
        mx = Input.GetAxis("Mouse X");
        my = Input.GetAxis("Mouse Y");

        //2. ���콺�� ������ ���� ����
        rotX += mx * rotSpeed * Time.deltaTime;
        rotY += my * rotSpeed * Time.deltaTime;

        //3. ������ ����ŭ ȸ�� ��Ű��.
        transform.localEulerAngles = new Vector3(0, rotX, 0);

        //x�� ȸ��(���� ȸ��)���� -90eh ~ 90�� ���̷� �����ϳ�.
        rotY = Mathf.Clamp(rotY, -60f, 60f);

        //����ȸ���� ī�޶� ȸ����Ű��.
        // Camera.main.transform.localEulerAngles = new Vector3(-rotY, 0, 0);
        trCam.localEulerAngles = new Vector3(-rotY, 0, 0);
    }
}
