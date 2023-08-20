using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool isOpen;

    [SerializeField] public float doorOpenAngle = 90f;
    [SerializeField] public float doorCloseAngle = 0f;

    //이동 속도
    [SerializeField] private float speed;

    //저장 위치
    private Vector3 savePos;

    private void Update()
    {
        if (isOpen)
        {
            //Y축 회전
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
        }

        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, speed * Time.deltaTime);
        }
    }
}
