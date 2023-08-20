using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [Header("������ġ����")]
    public bool isKeepWorldPosition;

    [Header("ȹ�������")]
    public bool isEatItem;

    [Header("�̵��ӵ�")]
    public float lerpSpeed = 10;

    //�̵�����ġ"
    Transform objectGrabPointTransform;

    //������ ���� �̵��ϱ� ����
    [Header("�����̵�")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;

    //���� ������ ������
    bool ishiddenObject = false;

    //������ �ٵ� �ʿ�
    public Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //����� �� �߷� ���� ���� �ʰ�
    //���� ��ġ�� �˷��ֱ�
    public GameObject PickUp()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        /*if (isEatItem)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Debug.Log("���� �� �ȵ�?");
        }*/

        return this.gameObject;
    }

    #region ���콺 ����Ʈ �ޱ�
    /*Vector3 mousePosition;
    
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }*/
    #endregion

    //�̵��� ��ġ �ޱ�
    public void Grab(Transform objectGrabPointTransform)
    {
        //������ ����ٴϱ� ���ֱ�
        ishiddenObject = false;
        //��ü ��� ������ ����
        this.objectGrabPointTransform = objectGrabPointTransform;
    }

    //������ �� -> �̵��� ��ġ ���ֱ�
    public void Drop()
    {
        this.objectGrabPointTransform = null;

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    //������ �ٵ� �̵�
    private void FixedUpdate()
    {

        //�̵��� ��ġ�� �ִٸ�
        if (objectGrabPointTransform != null)
        {
            //Lerp�̵� [���ձ� �ݴ�� ����]
            // 1. ī�޶�~�չ������� Ray�� ���� �ε��� �������� �Ÿ�
            // 2. ī�޶�~objectGrabPointTransform.position���� �Ÿ�
            // 1�� 2�� ª�� �Ÿ��� �ش��ϴ� ��ġ

            //��ǥ������ �̵�.

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // MovePosition�� ������ٵ� ����(rigidbody interpolation)�� Ȱ��ȭ �� ������, �� ������ ������ ���̿����� �ڿ������� �̵��� ���� �� ����
            rb.MovePosition(newPosition);
        }

        //������ hiddenObject �����ϸ� �ȵ�.
        else
        {
            if (ishiddenObject)
            {
                //�� ������ �ٵ� �̵������ϸ� ��鸮�� ���� �Ͼ. ��?
                transform.position = contactPlatform.transform.position - distance;
            }
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