using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectData
{
    //bool

    //잡는 방법
    GameObject PickUp(Player owner);

    //잡기
    void Grab(Transform objectGrabPointTransform);

    //놓기
    void Drop();
}
