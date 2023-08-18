using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이동할 오브젝트
public class PickableObject : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; } = true;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // 막 이동
    public GameObject PickUp()
    {
        if (rb != null)
            rb.isKinematic = true;

        return gameObject;
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        Debug.Log("잡았다");
    }

    public void Drop()
    {
        Debug.Log("놓쳤다");
    }
}
