using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableLerp : MonoBehaviour , IPickable
{
    //���� ����? ���ص� ��
    public bool KeepWorldPosition => throw new System.NotImplementedException();

    Rigidbody rb;

    //��ü ��� ������ ����
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (rb != null)
            rb.isKinematic = true;
        return gameObject;
    }

    public void Update()
    {
        //ī�޶� �ڽ� ��ġ �ް�
    }

    //�÷��̾�� ȣ���� �� �ֵ���
    //����Ʈ�� ���� ��ȯ�� �޵���
    public void Grab(Transform objectGrabPointTransform)
    {
        //��ü ��� ������ ����
        this.objectGrabPointTransform = objectGrabPointTransform;
        //������� �߷� ���ֱ�
        //rb.useGravity = false;
    }

    //���� -> �÷��̾�� ����
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
    }

    //�̵�
    private void FixedUpdate()
    {
        //��ü�� �ִٸ� 
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            //Lerp�̵�
            // 1. ī�޶�~�չ������� Ray�� ���� �ε��� �������� �Ÿ�
            // 2. ī�޶�~objectGrabPointTransform.position���� �Ÿ�
            // 1�� 2�� ª�� �Ÿ��� �ش��ϴ� ��ġ

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            rb.MovePosition(newPosition);
        }
    }
}
