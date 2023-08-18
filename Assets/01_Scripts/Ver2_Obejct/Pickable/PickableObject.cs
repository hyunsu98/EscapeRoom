using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̵��� ������Ʈ
public class PickableObject : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; } = true;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // �� �̵�
    public GameObject PickUp()
    {
        if (rb != null)
            rb.isKinematic = true;

        return gameObject;
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        Debug.Log("��Ҵ�");
    }

    public void Drop()
    {
        Debug.Log("���ƴ�");
    }
}
