using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableLerp : MonoBehaviour , IPickable
{
    //구현 굳이? 안해도 됨
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
        //카메라 자식 위치 받고
    }
}
