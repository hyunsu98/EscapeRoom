using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectData
{
    //bool

    //잡는 방법
    GameObject PickUp();

    //잡기
    void Grab(Transform objectGrabPointTransform);

    //놓기
    void Drop();
}
