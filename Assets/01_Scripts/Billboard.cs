using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        //���� �չ����� ī�޶��� �Փ����� ����
        transform.forward = Camera.main.transform.forward;
    }
}
