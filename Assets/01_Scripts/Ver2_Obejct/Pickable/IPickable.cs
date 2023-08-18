using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이동을 위한
public interface IPickable
{
    //위치 얻기
    bool KeepWorldPosition { get; }

    //픽업 방법
    GameObject PickUp();

}
