using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//내 앞으로 이동
//음식 , 무기
public class PickableItem : MonoBehaviour, IPickable
{
   
    //false 로컬위치설정
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        return this.gameObject;
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        Debug.Log("잡았다");
    }

    public void Drop()
    {
        Debug.Log("놓쳤다");
    }

    public void TEST()
    {
        Debug.Log("2");
    }
}
