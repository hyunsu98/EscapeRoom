using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableLerp : MonoBehaviour , IPickable
{
    //���� ����? ���ص� ��
    public bool KeepWorldPosition => throw new System.NotImplementedException();

    Rigidbody rb;

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
}
