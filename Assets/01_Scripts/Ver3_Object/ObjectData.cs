using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectData
{
    //bool

    //��� ���
    GameObject PickUp();

    //���
    void Grab(Transform objectGrabPointTransform);

    //����
    void Drop();
}
