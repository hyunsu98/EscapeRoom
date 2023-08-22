using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviourPun
{
    //문 열리는 동작
    public bool isOpen;

    [SerializeField] Vector3 endPos;

    //이동 속도
    [SerializeField] private float speed;

    private Vector3 savePos;

    private void Awake()
    {
        savePos = transform.localPosition;
    }

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
    
    //이벤트 한번만 호출하면 됨
    [PunRPC]
    public void DoorAction(int a)
    {
        isOpen = !isOpen;
    }
}
