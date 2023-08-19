using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //자식오브젝트로 넣기
        Debug.Log("닿았다");
        
    }

    private void OnTriggerExit(Collider other)
    {
        //other.transform.SetParent(null);
    }
}
