using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [Header("������ġ����")]
    public bool isKeepWorldPosition;

    public bool isKey;

    //������ �ٵ� �ʿ�
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

        //Ű ���ǹ� -> ���ȿ� �ִ� Ű�� �Ű����� ũ�⸦ �����ϰ� �����ϱ� ����
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

    //������ ���� �̵��ϱ� ����
    [Header("�����̵�")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;

    //���� ������ ������
    bool ishiddenObject = false;

    //������ �� -> �̵��� ��ġ ���ֱ�
    public void Drop()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    //������ �ٵ� �̵�s
    private void Update()
    {
        if (ishiddenObject)
        {
            //�� ������ �ٵ� �̵������ϸ� ��鸮�� ���� �Ͼ. ��?
            transform.position = contactPlatform.transform.position - distance;
        }
    }

    //���� �ȿ� ���� �� 
    //���� ������ �˷��ְ� �̵��� �� �ְ�
    private void OnTriggerEnter(Collider other)
    {
        //�浹 �ߴµ� �� ������Ʈ�� �������� �Ÿ�
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            //�浹�� ������Ʈ�� ��ġ�� �� ��ġ�� ���� �ض�.
            Debug.Log($"������ ������Ʈ {other.gameObject}");
            contactPlatform = other.gameObject;

            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            ishiddenObject = true;
        }
    }

    //������ ����ٴ��� �ʰ�
    private void OnTriggerExit(Collider other)
    {
        ishiddenObject = false;
    }
}
