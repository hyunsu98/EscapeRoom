using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�޶� ������ �ٸ���...
public class LHS_ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    //��ü ��� ������ ����
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        objectRigidbody = GetComponentInParent<Rigidbody>();
    }

    //�÷��̾�� ȣ���� �� �ֵ���
    //����Ʈ�� ���� ��ȯ�� �޵���
    public void Grab(Transform objectGrabPointTransform)
    {
        //��ü ��� ������ ����
        this.objectGrabPointTransform = objectGrabPointTransform;
        //������� �߷� ���ֱ�
        objectRigidbody.useGravity = false;
    }

    //����
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
    }
    private void Update()
    {
        //���� ) ȸ��
        /*if (objectGrabPointTransform != null)
        {
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                transform.Rotate(Vector3.up, 90);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                transform.Rotate(Vector3.up, -90);
            }
        }*/
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

            objectRigidbody.MovePosition(newPosition);
        }
    }

    //���� ������� �������� ������� ����
    //���ձ� �ݴ�� �ϱ�
    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            objectRigidbody.velocity = new Vector3(objectRigidbody.velocity.x, 0, objectRigidbody.velocity.z);
        }
    }*/
}
