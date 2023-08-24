using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviourPun
{
    public bool isOpen;

    [SerializeField] public float doorOpenAngle = 90f;
    
    [SerializeField] public float doorCloseAngle = 0f;

    //�̵� �ӵ�
    [SerializeField] private float speed;

    //���� ��ġ
    //private Vector3 savePos;
    Quaternion targetRotation;

    private void Update()
    {
        Debug.Log(isOpen);

        if (isOpen)
        {
            //Y�� ȸ��
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
        // ������
        if (val)
        {
            Debug.Log("����");
            //Y�� ȸ��
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
        }

        //������
        else
        {
            Debug.Log("����");
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, speed * Time.deltaTime);
        }
    }*/
}
