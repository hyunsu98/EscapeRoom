using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectData
{
    //bool

    //��� ���
    GameObject PickUp(Player owner);

    //���
    void Grab(Transform objectGrabPointTransform);

    //����
    void Drop();
}
