using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    //�� ������ ����

    public bool isOpen;

    [SerializeField] Vector3 endPos;

    //�̵� �ӵ�
    [SerializeField] private float speed;

    private Vector3 savePos;

    private void Awake()
    {
        savePos = transform.localPosition;
    }

    //�Լ� �����ϰ� �ص� �ǰڴ�.

    private void Update()
    {
        if (isOpen)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, endPos, speed * Time.deltaTime);
        }

        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, savePos, speed * Time.deltaTime);
        }
    }
}
