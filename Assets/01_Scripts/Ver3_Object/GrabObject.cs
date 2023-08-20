using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [Header("월드위치유지")]
    public bool isKeepWorldPosition;

    public bool isKey;

    //리지드 바디 필요
    public Rigidbody rb;


    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        //키 조건문 -> 병안에 있는 키가 옮겨질때 크기를 일정하게 유지하기 위해
        if(isKey)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = transform.lossyScale;
        }

        else
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        return this.gameObject;
    }

    //서랍과 같이 이동하기 위한
    [Header("서랍이동")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;

    //서랍 안인지 밖인지
    bool ishiddenObject = false;

    //놓았을 때 -> 이동할 위치 없애기
    public void Drop()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    //리지드 바디 이동s
    private void Update()
    {
        if (ishiddenObject)
        {
            //※ 리지드 바디 이동으로하면 흔들리는 현상도 일어남. 왜?
            transform.position = contactPlatform.transform.position - distance;
        }
    }

    //상자 안에 있을 시 
    //닿은 지점을 알려주고 이동할 수 있게
    private void OnTriggerEnter(Collider other)
    {
        //충돌 했는데 그 오브젝트가 서랍같은 거면
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            //충돌한 오브젝트의 위치와 내 위치와 같게 해라.
            Debug.Log($"숨겨진 오브젝트 {other.gameObject}");
            contactPlatform = other.gameObject;

            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            ishiddenObject = true;
        }
    }

    //나가면 따라다니지 않게
    private void OnTriggerExit(Collider other)
    {
        ishiddenObject = false;
    }
}
