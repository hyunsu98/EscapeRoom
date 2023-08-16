using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    //이상함
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    //드래그 되는 동안 다른 힘의 영향을 받지 않도록
    private void OnMouseDrag()
    {
        //마우스 위치
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z + transform.position.z);
        //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);

        //물체를 집어 들 때 물체가 z 축을 따라 텔레포트하거나 흔들리지 않도록 하기 위해
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //객체의 위치를 새로운 객체의 위치로 이동
        transform.position = objPosition;

        //중력
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
    }
}
