using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviourPun
{

    [SerializeField] private float doorOpenAngle = 90f;
    
    [SerializeField] private float doorCloseAngle = 0f;

    //�̵� �ӵ�
    [SerializeField] private float speed;

    public bool isOpen;
    public bool key;
    public bool finalKey;

    private void Update()
    {
        //�̼��� ���� �� ����
        if(key) //1�ܰ� ��
        {
            if(GameManager.instance.missionTwo == true)
            {
                //Y�� ȸ��
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
            }
        }

        else if(finalKey) //2�ܰ� ��
        {
            if (GameManager.instance.missionThree == true)
            {
                //Y�� ȸ��
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
            }
        }

        //�Ϲ� ��
        else
        {
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
    }


    [PunRPC]
    public void OpenDoorAction(int a)
    {
        isOpen = !isOpen;
    }
}
