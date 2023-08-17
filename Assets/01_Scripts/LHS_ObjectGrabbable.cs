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
        objectRigidbody = GetComponent<Rigidbody>();
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

    //�̵�
    private void FixedUpdate()
    {
        //��ü�� �ִٸ� 
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            //Lerp�̵�
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            objectRigidbody.MovePosition(newPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("��ѵ�");
            objectRigidbody.velocity = new Vector3(objectRigidbody.velocity.x, 0, objectRigidbody.velocity.z);
        }
    }
}
