using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //�ڽĿ�����Ʈ�� �ֱ�
        Debug.Log("��Ҵ�");
        
    }

    private void OnTriggerExit(Collider other)
    {
        //other.transform.SetParent(null);
    }
}
